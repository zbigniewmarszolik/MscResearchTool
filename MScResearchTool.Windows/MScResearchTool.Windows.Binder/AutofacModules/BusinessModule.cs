using Autofac;
using MScResearchTool.Windows.BusinessLogic.Businesses;
using MScResearchTool.Windows.BusinessLogic.Helpers;
using MScResearchTool.Windows.Domain.Businesses;

namespace MScResearchTool.Windows.Binder.AutofacModules
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HardwareInfoHelper>().As<HardwareInfoHelper>();
            builder.RegisterType<CrackingsBusiness>().As<ICrackingsBusiness>();
            builder.RegisterType<IntegrationsBusiness>().As<IIntegrationsBusiness>();
        }
    }
}
