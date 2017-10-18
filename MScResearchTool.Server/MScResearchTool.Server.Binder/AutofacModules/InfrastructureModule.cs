﻿using Autofac;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Infrastructure.Repositories;

namespace MScResearchTool.Server.Binder.AutofacModules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReportsRepository>().As<IReportsRepository>();
            builder.RegisterType<IntegrationTasksRepository>().As<IIntegrationTasksRepository>();
            builder.RegisterType<IntegrationDistributionsRepository>().As<IIntegrationDistributionsRepository>();
        }
    }
}
