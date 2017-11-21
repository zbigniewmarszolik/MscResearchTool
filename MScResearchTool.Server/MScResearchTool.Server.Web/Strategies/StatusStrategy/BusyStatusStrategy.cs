namespace MScResearchTool.Server.Web.Strategies.StatusStrategy
{
    public class BusyStatusStrategy : IStatusStrategy
    {
        public string DistributionsStatus()
        {
            return "One of more distributions in progress (or stuck).";
        }

        public string MainTaskStatus()
        {
            return "Main task in progress (or stuck). ";
        }
    }
}
