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
    class MostVisitedSubPage : SubPage
    {
        public BindableCollection<Product> Products = new();

        public override async Task OnUIReady()
        {
            Products.Replace(await Api.ShopApi.MostVisitedProducts());
        }
    }
}
