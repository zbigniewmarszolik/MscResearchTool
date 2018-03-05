using MScResearchTool.Server.Core.Domain;

namespace MScResearchTool.Server.Core.Models
{
    public class CrackingDistribution : CrackingBase
    {
        public virtual Cracking Task { get; set; }

        // Result properties:
        public virtual int DeviceRAM { get; set; }
        public virtual string DeviceCPU { get; set; }
        public virtual double DeviceResult { get; set; }
        public virtual double DeviceTime { get; set; }

        // Dictionary range properties:
        public char RangeBeginning { get; set; }
        public char RangeEnding { get; set; }
    }
}
