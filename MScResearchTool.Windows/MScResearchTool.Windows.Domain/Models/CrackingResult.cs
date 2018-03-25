namespace MScResearchTool.Windows.Domain.Models
{
    public class CrackingResult
    {
        public int Id { get; set; }
        public bool IsDistributed { get; set; }
        public string PasswordResult { get; set; }
        public double ElapsedSeconds { get; set; }
        public int RAM { get; set; }
        public string CPU { get; set; }
    }
}
