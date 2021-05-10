using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Base;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class CategoryCardItem : EzSubPage // Not working due to ViewModel replacement issue of the zebble viewmodel system
    {
        //public Bindable<Category> Data = new();

        //public string Name() => Data.Value.Name;
        //public override async Task OnRefresh()
        //{
        //}

        protected override async Task Setup()
        {
        }
    }
}
