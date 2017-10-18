using Autofac;
using MScResearchTool.Server.Web.Helpers;

namespace MScResearchTool.Server.Web.AutofacModules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IntegralFormulaHelper>().As<IntegralFormulaHelper>();
            builder.RegisterType<ParseDoubleHelper>().As<ParseDoubleHelper>();
        }
    }
}
