namespace MScResearchTool.Mobile.Droid.UI.Manual.Contract
{
    public interface IManualPresenter
    {
        void OnTakeView(IManualView view);
        void RestartFlow();
        void IntegrateButtonClicked();
        void ReconnectButtonClicked();
        void OnDestroy();
    }
}