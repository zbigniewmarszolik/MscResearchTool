namespace MScResearchTool.Server.Core.Models
{
    public class IntegralTrapezoidsResult : ModelBase
    {
        public string WindowsTime { get; set; }
        public string DroidTime { get; set; }
        public int AmountOfDroids { get; set; }
        public IntegralTrapezoidsTask Task { get; set; }
    }
}
