using System;

namespace MScResearchTool.Server.Web.ViewModels
{
    public class TaskViewModel
    {
        public int? ModelId { get; set; }
        public int DroidsCount { get; set; }
        public string TaskStatus { get; set; }
        public string TaskType { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
