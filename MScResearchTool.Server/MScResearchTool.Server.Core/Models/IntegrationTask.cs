using MScResearchTool.Server.Core.Domain;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Models
{
    public class IntegrationTask : IntegrationBase
    {
        public virtual int AmountOfDroids { get; set; }
        public IList<IntegrationDistribution> Distributions { get; set; }
    }
}
