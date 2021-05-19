using SQLite;

namespace Domain.Database.Dao.Base
{
    public class BaseDao
    {
        protected readonly ShopDatabase Database;

        public BaseDao(ShopDatabase database)
        {
            Database = database;
        }
    }
}
