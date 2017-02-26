using E_Store.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Entities
{
    public class OrderProcessor : IOrderProcessor
    {
        IUnitOfWork repository;
        public bool Processed { get; private set; }
        public void ProcessOrder(Cart cart, Customer customer, ShippingDetails shippingDetails)
        {
            try
            {
                Order order = new Order(DateTime.Now, shippingDetails);
                Customer existCustomer = repository.Customers.Find(c => c.Email == customer.Email).FirstOrDefault();
                if (existCustomer==null)
                {
                    customer.CreatingDate = DateTime.Now;
                    customer.Orders.Add(order);
                    repository.Customers.Create(customer);
                }
                else
                {
                    existCustomer.Orders.Add(order);
                    repository.Customers.Update(existCustomer);
                }
                          
                
                
                foreach (var item in cart.Items)
                {
                    OrderLine orderLine = new OrderLine(order.OrderId, item.Product.ProductId, item.Quantity);
                    repository.OrderLines.Create(orderLine);
                }
                repository.Save();
                Processed = true;
                
               
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                Processed = false;
                //throw;
            }
        }

        public OrderProcessor(IUnitOfWork context)
        {
            this.repository = context;
        }
    }
}
