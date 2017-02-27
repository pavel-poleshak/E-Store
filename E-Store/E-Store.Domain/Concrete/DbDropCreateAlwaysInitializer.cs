using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Concrete
{
    public class DbDropCreateAlwaysInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext db)
        {
            // Categories
            db.Categories.AddRange(new List<Category>()
            {
                new Category()
                {
                    Name ="Bikes",
                    Description="Bikes",
                    SubCategories = new List<SubCategory>()
                    {
                        new SubCategory() {Name="Mountain Bikes", Description="Mountain Bikes" },
                        new SubCategory() {Name="Road Bikes", Description="Road Bikes" },
                        new SubCategory() {Name="Touring Bikes", Description="Touring Bikes" }
                    }
                },
                new Category()
                {
                    Name="Components",
                    Description="Components",
                    SubCategories = new List<SubCategory>()
                    {
                        new SubCategory() {Name="Handlebars", Description="Handlebars" },
                        new SubCategory() {Name="Bottom Brackets", Description="Bottom Brackets" },
                        new SubCategory() {Name="Road Frames", Description="Road Frames" }
                    }
                },
                new Category()
                {
                    Name="Clothing",
                    Description="Clothing",
                    SubCategories = new List<SubCategory>()
                    {
                        new SubCategory() {Name="Bib-Shorts", Description="Bib-Shorts" },
                        new SubCategory() {Name="Gloves", Description="Gloves" },
                        new SubCategory() {Name="Vests", Description="Vests" }
                    }
                },
                new Category()
                {
                    Name="Accessories",
                    Description="Accessories",
                    SubCategories = new List<SubCategory>()
                    {
                        new SubCategory() {Name="Bike Racks", Description="Bike Racks" },
                        new SubCategory() {Name="Bottles and Cages", Description="Bottles and Cages" },
                        new SubCategory() {Name="Lights", Description="Lights" }
                    }
                } });
            db.SaveChanges();

            // Products
            db.Products.AddRange(new List<Product>()
            {
                new Product()
                {
                    Name="Mountain-100 Silver, 38",
                    Description="Mountain-100 Silver, 38",
                    Price=860.57M,
                    SubCategoryId=1
                },
                new Product()
                {
                    Name="Road-650 Red, 60",
                    Description="Road-650 Red, 60",
                    Price=1065.99M,
                    SubCategoryId=2
                },
                new Product()
                {
                    Name="Touring-3000 Blue, 62",
                    Description="Touring-3000 Blue, 62",
                    Price=1065.99M,
                    SubCategoryId=3
                },

                new Product()
                {
                    Name="LL Mountain Handlebars",
                    Description="LL Mountain Handlebars",
                    Price=740.01M,
                    SubCategoryId=4
                },
                new Product()
                {
                    Name="ML Bottom Bracket",
                    Description="ML Bottom Bracket",
                    Price=1065.99M,
                    SubCategoryId=5
                },
                new Product()
                {
                    Name="HL Road Frame - Red, 62",
                    Description="HL Road Frame - Red, 62",
                    Price=1065.99M,
                    SubCategoryId=6
                },
                new Product()
                {
                    Name="Men's Bib-Shorts, S",
                    Description="Men's Bib-Shorts, S",
                    Price=65.94M,
                    SubCategoryId=7
                },
                 new Product()
                {
                    Name="Half-Finger Gloves, M",
                    Description="Half-Finger Gloves, M",
                    Price=15.12M,
                    SubCategoryId=8
                },
                  new Product()
                {
                    Name="Classic Vest, M",
                    Description="Classic Vest, M",
                    Price=34.82M,
                    SubCategoryId=9
                },
                   new Product()
                {
                    Name="Hitch Rack - 4-Bike",
                    Description="Hitch Rack - 4-Bike",
                    Price=44.41M,
                    SubCategoryId=10
                },
                    new Product()
                {
                    Name="Mountain Bottle Cage",
                    Description="Mountain Bottle Cage",
                    Price=7.00M,
                    SubCategoryId=11
                },
                     new Product()
                {
                    Name="Headlights - Dual-Beam",
                    Description="Headlights - Dual-Beam",
                    Price=1.99M,
                    SubCategoryId=12
                },

            });
            db.SaveChanges();
        }
    }
}
