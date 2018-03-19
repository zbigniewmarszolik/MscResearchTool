using System;
using System.Collections.Generic;
using System.Linq;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Infrastructure.Connectors;
using NHibernate.Linq;

namespace MScResearchTool.Server.Infrastructure.Repositories
{
    public class CrackingsRepository : ICrackingsRepository
    {
        public void Create(Cracking crackingTask)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    session.Save(crackingTask);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Cracking> Read()
        {
            IList<Cracking> crackingTasks;

            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    crackingTasks = session.Query<Cracking>().ToList();
                }
            }
            catch (Exception ex)
            {
                crackingTasks = null;
                throw ex;
            }

            return crackingTasks;
        }

        public IList<Cracking> ReadEager()
        {
            IList<Cracking> crackingTasks;

            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    crackingTasks = session.Query<Cracking>().Fetch(x => x.Distributions).ToList();
                }
            }
            catch (Exception ex)
            {
                crackingTasks = null;
                throw ex;
            }

            return crackingTasks;
        }

        public void Update(Cracking crackingTask)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    var tx = session.BeginTransaction();

                    session.Update(crackingTask);

                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int crackingTaskId)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    var taskToDelete = session.Get("Cracking", crackingTaskId);

                    session.Delete(taskToDelete);
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
