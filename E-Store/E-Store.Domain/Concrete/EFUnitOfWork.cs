using E_Store.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Store.Domain.Entities;

namespace E_Store.Domain.Concrete
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private EFDbContext context;
        private EFCustomerRepository customerRepository;
        private EFProductRepository productRepository;
        private EFOrderRepository orderRepository;
        private EFOrderLineRepository orderLineRepository;

        public EFUnitOfWork()
        {
            this.context = new EFDbContext();
        }
        public EFUnitOfWork(string connectionString)
        {
            this.context = new EFDbContext(connectionString);
        }

        public IRepository<Customer> Customers
        {
            get
            {
                return customerRepository ?? new EFCustomerRepository(context);
            }
        }

        public IOrderLineRepository OrderLines
        {
            get
            {
                return orderLineRepository ?? new EFOrderLineRepository(context);
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                return orderRepository ?? new EFOrderRepository(context);
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                return productRepository ?? new EFProductRepository(context);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
