using Autofac;
using MScResearchTool.Windows.Domain.ViewModels;
using MScResearchTool.Windows.WPF.AutofacModules;
using MScResearchTool.Windows.WPF.Views;

namespace MScResearchTool.Windows.WPF
{
    public class Setup
    {
        private IContainer _container { get; set; }

        public Setup()
        {
            ContainerSetup();

            RunDesktop();
        }

        private void RunDesktop()
        {
            var mainVm = _container.Resolve<IMainVM>();

            var mainWindow = new MainWindow(mainVm);

            mainWindow.Show();
        }

        private void ContainerSetup()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<UserInterfaceModule>();

            _container = builder.Build();
        }
    }
}
