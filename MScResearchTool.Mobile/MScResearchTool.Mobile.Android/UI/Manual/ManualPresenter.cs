using MScResearchTool.Mobile.Android.UI.Manual.Contract;

namespace MScResearchTool.Mobile.Android.UI.Manual
{
    public class ManualPresenter : IManualPresenter
    {
        private IManualView _view { get; set; }

        public void OnTakeView(IManualView view)
        {
            _view = view;
        }

        public void OnDestroy()
        {
            _view = null;
        }
    }
}