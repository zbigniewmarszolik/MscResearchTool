using MScResearchTool.Mobile.Domain.Models;
using System;
using System.Threading.Tasks;

namespace MScResearchTool.Mobile.Domain.Services
{
    public interface ITasksService
    {
        Action<string> ConnectionErrorAction { get; set; }
        Task<TaskInfo> GetTasksAvailabilityAsync();
    }
}
