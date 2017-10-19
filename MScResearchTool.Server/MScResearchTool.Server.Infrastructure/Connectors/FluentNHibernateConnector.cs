using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MScResearchTool.Server.Infrastructure.Mappings;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace MScResearchTool.Server.Infrastructure.Connectors
{
    public static class FluentNHibernateConnector
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                .ConnectionString(c => c.Server("localhost").Database("msc_database").Username("root").Password("pass")).ShowSql())
                .Mappings(m => m.FluentMappings
                .AddFromAssemblyOf<IntegrationMap>()
                .AddFromAssemblyOf<IntegrationDistributionMap>()
                .AddFromAssemblyOf<ReportMap>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg)
                .Execute(false, true))
                .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}
