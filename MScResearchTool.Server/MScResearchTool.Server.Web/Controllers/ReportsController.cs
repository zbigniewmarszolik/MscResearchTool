using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Web.ViewModels;
using System.Collections.Generic;
using MScResearchTool.Server.Core.Businesses;
using System.Threading.Tasks;
using AutoMapper;
using MScResearchTool.Server.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace MScResearchTool.Server.Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private IReportsBusiness _reportsBusiness { get; set; }
        private IMapper _mapper { get; set; }

        public ReportsController(IReportsBusiness reportsBusiness, IMapper mapper)
        {
            _reportsBusiness = reportsBusiness;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var dataModels = await _reportsBusiness.ReadAllAsync();
            var vms = new List<ReportViewModel>();

            foreach(var item in dataModels)
            {
                var vm = _mapper.Map<Report, ReportViewModel>(item);
                vms.Add(vm);
            }

            return View(vms);
        }

        public async Task<IActionResult> DeleteReport(int removeId)
        {
            await _reportsBusiness.DeleteAsync(removeId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DownloadReport(int downloadId)
        {
            var report = await _reportsBusiness.ReadByIdAsync(downloadId);

            string fileName = report.Title;
            return File(report.ContentPdf, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public async Task<IActionResult> RemoveAllReports()
        {
            var reports = await _reportsBusiness.ReadAllAsync();

            foreach(var item in reports)
            {
                await _reportsBusiness.DeleteAsync(item.Id);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
