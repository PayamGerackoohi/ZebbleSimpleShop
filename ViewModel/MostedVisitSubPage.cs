using Domain.Api;
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
    class MostVisitedSubPage : EzSubPage
    {
        public BindableCollection<Product> Products { get; private set; } = new();

        public override async Task Setup()
        {
            Products.Replace(await Api.ShopApi.MostVisitedProducts());
        }
    }
}
