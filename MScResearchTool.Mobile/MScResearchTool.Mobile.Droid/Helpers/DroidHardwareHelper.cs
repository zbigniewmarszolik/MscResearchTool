using Android.OS;
using Android.Util;
using Java.IO;
using Java.Lang;
using Java.Text;
using Java.Util.Regex;

namespace MScResearchTool.Mobile.Droid.Helpers
{
    public class DroidHardwareHelper
    {
        private static string TAG = typeof(DroidHardwareHelper).Name;

        private string _cpuInfPath = "/sys/devices/system/cpu/cpu0/cpufreq/cpuinfo_max_freq";
        private string _ramInfoPath = "/proc/meminfo";

        public string GetProcessorInfo()
        {
            StringBuffer sb = new StringBuffer();
            sb.Append(Build.CpuAbi).Append(" @ ");
            if (new File(_cpuInfPath).Exists())
            {
                try
                {
                    BufferedReader br = new BufferedReader(new FileReader(new File(_cpuInfPath)));
                    string aLine;
                    while ((aLine = br.ReadLine()) != null)
                    {
                        var numericalValue = Integer.ParseInt(aLine);
                        var mhzValue = numericalValue / 1000;
                        sb.Append(mhzValue.ToString() + " MHz");
                    }
                    if (br != null)
                    {
                        br.Close();
                    }
                }
                catch (IOException e)
                {
                    Log.Info(TAG, e.Message);
                }
            }
            return sb.ToString();
        }

        public int GetMemoryAmount()
        {
            RandomAccessFile reader = null;
            String load = null;
            DecimalFormat twoDecimalForm = new DecimalFormat("#.##");
            var totalRam = 0.0;
            var mb = 0.0;
            try
            {
                reader = new RandomAccessFile(_ramInfoPath, "r");
                load = new String(reader.ReadLine());

                Java.Util.Regex.Pattern p = Java.Util.Regex.Pattern.Compile("(\\d+)");
                Matcher m = p.Matcher(load);
                String value = null;
                while (m.Find())
                {
                    value = new String(m.Group(1));
                }
                reader.Close();

                totalRam = Double.ParseDouble(value.ToString());

                mb = totalRam / 1024.0;
            }
            catch (Exception e)
            {
                Log.Info(TAG, e.Message);
            }

            return (int)mb;
        }
    }
}