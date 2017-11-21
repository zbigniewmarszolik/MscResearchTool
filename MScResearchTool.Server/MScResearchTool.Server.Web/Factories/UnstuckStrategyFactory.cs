using Autofac.Features.Indexed;
using MScResearchTool.Server.Core.Enums;
using MScResearchTool.Server.Web.Strategies.UnstuckStrategy;

namespace MScResearchTool.Server.Web.Factories
{
    public class UnstuckStrategyFactory
    {
        private IUnstuckStrategy _unstuckStrategy { get; set; }
        private IIndex<ETaskType, IUnstuckStrategy> _strategies { get; set; }

        public UnstuckStrategyFactory(IIndex<ETaskType, IUnstuckStrategy> strategies)
        {
            _strategies = strategies;
        }

        public IUnstuckStrategy ResolveUnstuckStrategy(ETaskType taskType)
        {
            _unstuckStrategy = _strategies[taskType];

            return _unstuckStrategy;
        }
    }
}
