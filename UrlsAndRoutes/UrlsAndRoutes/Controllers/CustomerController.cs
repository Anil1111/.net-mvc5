using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlsAndRoutes.Controllers
{
    [RoutePrefix("Users")]
    public class CustomerController : Controller
    {
        // GET: Customer
        [Route("~/Test")]
        public ActionResult Index()
        {
            ViewBag.Controller = "Customer";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        [Route("Add/{user}/{id:int}",Name ="AddRoute")]        //约束id的类型，给这条属性路由定义名称
        public string Create(string user,int id)
        {
            return $"User: {user}, ID: {id}";
        }

        [Route("Add/{user}/{password:alpha:length(6)}")]        //组合约束
        public string ChangePass(string user, string password)
        {
            return $"Change Pass Method - User: {user}, Pass: {password}";
        }

        public ActionResult List()
        {
            ViewBag.Controller = "Customer";
            ViewBag.Action = "List";
            return View("ActionName");
        }
    }
}