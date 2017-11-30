namespace MScResearchTool.Mobile.Droid.UI.Menu.Contract
{
    public interface IMenuPresenter
    {
        void OnTakeView(IMenuView view);
        void StartButtonClicked();
        void StopButtonClicked();
        void ManualButtonClicked();
        void OnDestroy();
    }
}