using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Store.Domain.Abstract
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
