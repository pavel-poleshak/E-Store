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

        [TestMethod]
        public void Can_Edit()
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
            Product p1 = (Product)controller.Edit(2).ViewData.Model;
            Product p2 = (Product)controller.Edit(3).ViewData.Model;

            // Assert
            Assert.AreEqual(p1.ProductId, 2);
            Assert.AreEqual(p2.ProductId, 3);
        }

        [TestMethod]
        public void Cant_Edit_Nonexistent_Product()
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

            //Act

            Product result = (Product)controller.Edit(6).Model;

            // Assert

            Assert.AreEqual(result, null);

            
                        
           
        }
    }
}
