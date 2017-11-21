using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Strategies.DeleteStrategy
{
    public interface IDeleteStrategy
    {
        Task DeleteAsync(int deleteId);
    }
}
