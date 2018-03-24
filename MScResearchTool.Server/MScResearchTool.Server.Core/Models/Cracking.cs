using MScResearchTool.Server.Core.Domain;
using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Models
{
    public class Cracking : CrackingBase
    {
        public virtual int DroidRanges { get; set; }
        public virtual IList<CrackingDistribution> Distributions { get; set; }

        // Result properties:
        public virtual string FullResult { get; set; }
        public virtual string PartialResult { get; set; }
        public virtual double FullTime { get; set; }
        public virtual double PartialTime { get; set; }
        public virtual int DesktopRAM { get; set; }
        public virtual string DesktopCPU { get; set; }
    }
}
