﻿using Domain.Api;
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
    class CategorySubPage : SubPage
    {
        public BindableCollection<Category> Categories = new();

        public override async Task OnUIReady()
        {
            Categories.Clear();
            Categories.Add(await Api.ShopApi.GetCategories());
            Categories.Refresh();
        }

        public IEnumerable<FolderData<Category>> GetData()
        {
            return FolderData<Category>.Compact(Categories, c => c.Categories);
        }

        public void OnCategorySelected(Category category)
        {
            Forward<CategoryPage>(vm => vm.Setup(category).RunInParallel());
        }
    }
}
