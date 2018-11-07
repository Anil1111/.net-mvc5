using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            //Arrange Products
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };

            //准备创建购物车
            Cart target = new Cart();

            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] result = target.Lines.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Product, p1);
            Assert.AreEqual(result[1].Product, p2);
        }

        //测试如果用户已经添加了一个产品，再添加同样产品
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Arrange Products
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };

            //准备创建购物车
            Cart target = new Cart();

            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] result = target.Lines.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Quantity, 11);
            Assert.AreEqual(result[1].Quantity, 1);
        }

        //测试用户从购物车删除产品
        [TestMethod]
        public void Can_Remove_Line()
        {
            //Arrange Products
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };
            Product p3 = new Product { ProductId = 3, Name = "P3" };

            //准备创建购物车
            Cart target = new Cart();

          
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);
            //Action
            target.RemoveLine(p2);

            //Assert
            Assert.AreEqual(target.Lines.Where(t=>t.Product==p2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
           
        }

        //计算购物车中各个物品总价
        [TestMethod]
        public void Can_Calculate_Cart_Total()
        {
            //Arrange Products
            Product p1 = new Product { ProductId = 1, Name = "P1",Price=100M };
            Product p2 = new Product { ProductId = 2, Name = "P2",Price=50M };
           

            //准备创建购物车
            Cart target = new Cart();

            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();
          

            //Assert
            
            Assert.AreEqual(result, 450M);

        }


        //清空购物车
        [TestMethod]
        public void Can_Clear_Contents()
        {
            //Arrange Products
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 50M };


            //准备创建购物车
            Cart target = new Cart();

            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            target.Clear();


            //Assert

            Assert.AreEqual(target.Lines.Count(), 0);

        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[] {
                new Product{ProductId=1,Name="P1",Category="Apples" },
            }.AsQueryable());

            Cart cart = new Cart();

            CartController target = new CartController(mock.Object,null);

            //Action
            target.AddToCart(cart, 1, null);

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductId, 1);
        }

        //测试购物车返回地址
        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[] {
                new Product{ProductId=1,Name="P1",Category="Apples" },
            }.AsQueryable());

            Cart cart = new Cart();

            CartController target = new CartController(mock.Object,null);

            //Action
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");

            //Assert
            Assert.AreEqual(result.RouteValues["Action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            Cart cart = new Cart();

            CartController target = new CartController(null,null);

            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").Model;

            //Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart();

            ShippingDetails shippingDetails = new ShippingDetails();

            CartController target = new CartController(null, mock.Object);

            //Action
            ViewResult result = target.Checkout(cart, shippingDetails);

            //Assert 检查，订单没有传递给处理器
            mock.Verify(t => t.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),Times.Never);
            //Assert 检查，方法返回默认视图
            Assert.AreEqual("", result.ViewName);
            //Assert 检查，非法模型
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }


        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

           

            CartController target = new CartController(null, mock.Object);
            //把错误添加到模型
            target.ModelState.AddModelError("error", "error");

            //Action 试图结算
            ViewResult result = target.Checkout(cart, new ShippingDetails());

            //Assert 检查，订单没有传递给处理器
            mock.Verify(t => t.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never);
            //Assert 检查，方法返回默认视图
            Assert.AreEqual("", result.ViewName);
            //Assert 检查，非法模型
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }


        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);



            CartController target = new CartController(null, mock.Object);
            
            

            //Action 试图结算
            ViewResult result = target.Checkout(cart, new ShippingDetails());

            //Assert 检查，订单已经被处理
            mock.Verify(t => t.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once);
            //Assert 检查，方法返回Completed视图
            Assert.AreEqual("Completed", result.ViewName);
            //Assert 检查，把有效模型传递给视图
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}
