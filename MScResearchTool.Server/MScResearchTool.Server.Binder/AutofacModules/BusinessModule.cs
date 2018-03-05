using Autofac;
using MScResearchTool.Server.BusinessLogic.Businesses;
using MScResearchTool.Server.BusinessLogic.Factories;
using MScResearchTool.Server.Core.Businesses;

namespace MScResearchTool.Server.Binder.AutofacModules
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReportsBusiness>().As<IReportsBusiness>();
            builder.RegisterType<TaskInfoBusiness>().As<ITaskInfoBusiness>();

            builder.RegisterType<CrackingsBusiness>().As<ICrackingsBusiness>();
            builder.RegisterType<CrackingDistributionsBusiness>().As<ICrackingDistributionsBusiness>();
            builder.RegisterType<CrackingResultsBusiness>().As<ICrackingResultsBusiness>();

            builder.RegisterType<IntegrationsBusiness>().As<IIntegrationsBusiness>();
            builder.RegisterType<IntegrationDistributionsBusiness>().As<IIntegrationDistributionsBusiness>();
            builder.RegisterType<IntegrationResultsBusiness>().As<IIntegrationResultsBusiness>();

            builder.RegisterType<UsersBusiness>().As<IUsersBusiness>();

            builder.RegisterType<IntegrationDistributionFactory>();
        }
    }
}
