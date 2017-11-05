﻿using MScResearchTool.Server.Core.Domain;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Models
{
    public class Integration : IntegrationBase
    {
        public virtual int DroidIntervals { get; set; }
        public virtual IList<IntegrationDistribution> Distributions { get; set; }

        // Result properties:
        public virtual double FullResult { get; set; }
        public virtual double PartialResult { get; set; }
        public virtual double FullTime { get; set; }
        public virtual double PartialTime { get; set; }
        public virtual int DesktopRAM { get; set; }
        public virtual string DesktopCPU { get; set; }

        // Report only:
        public virtual string UnresolvedFormula { get; set; }
    }
}
