namespace MScResearchTool.Mobile.Android.UI.Menu.Contract
{
    public interface IMenuView
    {
        void StartService();
        void StopService();
        void EnableBackgroundButton();
        void DisableBackgroundButton();
        void AssignStartToButton();
        void AssignStopToButton();
        void StartManualControl();
        void CloseForeground();
        void Shutdown();
    }
}