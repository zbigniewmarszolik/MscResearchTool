using MScResearchTool.Server.Core.Domain;
using System;

namespace MScResearchTool.Server.Core.Models
{
    public class Report : ModelBase
    {
        public virtual string Title { get; set; }
        public virtual DateTime GenerationDate { get; set; }
        public virtual byte[] ContentPdf { get; set; }
    }
}
