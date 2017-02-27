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
        IUnitOfWork repository;
        public NavController(IUnitOfWork repo)
        {
            repository = repo;
        }
        // GET: Nav
       [ChildActionOnly]
        public PartialViewResult Menu(string category = null)
        {
            //ViewBag.SelectedCategory = category;
            //IEnumerable<string> categories = repository.Categories.GetAll()
            //    .Select(p=>p.Name)
            //    .OrderBy(p=>p);
            //MenuItemsViewModel model = new MenuItemsViewModel()
            //{
            //    Categories = categories,
            //    SelectedCategory = category

            //};
            List<SubCategoryDTO> listSubCategories = (from cat in repository.Categories.GetAll()
                                                      select new SubCategoryDTO
                                                      {
                                                          CategoryId=cat.Id,
                                                          CategoryName = cat.Name,
                                                          SubCategories = (from subCategory in cat.SubCategories
                                                                           select subCategory).ToList()
                                                      }).ToList();
            //ViewBag.SubCategories = listSubCategories;
            return PartialView(listSubCategories.OrderBy(m => m.CategoryName));
        }
    }
}