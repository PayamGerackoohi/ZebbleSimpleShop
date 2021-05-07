using Domain.Api;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class HomePage : FullScreen
    {
        public Bindable<PageType> CurrentPage = PageType.Popular;
        public Bindable<Order> Cart = new();

        public HomePage()
        {
            Setup().RunInParallel();
        }

        private async Task Setup()
        {
            Cart.Value = await Api.ShopApi.GetCart();
        }

        public void TabSelected(PageType page)
        {
            CurrentPage.Value = page;
        }

        public async Task OnCartTapped() => Forward<CartPage>();

        public void OnSearchTapped() => Forward<SearchPage>(PageTransition.Fade);

        public void OnProfileTapped() => Forward<ProfilePage>(PageTransition.Fade);
    }
}
