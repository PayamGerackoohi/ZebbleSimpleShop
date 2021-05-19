using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Database
{
    public interface IUserDao : IBaseDao<User>
    {
        public User Read();
    }
}
