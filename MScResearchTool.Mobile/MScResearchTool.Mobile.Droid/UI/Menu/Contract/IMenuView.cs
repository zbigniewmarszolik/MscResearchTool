namespace MScResearchTool.Mobile.Droid.UI.Menu.Contract
{
    public interface IMenuView
    {
        void StartService();
        void StopService();
        bool IsComputingProcessRunning();
        void EnableBackgroundButton();
        void DisableBackgroundButton();
        void AssignStartToButton();
        void AssignStopToButton();
        void StartManualControl();
        void CloseForeground();
    }
}