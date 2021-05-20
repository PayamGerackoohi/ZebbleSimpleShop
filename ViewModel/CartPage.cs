using Domain.Api;
using Domain.Database;
using Domain.Models;
using Domain.Utils;
using Olive;
using System;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Base;

namespace ViewModel
{
    class CartPage : EzPage
    {
        public Bindable<Order> Cart { get; private set; } = new();
        public BindableCollection<OrderItem> OrderItems { get; private set; } = new();

        public override async Task Setup()
        {
            await OnRefresh();
        }

        public async Task Buy()
        {
            Cart.Value.OrderItems.Also(list =>
            {
                ("Buy " + Cart.Value.FormattedTotalPrice() + (list.None() ? "" : "\n") + list.Select(oi => $"{oi.Count} * [{oi.Product.Name}] = {oi.Price}").ToString("\n")).Toast(this);
            });
        }

        public void OnDataChanged()
        {
            Api.ShopApi.SaveCart(Cart.Value);
            Cart.Refresh();
            OrderItems.Refresh();
        }

        public void OnRemove(OrderItem orderItem)
        {
            Cart.Value.OrderItems.RemoveWhere(oi => oi.Id == orderItem.Id);
            OrderItems.Replace(Cart.Value.OrderItems);
            Api.ShopApi.RemoveFromCart(orderItem);
            OnDataChanged();
        }

        public override async Task OnRefresh()
        {
            Cart.Value = await Api.ShopApi.GetCart();
            OrderItems.Replace(Cart.Value.OrderItems);
            Cart.Refresh();
            //OrderItems.Refresh(); // evil rests here in peace >:)
            await base.OnRefresh();
        }
    }
}
