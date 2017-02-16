using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Concrete
{
    public class EFOrderContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public EFOrderContext():base("EStoreDB")
        {

        }
    }
}
