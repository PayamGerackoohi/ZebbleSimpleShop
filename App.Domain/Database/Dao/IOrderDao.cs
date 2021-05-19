using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Database
{
    public interface IOrderDao : IBaseDao<Order>
    {
        public Order GetCart();

        public void AddToCart(Product product);

        public void RemoveFromCart(OrderItem order);
    }
}
