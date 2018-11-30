using MvcModels.Infrastructure;
using MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcModels
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //ValueProviderFactories.Factories.Insert(0, new CustomValueProviderFactory());//注册自定义值提供器
           // ModelBinders.Binders.Add(typeof(AddressSummary), new AddressSummaryBinder()); //注册自定义模型绑定器
        }
    }
}
