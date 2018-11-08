using System;
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
   public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            //Arrange创建模仿存储库
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[] {
            new Product{ProductId=1,Name="P1"},
            new Product{ProductId=2,Name="P2"},
            new Product{ProductId=3,Name="P3"},
            });

            //Arrange控制器
            AdminController target = new AdminController(mock.Object);

            //Action 只有ViewResult这里才能拿到Model
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray() ;

            //Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
            
        }

        [TestMethod]
        public void Can_Edit_Product()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[] {
            new Product{ProductId=1,Name="P1"},
            new Product{ProductId=2,Name="P2"},
            new Product{ProductId=3,Name="P3"},
            });

            AdminController target = new AdminController(mock.Object);

            //Action

            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            //Assert

            Assert.AreEqual(1, p1.ProductId);
            Assert.AreEqual(2, p2.ProductId);
            Assert.AreEqual(3, p3.ProductId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[] {
            new Product{ProductId=1,Name="P1"},
            new Product{ProductId=2,Name="P2"},
            });

            AdminController target = new AdminController(mock.Object);

            //Action

            Product result = target.Edit(4).ViewData.Model as Product;
            

            //Assert

            Assert.IsNull(result);
 
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController target = new AdminController(mock.Object);
            Product product = new Product { Name = "Test" };

            //Action
            ActionResult result = target.Edit(product);

            //Assert检查方法结果类型
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController target = new AdminController(mock.Object);
            Product product = new Product { Name = "Test" };
            target.ModelState.AddModelError("Error", "Error");

            //Action
            ActionResult result = target.Edit(product);

            //Assert确认存储库未被调用
            mock.Verify(t => t.SaveProduct(It.IsAny<Product>()), Times.Never());
            //Assert确认方法的结果类型
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
