using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedmineTracker.Interfaces;

using MVP_RedmineTracker.MVP.Forms;


namespace RedmineTracker.MVP
{
    class Presenter : IPresenter
    {        
        private readonly IMainForm _mainView;
        private readonly IProjectForm _projectView;
        private readonly IModel _storage;
        private INewIssueForm _newIssueView;
        private IJournalsForm _journalsView;



        public Presenter(IMainForm v1, IProjectForm v2, IModel s)
        {
            _mainView = v1;
            _projectView = v2;
            _storage = s;
            
            
            _mainView.ShowIssues += () => ShowMyIss();
            _mainView.Initialize += () => getOldIssues();
            _mainView.CloseMainView += () => StopThread();
            _mainView.ShowProjects += () => ShowProjectsView();
            _mainView.NewIssue += () => ShowNewIssueView();
            _mainView.showJournals += () => OpenJournalsForm(_mainView.getSelectedIssueID());

            _projectView.ShowProjects += () => ShowProj();

            
            
        }        

        private void ShowMyIss()
        {
            _storage.getMyIssues();
        }

        private void ShowProj()
        {
            _storage.getMyProjects();
        }        

        private void getOldIssues()
        {
            _storage.getMyOldIssues();
        }

        private void ShowProjectsView()
        {
            _projectView.OpenView();
        }

        private void StopThread()
        {
            _storage.stopThread();
        }

        private void ShowNewIssueView()
        {
            //NewIssueForm myForm
            _newIssueView = new NewIssueForm();
            _newIssueView.OpenView();
            //_newIssueView.Init += () => InitReact();
        }       

        private void OpenJournalsForm(string str)
        {
            _journalsView = new JournalsForm(_storage, str);
            _journalsView.OpenView();
            //_storage.JournalsQuery(str);            
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
