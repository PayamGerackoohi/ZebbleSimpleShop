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
    partial class ProfileFavoritesSubPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            SetupUI();
            SetupViewModelBidings();
        }

        private void SetupUI()
        {
            SetupListHeight();
        }

        private void SetupListHeight()
        {
            List.Height.Changed.Event += () => ClipListChildren(10).RunInParallel();
        }

        override public async Task OnRendered()
        {
            await base.OnRendered();
        }

        private void SetupViewModelBidings()
        {
            Model.ShowRemoveConfirmationDialog.Changed += () => ShowRemoveConfirmationDialog(Model.ShowRemoveConfirmationDialog.Value).RunInParallel();
        }

        // todo: find a better solution than doing this each 100ms in a period of 1 second
        private async Task ClipListChildren(int count)
        {
            if (count < 1) return;
            await Task.Delay(100);
            List.ClipChildren = true;
            ClipListChildren(count - 1).RunInParallel();
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
