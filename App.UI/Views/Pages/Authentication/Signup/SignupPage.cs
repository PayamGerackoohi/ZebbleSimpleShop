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
    partial class SignUpPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            await SetupUI();
        }

        private async Task SetupUI()
        {
            SetupShadows();
        }

        private void SetupShadows()
        {
        }

        override public async Task OnRendered()
        {
            await base.OnRendered();
        }
    }
}
