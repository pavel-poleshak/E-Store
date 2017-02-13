using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage ="Укажите ваше имя")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Укажите адрес доставки")]
        public string Street { get; set; }
        [Required(ErrorMessage ="Укажите город")]
        public string City { get; set; }
        [Required(ErrorMessage ="Укажите страну")]
        public string Country { get; set; }
    }
}
