using Android.App;
using Android.Runtime;
using Autofac;
using MScResearchTool.Mobile.Droid.AutofacModules;
using MScResearchTool.Mobile.Binder.AutofacModules;
using System;
using Android.Util;

namespace MScResearchTool.Mobile.Droid
{
    [Application]
    public class App : Application
    {
        internal static IContainer Container { get; set; }

        private static string TAG = "MScResearchTool." + typeof(App).Name;

        public App(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {

        }

        public override void OnCreate()
        {
            Log.Info(TAG, "OnCreate()");

            InitializeContainer();

            base.OnCreate();
        }

        private static void InitializeContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<AndroidModule>();
            builder.RegisterModule<BusinessModule>();
            builder.RegisterModule<ServicesModule>();

            Container = builder.Build();
        }
    }
}