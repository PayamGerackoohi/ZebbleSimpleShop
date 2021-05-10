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
            //EzNav.Go<ProfilePage>(PageTransition.Fade);
            EzNav.Go<HomePage>(PageTransition.Fade);
        }
    }
}