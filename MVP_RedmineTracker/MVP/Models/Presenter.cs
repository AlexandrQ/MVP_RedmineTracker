using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedmineTracker.Interfaces;


namespace RedmineTracker.MVP
{
    class Presenter : IPresenter
    {        
        private readonly IMainForm _mainView;
        private readonly IModel _storage;
        public Presenter(IMainForm v1, IModel s)
        {
            _mainView = v1;            
            _storage = s;
            
            _mainView.ShowIssues += () => ShowMyIss();
            _mainView.Initialize += () => RunTimer();            
        }        

        private void ShowMyIss()
        {
            _storage.getMyIssues();
        }

        private void RunTimer()
        {
            _storage.RunTimer();
        }


        #region Presenter
        private void CancelAction(IView sender)
        {
            sender.CloseView();
        }
       
        public void Run()
        {
            _mainView.OpenView();            
        }
        #endregion Presenter

    }
}
