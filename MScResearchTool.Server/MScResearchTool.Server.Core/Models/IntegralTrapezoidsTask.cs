using MScResearchTool.Server.Core.Domain;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Models
{
    public class IntegralTrapezoidsTask : ModelBase
    {
        public bool IsActive { get; set; }
        public int AmountOfDroids { get; set; }
        public IntegralTrapezoidsResult Result { get; set; }
        public IList<IntegralTrapezoidsDistribution> Distributions { get; set; }
    }
}
