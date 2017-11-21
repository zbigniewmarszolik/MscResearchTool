namespace MScResearchTool.Server.Web.Strategies.StatusStrategy
{
    public class Status
    {
        public bool IsComponentFinished { get; }
        public bool IsComponentAvailable { get; }

        public Status(bool isComponentFinished, bool isComponentAvailable)
        {
            IsComponentFinished = isComponentFinished;
            IsComponentAvailable = isComponentAvailable;
        }
    }
}
