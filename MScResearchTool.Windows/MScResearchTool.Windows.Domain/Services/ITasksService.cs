using MScResearchTool.Windows.Domain.Models;
using System;
using System.Threading.Tasks;

namespace MScResearchTool.Windows.Domain.Services
{
    public interface ITasksService
    {
        Action<string> ConnectionErrorAction { get; set; }
        Task<TaskInfo> GetTasksAvailabilityAsync();
    }
}
