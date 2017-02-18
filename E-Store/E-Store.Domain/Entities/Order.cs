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
        public DateTime CreatingDate { get; private set; }
        public virtual ICollection<OrderLine> OrderLines { get;  private set; }
        public ShippingDetails ShippingDetails { get; private set; }

        public Order (DateTime creatingDate, ShippingDetails shippingDetails)
        {
            CreatingDate = creatingDate;           
            ShippingDetails = shippingDetails;
        }
        private Order() { }
        
    }
}
