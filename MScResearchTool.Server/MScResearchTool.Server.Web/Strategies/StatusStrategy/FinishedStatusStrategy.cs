namespace MScResearchTool.Server.Web.Strategies.StatusStrategy
{
    public class FinishedStatusStrategy : IStatusStrategy
    {
        public string DistributionsStatus()
        {
            return "All distributions finished.";
        }

        public string MainTaskStatus()
        {
            return "Main task finished. ";
        }
    }
}
