using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble.Mvvm;

namespace UI.Templates
{
    partial class SimpleNavBarPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            NavBar.AddShadow(2);
        }

        override public async Task OnRendered()
        {
            await base.OnRendered();
            NavBar.Trim();
        }
    }
}
