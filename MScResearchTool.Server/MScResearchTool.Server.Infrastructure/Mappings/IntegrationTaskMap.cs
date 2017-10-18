using FluentNHibernate.Mapping;
using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.Infrastructure.Mappings
{
    public class IntegrationTaskMap : ClassMap<IntegrationTask>
    {
        public IntegrationTaskMap()
        {
            Id(x => x.Id);
            Map(x => x.CreationDate);
            Map(x => x.DroidIntervals);
            Map(x => x.Accuracy);
            Map(x => x.UpBoundary);
            Map(x => x.DownBoundary);
            Map(x => x.IsTrapezoidMethodRequested);
            Map(x => x.Formula);
            Map(x => x.IsTaken);
            Map(x => x.IsFinished);
            HasMany(x => x.Distributions)
                .Inverse()
                .Cascade.All()
                .KeyColumn("IntegrationTaskId");
            Table("integrationTasks");
        }
    }
}
