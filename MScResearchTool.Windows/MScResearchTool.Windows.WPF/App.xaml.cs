using Autofac;
using MScResearchTool.Windows.Binder.AutofacModules;
using MScResearchTool.Windows.WPF.AutofacModules;
using System.Windows;

namespace MScResearchTool.Windows.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = InitializeContainer();

            var manager =  container.Resolve<ViewManager>();

            manager.Open();
        }

        private IContainer InitializeContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<UserInterfaceModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<BusinessModule>();

            return builder.Build();
        }
    }
}
