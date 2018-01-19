using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace MScResearchTool.Server.Web.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        private IHostingEnvironment _hostingEnvironment { get; set; }

        private string _overallPath { get; set; }

        private string _managerName { get; set; }
        private string _winClientName { get; set; }
        private string _apkName { get; set; }

        public FilesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

            var webRootPath = _hostingEnvironment.WebRootPath;

            _overallPath = Path.Combine(webRootPath, "apps");

            _managerName = "MSc Research Tool Account Creator.rar";
            _winClientName = "MSc Research Tool Client - Windows x86.rar";
            _apkName = "MScResearchTool.MScResearchTool.apk";
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DownloadAccountManager()
        {
            var managerPath = Path.Combine(_overallPath, _managerName);

            var file = System.IO.File.ReadAllBytes(managerPath);

            string fileName = _managerName;
            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpGet]
        public IActionResult DownloadWindowsClient()
        {
            var winClientPath = Path.Combine(_overallPath, _winClientName);

            var file = System.IO.File.ReadAllBytes(winClientPath);

            string fileName = _winClientName;
            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpGet]
        public IActionResult DownloadAndroidApplication()
        {
            var apkPath = Path.Combine(_overallPath, _apkName);

            var file = System.IO.File.ReadAllBytes(apkPath);

            string fileName = _apkName;
            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
