using Android.App;
using Android.OS;
using Autofac;
using MScResearchTool.Mobile.Binder.AutofacModules;

namespace MScResearchTool.Mobile.Android
{
    [Activity(Label = "MScResearchTool.Mobile.Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private IContainer _container { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ContainerSetup();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }

        private void ContainerSetup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ServicesModule>();
            _container = builder.Build();
        }
    }
}

