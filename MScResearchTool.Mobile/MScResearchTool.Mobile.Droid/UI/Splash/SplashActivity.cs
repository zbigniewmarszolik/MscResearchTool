﻿using Android.App;
using Android.OS;
using Android.Util;
using MScResearchTool.Mobile.Droid.UI.Menu;

namespace MScResearchTool.Mobile.Droid.UI.Splash
{
    [Activity(Theme = "@style/Theme.Splash", Icon = "@drawable/icon", Label = "MSc Research Tool", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        private static string TAG = typeof(SplashActivity).Name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Log.Info(TAG, "StartActivity(typeof(MenuActivity))");

            StartActivity(typeof(MenuActivity));
        }
    }
}