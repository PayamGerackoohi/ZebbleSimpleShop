using Domain.Api;
using Domain.Database;
using Domain.Models;
using Domain.Utils;
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
    class TestPage : EzPage
    {
        public Bindable<string> Data { get; private set; } = new();

        public override async Task Setup()
        {
            await OnRefresh();
        }

        public override async Task OnRefresh()
        {
            //var str = "";
            //ShopDatabase.Instance.OrderDao.GetCart().Also(cart =>
            //{
            //    str = $"{cart.OrderItems.Select(oi => oi.Count + "").ToString(", ")}";
            //    cart.OrderItems.Do(oi => oi.Count *= 2);
            //    ShopDatabase.Instance.OrderDao.Save(cart);
            //});
            //var cart = ShopDatabase.Instance.OrderDao.GetCart();

            //str += $"\n{cart.OrderItems.Select(oi => oi.Count + "").ToString(", ")}";
            var cats = ShopDatabase.Instance.CategoryDao.ReadAll();
            var str = cats.Select(c => c.PrintHierarchy()).ToString("");
            Data.Value = str;
            //Data.Value = $"Start\n\n{str}\n\nEnd";
            await base.OnRefresh();
        }
    }
}
