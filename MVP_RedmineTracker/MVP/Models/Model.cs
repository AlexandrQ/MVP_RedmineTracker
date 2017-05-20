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
        public event Action JournalsReceived;

        public Thread myThread;
        private bool mySwitch = true;

        public Issues myIssues;
        public Issues myOldIssues, myNewIssues;
        public Users myProjects;
        public Issues myJournals;
        public IDictionary<string, string> listOfChanges = new Dictionary<string, string>();

        
        
        public Model()
        {
            myThread = new Thread(this.CompareTwoIssues);            
        }       


        public Users getMyProjectsObj()
        {
            return myProjects;
        }

        public void getMyIssues()
        {
            myIssues = RequestIssues.Run();
            IssuesUpdated.Invoke();
        }

        public IDictionary<string, string> getListOfChange()
        {
            return listOfChanges;
        }

        public Issues getMyIssuesObj()
        {
            return myIssues;
        }

        public void getMyProjects()
        {            
            myProjects = RequestProjects.Run();
            ProjectsReceived.Invoke();
            
        }

        public void getMyOldIssues()
        {
            myOldIssues = RequestIssues.Run();
            myThread.Start();
        }

        public Issues getMyJournals()
        {
            return myJournals;
        }

        public void stopThread()
        {
            mySwitch = false;
        }

        public void JournalsQuery(string issueID, IJournalsForm jf)
        {            
            RequestJournals simpleReq = new RequestJournals();
            myJournals = simpleReq.Run(issueID);
            //JournalsReceived.Invoke();
            jf.ShowJournals();
        }


        void CompareTwoIssues()
        {
            while (mySwitch)
            {
                myNewIssues = RequestIssues.Run();

                if (!Issues.IssuesCount(myOldIssues, myNewIssues))
                {
                    NewIssuesAppeared.Invoke();
                }

                listOfChanges = Issues.IssuesChanges(myOldIssues, myNewIssues);


                if (listOfChanges.Count != 0)
                {
                    IssueChanged.Invoke();
                }
                Console.WriteLine("Compare Two Issues");
                myOldIssues = myNewIssues;
                Thread.Sleep(5000);
            }
        }
    }
}
