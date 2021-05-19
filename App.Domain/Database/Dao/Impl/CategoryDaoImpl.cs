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
    class CategoryDaoImpl : BaseDao, ICategoryDao
    {
        public CategoryDaoImpl(ShopDatabase database) : base(database) { }

        public void Create(Category category) => Database.InsertWithChildren(category);

        public List<Category> ReadAllRaw() => Database.GetAllWithChildren<Category>(recursive: true)?.AlsoList(c => c.PrepareDBRead())?.ToList();

        public List<Category> ReadAll() => Database.GetAllWithChildren<Category>(recursive: true)?.Where(c => c.IsRoot)?.AlsoList(c => c.PrepareDBRead())?.ToList();

        public Category ReadRaw(int id) => Database.GetAllWithChildren<Category>(recursive: true).Where(c => c.Id == id).FirstOrDefault();

        public void Save(Category category)
        {
            category.PrepareDBWrite();
            if (category.Id == 0)
                Create(category);
            else
                Update(category);
        }

        public void Update(Category category) => Database.UpdateWithChildren(category);

        public void Delete(Category category) => Database.Delete(category);

        public List<Category> GetSubCategories() => ReadAllRaw().Where(c => c.SubCategoryList.None()).ToList();

        public void SaveAll(List<Category> categories) => categories.Do(c => Save(c));
    }
}
