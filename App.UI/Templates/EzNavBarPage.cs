using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UI;
using UI.Pages;
using UI.Templates;
using ViewModel.Base;
using Zebble;
using Zebble.Mvvm;

namespace UI.Templates
{
    partial class EzNavBarPage
    {
        override protected async Task InitializeFromMarkup()
        {
            await base.InitializeFromMarkup();
            SetupViewModelBindings();
        }

        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            NavBar.AddShadow(2);
        }

        private void SetupViewModelBindings()
        {
            var model = GetViewModel();
            SetupAlertDialog(model);
            model.OnUIReady();
        }

        private void SetupAlertDialog(EzPage model)
        {
            model.AlertMessage.Also(b => b.Changed += () => b.Value.Also(message =>
            {
                if (message.HasValue())
                    switch (model.AlertType)
                    {
                        case AlertType.Toast:
                            message.Toast();
                            break;
                        case AlertType.Dialog:
                            message.Dialog();
                            break;
                        default:
                            throw new Exception($"AlertType: {model.AlertType}, is not supported.");
                    }
            }));
        }

        private EzPage GetViewModel()
        {
            var modelField = GetType().GetField("Model", BindingFlags.Instance | BindingFlags.Public)?.GetValue(this);
            if (modelField is EzPage model)
                return model;
            else
                throw new Exception("ViewModel is not of type EzPage!");
        }

        override public async Task OnRendered()
        {
            await base.OnRendered();
            NavBar.Trim(GetViewModel());
        }
    }
}
