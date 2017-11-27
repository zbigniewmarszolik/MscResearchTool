using Autofac;
using MScResearchTool.AccountCreator.Application.Managers;

namespace MScResearchTool.AccountCreator.Application.Modules
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountManager>();
        }
    }
}
