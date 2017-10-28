using Autofac;
using MScResearchTool.Mobile.BusinessLogic.Businesses;
using MScResearchTool.Mobile.Domain.Businesses;

namespace MScResearchTool.Mobile.Binder.AutofacModules
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IntegrationsBusiness>().As<IIntegrationsBusiness>();
        }
    }
}
