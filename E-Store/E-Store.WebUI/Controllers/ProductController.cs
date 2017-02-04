using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;

namespace E_Store.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;
        private int pageSize = 4;
        public ProductController(IProductsRepository productRepository)
        {
            repository = productRepository;
        }
        // GET: Product
        public ActionResult List(int page = 1)
        {
            return View(repository.Products
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize));
        }
    }
}