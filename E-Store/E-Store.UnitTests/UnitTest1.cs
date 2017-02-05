using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using E_Store.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace E_Store.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>()
            {
                new Product() {ProductId=1, Name="P1" },
                new Product() {ProductId=2, Name="P2" },
                new Product() {ProductId=3, Name="P3" },
                new Product() {ProductId=4, Name="P4" },
                new Product() {ProductId=5, Name="P5" },
                new Product() {ProductId=6, Name="P6" },
                new Product() {ProductId=7, Name="P7" },

            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //act
            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;

            //assert

            List<Product> products = result.ToList();
            Assert.IsTrue(products.Count == 3);
            Assert.AreEqual(products[0].Name, "P4");
            Assert.AreEqual(products[1].Name, "P5");          
            
           
        }
    }
}
