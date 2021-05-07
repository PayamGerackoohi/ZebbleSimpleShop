using Domain.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Api
{
    class Api
    {
        public static IShopApi ShopApi { get; set; } = new Fake.ShopApi();
    }
}
