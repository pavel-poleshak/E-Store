using E_Store.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Store.Domain.Entities;

namespace E_Store.Domain.Concrete
{
    public class OrderProcessor : IOrderProcessor
    {
        private IOrdersRepository repository;
        public OrderProcessor(IOrdersRepository repo)
        {
            repository = repo;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            if ((cart!=null)&& (shippingDetails!=null))
            {
                Order order = new Order(cart, shippingDetails);
                repository.SaveOrder(order);         
            }        
        }
    }
}
