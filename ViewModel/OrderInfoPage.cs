using Domain.Api;
using Domain.Models;
using Domain.Utils;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UI;
using Zebble;
using Zebble.Mvvm;

namespace ViewModel
{
    class OrderInfoPage : FullScreen
    {
        public Bindable<Order> Order { get; set; } = new();

        public async Task ShowProductDetail(Product product)
        {
            Forward<ProductDetailPage>(vm => vm.Setup(product).RunInParallel());
        }
    }
}
