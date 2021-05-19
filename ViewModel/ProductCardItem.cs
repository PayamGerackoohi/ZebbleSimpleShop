using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI;
using ViewModel.Base;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class ProductCardItem : EzSubPage // This ViewModel will be replaced by the last ViewModel of the list, so one cannot use Product, because it's the latest list item data not its own one. Hence, I've cached Product in the View and would use it later for the "void ShowDetails(Product)". Altogether, This ViewModel class does not obey MVVM structure by storing data in View and too clumsy to exist (No way, it's Zebble's ViewModel system fault).
    {
        public Bindable<Product> Data { get; set; } = new();

        public string Title() => Data.Value.Name;

        public string ShortCription() => Data.Value.ShortCription;

        public string Price() => Data.Value.FormattedPrice();

        public void ShowDetails(Product p)
        {
            Holder.EzForward<ProductDetailPage>(config: vm => vm.Setup(p).RunInParallel());
        }

        //public override async Task OnRefresh()
        //{
        //}

        public override async Task Setup()
        {
        }
    }
}
