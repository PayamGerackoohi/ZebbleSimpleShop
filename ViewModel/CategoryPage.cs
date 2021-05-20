using Domain.Api;
using Domain.Database;
using Domain.Models;
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
    class CategoryPage : EzPage
    {
        private Category Category = new();
        public Bindable<string> Title { get; private set; } = new();
        public BindableCollection<Product> Products { get; private set; } = new();

        public override async Task OnRefresh()
        {
            Products.Replace(await Api.ShopApi.GetProductsByCategory(Category.Id));
            Title.Value = Category.Name;
            await base.OnRefresh();
        }

        public async Task Setup(Category category)
        {
            Category = category;
            await OnRefresh();
        }

        public override async Task Setup()
        {
        }
    }
}
