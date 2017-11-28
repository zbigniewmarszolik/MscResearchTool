using Autofac;
using MScResearchTool.AccountCreator.Domain.Services;
using MScResearchTool.AccountCreator.Services.Factories;
using MScResearchTool.AccountCreator.Services.Services;

namespace MScResearchTool.AccountCreator.Binder.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersService>().As<IUsersService>();

            builder.RegisterType<HttpClientFactory>();
        }
    }
}
