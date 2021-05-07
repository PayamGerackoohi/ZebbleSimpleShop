namespace Android
{
    using Android.App;
    using Android.OS;
    using Android.Widget;
    using Content.PM;
    using System;
    using Zebble;
    using Zebble.AndroidOS;

    [Activity(Label = "Android", Icon = "@drawable/icon",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait,
        WindowSoftInputMode = Views.SoftInput.AdjustPan
        )]
    public class MainActivity : BaseActivity
    {
        static bool AlreadyCreated;

        //static MainActivity()
        //{
        //    UIRuntime.GetEntryAssembly = () => typeof(MainActivity).Assembly;
        //}

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);

            SetContentView(Resource.Layout.Main);
            Setup.Start(FindViewById<FrameLayout>(Resource.Id.Main_Layout), this).RunInParallel();

            if (AlreadyCreated)
            {
                Setup.SwitchActivity(FindViewById<FrameLayout>(Resource.Id.Main_Layout), this);
            }
            else
            {
                AlreadyCreated = true;
                Setup.Start(FindViewById<FrameLayout>(Resource.Id.Main_Layout), this).RunInParallel();
                await (StartUp.Current = new UI.StartUp()).Run();
            }
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        
    }
}
