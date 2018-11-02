using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int pageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(int page=1)
        {
            var list = repository.Products.OrderBy(t=>t.ProductId).Skip((page-1)*pageSize).Take(pageSize);
            return View(list);
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}