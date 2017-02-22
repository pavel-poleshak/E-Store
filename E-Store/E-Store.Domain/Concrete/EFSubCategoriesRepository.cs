using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Concrete
{
    public class EFSubCategoriesRepository:IRepository<SubCategory>
    {
        private EFDbContext context;
        public EFSubCategoriesRepository(EFDbContext context)
        {
            this.context = context;
        }

        public void Create(SubCategory item)
        {
            if (item!=null)
            {
                context.SubCategories.Add(item);
            }
        }

        public SubCategory Delete(int id)
        {
            SubCategory subCategoryToDelete = context.SubCategories.Find(id);
            if (subCategoryToDelete!=null)
            {
                context.SubCategories.Remove(subCategoryToDelete);
            }
            return subCategoryToDelete;
        }

        public IQueryable<SubCategory> Find(Func<SubCategory, bool> predicate)
        {
            return context.SubCategories.Where(predicate).AsQueryable();
        }

        public SubCategory Get(int id)
        {
            return context.SubCategories.Find(id);
        }

        public IQueryable<SubCategory> GetAll()
        {
            return context.SubCategories;
        }

        public void Update(SubCategory item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
