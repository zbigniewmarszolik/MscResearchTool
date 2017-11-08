using MScResearchTool.Server.Core.Domain;

namespace MScResearchTool.Server.Core.Models
{
    public class IntegrationDistribution : IntegrationBase
    {
        public virtual Integration Task { get; set; }

        // Result properties:
        public virtual int DeviceRAM { get; set; }
        public virtual string DeviceCPU { get; set; }
        public virtual double DeviceResult { get; set; }
        public virtual double DeviceTime { get; set; }
    }
}
