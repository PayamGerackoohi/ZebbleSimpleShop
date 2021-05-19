using Domain.Database.Dao.Base;
using Domain.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Database.Dao.Impl
{
    class CredentialDaoImpl : BaseDao, ICredentialDao
    {
        public CredentialDaoImpl(ShopDatabase database) : base(database) { }

        public void Create(Credential credential) => Database.Insert(credential);

        public List<Credential> ReadAll() => Database.Table<Credential>().ToList();

        public Credential Read() => ReadAll().FirstOrDefault();

        public void Save(Credential credential)
        {
            var c = Read();
            if (c == null)
                Create(credential);
            else
            {
                credential.Id = c.Id;
                Update(credential);
            }
        }

        public void Update(Credential credential) => Database.Update(credential);

        public void Delete(Credential credential) => Database.Delete(credential);
    }
}
