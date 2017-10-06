using Android.App;
using Android.OS;

namespace MScResearchTool.Mobile.Android
{
    [Activity(Label = "MScResearchTool.Mobile.Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

