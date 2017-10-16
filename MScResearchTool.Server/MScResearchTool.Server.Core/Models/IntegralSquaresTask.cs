using MScResearchTool.Server.Core.Domain;
using System;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Models
{
    public class IntegralSquaresTask : ModelBase
    {
        public virtual DateTime CreationDate { get; set; }
        public virtual int AmountOfDroids { get; set; }
        public virtual IList<IntegralSquaresDistribution> Distributions { get; set; }
    }
}
