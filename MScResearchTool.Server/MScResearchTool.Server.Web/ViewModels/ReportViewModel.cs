using System;

namespace MScResearchTool.Server.Web.ViewModels
{
    public class ReportViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime GenerationDate { get; set; }
        public byte[] ContentPdf { get; set; }
    }
}
