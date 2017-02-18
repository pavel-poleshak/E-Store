using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Customer> Customers { get; }
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderLine> OrderLines { get; }
        void Save();
    }
}
