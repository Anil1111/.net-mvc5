using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repositpry;

        public AdminController(IProductRepository repo)
        {
            repositpry = repo;
        }
        // GET: Admin
        public ViewResult Index()
        {
           
            return View(repositpry.Products);
        }

        public ViewResult Edit(int productid)
        {
            Product product = repositpry.Products.FirstOrDefault(t => t.ProductId == productid);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                repositpry.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productid)
        {
            Product delProduct = repositpry.DeleteProduct(productid);
            if(delProduct!=null)
            {
                TempData["message"] = string.Format($"{delProduct.Name} was deleted");

            }
            return RedirectToAction("Index");
        }

    }
}