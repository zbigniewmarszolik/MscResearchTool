using System;
using System.Collections.Generic;
using System.Linq;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Infrastructure.Connectors;
using NHibernate.Linq;

namespace MScResearchTool.Server.Infrastructure.Repositories
{
    public class CrackingDistributionsRepository : ICrackingDistributionsRepository
    {
        public void Create(CrackingDistribution crackingDistribution)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    session.Save(crackingDistribution);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<CrackingDistribution> Read()
        {
            IList<CrackingDistribution> crackingDistributions;

            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    crackingDistributions = session.Query<CrackingDistribution>().ToList();
                }
            }
            catch (Exception ex)
            {
                crackingDistributions = null;
                throw ex;
            }

            return crackingDistributions;
        }

        public IList<CrackingDistribution> ReadEager()
        {
            IList<CrackingDistribution> crackingDistributions;

            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    crackingDistributions = session.Query<CrackingDistribution>().Fetch(x => x.Task).ToList();
                }
            }
            catch (Exception ex)
            {
                crackingDistributions = null;
                throw ex;
            }

            return crackingDistributions;
        }

        public void Update(CrackingDistribution crackingDistribution)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    var tx = session.BeginTransaction();

                    session.Update(crackingDistribution);

                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int crackingDistributionId)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    var distributionToDelete = session.Get("CrackingDistribution", crackingDistributionId);

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
