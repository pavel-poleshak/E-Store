using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage ="Укажите ваше имя")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Укажите ваш E-Mail")]
        public string Email { get; set; }
        public DateTime CreatingDate { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}
