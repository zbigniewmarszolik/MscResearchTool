namespace MScResearchTool.Server.Web.Strategies.StatusStrategy
{
    public class AllDistributionsBusyStrategy : IStatusStrategy
    {
        public string DistributionsStatus()
        {
            return "All distributions in progress (or stuck).";
        }

        public string MainTaskStatus()
        {
            return "Main task in progress (or stuck). ";
        }
    }
}
