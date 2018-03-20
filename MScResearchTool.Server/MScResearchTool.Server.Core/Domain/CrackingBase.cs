using System.Collections.Generic;

namespace MScResearchTool.Server.Core.Domain
{
    public abstract class CrackingBase : TaskBase
    {
        // Task properties:
        public virtual byte[] ArchiveToCrack { get; set; }
        public virtual string ArchivePassword { get; set; }
        public virtual string FileName { get; set; }

        // Global dictionary property:
        public virtual IList<char> AvailableCharacters { get; set; }
    }
}
