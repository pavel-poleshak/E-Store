using E_Store.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Store.Domain.Entities;

namespace E_Store.Domain.Concrete
{
    public class EFOrderRepository : IOrdersRepository
    {
        private EFOrderContext context = new EFOrderContext();
        public IEnumerable<Order> Orders
        {
            get
            {
                return context.Orders;
            }
        }

        public void SaveOrder(Order order)
        {
            if (order!=null)
            {
                context.Orders.Add(order);
                context.SaveChanges();
            }            
        }
    }
}
