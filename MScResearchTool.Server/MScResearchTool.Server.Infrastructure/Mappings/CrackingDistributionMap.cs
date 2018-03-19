using FluentNHibernate.Mapping;
using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.Infrastructure.Mappings
{
    public class CrackingDistributionMap : ClassMap<CrackingDistribution>
    {
        public CrackingDistributionMap()
        {
            Id(x => x.Id);
            Map(x => x.CreationDate);
            Map(x => x.ArchiveToCrack);
            Map(x => x.ArchivePassword);
            Map(x => x.FileName);
            Map(x => x.SerializedContentFound).CustomSqlType("LONGTEXT");
            Map(x => x.RangeBeginning);
            Map(x => x.RangeEnding);
            Map(x => x.IsAvailable);
            Map(x => x.IsFinished);
            Map(x => x.IsArchiveUnbreakable);
            Map(x => x.DeviceRAM);
            Map(x => x.DeviceCPU);
            Map(x => x.DeviceResult);
            Map(x => x.DeviceTime);
            References(x => x.Task)
                .Column("CrackingId");
            Table("distributed_crackings");
        }
    }
}
