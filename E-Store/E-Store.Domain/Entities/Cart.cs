using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Entities
{
    public class Cart
    {
        private List<CartItem> cartItems = new List<CartItem>();

        public void AddItem(Product product, int quantity)
        {
            CartItem items = cartItems
                .Where(p => p.Product.ProductId == product.ProductId)
                .FirstOrDefault();

            if (items==null)
            {
                cartItems.Add(new CartItem()
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                items.Quantity += quantity;              
            }
        }

        public void RemoveItem(Product product)
        {
            cartItems.RemoveAll(c => c.Product.ProductId == product.ProductId);
        }

    }
}
