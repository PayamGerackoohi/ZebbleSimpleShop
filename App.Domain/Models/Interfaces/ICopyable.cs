using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Interfaces
{
    public interface ICopyable<T>
    {
        public T Copy();
    }
}
