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
        private readonly IModel _storage;
        private readonly IMainForm _mainView;
        private readonly IProjectForm _projectView;        
        private INewIssueForm _newIssueView;
        private IJournalsForm _journalsView;
        private IProjectDetails _detailsView;
        private IUsersListForm _usersListView;

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
            _mainView.ApplyFilter += () => SendFilterQuery(_mainView.getFilter());
            _mainView.ChangeStatus += () => ChangeStatusReq(_mainView.getSelectedIssueID(), _mainView.getSelectedStatusID());

            _projectView.ShowProjects += () => ShowProj();
            _projectView.ShowDetailsView += () => OpenProjDetailsForm(_projectView.getSelectedProjID());

            //_newIssueView.projectComboBoxSelected += () => UserQuery(_newIssueView.getSelectedProjID());
        }        

        private void ShowProj()
        {
            _storage.ProjectsQuery();
        }
        

        private void SendFilterQuery(IDictionary<string, string> myFilter)
        {
            _storage.FilterQuery(myFilter);
        }


        private void ChangeStatusReq(string IssueID, string statusID)
        {
            _storage.ChangeStatusQuery(IssueID, statusID);
        }

        


        private void initIssuesIssues()
        {
            _storage.IssuesQuery();
            _storage.ProjectsQuery();
            //_storage.ProjectComboInit();
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
            _newIssueView = new NewIssueForm(_storage);
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
            _detailsView.ShowListParticipans += () => OpenUsersListForm(_detailsView.getProjectID());
        }


        private void OpenUsersListForm(string projID)
        {
            _usersListView = new UsersListForm(projID, _storage);
            _usersListView.OpenView();
            //_storage.UsersListQuery(projID, _usersListView);
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
