using System.Collections.Generic;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Infrastructure.Connectors;
using System;
using NHibernate.Linq;
using System.Linq;

namespace MScResearchTool.Server.Infrastructure.Repositories
{
    public class IntegrationsRepository : IIntegrationsRepository
    {
        public void Create(Integration integrationTask)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    session.Save(integrationTask);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public IList<Integration> Read()
        {
            IList<Integration> integrationTasks;

            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    integrationTasks = session.Query<Integration>().ToList();
                }
            }
            catch (Exception ex)
            {
                integrationTasks = null;
            }

            return integrationTasks;
        }

        public IList<Integration> ReadEager()
        {
            IList<Integration> integrationTasks;

            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    integrationTasks = session.Query<Integration>().Fetch(x => x.Distributions).ToList();
                }
            }
            catch (Exception ex)
            {
                integrationTasks = null;
            }

            return integrationTasks;
        }

        public void Update(Integration integrationTask)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    var tx = session.BeginTransaction();

                    session.Update(integrationTask);

                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int integrationTaskId)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    var taskToDelete = session.Get("Integration", integrationTaskId);

                    session.Delete(taskToDelete);
                    session.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
