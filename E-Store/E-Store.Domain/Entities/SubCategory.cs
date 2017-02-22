
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Store.Domain.Entities
{
    public class SubCategory:BaseCategory
    {   
        [ForeignKey("Category")]
        public int? Category_Id { get; set; }   
        public virtual Category Category { get; set; }
    }
}