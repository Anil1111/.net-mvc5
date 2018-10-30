using System;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnssentialTools.Test
{
    [TestClass]
    public class UnitTest1
    {
        private IDiscountHelper GetTestObject()
        {
            return new MinmumDiscountHelper();
        }
        [TestMethod]
        public void Discount_Above_100()
        {
            //Arrange
            IDiscountHelper target = GetTestObject();
            decimal total = 200;

            //Action
            var discountedTotal = target.ApplyDiscount(total);

            //Assert
            Assert.AreEqual(total * 0.9m, discountedTotal);
        }
        [TestMethod]
        public void Discount_Bwtween_10_And_100()
        {
            IDiscountHelper target = GetTestObject();

            decimal tenDollarDiscount = target.ApplyDiscount(10);
            decimal hundredDollarDiscount = target.ApplyDiscount(100);
            decimal fiftyDollarDiscount = target.ApplyDiscount(50);

            Assert.AreEqual(5, tenDollarDiscount, "10 discount is wrong");
            Assert.AreEqual(95, hundredDollarDiscount, "100 discount is wrong");
            Assert.AreEqual(45, fiftyDollarDiscount, "50 discount is wrong");
        }

            [TestMethod]
        public void Discount_Less_Than_10()
        {
            IDiscountHelper target = GetTestObject();
            decimal discount5 = target.ApplyDiscount(5);
            decimal discount0 = target.ApplyDiscount(0);

            Assert.AreEqual(5, discount5);
            Assert.AreEqual(0, discount0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Discount_Negative_Total()
        {
            IDiscountHelper target = GetTestObject();

            target.ApplyDiscount(-1);
        }
    }
}
