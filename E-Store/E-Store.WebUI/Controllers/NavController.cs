using E_Store.Domain.Abstract;
using E_Store.WebUI.Models;
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
       [ChildActionOnly]
        public PartialViewResult Menu(string category = null)
        {
            //ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Products
                .Select(p => p.Category)
                .Where(p => p != null && p != "")
                .Distinct()
                .OrderBy(x => x);
            MenuItemsViewModel model = new MenuItemsViewModel()
            {
                Categories = categories,
                SelectedCategory = category

            };
            return PartialView(model);
        }
    }
}