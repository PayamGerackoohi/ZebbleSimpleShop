using Domain.Database.Dao.Base;
using Domain.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Database.Dao.Impl
{
    class OrderStatusDaoImpl : BaseDao, IOrderStatusDao
    {
        public OrderStatusDaoImpl(ShopDatabase database) : base(database) { }

        public void Create(OrderStatus orderStatus) => Database.Insert(orderStatus);

        public List<OrderStatus> ReadAll() => Database.GetAllWithChildren<OrderStatus>(recursive: true);

        public OrderStatus Read(int id) => Database.GetWithChildren<OrderStatus>(id);

        public void Save(OrderStatus orderStatus)
        {
            if (orderStatus.Id == 0)
                Create(orderStatus);
            Update(orderStatus);
        }

        public void Update(OrderStatus orderStatus) => Database.UpdateWithChildren(orderStatus);

        public void Delete(OrderStatus orderStatus) => Database.Delete(orderStatus);
    }
}
