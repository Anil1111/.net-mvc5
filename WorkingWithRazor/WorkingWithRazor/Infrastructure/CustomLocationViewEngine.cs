using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkingWithRazor.Infrastructure
{
    //自建一个ViewEngine, 需要在Global.asax中注册
    public class CustomLocationViewEngine: RazorViewEngine
    {
        public CustomLocationViewEngine()
        {
            ViewLocationFormats = new string[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Common/{0}.cshtml"//将查找共享视图的位置修改为View/Common
            };
        }
    }
}