using Autofac;
using MScResearchTool.Server.Core.Enums;
using MScResearchTool.Server.Web.Converters;
using MScResearchTool.Server.Web.Facades;
using MScResearchTool.Server.Web.Factories;
using MScResearchTool.Server.Web.Helpers;
using MScResearchTool.Server.Web.Strategies.DeleteStrategy;
using MScResearchTool.Server.Web.Strategies.UnstuckStrategy;

namespace MScResearchTool.Server.Web.AutofacModules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TaskTypeConverter>();

            builder.RegisterType<HashHelper>();
            builder.RegisterType<IntegralInitializationHelper>();
            builder.RegisterType<ParseDoubleHelper>();

            builder.RegisterType<DeleteStrategyFactory>().SingleInstance();
            builder.RegisterType<IntegrationVMFactory>();
            builder.RegisterType<StatusStrategyFactory>().SingleInstance();
            builder.RegisterType<TaskVMFactory>();
            builder.RegisterType<UnstuckStrategyFactory>().SingleInstance();
            builder.RegisterType<UserFactory>();

            builder.RegisterType<TaskVMFacade>();

            builder.RegisterType<IntegrationDeleteStrategy>().Keyed<IDeleteStrategy>(ETaskType.SquareIntegration);
            builder.RegisterType<IntegrationDeleteStrategy>().Keyed<IDeleteStrategy>(ETaskType.TrapezoidIntegration);
            builder.RegisterType<IntegrationUnstuckStrategy>().Keyed<IUnstuckStrategy>(ETaskType.SquareIntegration);
            builder.RegisterType<IntegrationUnstuckStrategy>().Keyed<IUnstuckStrategy>(ETaskType.TrapezoidIntegration);
        }
    }
}
