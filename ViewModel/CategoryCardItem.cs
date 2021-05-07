using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class CategoryCardItem : Zebble.Mvvm.ViewModel // Not working due to ViewModel replacement issue of the zebble viewmodel system
    {
        //public Bindable<Category> Data = new();

        //public string Name() => Data.Value.Name;
    }
}
