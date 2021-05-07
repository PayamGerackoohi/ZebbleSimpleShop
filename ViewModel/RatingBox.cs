using Domain.Utils;
using Domain.Models;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    class RatingBox : Zebble.Mvvm.ViewModel
    {
        public Bindable<Product> Data = new();

        public int GetRating() => (int)Math.Round(Data.Value.Rating.Saturate(1, 5));
    }
}
