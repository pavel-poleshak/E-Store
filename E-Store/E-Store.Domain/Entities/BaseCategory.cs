using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Entities
{
    public abstract class BaseCategory
    {
        public int Id { get; set; }
        [Display(Name ="Наименование категории")]
        [Required(ErrorMessage ="Укажите категорию")]
        public string Name { get; set; }
        [Display(Name ="Описание")]
        [Required(ErrorMessage ="Укажите описание для категории")]
        public string Description { get; set; }
    }
}
