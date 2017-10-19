using FluentNHibernate.Mapping;
using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.Infrastructure.Mappings
{
    public class IntegrationMap : ClassMap<Integration>
    {
        public IntegrationMap()
        {
            Id(x => x.Id);
            Map(x => x.CreationDate);
            Map(x => x.DroidIntervals);
            Map(x => x.Accuracy);
            Map(x => x.UpBoundary);
            Map(x => x.DownBoundary);
            Map(x => x.IsTrapezoidMethodRequested);
            Map(x => x.Formula);
            Map(x => x.IsAvailable);
            Map(x => x.IsFinished);
            Map(x => x.FullResult);
            Map(x => x.PartialResult);
            Map(x => x.FullTime);
            Map(x => x.PartialTime);
            Map(x => x.DesktopRAM);
            Map(x => x.DesktopCPU);
            HasMany(x => x.Distributions)
                .Inverse()
                .Cascade.All()
                .KeyColumn("IntegrationId");
            Table("integrations");
        }
    }
}
