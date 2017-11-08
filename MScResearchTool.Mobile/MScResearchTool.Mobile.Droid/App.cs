using Android.App;
using Android.Runtime;
using Autofac;
using MScResearchTool.Mobile.Droid.AutofacModules;
using MScResearchTool.Mobile.Binder.AutofacModules;
using System;

namespace MScResearchTool.Mobile.Droid
{
    [Application]
    public class App : Application
    {
        public static IContainer Container { get; set; }

        public App(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {

        }

        public override void OnCreate()
        {
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