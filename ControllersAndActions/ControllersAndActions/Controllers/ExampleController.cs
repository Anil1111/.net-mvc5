using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {
        // GET: Example
        public ViewResult Index()
        {
            ViewBag.Message = "Hello";
            ViewBag.Date = DateTime.Now;
            DateTime date = DateTime.Now;
            //return View("Homepage");
            //return View(date);
            return View();
        }

        public RedirectResult Redirect()
        {
            return RedirectPermanent("/Example/Index"); //永久重定向
        }


        public RedirectToRouteResult RedirectRoute()
        {
            
            return RedirectToRoute(new { controller = "Example", action = "Index", ID = "MyID" });
        }


        public HttpStatusCodeResult StatusCode()
        {
            //return new HttpStatusCodeResult(404, "URL cannot be serviced");

            //return HttpNotFound();//返回HttpNotFoundResult实例，继承于HttpStatusCodeResult

            return new HttpUnauthorizedResult();//返回401，未授权
        }


    }
}