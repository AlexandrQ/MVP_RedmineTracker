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

        private Thread myThread;
        private bool mySwitchForThread = true;

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
            myProjects = RequestProjects.Run();
            ProjectComboInit();
            ProjectsReceived.Invoke();
        }


        public void IssuesQuery()
        {
            myIssues = RequestIssues.Run();
            myThread.Start();
        }


        public void ChangeStatusQuery(string IssueID, string statusID)
        {            
            RequestIssues.RunPut(IssueID, statusID);
        }


        public void CreateNewIssueQuery(NewIssue myQuery)
        {
            RequestIssues.RunPost(myQuery);
        }


        public void FilterQuery(IDictionary<string, string> myQuery)
        {
            RequestIssuesFilter simpleReq = new RequestIssuesFilter();
            myFilterIssues = simpleReq.Run(myQuery);
            FilterApplied.Invoke();
        }


        public void JournalsQuery(string issueID, IJournalsForm _journalForm)
        {            
            RequestJournals simpleReq = new RequestJournals();
            myJournals = simpleReq.Run(issueID);            
            _journalForm.ShowJournals();
        }


        public void ProjDetailsQuery(string projID, IProjectDetails _detailsForm)
        {            
            RequestProject simpleReq = new RequestProject();
            projDetails = simpleReq.Run(projID);
            _detailsForm.ShowDetails();
        }


        public void UsersListQuery(string projID, IUsersListForm _UForm)
        {
            RequestMemberships simpleReq = new RequestMemberships();
            myMemberships = simpleReq.Run(projID);            
            _UForm.showUserList();
        }


        public void UsersListComboQuery(string projID, IUpdateIssuesForm _UIForm)
        {
            RequestMemberships simpleReq = new RequestMemberships();
            myMemberships = simpleReq.Run(projID);
            _UIForm.fillUserComboBox();
        }


        public void UsersListComboQuery(string projID, INewIssueForm _INewIssForm)
        {
            RequestMemberships simpleReq = new RequestMemberships();
            myMemberships = simpleReq.Run(projID);
            _INewIssForm.FillAssignee();
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
                myNewIssues = RequestIssues.Run();

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
