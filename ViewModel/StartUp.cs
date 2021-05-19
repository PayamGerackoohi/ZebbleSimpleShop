namespace ViewModel
{
    using Domain.Database;
    using Domain.Models;
    using System.Threading.Tasks;
    using ViewModel.Base;
    using Zebble;
    using System;
    using Olive;

    class StartUp
    {
        private static readonly bool Debug = false;

        public static async Task Run()
        {
            ShopDatabase.Setup();

            if (Debug)
                EzNav.Go<TestPage>(PageTransition.Fade);
            else
            {
                var user = ShopDatabase.Instance.UserDao.Read();
                if (user.Credential.StayLoggedIn)
                    EzNav.Go<HomePage>(PageTransition.Fade);
                else
                    EzNav.Go<LoginPage>(PageTransition.Fade);
            }
        }
    }
}
