using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public RedirectToRouteResult AddToCart(Cart cart,int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(t=>t.ProductId==productId);
            if(product!=null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });

        }

        public RedirectToRouteResult RemoveFromCart(Cart cart,int productId,string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(t => t.ProductId == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        //结算页面
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        //有了Cart模型绑定就可以删除方法
        //private Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart == null)
        //    { 
        //        cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}


    }
}