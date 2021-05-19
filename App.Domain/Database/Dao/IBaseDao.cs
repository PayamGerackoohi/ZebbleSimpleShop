using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Database
{
    public interface IBaseDao<T>
    {
        public void Create(T item);

        public List<T> ReadAll();

        public void Save(T item);

        public void Update(T item);

        public void Delete(T item);
    }
}
