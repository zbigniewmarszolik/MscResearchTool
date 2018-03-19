using FluentNHibernate.Mapping;
using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.Infrastructure.Mappings
{
    public class CrackingMap : ClassMap<Cracking>
    {
        public CrackingMap()
        {
            Id(x => x.Id);
            Map(x => x.CreationDate);
            Map(x => x.DroidRanges);
            Map(x => x.ArchiveToCrack);
            Map(x => x.ArchivePassword);
            Map(x => x.FileName);
            Map(x => x.SerializedContentFound).CustomSqlType("LONGTEXT");
            Map(x => x.IsAvailable);
            Map(x => x.IsFinished);
            Map(x => x.IsArchiveUnbreakable);
            Map(x => x.FullResult);
            Map(x => x.PartialResult);
            Map(x => x.FullTime);
            Map(x => x.PartialTime);
            Map(x => x.DesktopRAM);
            Map(x => x.DesktopCPU);
            HasMany(x => x.Distributions)
                .Inverse()
                .Cascade.All()
                .KeyColumn("CrackingId");
            Table("crackings");
        }
    }
}
