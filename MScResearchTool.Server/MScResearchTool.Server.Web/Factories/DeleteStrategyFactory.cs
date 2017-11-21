using Autofac.Features.Indexed;
using MScResearchTool.Server.Core.Enums;
using MScResearchTool.Server.Web.Strategies.DeleteStrategy;

namespace MScResearchTool.Server.Web.Factories
{
    public class DeleteStrategyFactory
    {
        private IDeleteStrategy _deleteStrategy { get; set; }
        private IIndex<ETaskType, IDeleteStrategy> _strategies { get; set; }

        public DeleteStrategyFactory(IIndex<ETaskType, IDeleteStrategy> strategies)
        {
            _strategies = strategies;
        }

        public IDeleteStrategy ResolveDeleteStrategy(ETaskType taskType)
        {
            _deleteStrategy = _strategies[taskType];

            return _deleteStrategy;
        }
    }
}
