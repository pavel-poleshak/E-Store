using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using E_Store.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

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
    }
}
