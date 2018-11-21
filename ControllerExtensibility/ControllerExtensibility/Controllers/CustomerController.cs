using ControllerExtensibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View("Result", new Result { ControllerName = "Customer", ActionName = "Index" });
        }

        [ActionName("Enumerate")] //自定义Action名称
        public ViewResult List()
        {
            return View("Result", new Result { ControllerName = "Customer", ActionName = "List" });
        }


        [NonAction]
        public ActionResult MyAction()
        {
            return View();
        }
    }
}