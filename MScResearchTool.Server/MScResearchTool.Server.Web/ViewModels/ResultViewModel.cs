using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.Web.ViewModels
{
    public class ResultViewModel
    {
        public IntegralSquaresResult IntegralSquares { get; set; }
        public IntegralTrapezoidsResult IntegralTrapezoids { get; set; }
        public int? ObjectId { get; set; }
        public int DroidsCount { get; set; }
        public string ResultType { get; set; }
        public string WinTime { get; set; }
        public string MobileTime { get; set; }

        public void AssignTypeAndDroids()
        {
            if(IntegralSquares == null && IntegralTrapezoids != null)
            {
                ResultType = "Integration by trapezoids method";
                DroidsCount = IntegralTrapezoids.AmountOfDroids;
                ObjectId = IntegralTrapezoids.Id;
                WinTime = IntegralTrapezoids.WindowsTime;
                MobileTime = IntegralTrapezoids.DroidTime;
            }

            else if (IntegralSquares != null && IntegralTrapezoids == null)
            {
                ResultType = "Integration by squares method";
                DroidsCount = IntegralSquares.AmountOfDroids;
                ObjectId = IntegralSquares.Id;
                WinTime = IntegralSquares.WindowsTime;
                MobileTime = IntegralSquares.DroidTime;
            }

            else if (IntegralSquares == null && IntegralTrapezoids == null)
            {
                ResultType = "UNKNOWN";
                DroidsCount = 0;
                ObjectId = null;
                WinTime = 0.ToString();
                MobileTime = 0.ToString();
            }
        }
    }
}
