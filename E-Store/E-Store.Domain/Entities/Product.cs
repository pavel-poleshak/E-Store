using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required(ErrorMessage ="Вы не указали стоимость")]
        [Range(typeof(decimal), "0,01", "9999999,99", ErrorMessage = "Пожалуйста, укажите стоимость товара")]
        //[DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^\d+\,\d{0,2}$", ErrorMessage ="Неверный формат записи")]
        public decimal Price { get; set; }
        
        [ForeignKey("SubCategory")]              
        public int? SubCategoryId { get; set; }
       
        [Display(Name ="Категория")]        
        public SubCategory SubCategory { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }        
    }
}
