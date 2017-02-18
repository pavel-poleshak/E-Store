using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Abstract
{
    public interface IRepository<T> where T:class
    {
        IQueryable<T> GetAll();
        T Get(int id);
        IQueryable<T> Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        T Delete(int id);
    }
}
