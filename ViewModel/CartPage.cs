using Domain.Api;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class CartPage : FullScreen
    {
        public Bindable<Order> Cart = new();

        public CartPage()
        {
            Setup().RunInParallel();
        }

        private async Task Setup()
        {
            Cart.Value = await Api.ShopApi.GetCart();
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
    }
}
