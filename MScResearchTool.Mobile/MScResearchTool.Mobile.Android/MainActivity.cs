using Android.App;
using Android.Content;
using Android.OS;
using Java.Lang;
using MScResearchTool.Mobile.Android.BackgroundServices;

namespace MScResearchTool.Mobile.Android
{
    [Activity(Label = "MSc Research Tool", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var dcService = new Intent(this, typeof(DistributedComputingService));
            StartService(dcService);
        }

        private void CheckForService()
        {
            ActivityManager activityManager = (ActivityManager)GetSystemService(ActivityService);
            foreach (var item in activityManager.GetRunningServices(Integer.MaxValue))
            {
                if (typeof(DistributedComputingService).Name.Equals(item.Service.ClassName))
                {
                    //change START TO STOP
                }
            }
        }

        private void AbortService()
        {
            ActivityManager activityManager = (ActivityManager)GetSystemService(ActivityService);
            activityManager.KillBackgroundProcesses(PackageName + "." + typeof(DistributedComputingService).Name);

            //change STOP to START
        }
    }
}

