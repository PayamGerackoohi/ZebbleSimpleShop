using Domain;
using Domain.Api;
using Domain.Models;
using Domain.Modules;
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
    partial class TestPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
        }

        public async Task<IEnumerable<FolderData<Category>>> GetData()
        {
            var categories = await Api.ShopApi.GetCategories();
            return FolderData<Category>.Compact(categories, c => c.Categories);
        }

        public async Task NotifyUser(string message) => await message.NotifyUser();

        override public async Task OnRendered()
        {
            await base.OnRendered();
        }
    }
}
