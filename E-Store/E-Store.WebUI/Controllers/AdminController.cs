using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using E_Store.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ViewResult EditProduct(int productId)
        {
            Product product = repository.Products.GetAll().FirstOrDefault(p => p.ProductId == productId);
            return View(product);
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
                return View(product);
            }
        }

        [HttpGet]
        public ViewResult CreateProduct()
        {
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
                return View();              
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

        public ActionResult ViewCustomers()
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
    }
}