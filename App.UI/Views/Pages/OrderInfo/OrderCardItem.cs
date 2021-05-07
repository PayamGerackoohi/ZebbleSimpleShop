﻿using Domain;
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
    partial class OrderCardItem
    {
        public OrderItem OrderItem { get; set; }
        public Action<Product> ShowProductDetail { get; set; }

        override public async Task OnInitializing()
        {
            await base.OnInitializing();
            this.AddShadow();
        }
    }
}
