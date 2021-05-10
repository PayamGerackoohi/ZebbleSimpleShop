using Domain.Api;
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
        private Category Category;
        public Bindable<string> Title = new();
        public BindableCollection<Product> Products = new();

        public override async Task OnRefresh()
        {
            Products.Replace(await Api.ShopApi.GetProductsByCategory(Category.Id));
            Title.Value = Category.Name;
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
