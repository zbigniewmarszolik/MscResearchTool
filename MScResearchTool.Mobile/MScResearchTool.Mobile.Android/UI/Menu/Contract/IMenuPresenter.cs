namespace MScResearchTool.Mobile.Android.UI.Menu.Contract
{
    public interface IMenuPresenter
    {
        void OnTakeView(IMenuView view);
        void StartButtonClicked();
        void StopButtonClicked();
        void ManualButtonClicked();
        void HideButtonClicked();
        void AbortApplication();
        void CheckForService();
        void OnDestroy();
    }
}