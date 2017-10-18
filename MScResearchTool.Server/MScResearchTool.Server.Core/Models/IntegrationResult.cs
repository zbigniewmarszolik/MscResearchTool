using MScResearchTool.Server.Core.Domain;

namespace MScResearchTool.Server.Core.Models
{
    public class IntegrationResult : ModelBase
    {
        public bool IsDistributed { get; set; }
        public double Result { get; set; }
        public double ElapsedSeconds { get; set; }
    }
}
