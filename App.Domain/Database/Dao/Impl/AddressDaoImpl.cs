using Domain.Database.Dao.Base;
using Domain.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Database.Dao.Impl
{
    class AddressDaoImpl : BaseDao, IAddressDao
    {
        public AddressDaoImpl(ShopDatabase database) : base(database) { }

        public void Create(Address address) => Database.InsertWithChildren(address);

        public List<Address> ReadAll() => Database.GetAllWithChildren<Address>(recursive: true);

        public Address Read() => ReadAll().FirstOrDefault();

        public void Save(Address address)
        {
            var a = Read();
            if (a == null)
                Create(address);
            else
            {
                address.Id = a.Id;
                Update(address);
            }
        }

        public void Update(Address address) => Database.UpdateWithChildren(address);

        public void Delete(Address address) => Database.Delete(address);
    }
}
