using Autofac;
using MScResearchTool.Win10.Domain.ViewModels;
using MScResearchTool.Win10.UWP.ViewModels;

namespace MScResearchTool.Win10.UWP.AutofacModules
{
    public class UserInterfaceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainVM>().As<IMainVM>();
        }
    }
}
