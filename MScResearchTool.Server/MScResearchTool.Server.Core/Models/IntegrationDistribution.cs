using MScResearchTool.Server.Core.Domain;

namespace MScResearchTool.Server.Core.Models
{
    public class IntegrationDistribution : IntegrationBase
    {
        public virtual Integration Task { get; set; }
    }
}
