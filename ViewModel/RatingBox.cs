using Domain.Utils;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.Base;
using System.Threading.Tasks;

namespace ViewModel
{
    class RatingBox : EzSubPage
    {
        public Bindable<Product> Data { get; set; } = new();

        public int GetRating() => (int)Math.Round(Data.Value.Rating.Saturate(1, 5));

        protected override async Task Setup()
        {
        }
    }
}
