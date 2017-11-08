namespace MScResearchTool.Windows.Domain.ViewModels
{
    public interface IStatusViewModel
    {
        string StatusText { get; set; }
        bool IsBusy { get; set; }
    }
}
