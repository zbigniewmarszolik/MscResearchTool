using Android.App;
using Android.OS;
using Android.Widget;
using MScResearchTool.Mobile.Droid.UI.Manual.Contract;
using Android.Views;
using MScResearchTool.Mobile.Droid.Attributes;
using Android.Util;

namespace MScResearchTool.Mobile.Droid.UI.Manual
{
    [Activity(Theme = "@style/Theme.MasterRT", Label = "MSc Research Tool")]
    public class ManualActivity : ViewBase, IManualView
    {
        [InjectDependency]
        private IManualPresenter _presenter { get; set; }

        private Button _crackButton { get; set; }
        private Button _integrateButton { get; set; }
        private Button _reconnectButton { get; set; }
        private ProgressBar _progressBar { get; set; }

        private static string TAG = typeof(ManualActivity).Name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Log.Info(TAG, "OnCreate()");
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_manual);

            ViewComponentsInitialization();

            _presenter.OnTakeView(this);
        }

        protected override void OnDestroy()
        {
            Log.Info(TAG, "OnDestroy()");
            base.OnDestroy();
        }

        protected override void ViewComponentsInitialization()
        {
            _crackButton = FindViewById<Button>(Resource.Id.CrackButton);
            _integrateButton = FindViewById<Button>(Resource.Id.IntegrateButton);
            _reconnectButton = FindViewById<Button>(Resource.Id.ReconnectButton);
            _progressBar = FindViewById<ProgressBar>(Resource.Id.MainProgressBar);

            _crackButton.Click += (sender, e) =>
            {
                _presenter.CrackButtonClicked();
            };

            _integrateButton.Click += (sender, e) =>
            {
                _presenter.IntegrateButtonClicked();
            };

            _reconnectButton.Click += (sender, e) =>
            {
                _presenter.ReconnectButtonClicked();
            };
        }

        public override void OnBackPressed()
        {
            Log.Info(TAG, "OnBackPressed()");
            base.OnBackPressed();
        }

        public void DisableAllButtons()
        {
            _crackButton.Enabled = false;
            _integrateButton.Enabled = false;
            _reconnectButton.Enabled = false;
        }

        public void EnableCracking()
        {
            _crackButton.Enabled = true;
        }

        public void EnableIntegration()
        {
            _integrateButton.Enabled = true;
        }

        public void EnableReconnect()
        {
            _reconnectButton.Enabled = true;
        }

        public void EnableProgressBar()
        {
            _progressBar.Visibility = ViewStates.Visible;
        }

        public void DisableProgressBar()
        {
            _progressBar.Visibility = ViewStates.Invisible;
        }

        public void ShowResult(string result, double seconds)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this, Resource.Style.alertDialog);

            builder.SetMessage("Task distribution finished with following result: " + result.ToString() + ". Elapsed time: " + seconds.ToString() + " seconds. ");

            builder.SetNeutralButton("OK", (sender, e) =>
            {
                _presenter.RestartFlow();
            });

            Dialog dialogBox = builder.Create();
            dialogBox.Show();
        }

        public void ShowServerError(string errorMessage)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this, Resource.Style.alertDialog);

            builder.SetMessage(errorMessage);

            builder.SetNeutralButton("OK", (sender, e) =>
            {
                EnableReconnect();
            });

            Dialog dialogBox = builder.Create();
            dialogBox.Show();
        }
    }
}