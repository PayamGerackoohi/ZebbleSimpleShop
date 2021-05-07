namespace Android
{
    using App;
    using OS;
    using Runtime;
    using System;
    using Zebble;

    //You can specify additional application information in this attribute
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer) { }

        public override void OnCreate()
        {
            base.OnCreate();
            UIRuntime.Initialize<MainApplication>("ZebbleTestApp");
            RegisterActivityLifecycleCallbacks(this);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState) { UIRuntime.CurrentActivity = activity; }

        public void OnActivityDestroyed(Activity activity) { }

        public void OnActivityPaused(Activity activity) { }

        public void OnActivityResumed(Activity activity) { UIRuntime.CurrentActivity = activity; }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState) { }

        public void OnActivityStarted(Activity activity)
        {
            UIRuntime.CurrentActivity = activity;
        }

        public void OnActivityStopped(Activity activity) { }
    }
}