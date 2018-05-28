using System.Threading.Tasks;
using MScResearchTool.Server.Core.Businesses;
using System.Linq;
using iTextSharp.text;
using System.IO;
using MScResearchTool.Server.Core.Models;
using MScResearchTool.Server.Core.Repositories;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;

namespace MScResearchTool.Server.BusinessLogic.Businesses
{
    public class ReportsBusiness : IReportsBusiness
    {
        private ICrackingsBusiness _crackingsBusiness { get; set; }
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private IReportsRepository _reportsRepository { get; set; }
        private BaseFont _baseFont { get; set; }
        private Font _titleStyle { get; set; }
        private Font _headerStyle { get; set; }
        private Font _bodyStyle { get; set; }

        public ReportsBusiness
            (ICrackingsBusiness crackingsBusiness,
            IIntegrationsBusiness integrationsBusiness,
            IReportsRepository reportsRepository)
        {
            _crackingsBusiness = crackingsBusiness;
            _integrationsBusiness = integrationsBusiness;
            _reportsRepository = reportsRepository;

            _baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            _titleStyle = new Font(_baseFont, 20, Font.BOLD);
            _headerStyle = new Font(_baseFont, 12, Font.BOLD);
            _bodyStyle = new Font(_baseFont, 12, Font.NORMAL);
        }

        public async Task GenerateCrackingReportAsync(int fullCrackingId)
        {
            var crackingsCollection = await _crackingsBusiness.ReadAllEagerAsync();
            var crackingToReport = crackingsCollection.Where(x => x.Id == fullCrackingId).First();

            var report = await PrepareCrackingReportAsync(crackingToReport);

            await Task.Run(() =>
            {
                _reportsRepository.Create(report);
            });
        }

        public async Task GenerateIntegrationReportAsync(int fullIntegrationId)
        {
            var integrationsCollection = await _integrationsBusiness.ReadAllEagerAsync();
            var integrationToReport = integrationsCollection.Where(x => x.Id == fullIntegrationId).First();

            var report = await PrepareIntegrationReportAsync(integrationToReport);

            await Task.Run(() =>
            {
                _reportsRepository.Create(report);
            });
        }

        public async Task<IList<Report>> ReadAllAsync()
        {
            return await ReadReportsAsync();
        }

        public async Task<Report> ReadByIdAsync(int reportId)
        {
            var reports = await ReadReportsAsync();
            var report = reports.First(x => x.Id == reportId);

            return report;
        }

        public async Task DeleteAsync(int reportId)
        {
            await Task.Run(() =>
            {
                _reportsRepository.Delete(reportId);
            });
        }

        private async Task<Report> PrepareCrackingReportAsync(Cracking cracking)
        {
            var report = new Report();

            var generationDate = DateTime.Now;
            var title = "Brute-force password breaking report for [" + cracking.Id + "] ID";
            var titleWithExtension = title + ".pdf";

            var founderBatteryUsage = cracking.Distributions.First(x => x.IsFounder).BatteryUsage;

            await Task.Run(() =>
            {
                using (var memoryStream = new MemoryStream())
                {
                    var document = new Document();
                    PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    document.Add(new Paragraph(title, _titleStyle));

                    document.Add(new Paragraph("ZIP archive name: " + cracking.FileName, _bodyStyle));
                    document.Add(new Paragraph("ZIP archive password: " + cracking.ArchivePassword, _bodyStyle));
                    document.Add(new Paragraph("Task creation date: " + cracking.CreationDate, _bodyStyle));
                    document.Add(new Paragraph("Task completion date:  " + generationDate, _bodyStyle));
                    document.Add(new Paragraph("Amount of distributions for Android OS: " + cracking.DroidRanges, _bodyStyle));

                    document.Add(new Paragraph(" "));

                    document.Add(new Paragraph("Desktop results section:", _headerStyle));

                    document.Add(new Paragraph("Time [seconds]: " + cracking.FullTime, _bodyStyle));
                    document.Add(new Paragraph("CPU info: " + cracking.DesktopCPU, _bodyStyle));
                    document.Add(new Paragraph("RAM amount [MB]: " + cracking.DesktopRAM, _bodyStyle));

                    document.Add(new Paragraph(" "));

                    document.Add(new Paragraph("Android results section:", _headerStyle));

                    document.Add(new Paragraph("Time [seconds]: " + cracking.PartialTime, _bodyStyle));
                    document.Add(new Paragraph("Device CPU info: " + cracking.Distributions.First(x => x.IsFounder).DeviceCPU, _bodyStyle));
                    document.Add(new Paragraph("Device RAM amount [MB]: " + cracking.Distributions.First(x => x.IsFounder).DeviceRAM, _bodyStyle));
                    document.Add(new Paragraph("Battery usage [%]: " + founderBatteryUsage, _bodyStyle));

                    document.Close();

                    report.ContentPdf = memoryStream.ToArray();
                }

                report.Title = titleWithExtension;
                report.GenerationDate = generationDate;
            });

            return report;
        }

        private async Task<Report> PrepareIntegrationReportAsync(Integration integration)
        {
            var report = new Report();

            var generationDate = DateTime.Now;
            var title = "";

            if (integration.IsTrapezoidMethodRequested)
            {
                title = "Integration by trapezoids report for [" + integration.Id + "] ID";
            }

            else title = "Integration by squares report for [" + integration.Id + "] ID";

            var titleWithExtension = title + ".pdf";

            await Task.Run(() =>
            {
                using (var memoryStream = new MemoryStream())
                {
                    var document = new Document();
                    PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    document.Add(new Paragraph(title, _titleStyle));

                    document.Add(new Paragraph("Formula: " + integration.UnresolvedFormula, _bodyStyle));
                    document.Add(new Paragraph("Accuracy: " + integration.Accuracy, _bodyStyle));
                    document.Add(new Paragraph("Lower integration limit: " + integration.DownBoundary, _bodyStyle));
                    document.Add(new Paragraph("Upper integration limit: " + integration.UpBoundary, _bodyStyle));
                    document.Add(new Paragraph("Task creation date: " + integration.CreationDate, _bodyStyle));
                    document.Add(new Paragraph("Task completion date:  " + generationDate, _bodyStyle));
                    document.Add(new Paragraph("Amount of distributions for Android OS: " + integration.DroidIntervals, _bodyStyle));

                    document.Add(new Paragraph(" "));

                    document.Add(new Paragraph("Desktop results section:", _headerStyle));

                    if (integration.IsResultNaN)
                        document.Add(new Paragraph("Result: NOT A NUMBER", _bodyStyle));

                    else document.Add(new Paragraph("Result: " + integration.FullResult, _bodyStyle));

                    document.Add(new Paragraph("Time [seconds]: " + integration.FullTime, _bodyStyle));
                    document.Add(new Paragraph("CPU info: " + integration.DesktopCPU, _bodyStyle));
                    document.Add(new Paragraph("RAM amount [MB]: " + integration.DesktopRAM, _bodyStyle));

                    document.Add(new Paragraph(" "));

                    document.Add(new Paragraph("Android results section:", _headerStyle));

                    if (integration.Distributions.Any(x => x.IsResultNaN))
                        document.Add(new Paragraph("Result: NOT A NUMBER", _bodyStyle));

                    else document.Add(new Paragraph("Result: " + integration.PartialResult, _bodyStyle));

                    document.Add(new Paragraph("Time [seconds]: " + integration.PartialTime, _bodyStyle));

                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph("Detailed Android calculations for each device:", _headerStyle));

                    for (var i = 1; i <= integration.Distributions.Count; i++)
                    {
                        if (integration.Distributions[i - 1].IsResultNaN)
                            document.Add(new Paragraph("[" + i + "] " + "device result: NOT A NUMBER", _bodyStyle));

                        else document.Add(new Paragraph("[" + i + "] " + "device result: " + integration.Distributions[i - 1].DeviceResult, _bodyStyle));

                        document.Add(new Paragraph("[" + i + "] " + "device time [seconds]: " + integration.Distributions[i - 1].DeviceTime, _bodyStyle));
                        document.Add(new Paragraph("[" + i + "] " + "device CPU info: " + integration.Distributions[i - 1].DeviceCPU, _bodyStyle));
                        document.Add(new Paragraph("[" + i + "] " + "device RAM amount [MB]: " + integration.Distributions[i - 1].DeviceRAM, _bodyStyle));
                        document.Add(new Paragraph("[" + i + "] " + "battery usage [%]: " + integration.Distributions[i - 1].BatteryUsage, _bodyStyle));
                    }

                    document.Close();

                    report.ContentPdf = memoryStream.ToArray();
                }

                report.Title = titleWithExtension;
                report.GenerationDate = generationDate;
            });

            return report;
        }

        private async Task<IList<Report>> ReadReportsAsync()
        {
            IList<Report> reports = null;

            await Task.Run(() =>
            {
                reports = _reportsRepository.Read();
            });

            return reports;
        }
    }
}
