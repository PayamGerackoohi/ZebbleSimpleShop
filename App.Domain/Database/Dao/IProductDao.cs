﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Database
{
    public interface IProductDao : IBaseDao<Product>
    {
        public Product Read(int id);

        public void SaveAll(List<Product> products);
    }
}
