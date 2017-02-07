using E_Store.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Store.WebUI.Controllers
{
    public class NavController : Controller
    {
        IProductsRepository repository;
        public NavController(IProductsRepository repo)
        {
            repository = repo;
        }
        // GET: Nav
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Products
                .Select(p => p.Category)
                .Where(p => p != null && p != "")
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}