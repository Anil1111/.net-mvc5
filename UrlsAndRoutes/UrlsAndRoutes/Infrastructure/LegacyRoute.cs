using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure
{
    public class LegacyRoute:RouteBase
    {
        /* 需要在WebConfig里面加上下面的内容。
             <system.webServer>
                <handlers>
                  <add name="Static64" path="*.html," verb="GET,HEAD,POST,DEBUG,PUT,DELETE,PATCH,OPTIONS" modules="IsapiModule" scriptProcessor="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\aspnet_isapi.dll"  preCondition="classicMode,runtimeVersionv4.0,bitness64" />
                  <add name="Static32" path="*.html" verb="GET,HEAD,POST,DEBUG,PUT,DELETE,PATCH,OPTIONS" modules="IsapiModule" scriptProcessor="C:\Windows\Microsoft.NET\Framework\v4.0.30319\aspnet_isapi.dll"  preCondition="classicMode,runtimeVersionv4.0,bitness32" />
                  <add name="(Static) ExtensionlessUrlHandler-Integrated-4.0" path="*.html" verb="GET,HEAD,POST,DEBUG,PUT,DELETE,PATCH,OPTIONS" type="System.Web.Handlers.TransferRequestHandler" preCondition="integratedMode,runtimeVersionv4.0" />
                </handlers>
              </system.webServer>

      不要直接加<modules runAllManagedModulesForAllRequests="true" />，所有css,js等文件都会走ASP.NET程序，会有性能问题
*/
        private string[] urls;

        public LegacyRoute(params string[] targetUrls)
        {
            urls = targetUrls;
        }

        //设输入定路由规则，将对应的.html的页面路由到Legacy控制器
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;
            string requestedURL = httpContext.Request.AppRelativeCurrentExecutionFilePath;
            if (urls.Contains(requestedURL, StringComparer.OrdinalIgnoreCase))
            {
                result = new RouteData(this, new MvcRouteHandler());
                result.Values.Add("controller", "Legacy");
                result.Values.Add("action", "GetLegacyURL");
                result.Values.Add("legacyURL", requestedURL);
            }
            return result;



        }
        //设定输出路由，在生成路由时，用.html的链接
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData result = null;

            if(values.ContainsKey("legacyURL") && urls.Contains((string)values["legacyURL"],StringComparer.OrdinalIgnoreCase))
            {
                result = new VirtualPathData(this, new UrlHelper(requestContext).Content((string)values["legacyURL"]).Substring(1));//加上Substring(1)是要去掉/
            }
            return result;
        }
    }
}