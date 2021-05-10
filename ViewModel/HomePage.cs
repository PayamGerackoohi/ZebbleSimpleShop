using Domain.Api;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using ViewModel.Base;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class HomePage : EzPage
    {
        public Bindable<PageType> CurrentPage { get; private set; } = PageType.Popular;
        public Bindable<Order> Cart { get; private set; } = new();

        public override async Task Setup()
        {
            await OnRefresh();
        }

        public void TabSelected(PageType page)
        {
            CurrentPage.Value = page;
        }

        public async Task OnCartTapped() => EzForward<CartPage>();

        public void OnSearchTapped() => EzForward<SearchPage>(PageTransition.Fade);

        public void OnProfileTapped() => EzForward<ProfilePage>(PageTransition.Fade);

        public override async Task OnRefresh()
        {
            Cart.Value = await Api.ShopApi.GetCart();
        }
    }
}
