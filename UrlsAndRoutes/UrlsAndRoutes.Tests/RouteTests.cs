using System;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UrlsAndRoutes.Tests
{
    [TestClass]
    public class RouteTests
    {
        private HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "Get")
        {
            //创建模仿请求
            //通过HttpRequestBase的AppRelativeCurrentExecutionFilePath属性暴露了想要测试的URL
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(t => t.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(t => t.HttpMethod).Returns(httpMethod);

            //创建模仿响应
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(t => t.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(t => t);

            //创建使用上述请求和响应的模仿上下文
            //通过模仿HttpContextBase的Request属性暴露了HttpRequestBase
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(t => t.Request).Returns(mockRequest.Object);
            mockContext.Setup(t => t.Response).Returns(mockResponse.Object);

            //返回模仿的上下文
            return mockContext.Object;
        }

        //该方法指定了要测试的URL，Controller和Action片段所期望的值，以及一个object,含有已定义的所有的附加变量所期望的值
        private void TestRouteMatch(string url, string controller, string action, object routeProperties = null, string httpMethod = "Get")
        {
            //准备
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //动作 处理路由
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            //断言
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));

        }

        //用以将路由系统获得的结果与片段变量期望的值进行比较
        private bool TestIncomingRouteResult(RouteData routeResult, string controller, string action, object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) =>
               {
                   return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
               };

            bool result = valCompare(routeResult.Values["controller"], controller)
                          && valCompare(routeResult.Values["action"], action);

            if (propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (var item in propInfo)
                {
                    if (!(routeResult.Values.ContainsKey(item.Name)
                        && valCompare(routeResult.Values[item.Name], item.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        //检查一个URL不工作的情况
        private void TestRouteFail(string url)
        {
            //Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //Action
            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            //Action

            Assert.IsTrue(result == null || result.Route == null);
        }



        //[TestMethod]
        //public void TestInComingRoutes()
        //{
        //    //对我们希望接受的URL进行检查
        //    TestRouteMatch("~/","Home", "Index");

        //    //检查通过片段获取的值
        //    TestRouteMatch("~/Customer", "Customer", "Index");
        //    TestRouteMatch("~/Customer/List", "Customer", "List");

        //    TestRouteMatch("~/Customer/List/All", "Customer", "List",new { id="All"});
        //    TestRouteMatch("~/Customer/List/All/Delete", "Customer", "List", new { id = "All",catchall="Delete" });
        //    TestRouteMatch("~/Customer/List/All/Delete/Perm", 
        //        "Customer", 
        //        "List", 
        //         new { id = "All", catchall = "Delete/Perm" });

        //    //确保太多或太少的片段数不会匹配

        //   // TestRouteFail("~/Customer/List/All/Delete");
        //}
    }
}
