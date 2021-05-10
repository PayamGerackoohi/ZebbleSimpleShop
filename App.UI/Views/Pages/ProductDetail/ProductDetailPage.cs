using Domain;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using ViewModel.Base.Interface;
using Zebble;
using Zebble.Plugin;

namespace UI.Pages
{
    partial class ProductDetailPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            await SetupUI();
        }

        private async Task SetupUI()
        {
            await AddSahdows();
        }

        private async Task AddSahdows()
        {
            InfoSection.AddShadow();
            BuySection.AddShadow();
            DescriptionSection.AddShadow();
        }
    }
}
