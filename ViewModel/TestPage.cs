using Domain.Api;
using Domain.Database;
using Domain.Models;
using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class TestPage : EzPage
    {
        public Bindable<string> Data { get; private set; } = new();

        public override async Task Setup()
        {
            await OnRefresh();
        }

        public override async Task OnRefresh()
        {
            //Toast("Test Toast 1");
            "Test Toast 1".Toast(this);
            //Data.Value = $"Start\n\n{str}\n\nEnd";
            await base.OnRefresh();
        }

        public void TestButtonClicked()
        {
            Toast("Test Toast 2");
        }
    }
}
