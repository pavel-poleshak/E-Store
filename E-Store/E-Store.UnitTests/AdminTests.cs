using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using E_Store.Domain.Abstract;
using System.Collections.Generic;
using E_Store.Domain.Entities;
using E_Store.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace E_Store.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Products.GetAll()).Returns(new List<Product>()
            {
                new Product() {ProductId=1 },
                new Product() {ProductId=2 },
                new Product() {ProductId=3 }
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            // Act
            List<Product> result = ((IQueryable<Product>)(controller.Index().Model)).ToList();

            // Assert
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0].ProductId, 1);
            Assert.AreEqual(result[1].ProductId, 2);
            Assert.AreEqual(result[2].ProductId, 3);
        }

        [TestMethod]
        public void Can_Edit_Product()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Products.GetAll()).Returns(new List<Product>()
            {
                new Product() {ProductId=1 },
                new Product() {ProductId=2 },
                new Product() {ProductId=3 }
            }.AsQueryable());

            mock.Setup(m => m.Categories.GetAll()).Returns(new List<Category>()
            {
                new Category() {Id=1, Name="Bikes", SubCategories=new List<SubCategory>()
                {
                    new SubCategory() {Id=1, Name="Road" },
                    new SubCategory() {Id=2, Name="Track" }
                } }
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            // Act
            Product p1 = (Product)controller.EditProduct(2).ViewData.Model;
            Product p2 = (Product)controller.EditProduct(3).ViewData.Model;

            // Assert
            Assert.AreEqual(p1.ProductId, 2);
            Assert.AreEqual(p2.ProductId, 3);
        }

        [TestMethod]
        public void Cant_Edit_NonExistent_Product()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Products.GetAll()).Returns(new List<Product>()
            {
                new Product() {ProductId=1 },
                new Product() {ProductId=2 },
                new Product() {ProductId=3 }
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            //Act

            var result = (Product)controller.EditProduct(6).Model;

            // Assert

            Assert.IsNotInstanceOfType(result, typeof(Product));
        }

        [TestMethod]
        public void Can_Save_Valid_Changes_When_Edit_Product()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Products.GetAll()).Returns(new List<Product>().AsQueryable());
            AdminController controller = new AdminController(mock.Object);
            Product p1 = new Product() { ProductId = 1 };

            // Act
            ActionResult result = controller.EditProduct(p1);

            // Assert
            mock.Verify(m => m.Products.Update(p1));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cant_Save_Product_With_Invalid_Changes_When_Edit()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            AdminController controller = new AdminController(mock.Object);
            Product p1 = new Product() { ProductId = 1 };
            controller.ModelState.AddModelError("Test Error", "Error");

            // Act
            ActionResult result = controller.EditProduct(p1);

            // Assert
            mock.Verify(m => m.Products.Update(It.IsAny<Product>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Products()
        {
            // Arrange
            Product product = new Product() { ProductId = 3, Name = "P3" };

            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Products.GetAll()).Returns(new List<Product>()
            {
                new Product() {ProductId=1, Name="P1" },
                new Product() {ProductId=2, Name="P2" },
                new Product() {ProductId=3, Name="P3" }
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            // Act
            controller.DeleteProduct(product.ProductId);           

            // Assert
            mock.Verify(m => m.Products.Delete(product.ProductId));

        }

        [TestMethod]
        public void Can_View_Create_Product_Model()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Categories.GetAll()).Returns(new List<Category>()
            {
                new Category()
                {
                    Id =1,
                    Name ="Category",
                    SubCategories =new List<SubCategory>()
                    {
                        new SubCategory() { Id=1, Name="SubCAtegory"}
                    }
                }
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            // Act
            var model = controller.CreateProduct().Model;

            // Assert
            Assert.AreEqual(typeof(Product), model.GetType());
        }

        [TestMethod]
        public void Cant_Create_New_Product_With_Incorrect_ModelState()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            AdminController controller = new AdminController(mock.Object);
            controller.ModelState.AddModelError("Error", "Error");

            // Act
            ActionResult result = controller.CreateProduct(new Product());

            // Assert
            mock.Verify(m => m.Products.Create(It.IsAny<Product>()), Times.Never);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Can_Create_New_Product_With_Correct_ModelState()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Products.GetAll()).Returns(new List<Product>().AsQueryable());
            AdminController controller = new AdminController(mock.Object);            

            // Act
            ActionResult result = controller.CreateProduct(new Product());

            // Assert
            mock.Verify(m => m.Products.Create(It.IsAny<Product>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Can_View_Customers_Model()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Customers.GetAll()).Returns(new List<Customer>().AsQueryable());
            AdminController controller = new AdminController(mock.Object);

            // Act
            List<Customer> list = ((IQueryable<Customer>)controller.ViewCustomers().Model).ToList();

            // Assert
            mock.Verify(m => m.Customers.GetAll(), Times.Once);
            Assert.AreEqual(0, list.Count);          
        }

        [TestMethod]
        public void Can_View_Create_New_Customer_Model()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            AdminController controller = new AdminController(mock.Object);

            // Act
            var act = controller.CreateCustomer();
            Customer model = (Customer)controller.CreateCustomer().Model;

            // Assert
            Assert.IsInstanceOfType(act,typeof(ViewResult));
        }

        [TestMethod]
        public void Cant_Create_New_Customer_With_Incorrect_ModelState()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            AdminController controller = new AdminController(mock.Object);
            controller.ModelState.AddModelError("Error", "Error");

            // Act
            ActionResult result = controller.CreateCustomer(new Customer());

            // Assert
            mock.Verify(m => m.Customers.Create(It.IsAny<Customer>()), Times.Never);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cant_Create_New_Customer_With_Correct_ModelState()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Customers.GetAll()).Returns(new List<Customer>().AsQueryable());
            AdminController controller = new AdminController(mock.Object);

            // Act
            ActionResult result = controller.CreateCustomer(new Customer());

            // Assert
            mock.Verify(m => m.Customers.Create(It.IsAny<Customer>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Can_Edit_Customer()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Customers.Get(It.IsAny<int>())).Returns(new Customer() { CustomerId = 1 });
            AdminController controller = new AdminController(mock.Object);

            // Act           
            Customer model = (Customer)controller.EditCustomer(1).Model;

            // Assert
            mock.Verify(m => m.Customers.Get(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(1, model.CustomerId);
        }

        [TestMethod]
        public void Cant_Save_Change_When_Edit_Customer_With_Incorrect_Model()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            AdminController controller = new AdminController(mock.Object);
            Customer c1 = new Customer() { CustomerId = 1 };
            controller.ModelState.AddModelError("Test Error", "Error");

            // Act
            ActionResult result = controller.EditCustomer(c1);

            // Assert
            mock.Verify(m => m.Customers.Update(It.IsAny<Customer>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Save_Change_When_Edit_Customer_With_Correct_Model()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Customers.GetAll()).Returns(new List<Customer>() { new Customer() { CustomerId = 1 } }.AsQueryable());
            AdminController controller = new AdminController(mock.Object);
            Customer c1 = new Customer() { CustomerId = 1 };           

            // Act
            ActionResult result = controller.EditCustomer(c1);

            // Assert
            mock.Verify(m => m.Customers.Update(It.IsAny<Customer>()), Times.Once());
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Can_Delete_Customer()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            Customer c = new Customer() { CustomerId = 1 };
            mock.Setup(m => m.Customers.GetAll()).Returns(new List<Customer>() { new Customer() { CustomerId = 1 } }.AsQueryable());           
            AdminController controller = new AdminController(mock.Object);

            // Act
            controller.DeleteCustomer(c.CustomerId);

            // Assert                      
            mock.Verify(m => m.Customers.Delete(c.CustomerId), Times.Once);
        }
        

        [TestMethod]
        public void Can_View_Orders_Model()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Orders.GetAll()).Returns(new List<Order>() { new Order(DateTime.Now, new ShippingDetails()) }.AsQueryable());
            AdminController controller = new AdminController(mock.Object);

            // Act
            List<Order> model = ((IEnumerable<Order>)controller.ViewOrders().Model).ToList();

            // Assert
            Assert.AreEqual(mock.Object.Orders.GetAll().Count(), model.Count());
        }

        [TestMethod]
        public void Can_Delete_Order()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(m => m.Orders.GetAll()).Returns(new List<Order>() { new Order(DateTime.Now, new ShippingDetails()) {OrderId=1 } }.AsQueryable());
            AdminController controller = new AdminController(mock.Object);

            // Act
            controller.DeleteOrder(1);

            // Assert
            mock.Verify(m => m.Orders.Delete(1));                  
        }








    }
}
