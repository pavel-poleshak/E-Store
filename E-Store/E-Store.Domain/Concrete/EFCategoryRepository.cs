using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Concrete
{
    public class EFCategoryRepository : IRepository<Category>
    {
        private EFDbContext context;
        public EFCategoryRepository(EFDbContext context)
        {
            this.context = context;
        }
        public void Create(Category item)
        {
            if (item!=null)
            {
                context.Categories.Add(item);
            }
        }

        public Category Delete(int id)
        {
            Category categoryToDelete = context.Categories.Find(id);
            if (categoryToDelete!=null)
            {
                context.Categories.Remove(categoryToDelete);
            }
            return categoryToDelete;
        }

        public IQueryable<Category> Find(Func<Category, bool> predicate)
        {
            return context.Categories.Where(predicate).AsQueryable();
        }

        public Category Get(int id)
        {
            return context.Categories.Find(id);
        }

        public IQueryable<Category> GetAll()
        {
            return context.Categories;
        }

        public void Update(Category item)
        {
            // Category category = context.Categories.FirstOrDefault(c => c.Id == item.Id);
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;

        }
    }
}
