using System;
using System.Web.Mvc;
using ControllersAndActions.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControllersAndActions.Tests
{
    [TestClass]
    public class ActionTest
    {
        [TestMethod]
        public void ControllerTest()
        {
            //Arrange Controller
            ExampleController target = new ExampleController();

            //Action 
            ViewResult result = target.Index();
            RedirectResult redirectResult = target.Redirect();
            RedirectToRouteResult redirectRouteResult = target.RedirectRoute();
            HttpStatusCodeResult httpStatusResult = target.StatusCode();
            //Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual("Hello", result.ViewBag.Message); //对ViewBag进行单元测试
            Assert.IsTrue(redirectResult.Permanent); //测试是否永久重定向
            Assert.AreEqual("/Example/Index", redirectResult.Url); //测试重定向URL

            Assert.IsFalse(redirectRouteResult.Permanent);
            Assert.AreEqual("Example", redirectRouteResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectRouteResult.RouteValues["action"]);
            Assert.AreEqual("MyID", redirectRouteResult.RouteValues["ID"]);
            Assert.AreEqual(401, httpStatusResult.StatusCode);

        }

        [TestMethod]
        public void ViewSelectionTest()
        {
            ExampleController target = new ExampleController();
            ViewResult result = target.Index();

            Assert.AreEqual("", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(DateTime));
        }

    }
}
