using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace UrlsAndRoutes.Infrastructure
{
    //自定义路由约束
    public class UserAgentConstraint : IRouteConstraint
    {
        private string requiredUserAgent;

        public UserAgentConstraint(string angentParam)
        {
            requiredUserAgent = angentParam;
        }
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            //约束内容。在此约束设置的Agent即浏览器是否与用户的浏览器相同
            return httpContext.Request.UserAgent != null && httpContext.Request.UserAgent.Contains(requiredUserAgent);
        }
    }
}