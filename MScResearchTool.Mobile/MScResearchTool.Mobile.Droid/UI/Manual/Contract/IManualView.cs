namespace MScResearchTool.Mobile.Droid.UI.Manual.Contract
{
    public interface IManualView
    {
        void DisableAllButtons();
        void EnableIntegration();
        void EnableReconnect();
        void EnableProgressBar();
        void DisableProgressBar();
        void ShowResult(double result, double seconds);
    }
}