using FluentNHibernate.Mapping;
using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.Infrastructure.Mappings
{
    public class IntegrationDistributionMap : ClassMap<IntegrationDistribution>
    {
        public IntegrationDistributionMap()
        {
            Id(x => x.Id);
            Map(x => x.CreationDate);
            Map(x => x.Accuracy);
            Map(x => x.UpBoundary);
            Map(x => x.DownBoundary);
            Map(x => x.IsTrapezoidMethodRequested);
            Map(x => x.Formula);
            Map(x => x.IsTaken);
            Map(x => x.IsFinished);
            References(x => x.Task)
                .Column("IntegrationTaskId");
            Table("integrationDistributions");
        }
    }
}
