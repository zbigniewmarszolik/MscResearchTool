using MScResearchTool.Mobile.Droid.UI.Menu.Contract;

namespace MScResearchTool.Mobile.Droid.UI.Menu
{
    public class MenuPresenter : IMenuPresenter
    {
        private IMenuView _view { get; set; }

        public void OnTakeView(IMenuView view)
        {
            _view = view;

            _view.DisableBackgroundButton();

            HandleComputingService();
        }

        public void StartButtonClicked()
        {
            _view.StartService();
            _view.AssignStopToButton();
            _view.EnableBackgroundButton();
        }

        public void StopButtonClicked()
        {
            _view.StopService();
            _view.AssignStartToButton();
            _view.DisableBackgroundButton();
        }

        public void ManualButtonClicked()
        {
            _view.StartManualControl();
        }

        public void HideButtonClicked()
        {
            _view.CloseForeground();
        }

        public void OnDestroy()
        {
            _view = null;
        }

        private void HandleComputingService()
        {
            var isInProgress = _view.IsComputingProcessRunning();

            if (isInProgress)
            {
                _view.AssignStopToButton();
                _view.EnableBackgroundButton();
            }

            else _view.DisableBackgroundButton();  
        }
    }
}