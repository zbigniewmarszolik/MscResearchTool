using Autofac;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.BusinessLogic.Factories;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Factories;

namespace MScResearchTool.Server.Binder.AutofacModules
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReportsBusiness>().As<IReportsBusiness>();
            builder.RegisterType<TaskInfoBusiness>().As<ITaskInfoBusiness>();
            builder.RegisterType<IntegrationsBusiness>().As<IIntegrationsBusiness>();
            builder.RegisterType<IntegrationDistributionsBusiness>().As<IIntegrationDistributionsBusiness>();
            builder.RegisterType<IntegrationResultsBusiness>().As<IIntegrationResultsBusiness>();
            builder.RegisterType<IntegrationDistributionFactory>().As<IIntegrationDistributionFactory>();
        }
    }
}
