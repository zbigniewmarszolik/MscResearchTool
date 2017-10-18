using System.Collections.Generic;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using MScResearchTool.Server.Infrastructure.Connectors;
using System;
using NHibernate.Linq;
using System.Linq;

namespace MScResearchTool.Server.Infrastructure.Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        public void Create(Report report)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    session.Save(report);
                }
            }
            catch(Exception ex)
            {

            }
            
        }

        public void Delete(int reportId)
        {
            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    var reportToDelete = session.Get("Report", reportId);

                    session.Delete(reportToDelete);
                    session.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public IList<Report> Read()
        {
            IList<Report> reports;

            try
            {
                using (var session = FluentNHibernateConnector.OpenSession())
                {
                    reports = session.Query<Report>().ToList();
                }
            }
            catch (Exception ex)
            {
                reports = null;
            }

            return reports;
        }
    }
}
