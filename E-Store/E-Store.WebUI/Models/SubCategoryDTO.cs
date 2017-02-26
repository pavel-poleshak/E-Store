using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Store.WebUI.Models
{
    public class SubCategoryDTO
    {
        public IEnumerable<SubCategory> SubCategories { get; set; }
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}