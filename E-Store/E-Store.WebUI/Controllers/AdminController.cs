using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using E_Store.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Globalization;
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
            return View(repository.Products.GetAll().Include(p=>p.SubCategory));
        }
        
        public ViewResult EditProduct(int productId)
        {
            Product product = repository.Products.GetAll().FirstOrDefault(p => p.ProductId == productId);
            if (product!=null)
            {
                List<SubCategoryDTO> listSubCategories = (from category in repository.Categories.GetAll()
                                                          select new SubCategoryDTO
                                                          {
                                                              CategoryName = category.Name,
                                                              SubCategories = (from subCategory in category.SubCategories
                                                                               select subCategory).ToList()
                                                          }).ToList();
                ViewBag.SubCategories = listSubCategories;
                return View(product);
            }
            return View("Index");
            
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
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
                return RedirectToAction("EditProduct", product.ProductId);
            }
        }

        [HttpGet]
        public ViewResult CreateProduct()
        {                       
            List<SubCategoryDTO> listSubCategories = (from category in repository.Categories.GetAll()
                                                      select new SubCategoryDTO
                                                      {
                                                          CategoryName = category.Name,
                                                          SubCategories = (from subCategory in category.SubCategories
                                                                           select subCategory).ToList()
                                                      }).ToList();
            ViewBag.SubCategories = listSubCategories;
            

            return View(new Product());
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
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
                return RedirectToAction("CreateProduct");              
            }

        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            Product deletedProduct = repository.Products.Delete(productId);
            
            if (deletedProduct!=null)
            {
                repository.Save();
                TempData["message"] = string.Format("Товар \"{0}\" был удален", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

        public ViewResult ViewCustomers()
        {
            return View(repository.Customers.GetAll());
        }

        [HttpGet]
        public ViewResult CreateCustomer()
        {
            return View(new Customer());
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CreatingDate = DateTime.Now;
                repository.Customers.Create(customer);
                repository.Save();
                TempData["message"] = string.Format("Пользователь {0} были успешно добавлен.", customer.Name);
                return RedirectToAction("ViewCustomers");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ViewResult EditCustomer(int customerId)
        {
            Customer customer = repository.Customers.Get(customerId);
            return View(customer);
        }

        [HttpPost]
        public ActionResult EditCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                repository.Customers.Update(customer);
                repository.Save();
                TempData["message"] = string.Format("Изменение к покупателю {0} были успешно применены.", customer.Name);
                return RedirectToAction("ViewCustomers");
            }
            else
            {
                return View(customer);
            }
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int customerId)
        {
           Customer deletedCustomer = repository.Customers.Delete(customerId);

            if (deletedCustomer != null)
            {
                repository.Save();
                TempData["message"] = string.Format("Покупатель \"{0}\" был удален", deletedCustomer.Name);
            }
            return RedirectToAction("ViewCustomers");
        }

        [HttpGet]
        public ViewResult ViewOrders()
        {
            var orders = (from order in repository.Orders.GetAll()
                          select order).Include(order => order.Customer); 
            return View(orders.ToList());
        }

        [HttpPost]
        public ActionResult DeleteOrder(int orderId)
        {
            Order deletedOrder = repository.Orders.Delete(orderId);

            if (deletedOrder != null)
            {
                repository.Save();
                TempData["message"] = string.Format("Заказ \"{0}\" был удален", deletedOrder.OrderId);
            }
            return RedirectToAction("ViewOrders");
        }
        [HttpGet]
        public ActionResult GetOrdersProducts(int orderId)
        {  
            var query = from order in repository.Orders.GetAll()
                        where order.OrderId==orderId
                        from orderLine in order.OrderLines
                        select new OrdersProductsViewModel
                        {
                            Product = orderLine.Product,
                            Quantity = orderLine.Quantity
                        };
            
            return PartialView("OrderDetails", query.ToList());
        }

        [HttpGet]
        public ViewResult ViewCategories()
        {
            return View(repository.Categories.GetAll());
        }

        [HttpGet]
        public ViewResult CreateCategory()
        {
            return View(new Category());
        }

        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                Category temp = repository.Categories.Find(c => c.Name == category.Name).FirstOrDefault();
                if (temp==null)
                {
                    repository.Categories.Create(category);
                    repository.Save();
                    TempData["message"] = string.Format("Категория {0} была успешно добавлена.", category.Name);
                    return RedirectToAction("ViewCategories");
                }
                else
                {
                    TempData["message"] = string.Format("Категория {0} уже существует.", category.Name);
                    return View(category);
                } 
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ViewResult EditCategory(int categoryId)
        {
            Category categoryToEdit = repository.Categories.Get(categoryId);
            return View(categoryToEdit);
        }

        [HttpPost]
        public ActionResult EditCategory (Category category)
        {
            if (ModelState.IsValid)
            {
                repository.Categories.Update(category);
                repository.Save();
                TempData["message"] = string.Format("Изменение к категории {0} были успешно применены.", category.Name);
                return RedirectToAction("ViewCategories");
            }
            else
            {
                return View(category);
            }
        }


        [HttpGet]
        public ActionResult GetSubCategories(int categoryId)
        {
            var query = (from category in repository.Categories.GetAll()
                        where category.Id == categoryId
                        select new SubCategoryDTO
                        {
                            SubCategories = category.SubCategories.ToList(),
                            CategoryId = category.Id
                        }).Single();
          
            return PartialView("SubCategories", query);
        }

        [HttpPost]
        public ActionResult CreateSubCategory(int categoryId, string name, string description)
        {
            SubCategory subCategory = new SubCategory() { Name = name, Description = description, Category_Id=categoryId };
            repository.SubCategories.Create(subCategory);           
            repository.Save();
            TempData["messageInSubCategoriesModal"] = string.Format("Подкатегория \"{0}\" была успешно добавлена.", subCategory.Name);
            return GetSubCategories(categoryId);            
        }

        [HttpPost]
        public ActionResult DeleteSubCategory(int subCategoryId, int categoryId)
        {
            SubCategory deletedSubCategory = repository.SubCategories.Delete(subCategoryId);
            if (deletedSubCategory!=null)
            {
                repository.Save();
                TempData["messageInSubCategoriesModal"]= string.Format("Подкатегория {0} была успешно удалена.", deletedSubCategory.Name);
            }
            return GetSubCategories(categoryId);
        }

        [HttpPost]
        public ActionResult DeleteCategory(int categoryId)
        {
            Category categoryToDelete = repository.Categories.Delete(categoryId);
            if (categoryToDelete!=null)
            {
                repository.Save();
                TempData["message"] = string.Format("Категория {0} была успешно удалена.", categoryToDelete.Name);
            }
            return RedirectToAction("ViewCategories");
        }


    }
}