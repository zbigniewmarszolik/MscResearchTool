using MScResearchTool.Server.Core.Domain;

namespace MScResearchTool.Server.Core.Models
{
    public class CrackingDistribution : CrackingBase
    {
        public virtual Cracking Task { get; set; }

        // Result properties:
        public virtual int DeviceRAM { get; set; }
        public virtual string DeviceCPU { get; set; }
        public virtual string DeviceResult { get; set; }
        public virtual double DeviceTime { get; set; }
        public virtual bool IsFounder { get; set; }

        // Dictionary range properties:
        public virtual char RangeBeginning { get; set; }
        public virtual char RangeEnding { get; set; }

        // Battery property:
        public virtual double BatteryUsage { get; set; }
    }
}
