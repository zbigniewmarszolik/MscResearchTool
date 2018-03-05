using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Domain
{
    public abstract class CrackingBase : TaskBase
    {
        // Task properties:
        public virtual byte[] ArchiveToCrack { get; set; }
        public virtual string ArchivePassword { get; set; }

        // Global dictionary property:
        public IList<char> AvailableCharacters { get; set; }

        // Result error property:
        public virtual bool IsArchiveUnbreakable { get; set; }
    }
}
