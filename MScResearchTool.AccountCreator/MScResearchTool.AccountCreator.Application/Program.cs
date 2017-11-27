using Autofac;
using MScResearchTool.AccountCreator.Binder.Modules;

namespace MScResearchTool.AccountCreator.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ServicesModule>();

            var container = builder.Build();

            var manager = container.Resolve<AccountManager>();

            manager.RunApp();
        }
    }
}
