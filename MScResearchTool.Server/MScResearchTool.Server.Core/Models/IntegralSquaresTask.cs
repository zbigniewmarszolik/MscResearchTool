using MScResearchTool.Server.Core.Domain;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Models
{
    public class IntegralSquaresTask : ModelBase
    {
        public bool IsActive { get; set; }
        public int AmountOfDroids { get; set; }
        public IntegralSquaresResult Result { get; set; }
        public IList<IntegralSquaresDistribution> Distributions { get; set; }
    }
}
