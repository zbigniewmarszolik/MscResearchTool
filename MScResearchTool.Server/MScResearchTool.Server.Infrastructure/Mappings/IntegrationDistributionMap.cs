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
            Map(x => x.IsAvailable);
            Map(x => x.IsFinished);
            Map(x => x.DeviceRAM);
            Map(x => x.DeviceCPU);
            References(x => x.Task)
                .Column("IntegrationId");
            Table("distributed_integrations");
        }
    }
}
