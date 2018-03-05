using System;

namespace MScResearchTool.Server.Core.Domain
{
    public abstract class IntegrationBase : TaskBase
    {
        // Basic properties:
        public virtual bool IsTrapezoidMethodRequested { get; set; }

        // Integral assumptions:
        public virtual double UpBoundary { get; set; }
        public virtual double DownBoundary { get; set; }
        public virtual int Accuracy { get; set; } // Amount of squares / trapezoids. From this number, the integration step is calculated in algorithm internally.

        // Integral formula:
        public virtual string Formula { get; set; }    

        // Result error property:
        public virtual bool IsResultNaN { get; set; }
    }
}
