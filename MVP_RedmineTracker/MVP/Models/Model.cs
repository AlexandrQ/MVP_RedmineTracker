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
        public event Action IssuesUpdated;
        public event Action NewIssuesAppeared;
        public event Action IssueChanged;
        public event Action ProjectsReceived;        

        private Thread myThread;
        private bool mySwitch = true;

        private Issues myIssues;
        private Issues myNewIssues;
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


        public IDictionary<string, string> getProjectCombo()
        {
            return ProjectCombo;
        }


        public void stopThread()
        {
            mySwitch = false;
        }


        public void ProjectsQuery()
        {
            myProjects = RequestProjects.Run();
            ProjectsReceived.Invoke();
        }


        public void IssuesQuery()
        {
            myIssues = RequestIssues.Run();
            myThread.Start();
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


        public void UsersListQuery(string projID, IUsersListForm UForm)
        {
            RequestMemberships simpleReq = new RequestMemberships();
            myMemberships = simpleReq.Run(projID);            
            UForm.showUserList();
        }

        public void ProjectConboInit()
        {
            foreach(Membership membership in myProjects.user.Memberships)
            {
                ProjectCombo.Add(membership.Project.Name, membership.Project.ID);
            }
        }

        private void CompareTwoIssues()
        {
            while (mySwitch)
            {
                myNewIssues = RequestIssues.Run();

                if (!Issues.IssuesCount(myIssues, myNewIssues))
                {
                    NewIssuesAppeared.Invoke();
                }

                listOfChanges = Issues.IssuesChanges(myIssues, myNewIssues);

                if (listOfChanges.Count != 0)
                {
                    IssueChanged.Invoke();
                }
                Console.WriteLine("Compare Two Issues");
                myIssues = myNewIssues;
                Thread.Sleep(5000);
            }
        }
    }
}
