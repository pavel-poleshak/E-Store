using E_Store.Domain.Abstract;
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
        public ActionResult Index()
        {
            return View();
        }
    }
}