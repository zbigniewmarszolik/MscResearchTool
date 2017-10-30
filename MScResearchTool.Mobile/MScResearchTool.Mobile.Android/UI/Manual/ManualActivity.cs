using Android.App;
using Android.OS;
using MScResearchTool.Mobile.Android.UI.Manual.Contract;
using Autofac;

namespace MScResearchTool.Mobile.Android.UI.Manual
{
    [Activity(Theme = "@style/Theme.MasterRT", Label = "MSc RT Hand Control")]
    public class ManualActivity : ViewBase, IManualView
    {
        private IManualPresenter _presenter { get; set; }

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
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}