using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using E_Store.WebUI.Models;
using System.Data.Entity;

namespace E_Store.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IUnitOfWork repository;
        public int pageSize = 4;
        private int GetCountOfItems(string cat, string subCat)
        {
            if (cat==null)
            {
                return repository.Products.GetAll().Count();
            }
            if ((cat!=null) && (subCat==null))
            {
                return repository.Products.GetAll().Where(p => p.SubCategory.Category.Name == cat).Count();
            }
            else
            {
                return repository.Products.GetAll().Where(p => (p.SubCategory.Category.Name == cat) && (p.SubCategory.Name == subCat)).Count();
            }           
        }
       
       
        
        public ProductController(IUnitOfWork unitOfWork)
        {
            repository = unitOfWork;
        }
       
        // GET: Product
        public ViewResult List(string category, string subCategory=null, int page = 1)
        {
            IEnumerable<Product> listOfProducts = new List<Product>();
            
           
            if (category==null)
            {
                listOfProducts = repository.Products.GetAll()
                                 .Include(p => p.SubCategory)
                                 .OrderBy(p => p.ProductId)
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize);
            }
            else
            {
                if (category!=null &&subCategory==null)
                {
                    listOfProducts = repository.Products.GetAll().Include(p => p.SubCategory)
                                .Where(p => p.SubCategory.Category.Name == category)
                                .OrderBy(p => p.ProductId)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);
                }
                else
                {
                    listOfProducts = repository.Products.GetAll().Include(p => p.SubCategory)
                                .Where(p => (p.SubCategory.Category.Name == category) && (p.SubCategory.Name == subCategory))
                                .OrderBy(p => p.ProductId)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize);
                }
                
            }

            ProductListViewModel model = new ProductListViewModel()
            {
                Products = listOfProducts,
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = GetCountOfItems(category, subCategory) 
                },
                CurrentCategory = category

            };
            return View(model);
        }
    }
}