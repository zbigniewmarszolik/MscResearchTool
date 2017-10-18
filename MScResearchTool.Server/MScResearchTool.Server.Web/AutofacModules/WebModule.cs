using Autofac;
using MScResearchTool.Server.Core.Factories;
using MScResearchTool.Server.Core.Helpers;
using MScResearchTool.Server.Web.Factories;
using MScResearchTool.Server.Web.Helpers;
using MScResearchTool.Server.Web.ViewModels;

namespace MScResearchTool.Server.Web.AutofacModules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IntegralInitializationHelper>().As<IIntegralInitializationHelper>();
            builder.RegisterType<ParseDoubleHelper>().As<IParseDoubleHelper>();
            builder.RegisterType<TaskVMFactory>().As<ITaskVMFactory<TaskViewModel>>();
        }
    }
}
