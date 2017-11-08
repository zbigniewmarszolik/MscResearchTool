using Microsoft.VisualBasic.Devices;
using System.Management;

namespace MScResearchTool.Windows.BusinessLogic.Helpers
{
    public class HardwareInfoHelper
    {
        public string GetCPUDetails()
        {
            uint processorClock = 0;
            string processorName = "";

            var searcher = new ManagementObjectSearcher("select MaxClockSpeed, Name from Win32_Processor");
            foreach (var item in searcher.Get())
            {
                processorClock = (uint)item["MaxClockSpeed"];
                processorName = item["Name"].ToString();
            }

            processorName = processorName.TrimEnd();

            var procInfo = processorName + " @ " + processorClock.ToString() + " MHz";

            return procInfo;
        }

        public int GetRAMAmountInMB()
        {
            var testRam = new ComputerInfo().TotalPhysicalMemory / 1024 / 1024;

            return (int)testRam;
        }
    }
}
