using System;

namespace MScResearchTool.Server.Core.Domain
{
    public abstract class TaskBase : ModelBase
    {
        // Basic properties:
        public virtual DateTime CreationDate { get; set; }

        // Status properties:
        public virtual bool IsAvailable { get; set; }
        public virtual bool IsFinished { get; set; }
    }
}
