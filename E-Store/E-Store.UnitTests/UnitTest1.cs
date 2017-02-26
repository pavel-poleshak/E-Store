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
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            
            mock.Setup(m => m.Products.GetAll()).Returns(new List<Product>()
            {
                new Product() {ProductId=1, Name="P1" },
                new Product() {ProductId=2, Name="P2" },
                new Product() {ProductId=3, Name="P3" },
                new Product() {ProductId=4, Name="P4" },
                new Product() {ProductId=5, Name="P5" },
                new Product() {ProductId=6, Name="P6" },
                new Product() {ProductId=7, Name="P7" },

            }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //act
            ProductListViewModel result = (ProductListViewModel)controller.List(null,null,2).Model;

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
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Products.GetAll()).Returns(new List<Product>()
            {
                new Product() {ProductId=1,Name="Screen" },
                new Product() {ProductId=2, Name="Mouse" },
                new Product() {ProductId=3,Name="keyboard" },
                new Product() {ProductId=4, Name="TouchPad" },
                new Product() {ProductId=5,Name="Cable" }
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //act
            ProductListViewModel result = (ProductListViewModel)controller.List(null,null,2).Model;
            PagingInfo pagingInfo = result.PagingInfo;


            //assert
            Assert.AreEqual( 2, pagingInfo.CurrentPage);
            Assert.AreEqual( 3, pagingInfo.ItemsPerPage);
            Assert.AreEqual( 5, pagingInfo.TotalItems);
            Assert.AreEqual( 2, pagingInfo.TotalPages); 
        }

        [TestMethod]
        public void Can_Add_Ordered_Categories_To_Main_Menu()
        {
            //arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Categories.GetAll()).Returns(new List<Category>
            {
                new Category() {Id=3, Name="Third" },
                new Category() {Id=1, Name="First" },
                new Category() {Id=2,Name="Second" }
                
            }.AsQueryable());           

            NavController controller = new NavController(mock.Object);

            //act          
            var model = (MenuItemsViewModel)controller.Menu().Model;
            List<string> categories = model.Categories.ToList();

            //assert

            Assert.AreEqual(categories.Count, 3);
            Assert.AreEqual(categories[0], "First");
            Assert.AreEqual(categories[1], "Second");
        }
        [TestMethod]
        public void Can_Get_Selected_Category()
        {
            //arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Categories.GetAll()).Returns(new List<Category>
            {
                new Category() {Id=3, Name="Third" },
                new Category() {Id=1, Name="First" },
                new Category() {Id=2,Name="Second" }

            }.AsQueryable());

            NavController controller = new NavController(mock.Object);
            string categoryToSelect = "First";

            //act

            var model = (MenuItemsViewModel)controller.Menu(categoryToSelect).Model;
            string selectedCategory = model.SelectedCategory;         
          
            //assert
            Assert.AreEqual(selectedCategory, categoryToSelect);

        }
        [TestMethod]
        public void Can_Get_Products_By_Category_And_SubCategory()
        {
            //arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>(); 
            mock.Setup(m => m.Products.GetAll()).Returns(new List<Product>
            {
                new Product() {ProductId=1,SubCategory=new SubCategory() {Name="Road", Category=new Category() { Name="Bikes"} } },
                new Product() {ProductId=2,SubCategory=new SubCategory() {Name="Track", Category=new Category() { Name="Bikes"} } }
                
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            //act
            var resultCategory = (ProductListViewModel)controller.List("Bikes").Model;
            var resultCategoryAndSubcategory = (ProductListViewModel)controller.List("Bikes", "Track").Model;

            int totalItemsByCategory = resultCategory.PagingInfo.TotalItems;
            int totalItemsBySubCategory = resultCategoryAndSubcategory.PagingInfo.TotalItems;

            List<Product> listByCategory = resultCategory.Products.ToList();
            List<Product> listByCategoryAndSubCategory = resultCategoryAndSubcategory.Products.ToList();

            //assert

            Assert.AreEqual(totalItemsByCategory, 2);
            Assert.AreEqual(totalItemsBySubCategory, 1);
            Assert.AreEqual(listByCategory[0].SubCategory.Name, "Road");
            Assert.AreEqual(listByCategoryAndSubCategory[0].SubCategory.Name, "Track");
        }
    }
}
