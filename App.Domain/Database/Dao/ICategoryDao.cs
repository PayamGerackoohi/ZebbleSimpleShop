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

        public List<Category> GetSubCategories();

        public void SaveAll(List<Category> categories);

        public Category ReadRaw(int id);
    }
}
