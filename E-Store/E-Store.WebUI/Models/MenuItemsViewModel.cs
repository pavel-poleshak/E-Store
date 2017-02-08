using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Store.WebUI.Models
{
    public class MenuItemsViewModel
    {
        public IEnumerable<string> Categories { get; set; }
        public string SelectedCategory { get; set; }

    }
}