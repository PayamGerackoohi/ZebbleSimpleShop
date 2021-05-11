namespace ViewModel
{
    using Domain.Api;
    using System.Threading.Tasks;
    using ViewModel.Base;
    using Zebble;
    using Zebble.Mvvm;

    class StartUp
    {
        public static async Task Run()
        {
            //EzNav.Go<ProfilePage>(PageTransition.Fade);
            var user = await Api.ShopApi.GetUser();
            if (user.Credential.StayLoggedIn)
                EzNav.Go<HomePage>(PageTransition.Fade);
            else
                EzNav.Go<LoginPage>(PageTransition.Fade);
        }
    }
}