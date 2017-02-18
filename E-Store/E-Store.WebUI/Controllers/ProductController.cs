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
        private IUnitOfWork repository;
        public int pageSize = 4;
       
        
        public ProductController(IUnitOfWork unitOfWork)
        {
            repository = unitOfWork;
        }
       
        // GET: Product
        public ViewResult List(string category, int page = 1)
        {
            IEnumerable<Product> listOfProducts = new List<Product>();
           
            if (category==null)
            {
                listOfProducts =  repository.Products.GetAll()
                                 .OrderBy(p=>p.ProductId)
                                 .Skip((page-1)*pageSize)
                                 .Take(pageSize);
            }
            else
            {
                listOfProducts = repository.Products.GetAll()
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
                    TotalItems = category == null ? repository.Products.GetAll().Count() :
                    repository.Products.GetAll().Where(p => p.Category == category).Count()
                },
                CurrentCategory = category

            };
            return View(model);
        }
    }
}