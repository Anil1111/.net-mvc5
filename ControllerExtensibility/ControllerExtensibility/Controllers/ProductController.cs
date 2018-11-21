﻿using ControllerExtensibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View("Result",new Result { ControllerName="Produt", ActionName="Index" });
        }

        public ViewResult List()
        {
            return View("Result", new Result { ControllerName = "Product", ActionName = "List" });
        }
    }
}