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
        }
    }
}
