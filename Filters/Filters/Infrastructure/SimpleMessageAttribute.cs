using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    //[AttributeUsage(AttributeTargets.Method,AllowMultiple =true)]
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple =true)] //表示可以运用于类和方法
    public class SimpleMessageAttribute : FilterAttribute, IActionFilter
    {
        public string Message { set; get; }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write(
            $"<div>[After Action: {Message}]</div>");
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Write(
                $"<div>[Before Action: {Message}]</div>");
        }

       
    }
}