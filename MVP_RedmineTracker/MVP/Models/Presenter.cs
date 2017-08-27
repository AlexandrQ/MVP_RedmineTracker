using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedmineTracker.Interfaces;
using RedmineRestApi.RedmineData;

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
        private INewProjectForm _newProjectView;
        private IUpdateIssuesForm _updateIssuesView;
        private IAuthenticationForm _authenticationForm;
        


        public Presenter(IMainForm v1, IAuthenticationForm v3, IProjectForm v2, IModel s)
        {
            _mainView = v1;
            _projectView = v2;
            _storage = s;
            _authenticationForm = v3;   
            
            _mainView.MainFormInitialized += () => initIssuesIssues();
            _mainView.CloseMainView += () => StopThread();
            _mainView.ShowProjects += () => ShowProjectsView();
            _mainView.NewIssue += () => ShowNewIssueView();
            _mainView.showJournals += () => OpenJournalsForm(_mainView.getSelectedIssueID());
            _mainView.ApplyFilter += () => SendFilterQuery(_mainView.getFilter());            
            _mainView.IssueUpdate += () => OpenUpdateIssueView(_mainView.getSelectedIssueID());
            _mainView.NewProject += () => ShowNewProjectView();

            _projectView.ShowProjects += () => ShowProj();
            _projectView.ShowDetailsView += () => OpenProjDetailsForm(_projectView.getSelectedProjID());

            _authenticationForm.checkAuthentication += () => checkAuthentication(_authenticationForm.getLogin(), _authenticationForm.getPassword());
            _storage.AuthenticationPassed += () => RunMV();            
        }        

        private void ShowProj()
        {
            _storage.ProjectsQuery();
        }
        

        private void SendFilterQuery(IDictionary<string, string> myFilter)
        {
            _storage.FilterQuery(myFilter);
        }


        private void SendChangeIssueQuery(string issID, UpdateIssue UpIss)
        {
            _storage.UpdateIssueQuery(issID, UpIss);
        }


        private void SendNewIssueQuery(NewIssue query)
        {
            _storage.CreateNewIssueQuery(query);
        }


        private void SendNewProjectQuery(NewProject query)
        {
            _storage.CreateNewProjectQuery(query);
        }


        private void checkAuthentication(string L, string P)
        {
            _storage.AuthenticationQuery(L, P);
        }     


        private void initIssuesIssues()
        {
            _storage.IssuesQuery();
            _storage.ProjectsQuery();            
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
            _newIssueView.CreateNewIssue += () => SendNewIssueQuery(_newIssueView.getNewIssue());
        }


        private void ShowNewProjectView()
        {
            _newProjectView = new NewProjectForm();
            _newProjectView.OpenView();
            _newProjectView.CreateNewProject += () => SendNewProjectQuery(_newProjectView.getNewProjectData());
        }


        private void OpenJournalsForm(string str)
        {
            _journalsView = new JournalsForm(_storage, str);
            _journalsView.OpenView();                   
        }


        private void OpenUpdateIssueView(string issueID)
        {
            _updateIssuesView = new UpdateIssuesForm(_storage, issueID);
            _updateIssuesView.OpenView();
            _updateIssuesView.changeIssue += () => SendChangeIssueQuery(_updateIssuesView.getIssueID(), _updateIssuesView.getUpdatedIssue());
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
            //_mainView.OpenView(); 
            _authenticationForm.OpenView();           
        }

        public void RunMV()
        {
            
            _mainView.OpenView();             
        }

        #endregion Presenter

    }
}
