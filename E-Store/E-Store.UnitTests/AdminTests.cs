using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using E_Store.Domain.Abstract;
using System.Collections.Generic;
using E_Store.Domain.Entities;
using E_Store.WebUI.Controllers;
using System.Linq;

namespace E_Store.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            // Arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>()
            {
                new Product() {ProductId=1 },
                new Product() {ProductId=2 },
                new Product() {ProductId=3 }
            });

            AdminController controller = new AdminController(mock.Object);

            // Act
            List<Product> result = ((IEnumerable<Product>)controller.Index().Model).ToList();

            // Assert
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0].ProductId, 1);
            Assert.AreEqual(result[1].ProductId, 2);
            Assert.AreEqual(result[2].ProductId, 3);
        }
    }
}
