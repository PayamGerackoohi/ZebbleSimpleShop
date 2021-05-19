using Domain.Database.Dao.Base;
using Domain.Models;
using Olive;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Database.Dao.Impl
{
    class ProductDaoImpl : BaseDao, IProductDao
    {
        public ProductDaoImpl(ShopDatabase database) : base(database) { }

        public void Create(Product product) => Database.InsertWithChildren(product);

        public List<Product> ReadAll() => Database.GetAllWithChildren<Product>(recursive: true);

        public Product Read(int id) => Database.GetWithChildren<Product>(id);

        public void Save(Product product)
        {
            if (product.Id == 0)
                Create(product);
            else
                Update(product);
        }

        public void Update(Product product) => Database.UpdateWithChildren(product);

        public void Delete(Product product) => Database.Delete(product);

        public void SaveAll(List<Product> products) => products.Do(p => Save(p));
    }
}
