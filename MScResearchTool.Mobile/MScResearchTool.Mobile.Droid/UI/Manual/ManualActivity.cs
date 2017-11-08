using Android.App;
using Android.OS;
using Android.Widget;
using MScResearchTool.Mobile.Droid.UI.Manual.Contract;
using Autofac;
using Android.Views;

namespace MScResearchTool.Mobile.Droid.UI.Manual
{
    [Activity(Theme = "@style/Theme.MasterRT", Label = "MSc Research Tool")]
    public class ManualActivity : ViewBase, IManualView
    {
        private IManualPresenter _presenter { get; set; }

        private Button _integrateButton { get; set; }
        private Button _reconnectButton { get; set; }
        private ProgressBar _progressBar { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_manual);

            ViewComponentsInitialization();

            _presenter = Container.Resolve<IManualPresenter>();
            _presenter.OnTakeView(this);
        }

        protected override void ViewComponentsInitialization()
        {
            _integrateButton = FindViewById<Button>(Resource.Id.IntegrateButton);
            _reconnectButton = FindViewById<Button>(Resource.Id.ReconnectButton);
            _progressBar = FindViewById<ProgressBar>(Resource.Id.MainProgressBar);

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
            base.OnBackPressed();
        }

        public void DisableAllButtons()
        {
            _integrateButton.Enabled = false;
            _reconnectButton.Enabled = false;
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

        public void ShowResult(double result, double seconds)
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
    }
}