using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using E_Store.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Store.WebUI.Controllers
{
    public class CartController : Controller
    {
        IProductsRepository repository;

        public CartController(IProductsRepository repo)
        {
            repository = repo;
        }
        // GET: Cart
        public ActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel()
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl

            });
        }
        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart==null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;

        }

        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product!=null)
            {
                GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl});
        }
        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product!=null)
            {
                GetCart().RemoveItem(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}