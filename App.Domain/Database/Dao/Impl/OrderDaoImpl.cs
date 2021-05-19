using Domain.Database.Dao.Base;
using Domain.Models;
using Olive;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Database.Dao.Impl
{
    class OrderDaoImpl : BaseDao, IOrderDao
    {
        public OrderDaoImpl(ShopDatabase database) : base(database) { }

        public void Create(Order order) => Database.Insert(order);

        public List<Order> ReadAll()
        {
            var orders = Database.GetAllWithChildren<Order>(recursive: true);
            orders.Do(order => order.OrderItems.Do(oi => oi.Product = Database.ProductDao.Read(oi.ProductId)));
            return orders;
        }

        public void Save(Order order)
        {
            Database.OrderStatusDao.Save(order.Status);
            order.OrderItems.Do(oi => Database.OrderItemDao.Save(oi));

            if (order.IsCart)
            {
                var cart = GetCart();
                if (cart == null)
                    Create(order);
                else
                    order.Id = cart.Id;
            }
            else
            {
                if (order.Id == 0)
                    Create(order);
            }
            Update(order);
        }

        public void Update(Order order) => Database.UpdateWithChildren(order);

        public void Delete(Order order)
        {
            order.OrderItems.Do(oi => Database.OrderItemDao.Delete(oi));
            Database.OrderStatusDao.Delete(order.Status);
            Database.Delete(order);
        }

        public Order GetCart() => ReadAll()?.Where(o => o.IsCart)?.FirstOrDefault();

        public void AddToCart(Product product)
        {
            var cart = GetCart();
            var item = cart.OrderItems.Where(oi => oi.Product.Id == product.Id).FirstOrDefault();
            if (item == null)
                cart.OrderItems.Add(new OrderItem { Product = product });
            else
                item.Count++;
            Save(cart);
        }

        public void RemoveFromCart(OrderItem order)
        {
            var cart = GetCart();
            cart.OrderItems.RemoveWhere(oi => oi.Id == order.Id);
            Database.OrderItemDao.Delete(order);
            Save(cart);
        }
    }
}
