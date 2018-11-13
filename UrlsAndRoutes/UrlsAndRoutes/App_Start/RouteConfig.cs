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
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "UrlsAndRoutes.Controllers" }
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
