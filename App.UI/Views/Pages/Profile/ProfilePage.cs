using Domain;
using Domain.Models;
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
        }

        private void UpdateFavorites()
        {
            FavoritesSubPage.Model.Favorites.Replace(Model.Favorites.Value);
            FavoritesSubPage.Model.Favorites.Refresh();
        }

        private void SeutpShadows()
        {
            InfoButton.AddShadow();
            OrdersButton.AddShadow();
            FavoritesButton.AddShadow();
            SecurityButton.AddShadow();
        }
    }
}
