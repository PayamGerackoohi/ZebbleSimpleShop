namespace ViewModel
{
    using System.Threading.Tasks;
    using Zebble;
    using Zebble.Mvvm;
    using Olive;
    using Domain.Api;
    using Domain.Models;
    using System.Linq;
    using System;

    class StartUp
    {
        public static async Task Run()
        {
            //var user = await Api.ShopApi.GetUser();
            //ViewModel.Go<OrderInfoPage>(vm => vm.Order = user.Orders.First(), PageTransition.Fade);
            ViewModel.Go<HomePage>(PageTransition.Fade);
            //ViewModel.Go<ProfilePage>(PageTransition.Fade);
        }
    }
}