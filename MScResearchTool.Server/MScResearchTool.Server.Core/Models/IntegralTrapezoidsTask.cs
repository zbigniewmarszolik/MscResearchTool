using MScResearchTool.Server.Core.Domain;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Models
{
    public class IntegralTrapezoidsTask : ModelBase
    {
        public virtual int AmountOfDroids { get; set; }
        public virtual IList<IntegralTrapezoidsDistribution> Distributions { get; set; }
    }
}
