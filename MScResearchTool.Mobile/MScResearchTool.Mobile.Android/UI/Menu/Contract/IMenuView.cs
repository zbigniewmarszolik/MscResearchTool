namespace MScResearchTool.Mobile.Android.UI.Menu.Contract
{
    public interface IMenuView
    {
        void StartService();
        void StopService();
        bool IsDistributedComputingRunning();
        void AssignStartToButton();
        void AssignStopToButton();
        void StartManualControl();
        void CloseForeground();
        void Shutdown();
    }
}