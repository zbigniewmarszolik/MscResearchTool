using MScResearchTool.Server.Core.Domain;

namespace MScResearchTool.Server.Core.Models
{
    public class IntegralSquaresResult : ModelBase
    {
        public string WindowsTime { get; set; }
        public string DroidTime { get; set; }
        public int AmountOfDroids { get; set; }
        public IntegralSquaresTask Task { get; set; }
    }
}
