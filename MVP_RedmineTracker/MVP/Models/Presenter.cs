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
        private IProjectDetails _detailsView;


        public Presenter(IMainForm v1, IProjectForm v2, IModel s)
        {
            _mainView = v1;
            _projectView = v2;
            _storage = s;            
            
            _mainView.MainFormInitialized += () => initIssuesIssues();
            _mainView.CloseMainView += () => StopThread();
            _mainView.ShowProjects += () => ShowProjectsView();
            _mainView.NewIssue += () => ShowNewIssueView();
            _mainView.showJournals += () => OpenJournalsForm(_mainView.getSelectedIssueID());

            _projectView.ShowProjects += () => ShowProj();
            _projectView.ShowDetailsView += () => OpenProjDetailsForm(_projectView.getSelectedProjID());            
            
        }        

        private void ShowProj()
        {
            _storage.getMyProjects();
        }        

        private void initIssuesIssues()
        {
            _storage.IssuesRequest();
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
            _newIssueView = new NewIssueForm();
            _newIssueView.OpenView();            
        }       

        private void OpenJournalsForm(string str)
        {
            _journalsView = new JournalsForm(_storage, str);
            _journalsView.OpenView();                   
        }

        private void OpenProjDetailsForm(string projID)
        {
            _detailsView = new ProjectDetailsForm(projID, _storage);
            _detailsView.OpenView();
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
