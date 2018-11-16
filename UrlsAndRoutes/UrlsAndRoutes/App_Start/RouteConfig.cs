using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;
using UrlsAndRoutes.Infrastructure;

namespace UrlsAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = true;

            routes.MapMvcAttributeRoutes(); //开启属性路由

            routes.IgnoreRoute("Content/{filename}.html");//不允许绕过路由

            //在C:\Windows\System32\inetsrv\Config找到IIS的配置文件applicationHost.config
            //在<modules>里面找到<add name="UrlRoutingModule-4.0" type="System.Web.Routing.UrlRoutingModule" preCondition="managedHandler,runtimeVersionv4.0" />
            //修改成<add name="UrlRoutingModule-4.0" type="System.Web.Routing.UrlRoutingModule" preCondition="" />
            //这个修改告诉IIS在对磁盘文件的请求到达MVC路由之前，不要对它进行拦截.
            //如果没有匹配这个URL的控制器和Action，会报错
            //本路由就是为磁盘文件设置的路由
            routes.MapRoute("DiskFile", "Content/StaticContent.html",
                            new { controller = "Customer", action = "List" });



            //当请求/SayHello时，会使用CustomRouteHandler并且调用CustomHttpHandler返回Hello
            routes.Add(new Route("SayHello", new CustomRouteHandler()));
            
            routes.Add(new LegacyRoute(
                "~/articles/Windows_3.1_Overview.html", 
                "~/old/.NET_1.0_Class_Library"));

            //添加了Admin Area以后，如果访问/Home/Index会报错，因为路由系统发现两个Home控制器。
            //Area中的路由会默认加上在本命名空间的约束，所以用Area的路由访问不会报错而这里会报错
            //为了解决Area路由的命名空间冲突，需要将主控制器命名空间优先
            routes.MapRoute(
               "MyRoute",
               "{controller}/{action}",
               null,
               new[] { "UrlsAndRoutes.Controllers" }  //为了解决Area路由的命名空间冲突，需要将主控制器命名空间优先

               );
            //使用Html.RouteLink("xxx","MyOtherRoute","Index","Customer")即可跳过上面的路由，而采用这条路由
            routes.MapRoute(
                 "MyOtherRoute",
                 "App/{action}",
                 new { controller = "Home"},
                 new[] { "UrlsAndRoutes.Controllers" }  //为了解决Area路由的命名空间冲突，需要将主控制器命名空间优先
                );




            //routes.MapRoute(
            //    "ChromeRoute",
            //    "{*catchall}",
            //    new { controller="Home",action="Index"},
            //    new { customConstrain=new UserAgentConstraint("Chrome")}, //自定义约束，匹配Chrome浏览器
            //    new[] { "UrlsAndRoutes.AdditionalController" }
            //    );


            // routes.MapRoute(
            //    "MyRoute",
            //    "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new {
            //          controller ="^H.*",                           //对Controller约束只匹配H开头的Controller
            //          action ="^Index$|^About$",                    //对Action约束只匹配Index和About
            //          httpMethod= new HttpMethodConstraint("GET"),  //只匹配HTTP的GET方法
            //          id=new CompoundRouteConstraint(new IRouteConstraint[]  //约束组合
            //          {
            //             new AlphaRouteConstraint(),                 //匹配字母约束
            //             new MinLengthRouteConstraint(6)             //匹配最小长度约束
            //          })  

            //         },
            //    new[] { "UrlsAndRoutes.Controllers" });


            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //new[] { "UrlsAndRoutes.AdditionalController" } 表示先在UrlsAndRoutes.AdditionalController命名空间下面找
            //如果加了其他命名空间，会有同样的优先级
            //如果需要区分命名空间的优先级，需要创建多条路由
            //Route myRoute= routes.MapRoute(
            //    "MyRoute",
            //    "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new[] { "UrlsAndRoutes.AdditionalController" });

            //设置只在指定命名空间内查找，如果找不到匹配控制器不会搜索其他地方
            //myRoute.DataTokens["UseNamespaceFallback"] = false;


            //*catchall会匹配任意片段，Home/Index后面的将会以id=xxx/xxx/xx来表示
            //routes.MapRoute(
            //    "MyRoute",
            //    "{controller}/{action}/{id}/{*catchall}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional });


            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
            //routes.MapRoute("ShopSchema2", "Shop/OldAction", new { controller = "Home",action="Index"});

            //routes.MapRoute("ShopSchema", "Shop/{action}", new { controller = "Home" });

            //routes.MapRoute("", "X{controller}/{action}");


            //routes.MapRoute("MyRoute", "{controller}/{action}", new { controller = "Home", action = "Index" });

            //routes.MapRoute(
            //    "",
            //    "Public/{controller}/{action}",
            //    new { controller = "Home", action = "Index" }
            //);
        }
    }
}
