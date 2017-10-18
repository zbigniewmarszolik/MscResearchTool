using System.Collections.Generic;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Infrastructure.Connectors;
using System;
using NHibernate.Linq;
using System.Linq;

namespace MScResearchTool.Server.Infrastructure.Repositories
{
    public class IntegrationTasksRepository : IIntegrationTasksRepository
    {
        public void Create(IntegrationTask integrationTask)
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

        public void Delete(int integrationTaskId)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    var taskToDelete = session.Get("IntegrationTask", integrationTaskId);

                    session.Delete(taskToDelete);
                    session.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public IList<IntegrationTask> Read()
        {
            IList<IntegrationTask> integrationTasks;

            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    integrationTasks = session.Query<IntegrationTask>().ToList();
                }
            }
            catch (Exception ex)
            {
                integrationTasks = null;
            }

            return integrationTasks;
        }

        public void Update(IntegrationTask integrationTask)
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
    }
}
