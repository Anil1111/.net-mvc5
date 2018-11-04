using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
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

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products.Where(t => category == null || t.Category == category)
                                              .OrderBy(t => t.ProductId)
                                              .Skip((page - 1) * pageSize)
                                              .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(t => t.Category == category).Count()

                },
                CurrentCategory = category

            };
            return View(model);
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}