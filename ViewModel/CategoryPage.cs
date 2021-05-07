using Domain.Api;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class CategoryPage : FullScreen
    {
        public Bindable<string> Title = new();
        public BindableCollection<Product> Products = new();

        public async Task Setup(Category category)
        {
            Products.Replace(await Api.ShopApi.GetProductsByCategory(category.Id));
            Title.Value = category.Name;
        }
    }
}
