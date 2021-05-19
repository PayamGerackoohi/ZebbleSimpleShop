using Domain.Database.Dao.Base;
using Domain.Models;
using Domain.Utils;
using Olive;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Database.Dao.Impl
{
    class UserDaoImpl : BaseDao, IUserDao
    {
        public UserDaoImpl(ShopDatabase database) : base(database) { }

        public void Create(User user) => Database.Insert(user);

        public List<User> ReadAll()
        {
            var users = Database.GetAllWithChildren<User>(recursive: true);
            users.Do(user => user.Orders.Do(order =>
            {
                order.OrderItems.Do(oi => oi.Product = Database.ProductDao.Read(oi.ProductId));
                order.Status = Database.OrderStatusDao.Read(order.StatusId);
            }));
            return users;
        }

        public User Read() => ReadAll().FirstOrDefault();

        public void Save(User user)
        {
            Database.CredentialDao.Save(user.Credential);
            Database.AddressDao.Save(user.Address);

            var u = Read();
            if (u == null)
                Create(user);
            else
            {
                user.Id = u.Id;
            }
            Update(user);
        }

        public void Update(User user) => Database.UpdateWithChildren(user);

        public void Delete(User user) => Database.Delete(user);
    }
}
