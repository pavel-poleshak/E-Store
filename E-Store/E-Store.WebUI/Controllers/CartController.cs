using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using E_Store.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Store.WebUI.Controllers
{
    public class CartController : Controller
    {
        IUnitOfWork repository;
        IOrderProcessor orderProcessor;


        public CartController(IUnitOfWork repo, IOrderProcessor orderProcessor)
        {
            repository = repo;
            this.orderProcessor = orderProcessor;
            
                        
        }
        // GET: Cart
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel()
            {
                Cart = cart,
                ReturnUrl = returnUrl

            });
        }       

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.GetAll().FirstOrDefault(p => p.ProductId == productId);
            if (product!=null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl});
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products.GetAll().FirstOrDefault(p => p.ProductId == productId);
            if (product!=null)
            {
                cart.RemoveItem(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        [ChildActionOnly]
        public PartialViewResult CartSummary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new CheckoutViewModel());
        }

        [HttpPost]
        public ActionResult Checkout(Cart cart, CheckoutViewModel checkOutViewModel)
        {
            orderProcessor.ProcessOrder(cart,checkOutViewModel.Customer, checkOutViewModel.ShippingDetails);
            if (!orderProcessor.Processed)
            {
                return View();
            }
            TempData["message"] = "Заказ успешно оформлен";
            return RedirectToAction("List", "Product");
        }

        
    }
}