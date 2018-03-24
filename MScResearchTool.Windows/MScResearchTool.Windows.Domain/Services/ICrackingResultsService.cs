using MScResearchTool.Windows.Domain.Models;
using System;
using System.Threading.Tasks;

namespace MScResearchTool.Windows.Domain.Services
{
    public interface ICrackingResultsService
    {
        Action<string> ConnectionErrorAction { get; set; }
        Task PostResultAsync(CrackingResult result);
    }
}
