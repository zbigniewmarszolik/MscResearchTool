using MScResearchTool.Windows.Domain.Models;
using System.Threading.Tasks;

namespace MScResearchTool.Windows.Domain.Businesses
{
    public interface ICrackingsBusiness
    {
        Task<CrackingResult> BreakPasswordAsync(Cracking crackingTask);
    }
}
