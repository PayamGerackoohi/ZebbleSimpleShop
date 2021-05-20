using Domain.Api;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Bindable<bool> CleanFlag = false;
        public BindableCollection<Product> Results { get; private set; } = new();

        // Null means it's an old request and must be ignored. On the other hand, Empty must not be ignored, but to empty the list.
        [SuppressMessage("Design", "GCop126: To handle both null and empty string scenarios, use IsEmpty/HasValue instead \"Dialog(\"keyword is null\")\"")]
        public async Task OnSearch(string keyword)
        {
            if (keyword == null)
                Dialog("keyword is null");
            if (IsSearching)
                Keyword = keyword;
            else
            {
                IsSearching = true;
                var trimmedKeyword = keyword.Remove(" ");
                var results = new List<Product>();
                if (trimmedKeyword.HasValue())
                    results = await Api.ShopApi.GetProduct(trimmedKeyword);
                NotifyUser(keyword, results.Count);
                Update(results);
                await Task.Delay(SearchInterval);
                IsSearching = false;
                if (Keyword.HasValue())
                {
                    var temporaryKeyword = Keyword;
                    Keyword = null;
                    OnSearch(temporaryKeyword).RunInParallel();
                }
            }
        }

        private void Update(IEnumerable<Product> results)
        {
            Results.Replace(results);
            Results.Refresh();
        }

        private void NotifyUser(string keyword, int count) =>
            $"On searching \"{keyword}\" {count} result{(count > 1 ? "s are" : " is")} found.".Toast(this);

        public override async Task OnBack()
        {
            await base.OnBack();
            await CleanUp();
        }

        private async Task CleanUp()
        {
            CleanFlag.Value = !CleanFlag.Value;
            Results.Clear();
            Keyword = null;
        }

        public override async Task OnRefresh()
        {
            await base.OnRefresh();
        }

        public override async Task Setup()
        {
        }
    }
}
