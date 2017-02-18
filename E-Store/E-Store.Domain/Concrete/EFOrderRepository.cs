using E_Store.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Store.Domain.Entities;
using System.Data.Entity;

namespace E_Store.Domain.Concrete
{
    public class EFOrderRepository : IRepository<Order>
    {
        private EFDbContext context;

        public EFOrderRepository(EFDbContext context)
        {
            this.context = context;
        }
               
        public void Create(Order item)
        {
            if (item!=null)
            {
                context.Orders.Add(item);
            }            
        }

        public void Delete(int id)
        {
            Order order = context.Orders.Find(id);
            if (order!=null)
            {
                context.Orders.Remove(order);
            }
        }

        public IQueryable<Order> Find(Func<Order, bool> predicate)
        {
            return context.Orders.Where(predicate).AsQueryable();
        }

        public Order Get(int id)
        {
            return context.Orders.Find(id);
        }

        public IQueryable<Order> GetAll()
        {
            return context.Orders;
        }

        public void Update(Order item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
