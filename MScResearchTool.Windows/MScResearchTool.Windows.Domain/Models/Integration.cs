using System;

namespace MScResearchTool.Windows.Domain.Models
{
    public class Integration
    {
        public int Id { get; set; }
        public bool IsTrapezoidMethodRequested { get; set; }
        public double UpBoundary { get; set; }
        public double DownBoundary { get; set; }
        public int Accuracy { get; set; }
        public string Formula { get; set; }
    }
}
