using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RedmineTracker.Interfaces;
using RedmineRestApi.RedmineData;
using RedmineRestApi.HttpRest;
using System.Threading;

using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Collections.Specialized;
using System.Web;

namespace RedmineTracker.MVP
{
    public class Model : IModel
    {                
        public event Action IssueChanged;
        public event Action FilterApplied;
        public event Action ProjectsReceived;
        public event Action NewIssuesAppeared;
        public event Action AuthenticationPassed;
        public event Action AuthenticationFailed;
        public event Action NoInternetConnection;

        private Thread myThread;
        private bool mySwitchForThread = true;

        private string myLogin;
        private string myPassword;
        private Issues myIssues;
        private Issues myNewIssues;
        private Issues myFilterIssues;
        private Users myProjects;
        private Issues myJournals;
        private Projects projDetails;        
        private Memberships myMemberships;
        private IDictionary<string, string> listOfChanges;

        private readonly IDictionary<string, string> StatusValue = new Dictionary<string, string>()
        {
            { "New", "1" },
            { "In Progress", "2"},
            { "Resolved", "3" },
            { "Feedback", "4" },
            { "Closed", "5" },
            { "Rejected", "6" },
        };

        private readonly IDictionary<string, string> PriorityValue = new Dictionary<string, string>()
        {
            { "Low", "1" },
            { "Normal", "2"},
            { "High", "3" },
            { "Urgent", "4" },
            { "Immediate", "5" },
        };

        private IDictionary<string, string> ProjectCombo = new Dictionary<string, string>();


        public Model()
        {
            myThread = new Thread(this.CompareTwoIssues);            
        }


        public Issues getMyIssues()
        {
            return myIssues;
        }

        public Issues getMyFilterIssues()
        {
            return myFilterIssues;
        }


        public Users getMyProjects()
        {
            return myProjects;
        }       
         

        public IDictionary<string, string> getListOfChange()
        {
            return listOfChanges;
        }


        public Issues getMyJournals()
        {
            return myJournals;
        }


        public Projects getProjectDetails()
        {
            return projDetails;
        }


        public Memberships getMemberships()
        {
            return myMemberships;
        }


        public IDictionary<string, string> getStatusValue()
        {
            return StatusValue;
        }


        public IDictionary<string, string> getPriorityValue()
        {
            return PriorityValue;
        }


        public IDictionary<string, string> getProjectComboValue()
        {
            return ProjectCombo;
        }


        public void stopThread()
        {
            mySwitchForThread = false;
        }


        public void ProjectsQuery()
        {
            myProjects = RequestProjects.Run(myLogin, myPassword);
            ProjectComboInit();
            ProjectsReceived.Invoke();
        }


        public void IssuesQuery()
        {
            myIssues = RequestIssues.Run(myLogin, myPassword);
            myThread.Start();
        }      


        public void CreateNewIssueQuery(NewIssue myQuery)
        {
            RequestIssues.RunPost(myQuery, myLogin, myPassword);
        }

        public void CreateNewProjectQuery(NewProject myQuery)
        {
            RequestProject.POSTNewProject(myQuery, myLogin, myPassword);
        }


        public void FilterQuery(IDictionary<string, string> myQuery)
        {            
            myFilterIssues = RequestIssuesFilter.Run(myQuery, myLogin, myPassword);
            FilterApplied.Invoke();
        }


        public void UpdateIssueQuery(string IssID, UpdateIssue updatedIssue /*IDictionary<string, string> myFilter*/)
        {          
            //добавить здесь избавление от null
            foreach (Issue myIss in myIssues.issues)
            {
                if (myIss.ID.Equals(IssID))
                {
                    if (updatedIssue.assigned_to_id == null) updatedIssue.assigned_to_id = myIss.Assigned_to.ID;
                    if (updatedIssue.done_ratio == null) updatedIssue.done_ratio = myIss.Done_ratio;
                    if (updatedIssue.priority_id == null) updatedIssue.priority_id = myIss.Priority.ID;
                    if (updatedIssue.status_id == null) updatedIssue.status_id = myIss.Status.ID;
                    if (updatedIssue.project_id == null) updatedIssue.project_id = myIss.Project.ID;
                }
            }           

            RequestIssues.RunPut(IssID, updatedIssue, myLogin, myPassword);
        }


        public void JournalsQuery(string issueID, IJournalsForm _journalForm)
        {            
            myJournals = RequestJournals.Run(issueID, myLogin, myPassword);
            _journalForm.ShowJournals();
        }


        public void ProjDetailsQuery(string projID, IProjectDetails _detailsForm)
        {                        
            projDetails = RequestProject.Run(projID, myLogin, myPassword);
            _detailsForm.ShowDetails();
        }


        public void UsersListQuery(string projID, IUsersListForm _UForm)
        {            
            myMemberships = RequestMemberships.Run(projID, myLogin, myPassword);
            _UForm.showUserList();
        }


        public void UsersListComboQuery(string projID, IUpdateIssuesForm _UIForm)
        {            
            myMemberships = RequestMemberships.Run(projID, myLogin, myPassword);
            _UIForm.fillUserComboBox();
        }


        public void UsersListComboQuery(string projID, INewIssueForm _INewIssForm)
        {
            
            myMemberships = RequestMemberships.Run(projID, myLogin, myPassword);
            _INewIssForm.FillAssignee();
        }

        public void AuthenticationQuery(string L, string P)
        {
            myLogin = L;
            myPassword = P;
            string s = RequestIssues.AuthenticationQuery(L, P);
            if ( s == "OK")
            {
                AuthenticationPassed.Invoke();
            }
            else if (s == "Ethernet error") NoInternetConnection.Invoke();
            else AuthenticationFailed.Invoke();
        }

        private void ProjectComboInit()
        {
            ProjectCombo.Clear();
            foreach (Membership membership in myProjects.user.Memberships)
            {
                ProjectCombo.Add(membership.Project.Name, membership.Project.ID);
            }
        }
        
        
        

        private void CompareTwoIssues()
        {
            while (mySwitchForThread)
            {
                myNewIssues = RequestIssues.Run(myLogin, myPassword);

                if (!Issues.IssuesCount(myIssues, myNewIssues))
                {
                    myIssues = myNewIssues;
                    NewIssuesAppeared.Invoke();
                }

                listOfChanges = Issues.IssuesChanges(myIssues, myNewIssues);

                if (listOfChanges.Count != 0)
                {
                    myIssues = myNewIssues;
                    IssueChanged.Invoke();
                }
                Console.WriteLine("Compare Two Issues");                
                Thread.Sleep(5000);
            }
        }
    }
}
