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
        [Required(ErrorMessage ="Пожалуйста, введите наименование")]
        public string Name { get; set; }

        [Display(Name ="Описание")]
        [Required(ErrorMessage ="Пожалуйста, введите описание товара")]
        public string Description { get; set; }

        [Display(Name ="Цена")]        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, укажите стоимость товара")]
        public decimal Price { get; set; }

        [Display(Name ="Категория")]
        [Required(ErrorMessage = "Пожалуйста, введите категорию товара")]
        public SubCategory SubCategory { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
