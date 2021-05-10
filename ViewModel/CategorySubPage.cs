using Domain.Api;
using Domain.Models;
using Domain.Modules;
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
    class CategorySubPage : EzSubPage
    {
        public BindableCollection<Category> Categories = new();

        public IEnumerable<FolderData<Category>> GetData()
        {
            return FolderData<Category>.Compact(Categories, c => c.Categories);
        }

        public void OnCategorySelected(Category category)
        {
            Holder.EzForward<CategoryPage>(config: vm => vm.Setup(category).RunInParallel());
        }

        //public override async Task Setup()
        protected override async Task Setup()
        {
            Categories.Clear();
            Categories.Add(await Api.ShopApi.GetCategories());
            Categories.Refresh();
        }
    }
}
