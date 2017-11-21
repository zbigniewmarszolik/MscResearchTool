using Autofac;
using MScResearchTool.Mobile.Domain.Services;
using MScResearchTool.Mobile.Services.Factories;
using MScResearchTool.Mobile.Services.Services;

namespace MScResearchTool.Mobile.Binder.AutofacModules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpClientFactory>();

            builder.RegisterType<TasksService>().As<ITasksService>();
            builder.RegisterType<IntegrationsService>().As<IIntegrationsService>();
            builder.RegisterType<IntegrationResultsService>().As<IIntegrationResultsService>();
        }
    }
}
