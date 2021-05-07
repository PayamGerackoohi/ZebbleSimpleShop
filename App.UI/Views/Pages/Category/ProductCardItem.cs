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
    partial class ProductCardItem
    {
        public Product Product;

        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            Product = Model.Data.Value;
            CardItem.AddShadow();
        }

        public async Task ShowDetails()
        {
            Model.ShowDetails(Product);
        }
    }
}
