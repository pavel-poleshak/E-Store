using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using E_Store.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Moq;
using E_Store.Domain.Abstract;
using E_Store.WebUI.Controllers;
using System.Web.Mvc;
using E_Store.WebUI.Models;

namespace E_Store.UnitTests
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Can_Add_New_Items()           
        {
            //aarrange
            Product p1 = new Product() { ProductId = 1, Name = "Kaspersky AV" };
            Product p2 = new Product() { ProductId = 2, Name = "NOD32" };
            Cart cart = new Cart();

            //act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            List<CartItem> items = cart.Items.ToList();

            //assert
            Assert.AreEqual(items[0].Product, p1);
            Assert.AreEqual(items[1].Product, p2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Items()
        {
            //arrange
            Product p1 = new Product() { ProductId = 2, Name = "Kaspersky AV" };
            Product p2 = new Product() { ProductId = 1, Name = "NOD32" };
            Cart cart = new Cart();

            //act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 2);

            List<CartItem> items = cart.Items.OrderBy(c => c.Product.ProductId).ToList();

            //assert

            Assert.AreEqual(items[0].Product, p2);
            Assert.AreEqual(items[0].Quantity, 1);
            Assert.AreEqual(items[1].Product, p1);
            Assert.AreEqual(items[1].Quantity, 3);
        }

        [TestMethod]
        public void Can_Remove_Items_From_Cart()
        {
            //arrange
            Product p1 = new Product() { ProductId = 2, Name = "Kaspersky AV" };
            Product p2 = new Product() { ProductId = 1, Name = "NOD32" };
            Product p3 = new Product() { ProductId = 3, Name = "AVG" };
            Cart cart = new Cart();

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            cart.AddItem(p3, 3);

            //act

            cart.RemoveItem(p1);

            //assert

            Assert.AreEqual(cart.Items.Where(p => p.Product.ProductId == p1.ProductId).Count(), 0);
            Assert.AreEqual(cart.Items.Count(), 2);
        }

        [TestMethod]
        public void Can_Calculate_Carts_Price()
        {
            //arrange
            Product p1 = new Product() { ProductId = 2, Name = "Kaspersky AV", Price = 10M };
            Product p2 = new Product() { ProductId = 1, Name = "NOD32", Price = 13M };
            Product p3 = new Product() { ProductId = 3, Name = "AVG", Price = 8M };

            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p3, 3);

            //act

            decimal totalPrice = cart.CalculateTotalPrice();

            //assert

            Assert.AreEqual(totalPrice, 60M);

        }
        [TestMethod]
        public void Can_Clear_Cart()
        {
            //arange
            Product p1 = new Product() { ProductId = 2, Name = "Kaspersky AV", Price = 10M };
            Product p2 = new Product() { ProductId = 1, Name = "NOD32", Price = 13M };
            Product p3 = new Product() { ProductId = 3, Name = "AVG", Price = 8M };

            Cart cart = new Cart();
            cart.AddItem(p1,1);
            cart.AddItem(p2,1);
            cart.AddItem(p3,1);

            //act

            cart.Clear();

            //assert

            Assert.AreEqual(cart.Items.Count(), 0);

        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(p => p.Products.GetAll()).Returns(new List<Product>()
            {
                new Product() {ProductId=1, Name="Kaspersky AV", SubCategoryId=1, Price=10M }
            }.AsQueryable());

            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object,null);

            // Act
            controller.AddToCart(cart, 1, null);

            // Assert
            Assert.AreEqual(cart.Items.Count(), 1);
            Assert.AreEqual(cart.Items.ToList()[0].Product.ProductId, 1);

        }

        [TestMethod]
        public void Adding_Product_To_Cart_And_Redirect_To_Cart()
        {

            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(p => p.Products.GetAll()).Returns(new List<Product>()
            {
                new Product() {ProductId=1, Name="Kaspersky AV", SubCategoryId=1, Price=10M }
            }.AsQueryable());

            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object, null);

            // Act
            RedirectToRouteResult result = controller.AddToCart(cart, 1, "testUrl");

            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "testUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Content()
        {
            // Arrange
            Cart cart = new Cart();
            CartController controller = new CartController(null, null);

            // Act
            CartIndexViewModel result = (CartIndexViewModel)controller.Index(cart, "testUrl").Model;

            // Assert
            Assert.AreEqual(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "testUrl");
        }

        [TestMethod]
        public void Can_View_CheckoutContent()
        {
            // Arrange            
            CartController controller = new CartController(null, null);

            // Act
            CheckoutViewModel model = (CheckoutViewModel)controller.Checkout().Model;

            // Arrange
            Assert.AreEqual(model.Customer, null);
            Assert.AreEqual(model.ShippingDetails, null);
        }

        [TestMethod]
        public void Can_Add_Order_To_Db()
        {
            // Arrange
            Mock<IUnitOfWork> mockDb = new Mock<IUnitOfWork>();
            mockDb.Setup(m => m.Customers.GetAll()).Returns(new List<Customer>().AsQueryable());
            mockDb.Setup(m => m.OrderLines.GetAll()).Returns(new List<OrderLine>().AsQueryable());

            OrderProcessor orderProcessor = new OrderProcessor(mockDb.Object);  
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            CheckoutViewModel checkoutModel = new CheckoutViewModel()
            {
                Customer = new Customer(),
                ShippingDetails = new ShippingDetails()
            }; 
            CartController controller = new CartController(mockDb.Object, orderProcessor);


            // Act
            controller.Checkout(cart, checkoutModel);


            // Assert
            mockDb.Verify(m => m.Customers.Create(It.IsAny<Customer>()));
            mockDb.Verify(m => m.OrderLines.Create(It.IsAny<OrderLine>()));
            mockDb.Verify(m => m.Save());



        }





    }
}
