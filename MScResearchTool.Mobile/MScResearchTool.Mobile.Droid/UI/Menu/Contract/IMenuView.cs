namespace MScResearchTool.Mobile.Droid.UI.Menu.Contract
{
    public interface IMenuView
    {
        void RunService();
        void TerminateService();
        bool IsComputingProcessRunning();
        void AssignStartToButton();
        void AssignStopToButton();
        void StartManualControl();
    }
}