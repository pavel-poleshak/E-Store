using E_Store.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Store.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IProductsRepository repository;

        public AdminController(IProductsRepository repo)
        {
            repository = repo;
        }
        // GET: Admin
        public ViewResult Index()
        {
            return View(repository.Products);
        }
    }
}