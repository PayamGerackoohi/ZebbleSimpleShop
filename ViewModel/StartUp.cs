namespace ViewModel
{
    using System.Threading.Tasks;
    using ViewModel.Base;
    using Zebble;
    using Zebble.Mvvm;

    class StartUp
    {
        public static async Task Run()
        {
            //var user = await Api.ShopApi.GetUser();
            //ViewModel.Go<OrderInfoPage>(vm => vm.Order = user.Orders.First(), PageTransition.Fade);
            EzNav.Go<HomePage>(PageTransition.Fade);
            //EzNav.Go<HomePage>(PageTransition.Fade, vm => { });
            //ViewModel.Go<ProfilePage>(PageTransition.Fade);
        }
    }
}