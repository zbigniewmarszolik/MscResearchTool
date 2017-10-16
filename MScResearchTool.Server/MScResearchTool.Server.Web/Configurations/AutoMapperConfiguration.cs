using AutoMapper;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.ViewModels;

namespace MScResearchTool.Server.Web.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<ReportViewModel, Report>();
            CreateMap<Report, ReportViewModel>();
        }
    }
}
