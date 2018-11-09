using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
   public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            //Arrange product with image
            Product prod = new Product
            {
                ProductId = 2,
                Name = "Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            //Arrange Mock
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[]
            {
                new Product{ProductId=1,Name="P1"},
                new Product{ProductId=3,Name="P3"},
                prod
            }.AsQueryable());

            //Arrange controller
            ProductController target = new ProductController(mock.Object);

            //Action
            ActionResult result = target.GetImage(2);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(prod.ImageMimeType, ((FileResult)result).ContentType);



        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            //Arrange Mock
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(t => t.Products).Returns(new Product[]
            {
                new Product{ProductId=1,Name="P1"},
                new Product{ProductId=3,Name="P3"},
         
            }.AsQueryable());

            //Arrange controller
            ProductController target = new ProductController(mock.Object);
            //Action
            ActionResult result = target.GetImage(2);

            //Assert
            Assert.IsNull(result);
        }
    }
}
