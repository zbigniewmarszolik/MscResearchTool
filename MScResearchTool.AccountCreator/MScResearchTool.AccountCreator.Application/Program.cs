using Autofac;
using MScResearchTool.AccountCreator.Application.Managers;
using MScResearchTool.AccountCreator.Application.Modules;
using MScResearchTool.AccountCreator.Binder.Modules;

namespace MScResearchTool.AccountCreator.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<AppModule>();
            builder.RegisterModule<ServicesModule>();

            var container = builder.Build();

            var manager = container.Resolve<AccountManager>();

            manager.RunApp();
        }
    }
}
