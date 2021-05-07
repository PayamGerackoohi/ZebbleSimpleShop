using Domain.Api;
using Domain.Models;
using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class ProfilePage : FullScreen
    {
        public Bindable<User> User { get; set; } = new User();
        public BindableCollection<Order> Orders { get; set; } = new();
        public BindableCollection<Product> Favorites { get; set; } = new();

        public ProfilePage()
        {
            Setup().RunInParallel();
        }

        private async Task Setup()
        {
            User.Value = await Api.ShopApi.GetUser();
            Orders.Replace(User.Value.Orders);
            Favorites.Replace(User.Value.Favorits);
        }

        public async Task ShowOrderInfo(Order order)
        {
            Forward<OrderInfoPage>(vm => vm.Order.Value = order);
        }

        public async Task ShowProductDetail(Product product)
        {
            Forward<ProductDetailPage>(vm => vm.Setup(product).RunInParallel());
        }

        public async Task RefreshFavorites()
        {
            User.Value = await Api.ShopApi.GetUser();
            Favorites.Replace(User.Value.Favorits);
        }
    }
}
