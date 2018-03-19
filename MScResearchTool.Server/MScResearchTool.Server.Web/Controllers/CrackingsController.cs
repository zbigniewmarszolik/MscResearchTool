using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MScResearchTool.Server.Core.Businesses;
using MScResearchTool.Server.Core.Enums;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Web.Builders;
using MScResearchTool.Server.Web.Helpers;
using MScResearchTool.Server.Web.ViewModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MScResearchTool.Server.Web.Controllers
{
    [Authorize]
    public class CrackingsController : Controller
    {
        private ICrackingsBusiness _crackingsBusiness { get; set; }
        private ICrackingDistributionsBusiness _crackingDistributionsBusiness { get; set; }
        private ICrackingResultsBusiness _crackingResultsBusiness { get; set; }
        private CrackingInitializationHelper _crackingInitializationHelper { get; set; }

        public CrackingsController
            (ICrackingsBusiness crackingsBusiness,
            ICrackingDistributionsBusiness crackingDistributionsBusiness,
            ICrackingResultsBusiness crackingResultsBusiness,
            CrackingInitializationHelper crackingInitializationHelper)
        {
            _crackingsBusiness = crackingsBusiness;
            _crackingDistributionsBusiness = crackingDistributionsBusiness;
            _crackingResultsBusiness = crackingResultsBusiness;
            _crackingInitializationHelper = crackingInitializationHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CrackingViewModel crackingVm, IFormFile archive, string submitter)
        {
            ViewData["InputError"] = null;

            if (crackingVm.RangesCount < 2)
            {
                ViewData["InputError"] = "Number of ranges must be at least 2 or greater.";
                return View();
            }

            if (archive == null)
            {
                ViewData["InputError"] = "Archive file cannot be null.";
                return View();
            }

            var fileAsArray = _crackingInitializationHelper.ProcessFormFileToArray(archive);

            var isArchiveUnzippable = _crackingInitializationHelper.IsArchiveExtractable(fileAsArray, crackingVm.ArchivePassword);

            if (isArchiveUnzippable == false)
            {
                ViewData["InputError"] = "Wrong file or password provided. It can not be used. Please try again.";
                return View();
            }

            var builder = new CrackingBuilder();
            builder.NumberOfDistributions = crackingVm.RangesCount;
            builder.Password = crackingVm.ArchivePassword;
            builder.FileName = Path.GetFileNameWithoutExtension(archive.FileName);
            builder.ArchiveContent = fileAsArray;
            builder.CharactersDictionary = CrackingCharacters.Instance().Characters;
            builder.SerializedArchiveContent = _crackingInitializationHelper.SerializedContent;

            var cracking = builder.Build();

            await _crackingsBusiness.DistributeAndPersistAsync(cracking);

            switch (submitter)
            {
                case "Add single integration task":
                    return RedirectToAction("Creation", "Tasks");

                case "Add integration task and prepare next":
                    return View();

                default: return RedirectToAction("Creation", "Tasks");
            }
        }

        [AllowAnonymous]
        [Route("Api/PostCrackingResult/{mode}")]
        [HttpPut]
        public async Task<IActionResult> PostCrackingResult([FromBody]CrackingResult cracked)
        {
            await _crackingResultsBusiness.ProcessResultAsync(cracked);

            return Ok();
        }

        [AllowAnonymous]
        [Route("Api/GetCracking/{mode}")]
        [HttpGet]
        public async Task<IActionResult> GetCracking(string mode)
        {
            if (mode == ECalculationMode.Single.ToString())
            {
                var dto = await GetFullTask();

                if (dto != null)
                {
                    dto.IsAvailable = false;
                    await _crackingsBusiness.UpdateAsync(dto);
                }

                return Ok(dto);
            }

            else if (mode == ECalculationMode.Distributed.ToString())
            {
                var dto = await GetDistributedTask();

                if(dto != null)
                {
                    dto.IsAvailable = false;
                    await _crackingDistributionsBusiness.UpdateAsync(dto);
                }

                dto.Task = null;

                return Ok(dto);
            }

            else return Ok(null);
        }

        private async Task<Cracking> GetFullTask()
        {
            var result = await _crackingsBusiness.ReadAvailableAsync();

            return result.FirstOrDefault();
        }

        private async Task<CrackingDistribution> GetDistributedTask()
        {
            var result = await _crackingDistributionsBusiness.ReadAvailableAsync();

            return result.FirstOrDefault();
        }
    }
}
