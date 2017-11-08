using Android.App;
using Android.OS;
using MScResearchTool.Mobile.Droid.UI.Menu;

namespace MScResearchTool.Mobile.Droid.UI.Splash
{
    [Activity(Theme = "@style/Theme.Splash", Label = "MSc Research Tool Launcher", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(typeof(MenuActivity));
        }
    }
}