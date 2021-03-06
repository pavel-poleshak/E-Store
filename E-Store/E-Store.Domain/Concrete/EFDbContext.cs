﻿using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Concrete
{
    public class EFDbContext:DbContext
    {
        public EFDbContext() : base("EStoreDB") { Database.SetInitializer<EFDbContext>(new DbDropCreateAlwaysInitializer()); }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        public EFDbContext(string connectionString = "EStoreDB") : base(connectionString)
        {
            Database.SetInitializer<EFDbContext>(new DbDropCreateAlwaysInitializer());
        }

       
    }
}
