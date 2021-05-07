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
    partial class ProfileFavoritesSubPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            SetupViewModelBidings();
        }

        private void SetupViewModelBidings()
        {
            Model.ShowRemoveConfirmationDialog.Changed += () => ShowRemoveConfirmationDialog(Model.ShowRemoveConfirmationDialog.Value).RunInParallel();
        }

        private async Task ShowRemoveConfirmationDialog(Product product)
        {
            if (product == null)
                return;
            var key = await Alert.Show(
                "Remove Favorite",
                $"Are you sure about removing this product from your favorites list?\n" +
                $"Name: {product.Name}\n" +
                $"Id: {product.Id}",
                new KeyValuePair<string, int>("Yes", 1),
                new KeyValuePair<string, int>("No", 2));
            if (key == 1)
            {
                await Model.RemoveItem(product);
                Model.RefreshFavorites();
                await List.Remove(product, false);
                Alert.Toast("Item successfully removed.", false).RunInParallel();
            }
        }
    }
}
