using System;

namespace MScResearchTool.Windows.Domain.ViewModels
{
    public interface ILoadableWindowViewModel
    {
        Action WindowLoaded { get; set; }
    }
}
