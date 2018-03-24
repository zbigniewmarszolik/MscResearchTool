using MScResearchTool.Mobile.Domain.Models;
using System.Threading.Tasks;

namespace MScResearchTool.Mobile.Domain.Services
{
    public interface ICrackingResultsService
    {
        Task PostResultAsync(CrackingResult result);
    }
}
