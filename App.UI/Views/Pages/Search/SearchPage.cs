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
    partial class SearchPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            SetupUI();
            SetupViewModelBindings();
        }

        private void SetupViewModelBindings()
        {
            Model.CleanFlag.Changed += () => { SearchInput.Text = ""; };
        }

        private void SetupUI()
        {
            SetupShadows();
        }

        private void SetupShadows()
        {
            SearchInput.AddShadow(2);
        }
    }
}
