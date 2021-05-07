using Domain.Api;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble.Mvvm;

namespace ViewModel
{
    class ProductDetailPage : FullScreen
    {
        public Bindable<Product> Data = new();
        public Bindable<bool> IsFavorite = false;

        public async Task Setup(Product product)
        {
            Data.Value = product;
            await InitiateIsFavorite();
        }

        private async Task InitiateIsFavorite()
        {
            var user = await Api.ShopApi.GetUser();
            var isFav = user.Favorits.Any(f => f == Data.Value);
            IsFavorite.Value = isFav;
        }

        public async Task OnBuyButtonClicked()
        {
            var cart = await Api.ShopApi.GetCart();
            var list = cart.OrderItems;
            var item = list.Where(oi => oi.Product.Id == Data.Value.Id).FirstOrDefault();
            if (item == null)
                list.Add(new OrderItem { Product = Data.Value });
            else
                item.Count++;
            "The Product is added to your cart.".Toast();
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
            "Product is added to the favorites list.".Toast();
        }

        private async Task RemoveFavorite()
        {
            await Api.ShopApi.RemoveFavorite(Data.Value.Id);
            "Product is removed from the favorites list.".Toast();
        }
    }
}
