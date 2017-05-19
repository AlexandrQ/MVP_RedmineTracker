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
        private NewIssueForm _newIssueView;



        public Presenter(IMainForm v1, IProjectForm v2, IModel s)
        {
            _mainView = v1;
            _projectView = v2;
            _storage = s;
            
            
            _mainView.ShowIssues += () => ShowMyIss();
            _mainView.Initialize += () => getOldIssues();
            //_mainView.Timer_Tick += () => CheckChangesIssue();
            _mainView.ShowProjects += () => ShowProjectsView();
            _mainView.NewIssue += () => ShowNewIssueView();

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

        /*private void CheckChangesIssue()
        {
            _storage.CompareTwoIssues();
        }*/

        private void getOldIssues()
        {
            _storage.getMyOldIssues();
        }

        private void ShowProjectsView()
        {
            _projectView.OpenView();
        }

        private void ShowNewIssueView()
        {
            //NewIssueForm myForm
            _newIssueView = new NewIssueForm();
            _newIssueView.OpenView();
            _newIssueView.Init += () => InitReact();
        }

        private void InitReact()
        {
            Console.WriteLine("InitReact");
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
