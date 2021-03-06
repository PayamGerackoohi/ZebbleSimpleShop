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
    partial class CategoryCardItem
    {
        public new Category Data { get; set; } = new();
        public bool IsHead { get; set; } = false;
        public string DataValue { get => $"{(IsHead ? "❖ " : "")}{Data.Name}"; }

        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            CardItem.AddShadow();
        }
    }
}
