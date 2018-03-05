using MScResearchTool.Server.Core.Models;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Repositories
{
    public interface ICrackingsRepository
    {
        void Create(Cracking crackingTask);
        IList<Cracking> Read();
        IList<Cracking> ReadEager();
        void Update(Cracking crackingTask);
        void Delete(int crackingTaskId);
    }
}
