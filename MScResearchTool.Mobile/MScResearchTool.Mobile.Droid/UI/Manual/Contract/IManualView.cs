namespace MScResearchTool.Mobile.Droid.UI.Manual.Contract
{
    public interface IManualView
    {
        void DisableAllButtons();
        void EnableCracking();
        void EnableIntegration();
        void EnableReconnect();
        void EnableProgressBar();
        void DisableProgressBar();
        void ShowResult(string result, double seconds);
        void ShowServerError(string errorMessage);
    }
}