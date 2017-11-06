﻿using MScResearchTool.Mobile.Android.UI.Menu.Contract;

namespace MScResearchTool.Mobile.Android.UI.Menu
{
    public class MenuPresenter : IMenuPresenter
    {
        private IMenuView _view { get; set; }

        public void OnTakeView(IMenuView view)
        {
            _view = view;
        } 

        public void StartButtonClicked()
        {
            _view.StartService();
            _view.AssignStopToButton();
            _view.EnableBackgroundButton();
        }

        public void StopButtonClicked()
        {
            _view.StopService();
            _view.AssignStartToButton();
            _view.DisableBackgroundButton();
        }

        public void ManualButtonClicked()
        {
            _view.StartManualControl();
        }

        public void HideButtonClicked()
        {
            _view.CloseForeground();
        }

        public void AbortApplication()
        {
            _view.Shutdown();
        }

        public void OnDestroy()
        {
            _view = null;
        }
    }
}