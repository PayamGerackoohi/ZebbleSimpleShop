using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UI;
using UI.Pages;
using UI.Templates;
using ViewModel.Base;
using ViewModel.Base.Interface;
using Zebble;
using Zebble.Mvvm;

namespace UI.Templates
{
    partial class EzNavBarPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            NavBar.AddShadow(2);
        }

        override public async Task OnRendered()
        {
            await base.OnRendered();
            var modelField = GetType().GetField("Model", BindingFlags.Instance | BindingFlags.Public)?.GetValue(this);
            if (modelField is EzPage model)
                NavBar.Trim(model);
        }
    }
}
