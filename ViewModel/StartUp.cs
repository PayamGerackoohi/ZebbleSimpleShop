namespace ViewModel
{
    using Domain.Database;
    using Domain.Models;
    using System.Threading.Tasks;
    using ViewModel.Base;
    using Zebble;
    using System;
    using Olive;
    using System.Diagnostics.CodeAnalysis;

    class StartUp
    {
        // in order to prevent #pragma warning disable CS0162 // Unreachable code detected
        [SuppressMessage("Design", "GCop139: Use constant instead of field.")]
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
