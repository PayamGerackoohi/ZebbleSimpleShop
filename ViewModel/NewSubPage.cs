﻿using Domain.Api;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class NewSubPage : EzSubPage
    {
        public BindableCollection<Product> Products = new();

        //public override async Task OnRefresh()
        //{
        //}

        protected override async Task Setup()
        {
            Products.Replace(await Api.ShopApi.NewProducts());
        }
    }
}
