﻿using Domain.Api;
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
    class CartPage : EzPage
    {
        public Bindable<Order> Cart = new();

        public override async Task Setup()
        {
            await OnRefresh();
        }

        public async Task Buy()
        {
            ($"Buy {Cart.Value.FormattedTotalPrice()}\n" + Cart.Value.OrderItems.Select(oi => $"{oi.Count} * [{oi.Product.Name}] = {oi.Price}").ToString("\n")).Toast();
        }

        public void OnDataChanged()
        {
            Cart.Refresh();
        }

        public void OnRemove(OrderItem orderItem)
        {
            Cart.Value.OrderItems.RemoveWhere(oi => oi.Id == orderItem.Id);
            Cart.Refresh();
        }

        public override async Task OnRefresh()
        {
            Cart.Value = await Api.ShopApi.GetCart();
        }
    }
}
