using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using MScResearchTool.Mobile.Droid.Attributes;
using MScResearchTool.Mobile.Droid.BackgroundServices;
using MScResearchTool.Mobile.Droid.Converters;
using MScResearchTool.Mobile.Droid.Enums;
using MScResearchTool.Mobile.Droid.Helpers;
using MScResearchTool.Mobile.Droid.UI.Manual;
using MScResearchTool.Mobile.Droid.UI.Menu.Contract;

namespace MScResearchTool.Mobile.Droid.UI.Menu
{
    [Activity(Theme = "@style/Theme.MasterRT", Label = "MSc Research Tool")]
    public class MenuActivity : ViewBase, IMenuView
    {
        [InjectDependency]
        private IMenuPresenter _presenter { get; set; }
        [InjectDependency]
        private ProcessHelper _processHelper { get; set; }
        [InjectDependency]
        private ButtonValuesConverter _buttonValuesConverter { get; set; }

        private Button _startStopButton { get; set; }
        private Button _manualButton { get; set; }

        private static string TAG = typeof(MenuActivity).Name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Log.Info(TAG, "OnCreate()");
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_menu);

            ViewComponentsInitialization();

            _presenter.OnTakeView(this);

            HuaweiProtectedSettings();
        }

        protected override void OnDestroy()
        {
            Log.Info(TAG, "OnDestroy()");
            _presenter.OnDestroy();
            base.OnDestroy();
        }

        protected override void ViewComponentsInitialization()
        {
            _startStopButton = FindViewById<Button>(Resource.Id.StartStopButton);
            _manualButton = FindViewById<Button>(Resource.Id.ManualButton);

            _startStopButton.Click += (sender, e) =>
            {
                if (_startStopButton.Text == _buttonValuesConverter.EnumeratorToString(EButtonValues.START))
                {
                    _presenter.StartButtonClicked();
                }

                else _presenter.StopButtonClicked();
            };

            _manualButton.Click += (sender, e) =>
            {
                _presenter.ManualButtonClicked();
            };
        }

        public override void OnBackPressed()
        {
            Log.Info(TAG, "OnBackPressed()");
            base.OnBackPressed();
        }

        public void RunService()
        {
            var dcService = new Intent(ApplicationContext, typeof(ComputingBackgroundService));
            StartService(dcService);
        }

        public void TerminateService()
        {
            var dcService = new Intent(ApplicationContext, typeof(ComputingBackgroundService));
            StopService(dcService);
        }

        public bool IsComputingProcessRunning()
        {
            if (_processHelper.IsComputingServiceProcessRunning(this))
                return true;

            else return false;
        }

        public void AssignStartToButton()
        {
            _startStopButton.Text = _buttonValuesConverter.EnumeratorToString(EButtonValues.START);
        }

        public void AssignStopToButton()
        {
            _startStopButton.Text = _buttonValuesConverter.EnumeratorToString(EButtonValues.STOP);
        }

        public void StartManualControl()
        {
            if (_processHelper.IsComputingServiceProcessRunning(this))
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this, Resource.Style.alertDialog);

                builder.SetMessage("The background computing service has to be stopped before turning on manual control. " +
                    "Stop the service and start manual control?");

                builder.SetPositiveButton("Yes", (sender, e) =>
                {
                    TerminateService();
                    AssignStartToButton();
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

        private void HuaweiProtectedSettings()
        {
            var sharedPreferences = GetSharedPreferences("protected", FileCreationMode.Private);

            if ("huawei".Equals(Build.Manufacturer, System.StringComparison.CurrentCultureIgnoreCase) && !sharedPreferences.GetBoolean("protected", false))
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this, Resource.Style.alertDialog);

                builder.SetMessage("This application requires to be enabled in Protected Applications to work properly. Do you want to enable it now?");

                builder.SetPositiveButton("Yes", (sender, e) =>
                {
                    var intent = new Intent();
                    intent.SetComponent(new ComponentName("com.huawei.systemmanager", "com.huawei.systemmanager.optimize.process.ProtectActivity"));
                    StartActivity(intent);
                    sharedPreferences.Edit().PutBoolean("protected", true).Commit();
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
}