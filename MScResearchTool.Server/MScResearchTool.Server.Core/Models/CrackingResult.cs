using MScResearchTool.Server.Core.Domain;

namespace MScResearchTool.Server.Core.Models
{
    public class CrackingResult : ModelBase
    {
        public bool IsDistributed { get; set; }
        public double BatteryUsage { get; set; }
        public string PasswordResult { get; set; }
        public double ElapsedSeconds { get; set; }
        public int RAM { get; set; }
        public string CPU { get; set; }
    }
}
