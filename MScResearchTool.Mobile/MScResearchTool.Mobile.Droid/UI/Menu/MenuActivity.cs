using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Autofac;
using Java.Lang;
using MScResearchTool.Mobile.Droid.BackgroundServices;
using MScResearchTool.Mobile.Droid.Helpers;
using MScResearchTool.Mobile.Droid.Types;
using MScResearchTool.Mobile.Droid.UI.Manual;
using MScResearchTool.Mobile.Droid.UI.Menu.Contract;

namespace MScResearchTool.Mobile.Droid.UI.Menu
{
    [Activity(Theme = "@style/Theme.MasterRT", Label = "MSc Research Tool")]
    public class MenuActivity : ViewBase, IMenuView
    {
        private IMenuPresenter _presenter { get; set; }
        private ProcessHelper _processHelper { get; set; }

        private Button _startStopButton { get; set; }
        private Button _manualButton { get; set; }
        private Button _hideButton { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_menu);

            ViewComponentsInitialization();

            _processHelper = Container.Resolve<ProcessHelper>();

            _presenter = Container.Resolve<IMenuPresenter>();
            _presenter.OnTakeView(this);
        }

        protected override void OnDestroy()
        {
            _presenter.OnDestroy();
            base.OnDestroy();
        }

        protected override void ViewComponentsInitialization()
        {
            _startStopButton = FindViewById<Button>(Resource.Id.StartStopButton);
            _manualButton = FindViewById<Button>(Resource.Id.ManualButton);
            _hideButton = FindViewById<Button>(Resource.Id.HideButton);

            _startStopButton.Click += (sender, e) =>
            {
                if (_startStopButton.Text == EButtonValues.START.ToString())
                {
                    _presenter.StartButtonClicked();
                }

                else _presenter.StopButtonClicked();
            };

            _manualButton.Click += (sender, e) =>
            {
                _presenter.ManualButtonClicked();
            };

            _hideButton.Click += (sender, e) =>
            {
                _presenter.HideButtonClicked();
            };
        }

        public override void OnBackPressed()
        {
            AbortApplicationForeground();
        }

        public void StartService()
        {
            var dcService = new Intent(this, typeof(ComputingBackgroundService));
            StartService(dcService);
        }

        public void StopService()
        {
            _processHelper.KillComputingServiceProcess(this);
        }

        public bool IsComputingProcessRunning()
        {
            if (_processHelper.IsComputingServiceProcessRunning(this))
                return true;

            else return false;
        }

        public void EnableBackgroundButton()
        {
            _hideButton.Enabled = true;
        }

        public void DisableBackgroundButton()
        {
            _hideButton.Enabled = false;
        }

        public void AssignStartToButton()
        {
            _startStopButton.Text = EButtonValues.START.ToString();
        }

        public void AssignStopToButton()
        {
            _startStopButton.Text = EButtonValues.STOP.ToString();
        }

        public void StartManualControl()
        {
            if(_processHelper.IsComputingServiceProcessRunning(this))
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this, Resource.Style.alertDialog);

                builder.SetMessage("The background computing service has to be stopped before turning on manual control. " +
                    "Stop the service and start manual control?");

                builder.SetPositiveButton("Yes", (sender, e) =>
                {
                    _processHelper.KillComputingServiceProcess(this);
                    StartActivity(typeof(ManualActivity));
                });

                builder.SetNegativeButton("No", (sender, e) =>
                {
                    return;
                });

                Dialog dialogBox = builder.Create();
                dialogBox.Show();
            }

            else StartActivity(typeof(ManualActivity));
        }

        public void CloseForeground()
        {
            AbortApplicationForeground();
        }

        private void AbortApplicationForeground()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this, Resource.Style.alertDialog);

            builder.SetMessage("Are you sure to allow the application to run as background process? " +
                "It may keep consuming energy and computing resources.");

            builder.SetPositiveButton("Yes", (sender, e) =>
            {
                JavaSystem.Exit(0);
            });

            builder.SetNegativeButton("No", (sender, e) =>
            {
                return;
            });

            Dialog dialogBox = builder.Create();
            dialogBox.Show();
        }
    }
}