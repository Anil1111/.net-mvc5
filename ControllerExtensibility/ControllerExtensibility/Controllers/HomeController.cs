using ControllerExtensibility.Infrastructure;
using ControllerExtensibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("Result", new Result { ControllerName = "Home", ActionName = "Index" });
        }

        [Local]//通过动作方法选择器，处理有歧义的Action
        [ActionName("Index")]
        public ActionResult LocalIndex()
        {
            return View("Result", new Result { ControllerName = "Home", ActionName = "LocalIndex" });
        }

        //在控制器重写HandleUnknownAction方法处理客户端请求不存在的Action的情况
        protected override void HandleUnknownAction(string actionName)
        {
            Response.Write($"You requested the {actionName} action");
        }
    }
}