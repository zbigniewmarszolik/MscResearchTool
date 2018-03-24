using MScResearchTool.Windows.Domain.Models;
using System;
using System.Threading.Tasks;

namespace MScResearchTool.Windows.Domain.Services
{
    public interface ICrackingsService
    {
        Action<string> ConnectionErrorAction { get; set; }
        Task<Cracking> GetCrackingAsync();
    }
}
