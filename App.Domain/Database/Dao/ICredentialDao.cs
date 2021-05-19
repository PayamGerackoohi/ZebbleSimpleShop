using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Database
{
    public interface ICredentialDao : IBaseDao<Credential>
    {
        public Credential Read();
    }
}
