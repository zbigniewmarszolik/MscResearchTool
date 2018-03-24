using Autofac;
using MScResearchTool.Windows.Domain.Services;
using MScResearchTool.Windows.Services.Factories;
using MScResearchTool.Windows.Services.Services;

namespace MScResearchTool.Windows.Binder.AutofacModules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpClientFactory>().As<HttpClientFactory>();
            builder.RegisterType<TasksService>().As<ITasksService>();
            builder.RegisterType<CrackingsService>().As<ICrackingsService>();
            builder.RegisterType<CrackingResultsService>().As<ICrackingResultsService>();
            builder.RegisterType<IntegrationsService>().As<IIntegrationsService>();
            builder.RegisterType<IntegrationResultsService>().As<IIntegrationResultsService>();
        }
    }
}
