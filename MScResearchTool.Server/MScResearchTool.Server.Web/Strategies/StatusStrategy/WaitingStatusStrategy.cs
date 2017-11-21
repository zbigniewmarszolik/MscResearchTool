namespace MScResearchTool.Server.Web.Strategies.StatusStrategy
{
    public class WaitingStatusStrategy : IStatusStrategy
    {
        public string DistributionsStatus()
        {
            return "Distributions waiting.";
        }

        public string MainTaskStatus()
        {
            return "Main task waiting. ";
        }
    }
}
