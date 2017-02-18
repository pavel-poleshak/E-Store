using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Concrete
{
    public class EFCustomerRepository : IRepository<Customer>
    {
        EFDbContext context;

        public EFCustomerRepository(EFDbContext context)
        {
            this.context = context;
        }
        public void Create(Customer item)
        {
            if (item!=null)
            {
                context.Customers.Add(item);
            }

        }

        public void Delete(int id)
        {
            Customer customer = context.Customers.Find(id);
            if (customer!=null)
            {
                context.Customers.Remove(customer);
            }
        }

        public IQueryable<Customer> Find(Func<Customer, bool> predicate)
        {
            return context.Customers.Where(predicate).AsQueryable();
        }

        public Customer Get(int id)
        {
            return context.Customers.Find(id);
        }

        public IQueryable<Customer> GetAll()
        {
            return context.Customers.AsQueryable();
        }

        public void Update(Customer item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
    }
}
