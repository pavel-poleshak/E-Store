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
    public class EFProductRepository : IRepository<Product>
    {
        private EFDbContext context;

        public EFProductRepository(EFDbContext context)
        {
            this.context = context;
        }
        public void Create(Product item)
        {
            if (item!=null)
            {
                context.Products.Add(item);
            }            
        }

        public Product Delete(int id)
        {
            Product product = context.Products.Find(id);
            if (product!=null)
            {
                context.Products.Remove(product);
            }
            return product;
        }

        public IQueryable<Product> Find(Func<Product, bool> predicate)
        {
            return context.Products.Where(predicate).AsQueryable();
        }

        public Product Get(int id)
        {
            return context.Products.Find(id);
        }

        public IQueryable<Product> GetAll()
        {
           return context.Products;
        }

        public void Update(Product item)
        {
            Product productToUpdate = context.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
            productToUpdate.Name = item.Name;
            productToUpdate.Description = item.Description;
            productToUpdate.Price = item.Price;
            productToUpdate.SubCategory = item.SubCategory;
            context.Entry(productToUpdate).State=EntityState.Modified;
        }
    }
}
