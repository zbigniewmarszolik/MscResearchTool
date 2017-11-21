using MScResearchTool.Mobile.Domain.Models;
using System;
using System.Threading.Tasks;

namespace MScResearchTool.Mobile.Domain.Services
{
    public interface ITasksService
    {
        Task<TaskInfo> GetTasksAvailabilityAsync();
    }
}
