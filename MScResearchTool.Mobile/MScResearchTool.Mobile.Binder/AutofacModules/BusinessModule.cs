using Autofac;
using MScResearchTool.Mobile.BusinessLogic.Businesses;
using MScResearchTool.Mobile.BusinessLogic.Helper;
using MScResearchTool.Mobile.Domain.Businesses;

namespace MScResearchTool.Mobile.Binder.AutofacModules
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnzippingHelper>().As<UnzippingHelper>();
            builder.RegisterType<CrackingsBusiness>().As<ICrackingsBusiness>();
            builder.RegisterType<IntegrationsBusiness>().As<IIntegrationsBusiness>();
        }
    }
}
