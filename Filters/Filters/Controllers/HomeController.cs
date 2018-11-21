using Filters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Filters.Controllers
{
    public class HomeController : Controller
    {
        private Stopwatch timer;
        [Authorize(Users="admin")]
        public string Index()
        {
            return "This is the Index action on the Home controller";
        }

        [GoogleAuth]
        [Authorize(Users ="bob@google.com")]
        public string List()
        {
            return "This is the list action on the Home controller";
        }

        //[RangeException]
        [HandleError(ExceptionType =typeof(ArgumentOutOfRangeException),View ="RangeError")]
        public string RanngeTest(int id)
        {
            if(id>100)
            {
                return $"The id value is: {id}";
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", id, "");
            }
        }

        //[CustomAction]
        //[ProfileAll]
        //[ProfileAction]
        //[ProfileResult]
        public string FilterTest()
        {
            Thread.Sleep(2000);
            return "This is the FilterTest action";
        }

        //控制器也实现了IActionFilter和IResultFilter接口，可以直接在控制器里面重写下面的两个方法
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();
            filterContext.HttpContext.Response.Write(
                $"<div>Total elapsed time:{timer.Elapsed.TotalSeconds:F6}</div>");
        }
    }
}