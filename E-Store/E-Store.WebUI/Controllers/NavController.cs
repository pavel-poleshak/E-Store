﻿using E_Store.Domain.Abstract;
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
        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = repository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}