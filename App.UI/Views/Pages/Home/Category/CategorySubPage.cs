using Domain;
using Domain.Api;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Modules;
using UI;
using Zebble;
using Zebble.Plugin;
using ViewModel.Base;

namespace UI.Pages
{
    partial class CategorySubPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
        }
    }

    public class CategoryViewGen : IViewGenerator<Category>
    {
        public View Generate(Category data, bool isHead, EzPage holder)
        {
            var view = new CategoryCardItem { Data = data, IsHead = isHead }.Set(x => x.Model.Holder = holder);
            return view;
        }
    }
}
