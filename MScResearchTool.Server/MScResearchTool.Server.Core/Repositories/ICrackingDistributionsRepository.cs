using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Repositories
{
    public interface ICrackingDistributionsRepository
    {
        void Create(CrackingDistribution crackingDistribution);
        IList<CrackingDistribution> Read();
        IList<CrackingDistribution> ReadEager();
        void Update(CrackingDistribution crackingDistribution);
        void Delete(int crackingDistributionId);
    }
}
