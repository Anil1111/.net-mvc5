using Filters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters
{
    public class FilterConfig
    {
        //添加全局过滤器
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());//HandleErrorAttribute是必须要添加的
            filters.Add(new ProfileAllAttribute());
        }
    }
}