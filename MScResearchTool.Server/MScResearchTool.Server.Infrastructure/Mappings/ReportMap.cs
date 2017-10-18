using FluentNHibernate.Mapping;
using MScResearchTool.Server.Core.Models;

namespace MScResearchTool.Server.Infrastructure.Mappings
{
    public class ReportMap : ClassMap<Report>
    {
        public ReportMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.GenerationDate);
            Map(x => x.ContentPdf);
            Table("reports");
        }
    }
}
