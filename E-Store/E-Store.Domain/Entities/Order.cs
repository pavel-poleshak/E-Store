using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime CreatingDate { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public ShippingDetails ShippingDetails { get; set; }

        public Order (DateTime creatingDate, ShippingDetails shippingDetails)
        {
            CreatingDate = creatingDate;           
            ShippingDetails = shippingDetails;
        }
        private Order() { }
        
    }
}
