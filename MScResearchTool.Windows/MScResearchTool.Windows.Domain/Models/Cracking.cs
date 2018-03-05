using System.Collections.Generic;

namespace MScResearchTool.Windows.Domain.Models
{
    public class Cracking
    {
        public int Id { get; set; }
        public virtual byte[] ArchiveToCrack { get; set; }
        public IList<char> AvailableCharacters { get; set; }
    }
}
