using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using E_Store.WebUI.Models;

namespace E_Store.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;
        public int pageSize = 4;
       
        
        public ProductController(IProductsRepository productRepository)
        {
            repository = productRepository;
        }
       
        // GET: Product
        public ViewResult List(string category, int page = 1)
        {
            IEnumerable<Product> listOfProducts = new List<Product>();
           
            if (category==null)
            {
                listOfProducts =  repository.Products
                                 .OrderBy(p=>p.ProductId)
                                 .Skip((page-1)*pageSize)
                                 .Take(pageSize);
            }
            else
            {
                listOfProducts = repository.Products
                                .Where(p =>p.Category == category)
                                .OrderBy(p => p.ProductId)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);
            }
            ProductListViewModel model = new ProductListViewModel()
            {
                Products = listOfProducts,
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? repository.Products.Count() :
                    repository.Products.Where(p => p.Category == category).Count()
                },
                CurrentCategory = category

            };
            return View(model);
        }
    }
}