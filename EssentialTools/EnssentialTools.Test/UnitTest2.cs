using System;
using System.Linq;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EnssentialTools.Test
{
    [TestClass]
    public class UnitTest2
    {
        private Product[] products = {
            new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
            new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
            new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
        };
        [TestMethod]
        public void Sun_Products_Correctly()
        {
            //准备
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(t => t.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            //var discounter = new MinmumDiscountHelper();
            var target = new LinqValueCaculator(mock.Object);
            var goalTotal = products.Sum(t => t.Price);

            //动作
            var result = target.ValueProducts(products);

            Assert.AreEqual(goalTotal, result);
        }
    }
}
