using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Entities
{
    public class Category:BaseCategory
    {
        public virtual ICollection<SubCategory> SubCategories { get; set; }
        public Category()
        {
            SubCategories = new List<SubCategory>();
        }
    }

}
