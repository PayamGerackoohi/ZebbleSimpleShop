using Domain.Api;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using ViewModel.Base;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class SearchPage : EzPage
    {
        private const int SearchInterval = 1000;
        private bool IsSearching = false;
        private string Keyword = null;
        public BindableCollection<Product> Results { get; private set; } = new();

        public async Task OnSearch(string keyword)
        {
            if (keyword == null)
                Alert.Show("keyword is null").RunInParallel();
            if (IsSearching)
                Keyword = keyword;
            else
            {
                IsSearching = true;
                var trimmedKeyword = keyword.Remove(" ");
                List<Product> results = new();
                if (!trimmedKeyword.None())
                    results = await Api.ShopApi.GetProduct(trimmedKeyword);
                NotifyUser(keyword, results.Count);
                Update(results);
                await Task.Delay(SearchInterval);
                IsSearching = false;
                if (Keyword.HasValue())
                {
                    var k = Keyword;
                    Keyword = null;
                    OnSearch(k).RunInParallel();
                }
            }

        }

        private void Update(List<Product> results)
        {
            Results.Replace(results);
            Results.Refresh();
        }

        private void NotifyUser(string keyword, int count) =>
            $"On searching \"{keyword}\" {count} result{(count > 1 ? "s are" : " is")} found.".Toast();

        public override async Task OnRefresh()
        {
        }

        public override async Task Setup()
        {
        }
    }
}
