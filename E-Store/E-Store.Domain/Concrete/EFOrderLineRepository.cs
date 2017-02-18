using E_Store.Domain.Abstract;
using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Concrete
{
    public class EFOrderLineRepository : IOrderLineRepository
    {
        EFDbContext context;

        public EFOrderLineRepository(EFDbContext context)
        {
            this.context = context;
        }

        public void Create(OrderLine item)
        {
            if (item!=null)
            {
                context.OrderLines.Add(item);
            };
        }

        public IQueryable<OrderLine> GetAll()
        {
            return context.OrderLines;
        }
    }
}
