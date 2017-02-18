using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Entities
{
    public class OrderLine
    {        
        [Key, ForeignKey("Order")]
        [Column(Order = 0)]
        public int OrderId { get; set; }

        [Key, ForeignKey("Product")]
        [Column(Order = 1)]
        public int ProductId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public OrderLine(int orderId, int productId, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
        private OrderLine() { }
    }
}
