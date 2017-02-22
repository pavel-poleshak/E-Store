using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Store.WebUI.Models
{
    public class OrdersProductsViewModel
    {
        public Product Product { get; set; }    
        public int Quantity { get; set; }

    }
}