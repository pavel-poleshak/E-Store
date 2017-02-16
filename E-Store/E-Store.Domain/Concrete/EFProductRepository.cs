using E_Store.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Store.Domain.Entities;

namespace E_Store.Domain.Concrete
{
    public class EFProductRepository : IProductsRepository
    {
        EFProductContext context = new EFProductContext();
        public IEnumerable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = context.Products.Find(productId);
            if (dbEntry!=null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId==0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductId);
                if (dbEntry!=null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Category = product.Category;
                    dbEntry.Price = product.Price;
                }
            }
            context.SaveChanges();
        }
    }
}
