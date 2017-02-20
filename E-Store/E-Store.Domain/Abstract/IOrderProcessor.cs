using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Abstract
{
    public interface IOrderProcessor
    {
        bool Processed { get; }
        void ProcessOrder(Cart cart, Customer customer, ShippingDetails shippingDetails);
        
    }
}
