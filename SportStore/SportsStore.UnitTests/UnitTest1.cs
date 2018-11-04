﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        //测试分页方法
        [TestMethod]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[] {
                new Product{ProductId=1,Name="P1"},
                new Product{ProductId=2,Name="P2"},
                new Product{ProductId=2,Name="P3"},
                new Product{ProductId=2,Name="P4"},
                new Product{ProductId=2,Name="P5"}
            });

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //Action
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null,2).Model;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");

        }

        //测试分页生成html
        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //Action
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                          + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                          + @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());
        }

        //测试分页页面模型视图数据

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[] {
                new Product{ProductId=1,Name="P1"},
                new Product{ProductId=2,Name="P2"},
                new Product{ProductId=2,Name="P3"},
                new Product{ProductId=2,Name="P4"},
                new Product{ProductId=2,Name="P5"}
            });

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //Action
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null,2).Model;

            //Assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);

        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[] {
                new Product{ProductId=1,Name="P1", Category="Cat1"},
                new Product{ProductId=2,Name="P2",Category="Cat2"},
                new Product{ProductId=2,Name="P3",Category="Cat1"},
                new Product{ProductId=2,Name="P4",Category="Cat2"},
                new Product{ProductId=2,Name="P5",Category="Cat3"}
            });
            
            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //Action
            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[0].Category == "Cat2");

        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[] {
                new Product{ProductId=1,Name="P1", Category="Apples"},
                new Product{ProductId=2,Name="P2",Category="Apples"},
                new Product{ProductId=2,Name="P3",Category="Plums"},
                new Product{ProductId=2,Name="P4",Category="Oranges"},
            });

            NavController target = new NavController(mock.Object);

            string[] result = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Apples");
            Assert.AreEqual(result[1], "Oranges");
            Assert.AreEqual(result[2], "Plums");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[] {
                new Product{ProductId=1,Name="P1", Category="Apples"},
                new Product{ProductId=4,Name="P2",Category="Oranges"},
         
            });
            NavController target = new NavController(mock.Object);
            string categoryToSelect = "Apples";

            //Action

            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        //特定分类的产品数
        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[] {
                new Product{ProductId=1,Name="P1", Category="Cat1"},
                new Product{ProductId=2,Name="P2",Category="Cat2"},
                new Product{ProductId=2,Name="P3",Category="Cat1"},
                new Product{ProductId=2,Name="P4",Category="Cat2"},
                new Product{ProductId=2,Name="P5",Category="Cat3"}
            });

            ProductController target = new ProductController(mock.Object);
            target.pageSize = 3;

            //Action
            int res1 = ((ProductsListViewModel)target.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModel)target.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModel)target.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProductsListViewModel)target.List(null).Model).PagingInfo.TotalItems;

            //Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);


        }
    }
}
