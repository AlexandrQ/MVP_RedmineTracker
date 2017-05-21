using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RedmineRestApi.RedmineData;
using System.Threading.Tasks;

namespace RedmineTracker.Interfaces
{
    interface IModel
    {
        event Action IssuesUpdated;
        event Action NewIssuesAppeared;
        event Action IssueChanged;
        event Action ProjectsReceived;
        event Action JournalsReceived;

        Issues getMyIssues();
        //Issues getMyIssuesObj();
        Issues getMyJournals();
        Projects getProjectDetails();
        void JournalsQuery(string issueID, IJournalsForm jf);
        void ProjDetailsQuery(string projID, IProjectDetails _detailsForm);
        IDictionary<string, string> getListOfChange();
        Users getMyProjectsObj();
        void getMyProjects();
        void stopThread();
        
        void IssuesRequest();
    }
}
