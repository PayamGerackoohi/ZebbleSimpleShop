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
    class ProfileFavoritesSubPage : EzSubPage
    {
        public BindableCollection<Product> Favorites { get; private set; } = new();
        public Bindable<Product> ShowRemoveConfirmationDialog { get; private set; } = new();
        public Action<Product> ShowDetail { get; set; } = p => { };
        public Action RefreshFavorites { get; set; } = () => { };

        public void RemoveButtonClicked(Product product)
        {
            ShowRemoveConfirmationDialog.Value = product;
        }

        public async Task RemoveItem(Product product)
        {
            await Api.ShopApi.RemoveFavorite(product.Id);
        }

        //public override async Task OnRefresh()
        //{
        //}

        protected override async Task Setup()
        {
        }
    }
}
