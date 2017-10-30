namespace MScResearchTool.Mobile.Android.UI.Manual.Contract
{
    public interface IManualPresenter
    {
        void OnTakeView(IManualView view);
        void OnDestroy();
    }
}