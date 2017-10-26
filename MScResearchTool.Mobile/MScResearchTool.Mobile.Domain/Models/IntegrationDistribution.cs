namespace MScResearchTool.Mobile.Domain.Models
{
    public class IntegrationDistribution
    {
        public int Id { get; set; }
        public bool IsTrapezoidMethodRequested { get; set; }
        public double UpBoundary { get; set; }
        public double DownBoundary { get; set; }
        public int Accuracy { get; set; }
        public string Formula { get; set; }
    }
}
