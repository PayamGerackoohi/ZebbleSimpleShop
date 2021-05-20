using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Database
{
    public interface ICategoryDao : IBaseDao<Category>
    {
        public List<Category> ReadAllRaw();

        public Category ReadRaw(int id);

        public List<Category> ReadChildrenOf(int parentId);

        public List<Category> GetSubCategories();

        public void SaveAll(List<Category> categories);

        public void SaveRaw(Category category);
    }
}
