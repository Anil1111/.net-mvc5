using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Views.Infrastructure;

namespace Views
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Clear();//如果不加此句，则Razor引擎还会使用，加上后就只会用自定义引擎
            ViewEngines.Engines.Add(new DebugDataViewEngine());
        }
    }
}
