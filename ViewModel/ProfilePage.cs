using Domain.Api;
using Domain.Models;
using Domain.Utils;
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
    class ProfilePage : EzPage
    {
        public Bindable<User> User { get; private set; } = new();
        public BindableCollection<Order> Orders { get; private set; } = new();
        public BindableCollection<Product> Favorites { get; private set; } = new();

        public override async Task Setup()
        {
            await OnRefresh();
        }

        public async Task ShowOrderInfo(Order order)
        {
            EzForward<OrderInfoPage>(config: vm => vm.Order.Value = order);
        }

        public async Task ShowProductDetail(Product product)
        {
            EzForward<ProductDetailPage>(config: vm => vm.Setup(product).RunInParallel());
        }

        public async Task RefreshFavorites()
        {
            User.Value = await Api.ShopApi.GetUser();
            Favorites.Replace(User.Value.Favorites);
        }

        public override async Task OnRefresh()
        {
            User.Value = await Api.ShopApi.GetUser();
            Orders.Replace(User.Value.Orders);
            Favorites.Replace(User.Value.Favorites);
            User.Refresh();
            // Orders.Refresh(); // evil rests here in peace >:)
            // Favorites.Refresh(); // evil rests here in peace >:)
            await base.OnRefresh();
        }
    }
}
