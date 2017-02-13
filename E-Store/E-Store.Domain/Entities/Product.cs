using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        [Display(Name ="Наименование")]
        public string Name { get; set; }
        [Display(Name ="Описание")]
        public string Description { get; set; }
        [Display(Name ="Цена")]
        public decimal Price { get; set; }
        [Display(Name ="Категория")]
        public string Category { get; set; }
    }
}
