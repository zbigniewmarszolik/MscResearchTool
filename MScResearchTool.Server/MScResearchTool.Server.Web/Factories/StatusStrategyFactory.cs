using MScResearchTool.Server.Web.Strategies.StatusStrategy;
using System.Collections.Generic;
using System.Linq;

namespace MScResearchTool.Server.Web.Factories
{
    public class StatusStrategyFactory
    {
        public IStatusStrategy CreateForMainTask(Status state)
        {
            if (!state.IsComponentFinished && !state.IsComponentAvailable)
                return new BusyStatusStrategy();

            else if (state.IsComponentFinished)
                return new FinishedStatusStrategy();

            else return new WaitingStatusStrategy();
        }

        public IStatusStrategy CreateForDistributions(IList<Status> distributionsStates)
        {
            if (distributionsStates.All(x => x.IsComponentFinished))
                return new FinishedStatusStrategy();

            else if (distributionsStates.All(x => x.IsComponentAvailable))
                return new WaitingStatusStrategy();

            else if (distributionsStates.All(x => x.IsComponentAvailable == false && x.IsComponentFinished == false))
                return new AllDistributionsBusyStrategy();

            else return new BusyStatusStrategy();
        }
    }
}
