using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Repositories
{
    public interface IReportsRepository
    {
        void Create(Report report);
        IList<Report> Read();
        void Delete(int reportId);
    }
}
