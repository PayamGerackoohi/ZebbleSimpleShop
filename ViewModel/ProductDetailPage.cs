using Domain.Api;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;
using Zebble.Mvvm;

namespace ViewModel
{
    class ProductDetailPage : EzPage
    {
        public Bindable<Product> Data { get; private set; } = new();
        public Bindable<bool> IsFavorite { get; private set; } = false;

        public async Task Setup(Product product)
        {
            Data.Value = product;
            await InitiateIsFavorite();
        }

        private async Task InitiateIsFavorite()
        {
            var user = await Api.ShopApi.GetUser();
            var isFav = user.Favorites.Any(f => f.Id == Data.Value.Id);
            IsFavorite.Value = isFav;
        }

        public async Task OnBuyButtonClicked()
        {
            await Api.ShopApi.AddToCart(Data.Value);
            "The Product is added to your cart.".Toast(this);
        }

        public async Task OnFavoriteButtonClicked()
        {
            IsFavorite.Value = !IsFavorite.Value;
            if (IsFavorite.Value)
                await AddFavorite();
            else
                await RemoveFavorite();
        }

        private async Task AddFavorite()
        {
            await Api.ShopApi.AddFavorite(Data.Value.Id);
            "Product is added to the favorites list.".Toast(this);
        }

        private async Task RemoveFavorite()
        {
            await Api.ShopApi.RemoveFavorite(Data.Value.Id);
            "Product is removed from the favorites list.".Toast(this);
        }

        public override async Task OnRefresh()
        {
            await base.OnRefresh();
        }

        public override async Task Setup()
        {
        }
    }
}
