using Android.App;
using Android.Content;
using System.Collections.Generic;

namespace MScResearchTool.Mobile.Droid.Helpers
{
    public class ProcessHelper
    {
        private readonly string _computingProcessName = "msc.research.tool.bgservice_process";

        public bool IsComputingServiceProcessRunning(Context context)
        {
            ActivityManager activityManager = (ActivityManager)context.GetSystemService(Context.ActivityService);

            IList<ActivityManager.RunningAppProcessInfo> informations = activityManager.RunningAppProcesses;

            if (informations != null)
            {
                foreach(var item in informations)
                {
                    if (item.ProcessName.Equals(_computingProcessName))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void KillComputingServiceProcess(Context context)
        {
            ActivityManager activityManager = (ActivityManager)context.GetSystemService(Context.ActivityService);

            IList<ActivityManager.RunningAppProcessInfo> informations = activityManager.RunningAppProcesses;

            if (informations != null)
            {
                foreach (var item in informations)
                {
                    if (item.ProcessName.Equals(_computingProcessName))
                    {
                        Android.OS.Process.KillProcess(item.Pid);
                    }
                }
            }
        }
    }
}