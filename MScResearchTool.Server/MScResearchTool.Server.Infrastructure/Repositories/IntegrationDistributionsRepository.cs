using System.Collections.Generic;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Infrastructure.Connectors;
using NHibernate.Linq;
using System.Linq;
using System;

namespace MScResearchTool.Server.Infrastructure.Repositories
{
    public class IntegrationDistributionsRepository : IIntegrationDistributionsRepository
    {
        public void Create(IntegrationDistribution integrationDistribution)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    session.Save(integrationDistribution);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<IntegrationDistribution> Read()
        {
            IList<IntegrationDistribution> integrationDistributions;

            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    integrationDistributions = session.Query<IntegrationDistribution>().ToList();
                }
            }
            catch (Exception ex)
            {
                integrationDistributions = null;
                throw ex;
            }

            return integrationDistributions;
        }

        public IList<IntegrationDistribution> ReadEager()
        {
            IList<IntegrationDistribution> integrationDistributions;

            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    integrationDistributions = session.Query<IntegrationDistribution>().Fetch(x => x.Task).ToList();
                }
            }
            catch (Exception ex)
            {
                integrationDistributions = null;
                throw ex;
            }

            return integrationDistributions;
        }

        public void Update(IntegrationDistribution integrationDistribution)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    var tx = session.BeginTransaction();

                    session.Update(integrationDistribution);

                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int integrationDistributionId)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    var distributionToDelete = session.Get("IntegrationDistribution", integrationDistributionId);

                    session.Delete(distributionToDelete);
                    session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
