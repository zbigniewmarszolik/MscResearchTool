using Autofac;
using MScResearchTool.Server.Web.Factories;
using MScResearchTool.Server.Web.Helpers;

namespace MScResearchTool.Server.Web.AutofacModules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IntegralInitializationHelper>().As<IntegralInitializationHelper>();
            builder.RegisterType<ParseDoubleHelper>().As<ParseDoubleHelper>();
            builder.RegisterType<IntegrationFactory>().As<IntegrationFactory>();
            builder.RegisterType<IntegrationVMFactory>().As<IntegrationVMFactory>();
            builder.RegisterType<TaskVMFactory>().As<TaskVMFactory>();
        }
    }
}
