namespace MScResearchTool.Mobile.Domain.Models
{
    public class CrackingDistribution
    {
        public int Id { get; set; }
        public virtual byte[] ArchiveToCrack { get; set; }
    }
}
