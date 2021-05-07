using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public interface IValidable
    {
        public bool IsValid();
    }
}
