using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Database
{
    public interface IAddressDao : IBaseDao<Address>
    {
        public Address Read();
    }
}
