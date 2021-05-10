using Domain;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using ViewModel.Base;
using Zebble;
using Zebble.Plugin;

namespace UI.Pages
{
    partial class ProfileFavoriteCardItem
    {
        public EzPage ModelHolder { get; set; }
        public Product Favorite { get; set; } = new();
        public Action<Product> ShowDetail { get; set; } = p => { };
        public Action<Product> RemoveButtonClicked { get; set; } = p => { };

        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            this.AddShadow();
        }
    }
}
