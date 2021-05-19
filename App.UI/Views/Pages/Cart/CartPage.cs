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
    partial class CartPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            SetupUI();
        }

        private void SetupUI()
        {
            HeaderPrice.AddShadow(2);
            BuyButton.AddShadow(-2);
            SetupListHeight();
        }

        private void SetupListHeight()
        {
            List.Height.Changed.Event += () => ClipListChildren(10).RunInParallel();
        }

        // todo: find a better solution than doing this each 500ms in a period of 5"
        private async Task ClipListChildren(int count)
        {
            if (count < 1) return;
            await Task.Delay(500);
            List.ClipChildren = true;
            ClipListChildren(count - 1).RunInParallel();
        }
    }
}
