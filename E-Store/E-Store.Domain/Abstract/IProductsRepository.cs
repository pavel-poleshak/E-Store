﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Store.Domain.Entities;

namespace E_Store.Domain.Abstract
{
    interface IProductsRepository
    {
        IEnumerable<Product> Products { get; }
    }
}
