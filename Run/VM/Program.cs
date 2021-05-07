namespace App
{
    using App.Tests;
    using Olive;
    using System.Threading.Tasks;
    using Test = Zebble.Mvvm.TestScript;
    using Zebble;

    class Program
    {
        static Intention Doing = Intention.Development;

        public static async Task Main(string[] args)
        {
            UIRuntime.Initialize<Program>("ZebbleTestApp");

            await ViewModel.StartUp.Run();

            if (Doing == Intention.OneTest) await Test.Run<LoginTest>();
            if (Doing == Intention.AllTests) await Test.RunAll();
            if (Doing == Intention.Development) await DevContext.Reach();

            if (args.None()) Zebble.Console.Start(args);
        }
    }

    enum Intention { Development, OneTest, AllTests }
}