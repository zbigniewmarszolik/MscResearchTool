using MScResearchTool.Mobile.Droid.UI.Menu.Contract;

namespace MScResearchTool.Mobile.Droid.UI.Menu
{
    public class MenuPresenter : IMenuPresenter
    {
        private IMenuView _view { get; set; }

        public void OnTakeView(IMenuView view)
        {
            _view = view;

            HandleComputingService();
        }

        public void StartButtonClicked()
        {
            _view.RunService();
            _view.AssignStopToButton();
        }

        public void StopButtonClicked()
        {
            _view.TerminateService();
            _view.AssignStartToButton();
        }

        public void ManualButtonClicked()
        {
            _view.StartManualControl();
        }

        public void OnDestroy()
        {
            _view = null;
        }

        private void HandleComputingService()
        {
            var isInProgress = _view.IsComputingProcessRunning();

            if (isInProgress)
                _view.AssignStopToButton();

            else _view.AssignStartToButton();
        }
    }
}