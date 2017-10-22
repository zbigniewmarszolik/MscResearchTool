namespace MScResearchTool.Windows.Domain.ViewModels
{
    public interface IMainViewModel
    {
        ISelectionViewModel SelectionViewModel { get; set; }
        IStatusViewModel StatusViewModel { get; set; }
    }
}
