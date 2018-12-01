using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ClientFeatures//注意命名空间
{
    //自定义捆绑包，必须安装microsoft.AspNet.Web.Optimization包
    //还需要在Global.asax中注册
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/*.css"));
            bundles.Add(new ScriptBundle("~/bundles/clientfeaturesscripts").Include("~/Scripts/jquery-{version}.js",
                                                                                    "~/Scripts/jquery.validate.js",
                                                                                    "~/Scripts/jquery.validate.unobtrusive.js",
                                                                                    "~/Scripts/jquery.unobtrusive-ajax.js"));
        }
    }
}