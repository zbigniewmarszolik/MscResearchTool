using System.Collections.Generic;

namespace MScResearchTool.Mobile.Domain.Models
{
    public class CrackingDistribution
    {
        public int Id { get; set; }
        public virtual byte[] ArchiveToCrack { get; set; }
        public IList<char> AvailableCharacters { get; set; }
        public char RangeBeginning { get; set; }
        public char RangeEnding { get; set; }
    }
}
