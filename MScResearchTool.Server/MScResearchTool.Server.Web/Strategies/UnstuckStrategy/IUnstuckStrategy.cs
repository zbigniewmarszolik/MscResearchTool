using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Strategies.UnstuckStrategy
{
    public interface IUnstuckStrategy
    {
        Task UnstuckAsync(int unstuckId);
    }
}
