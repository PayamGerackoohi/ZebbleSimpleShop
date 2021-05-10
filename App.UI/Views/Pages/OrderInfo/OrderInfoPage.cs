using Domain;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble;
using Zebble.Plugin;

namespace UI.Pages
{
    partial class OrderInfoPage
    {
        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            SetupUI();
            SetupModelBinding();
        }

        private void SetupModelBinding()
        {
            Model.Order.Changed += () => UpdateOrderGrid();
        }

        private void SetupUI()
        {
            Description.AddShadow();
            SetupOrderGrid();
        }

        private void SetupOrderGrid()
        {
            UpdateOrderGrid();
        }

        private void UpdateOrderGrid()
        {
            OrderGrid.ClearChildren();
            Model.Order.Value.OrderItems.Do(oi => OrderGrid.Add(new OrderCardItem
            {
                OrderItem = oi,
                ShowProductDetail = p => Model.ShowProductDetail(p).RunInParallel()
            }.Set(x => x.ModelHolder = Model)));
            if (Model.Order.Value.OrderItems.Count % 2 == 1)
                OrderGrid.Add(new Canvas().Set(c => c.Margin(8)));
        }
    }
}
