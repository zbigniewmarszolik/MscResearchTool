using Autofac;
using MScResearchTool.Windows.Domain.ViewModels;
using MScResearchTool.Windows.WPF.ViewModels;

namespace MScResearchTool.Windows.WPF.AutofacModules
{
    public class UserInterfaceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().As<IMainViewModel>().SingleInstance();
            builder.RegisterType<SelectionViewModel>().As<ISelectionViewModel>();
            builder.RegisterType<StatusViewModel>().As<IStatusViewModel>();
            builder.RegisterType<ViewManager>().As<ViewManager>();
        }
    }
}
