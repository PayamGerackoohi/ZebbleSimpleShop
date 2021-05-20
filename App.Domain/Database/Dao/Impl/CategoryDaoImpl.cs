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

        private List<Category> LoadAll() => Database.GetAllWithChildren<Category>(recursive: true);

        public List<Category> ReadAllRaw() => LoadAll()?.AlsoList(c => c.PrepareDBRead())?.ToList();

        public List<Category> ReadAll() => LoadAll()?.Where(c => c.IsRoot)?.AlsoList(c => c.PrepareDBRead())?.ToList();

        public List<Category> ReadChildrenOf(int parentId) => LoadAll()?.Where(c => c.ParentId == parentId)?.AlsoList(c => c.PrepareDBRead())?.ToList();

        public Category ReadRaw(int id) => LoadAll().Where(c => c.Id == id).FirstOrDefault();

        public void Save(Category category)
        {
            if (category.Id == 0)
                Create(category);
            category.PrepareDBWrite();
            Update(category);
        }

        public void SaveRaw(Category category)
        {
            if (category.Id == 0)
                Create(category);
            Update(category);
        }

        public void Update(Category category) => Database.UpdateWithChildren(category);

        public void Delete(Category category) => Database.Delete(category);

        public List<Category> GetSubCategories() => ReadAllRaw().Where(c => c.SubCategoryList.None()).ToList();

        public void SaveAll(List<Category> categories) => categories.Do(c => Save(c));
    }
}
