using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.Web.ViewModels
{
    public class TaskViewModel
    {
        public IntegralSquaresTask IntegralSquares { get; set; }
        public IntegralTrapezoidsTask IntegralTrapezoids { get; set; }
        public int? ObjectId { get; set; }
        public int DroidsCount { get; set; }
        public string TaskType { get; set; }

        public void AssignTypeAndDroids()
        {
            if (IntegralSquares == null && IntegralTrapezoids != null)
            {
                TaskType = "Integration by trapezoids method";
                DroidsCount = IntegralTrapezoids.AmountOfDroids;
                ObjectId = IntegralTrapezoids.Id;
            }

            else if (IntegralSquares != null && IntegralTrapezoids == null)
            {
                TaskType = "Integration by squares method";
                DroidsCount = IntegralSquares.AmountOfDroids;
                ObjectId = IntegralSquares.Id;
            }

            else if (IntegralSquares == null && IntegralTrapezoids == null)
            {
                TaskType = "UNKNOWN";
                DroidsCount = 0;
                ObjectId = null;
            }
        }
    }
}
