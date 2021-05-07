namespace UI
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using Zebble.Testing;

    public partial class StartUp : Zebble.StartUp
    {
        public override async Task Run()
        {
            await InstallIfNeeded();

            CssStyles.LoadAll();

            Zebble.Device.App.ReceivedMemoryWarning += CleanUpMemory;

            Launch().RunInParallel();
        }

        void CleanUpMemory()
        {
            // Note: Before raising this event, Zebble will have already done the following:            
            //         - Display an output log message
            //         - Dispose navigation cache
            //         - Force Garbage collection

            // TODO: If applicable, you can also remove any custom data that you have cached in memory.
            // Tip: You can detect potential memory leaks by using a tool such as DotMemory from Jet Brains.
        }

        protected override bool IsTestMode() => false;

        public async Task Launch()
        {
            Zebble.Mvvm.Templates.Register(Assembly.GetExecutingAssembly());
            await LoadFirstPage();

            if (IsTestMode()) TestEngine.Run();
        }

        public static Task LoadFirstPage()
        {
            // Testing a specific page?  
            // -------------------------------
            // return Zebble.Mvvm.TestRender.Page<ViewModel.MyPage>()
            //    .Configure(c => c...)
            //    .FireEvents() // -> only if you need. Mostly you don't.
            //    .Run();

            // Run normally?
            // -------------------------------
            return ViewModel.StartUp.Run();
        }

    }
}