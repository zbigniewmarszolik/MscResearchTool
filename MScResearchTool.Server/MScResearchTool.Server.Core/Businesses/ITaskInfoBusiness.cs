using MScResearchTool.Server.Core.Models;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Core.Businesses
{
    public interface ITaskInfoBusiness
    {
        Task<TaskInfo> GetFullTasksAvailabilityAsync();
        Task<TaskInfo> GetDistributedTasksAvailabilityAsync();
    }
}
