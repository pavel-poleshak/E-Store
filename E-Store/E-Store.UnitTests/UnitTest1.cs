using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using E_Store.WebUI.Controllers;
using E_Store.WebUI.HtmlHelpers;
using E_Store.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Runtime;
using Microsoft.CSharp;

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
            controller.pageSize = 3;

            //act
            ProductListViewModel result = (ProductListViewModel)controller.List(null,2).Model;

            //assert

            List<Product> products = result.Products.ToList();
            Assert.IsTrue(products.Count == 3);
            Assert.AreEqual(products[0].Name, "P4");
            Assert.AreEqual(products[1].Name, "P5"); 
        }
        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //arrange
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>()
            {
                new Product() {ProductId=1,Name="Screen" },
                new Product() {ProductId=2, Name="Mouse" },
                new Product() {ProductId=3,Name="keyboard" },
                new Product() {ProductId=4, Name="TouchPad" },
                new Product() {ProductId=5,Name="Cable" }
            });

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //act
            ProductListViewModel result = (ProductListViewModel)controller.List(null,2).Model;
            PagingInfo pagingInfo = result.PagingInfo;


            //assert
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);


        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            //arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product() {ProductId=1, Category="Motherboards" },
                new Product() {ProductId=2, Category="Videocards" },
                new Product() {ProductId=3, Category="Splitters" },
                new Product() {ProductId=4, Category=null },
                new Product() {ProductId=5, Category="Videocards" },
                new Product() {ProductId=6, Category="" },
                new Product() {ProductId=7 }

            });

            NavController controller = new NavController(mock.Object);

            //act          
            List<string> categories = ((IEnumerable<string>)controller.Menu().Model).ToList();

            //assert

            Assert.AreEqual(categories.Count, 3);
            Assert.AreEqual(categories[0], "Motherboards");
            Assert.AreEqual(categories[1], "Splitters");
        }
        [TestMethod]
        public void Can_Get_Selected_Category()
        {
            //arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product() {ProductId=1, Name="Asus GTX", Category="VGA" },
                new Product() {ProductId=2, Name="Gigabyte R290", Category="HDMI" }
            });

            NavController controller = new NavController(mock.Object);
            string categoryToSelect = "VGA";

            //act

            string selectedCategory = controller.Menu(categoryToSelect).ViewBag.SelectedCategory;          
          
            //assert
            Assert.AreEqual(selectedCategory, categoryToSelect);

        }
        [TestMethod]
        public void Can_Get_Products_By_Category()
        {
            //arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product() {ProductId=1, Category=null },
                new Product() {ProductId=2,Category="VGA" },
                new Product() {ProductId=3,Category=null },
                new Product() {ProductId=4,Category="VGA" },
                new Product() {ProductId=5,Category="HDMI" },
                new Product() {ProductId=6,Category="DVI" },
                new Product() {ProductId=7,Category=null }
            });

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 10;

            //act
            var result = (ProductListViewModel)controller.List("HDMI").Model;
            int totalItems = result.PagingInfo.TotalItems;

            List<Product> list = result.Products.ToList();
            //assert

            Assert.AreEqual(totalItems, 1);
            Assert.AreEqual(list[0].Category, "HDMI");
            //Assert.AreEqual(list[1].Category, "VGA");
        }
    }
}
