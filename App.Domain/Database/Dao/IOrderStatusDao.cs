using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Database
{
    public interface IOrderStatusDao : IBaseDao<OrderStatus>
    {
        public OrderStatus Read(int id);
    }
}
