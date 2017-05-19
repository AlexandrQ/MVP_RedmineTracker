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

        void getMyIssues();
        Issues getMyIssuesObj();
        IDictionary<string, string> getListOfChange();
        Users getMyProjectsObj();
        void getMyProjects();
        
        void getMyOldIssues();
    }
}
