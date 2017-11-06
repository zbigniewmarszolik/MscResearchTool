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
        private IIntegrationsBusiness _integrationsBusiness { get; set; }
        private IReportsRepository _reportsRepository { get; set; }

        public ReportsBusiness(IIntegrationsBusiness integrationsBusiness, IReportsRepository reportsRepository)
        {
            _integrationsBusiness = integrationsBusiness;
            _reportsRepository = reportsRepository;
        }

        public async Task GenerateIntegrationReportAsync(int fullIntegrationid)
        {
            var integrationsCollection = await _integrationsBusiness.ReadAllEagerAsync();
            var integrationToReport = integrationsCollection.Where(x => x.Id == fullIntegrationid).First();

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

        private async Task<Report> PrepareIntegrationReportAsync(Integration integration)
        {
            Report report = new Report();

            var generationDate = DateTime.Now;
            var title = "";

            if (integration.IsTrapezoidMethodRequested)
            {
                title = "Integration by trapezoids report for [" + integration.Id + "] ID";
            }

            else title = "Integration by squares report for [" + integration.Id + "] ID";

            var titleWithExtension = title + ".pdf";

            var bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            var titleStyle = new Font(bfTimes, 20, Font.BOLD);
            var headerStyle = new Font(bfTimes, 12, Font.BOLD);
            var standardStyle = new Font(bfTimes, 12, Font.NORMAL);

            await Task.Run(() =>
            {
                using (var memoryStream = new MemoryStream())
                {
                    var document = new Document();
                    PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    document.Add(new Paragraph(title, titleStyle));

                    document.Add(new Paragraph("Formula: " + integration.UnresolvedFormula, standardStyle));
                    document.Add(new Paragraph("Accuracy: " + integration.Accuracy, standardStyle));
                    document.Add(new Paragraph("Lower integration limit: " + integration.DownBoundary, standardStyle));
                    document.Add(new Paragraph("Upper integration limit: " + integration.UpBoundary, standardStyle));
                    document.Add(new Paragraph("Task creation date: " + integration.CreationDate, standardStyle));
                    document.Add(new Paragraph("Task completion date:  " + generationDate, standardStyle));
                    document.Add(new Paragraph("Amount of distributions for Android OS: " + integration.DroidIntervals, standardStyle));

                    document.Add(new Paragraph(" "));

                    document.Add(new Paragraph("Desktop results section:", headerStyle));

                    if (integration.IsResultNaN)
                        document.Add(new Paragraph("Result: NOT A NUMBER", standardStyle));

                    else document.Add(new Paragraph("Result: " + integration.FullResult, standardStyle));

                    document.Add(new Paragraph("Time [seconds]: " + integration.FullTime, standardStyle));
                    document.Add(new Paragraph("CPU info: " + integration.DesktopCPU, standardStyle));
                    document.Add(new Paragraph("RAM amount [MB]: " + integration.DesktopRAM, standardStyle));

                    document.Add(new Paragraph(" "));

                    document.Add(new Paragraph("Android results section:", headerStyle));

                    if (integration.Distributions.Any(x => x.IsResultNaN))
                        document.Add(new Paragraph("Result: NOT A NUMBER", standardStyle));

                    else document.Add(new Paragraph("Result: " + integration.PartialResult, standardStyle));

                    document.Add(new Paragraph("Time [seconds]: " + integration.PartialTime, standardStyle));

                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph("Detailed Android calculations for each device:", headerStyle));

                    for (var i = 1; i <= integration.Distributions.Count; i++)
                    {
                        if(integration.Distributions[i - 1].IsResultNaN)
                            document.Add(new Paragraph("[" + i + "] " + "device result: NOT A NUMBER", standardStyle));

                        else document.Add(new Paragraph("[" + i + "] " + "device result: " + integration.Distributions[i - 1].DeviceResult, standardStyle));

                        document.Add(new Paragraph("[" + i + "] " + "device time [seconds]: " + integration.Distributions[i - 1].DeviceTime, standardStyle));
                        document.Add(new Paragraph("[" + i + "] " + "device CPU info: " + integration.Distributions[i - 1].DeviceCPU, standardStyle));
                        document.Add(new Paragraph("[" + i + "] " + "device RAM amount [MB]: " + integration.Distributions[i - 1].DeviceRAM, standardStyle));
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
