using System;

namespace MScResearchTool.Server.Core.Domain
{
    public abstract class IntegrationBase : ModelBase
    {
        // Basic properties:
        public virtual DateTime CreationDate { get; set; }
        public virtual bool IsTrapezoidMethodRequested { get; set; }

        // Integral assumptions:
        public virtual double UpBoundary { get; set; }
        public virtual double DownBoundary { get; set; }
        public virtual int Precision { get; set; } // Amount of squares / trapezoids. From this number, the integration step is calculated in algorithm internally.

        // Integral formula:
        public virtual string Formula { get; set; }
    }
}
