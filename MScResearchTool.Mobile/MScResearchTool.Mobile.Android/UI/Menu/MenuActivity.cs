using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Autofac;
using Java.Lang;
using MScResearchTool.Mobile.Android.BackgroundServices;
using MScResearchTool.Mobile.Android.Types;
using MScResearchTool.Mobile.Android.UI.Manual;
using MScResearchTool.Mobile.Android.UI.Menu.Contract;

namespace MScResearchTool.Mobile.Android.UI.Menu
{
    [Activity(Theme = "@style/Theme.MasterRT", Label = "MSc Research Tool")]
    public class MenuActivity : ViewBase, IMenuView
    {
        private IMenuPresenter _presenter { get; set; }

        private Button _startStopButton { get; set; }
        private Button _manualButton { get; set; }
        private Button _hideButton { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_menu);

            ViewComponentsInitialization();

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

            _hideButton.Enabled = false;

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
            _presenter.AbortApplication();
        }

        public void StartService()
        {
            var dcService = new Intent(this, typeof(DistributedComputingService));
            StartService(dcService);
        }

        public void StopService()
        {
            //ActivityManager activityManager = (ActivityManager)GetSystemService(ActivityService);
            //activityManager.KillBackgroundProcesses(PackageName + "." + typeof(DistributedComputingService).Name);

            ApplicationContext.StopService(new Intent(this, typeof(DistributedComputingService)));
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
            StartActivity(typeof(ManualActivity));
        }

        public void CloseForeground()
        {
            Finish();
        }

        public void Shutdown()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this, Resource.Style.alertDialog);

            builder.SetMessage("Are you sure to definitely close the application and its running background processes?");

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