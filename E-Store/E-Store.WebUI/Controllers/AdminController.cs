using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Store.WebUI.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        IUnitOfWork repository;

        public AdminController(IUnitOfWork repo)
        {
            repository = repo;
        }
        // GET: Admin
        public ViewResult Index()
        {
            return View(repository.Products.GetAll());
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products.GetAll().FirstOrDefault(p => p.ProductId == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Products.Update(product);
                repository.Save();
                TempData["message"] = string.Format("Изменение к товару {0} были успешно применены.", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Products.Create(product);
                repository.Save();
                TempData["message"]= string.Format("Товар {0} были успешно добавлен.", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View();              
            }

        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = repository.Products.Delete(productId);
            repository.Save();
            if (deletedProduct!=null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удален", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}