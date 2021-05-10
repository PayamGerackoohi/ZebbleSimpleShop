using Domain;
using Domain.Models;
using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble;
using Zebble.Plugin;

namespace UI.Pages
{
    partial class ProfilePage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            SeutpShadows();
            UpdateFavorites();
            Model.Favorites.Changed += () => UpdateFavorites();
            //FavoritesSubPage.List.Height.Changed.Event += () => OnListHeightChanged();
        }

        //private void OnListHeightChanged()
        //{
        //    FavoritesExpander.ClipChildren = true;
        //$"OnListHeightChanged is called. FavoritesExpander.ClipChildren: {FavoritesExpander.ClipChildren}".Toast();
        //}

        private void UpdateFavorites()
        {
            FavoritesSubPage.Model.Favorites.Also(x =>
            {
                x.Replace(Model.Favorites.Value);
                x.Refresh();
            });
            FavoritesExpander.ClipChildren = true;
        }

        private void SeutpShadows()
        {
            InfoButton.AddShadow();
            OrdersButton.AddShadow();
            FavoritesButton.AddShadow();
            SecurityButton.AddShadow();
        }

        private void InformErrorLog()
        {
            $"FavList.Children: {FavoritesSubPage.List.AllChildren.Count()}, FavList.DataSource: {FavoritesSubPage.List.DataSource.Count()}\nFavoritesExpander.ClipChildren: {FavoritesExpander.ClipChildren}".Toast();
            FavoritesExpander.ClipChildren = true;
        }
    }
}
