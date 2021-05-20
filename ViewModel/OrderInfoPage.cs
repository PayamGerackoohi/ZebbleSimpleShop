using Domain.Api;
using Domain.Models;
using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class OrderInfoPage : EzPage
    {
        public Bindable<Order> Order { get; private set; } = new();

        public override async Task OnRefresh()
        {
            await base.OnRefresh();
        }

        public override async Task Setup()
        {
        }

        public async Task ShowProductDetail(Product product)
        {
            EzForward<ProductDetailPage>(config: vm => vm.Setup(product).RunInParallel());
        }
    }
}
