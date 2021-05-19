using Domain.Database.Dao.Base;
using Domain.Models;
using Olive;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Database.Dao.Impl
{
    class OrderItemDaoImpl : BaseDao, IOrderItemDao
    {
        public OrderItemDaoImpl(ShopDatabase database) : base(database) { }

        public void Create(OrderItem orderItem) => Database.InsertWithChildren(orderItem);

        public List<OrderItem> ReadAll() => Database.GetAllWithChildren<OrderItem>(recursive: true);

        public void Save(OrderItem orderItem)
        {
            if (orderItem.Id == 0)
                Create(orderItem);
            else
                Update(orderItem);
        }

        public void Update(OrderItem orderItem) => Database.UpdateWithChildren(orderItem);

        public void Delete(OrderItem orderItem) => Database.Delete(orderItem);

        public void SaveAll(List<OrderItem> orderItems) => orderItems.Do(oi => Save(oi));
    }
}
