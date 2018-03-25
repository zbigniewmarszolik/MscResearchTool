namespace MScResearchTool.Mobile.Domain.Models
{
    public class IntegrationResult
    {
        public int Id { get; set; }
        public bool IsDistributed { get; set; }
        public double BatteryUsage { get; set; }
        public double Result { get; set; }
        public double ElapsedSeconds { get; set; }
        public int RAM { get; set; }
        public string CPU { get; set; }
    }
}
