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
        public IEnumerable<CartItem> Products { get;  private set; }
        public ShippingDetails ShippingDetails { get; private set; }

        public Order (Cart cart, ShippingDetails shippingDetails)
        {
            CreatingDate = DateTime.Now;
            Products = cart.Items;
            ShippingDetails = shippingDetails;
        }
        
    }
}
