using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Store.WebUI.Models
{
    public class CheckoutViewModel
    {
        public Customer Customer { get; set; }
        public ShippingDetails ShippingDetails { get; set; } 
    }
}