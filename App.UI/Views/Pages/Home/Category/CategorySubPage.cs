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

namespace UI.Pages
{
    partial class CategorySubPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            await Model.OnUIReady();
        }
    }

    public class CategoryViewGen : IViewGenerator<Category>
    {
        public View Generate(Category data, bool isHead)
        {
            var view = new CategoryCardItem { Data = data, IsHead = isHead };
            return view;
        }
    }
}
