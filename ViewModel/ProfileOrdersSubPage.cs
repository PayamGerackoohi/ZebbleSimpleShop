using Domain.Api;
using Domain.Models;
using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI;
using ViewModel.Base;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class ProfileOrdersSubPage : EzSubPage
    {
        public BindableCollection<Order> Orders { get; private set; } = new();

        public Action<Order> ShowOrderInfo { get; set; } = o => { };

        public override async Task Setup()
        {
        }
    }
}