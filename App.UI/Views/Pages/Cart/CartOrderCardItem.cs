using Domain;
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
using Zebble.Plugin;

namespace UI.Pages
{
    partial class CartOrderCardItem
    {
        public EzPage ModelHolder { get; set; }
        public OrderItem OrderItem { get; set; } = new();
        public IEnumerable<string> Counts { get => Enumerable.Range(1, 5).Select(i => i.ToString()); }
        public Action OnDataChanged { get; set; }
        public Action<OrderItem> OnRemove { get; set; }

        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            SetupUI();
            SetupUIBindings();
        }

        private void SetupUIBindings()
        {
            CountPicker.Label.On(x => x.TextChanged, () => OnCountChanged());
        }

        private void SetupUI()
        {
            this.AddShadow();
        }

        private void OnCountChanged()
        {
            if (CountPicker.SelectedItem.None())
                return;
            OrderItem.Count = CountPicker.SelectedItem.To<int>();
            OnDataChanged();
        }
    }
}
