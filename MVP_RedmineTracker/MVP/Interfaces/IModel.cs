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
        event Action IssueChanged;
        event Action IssuesUpdated;
        event Action ProjectsReceived;
        event Action NewIssuesAppeared;

        Issues getMyIssues();
        Users getMyProjects();
        Issues getMyJournals();        
        Projects getProjectDetails();
        Memberships getMemberships();
        IDictionary<string, string> getStatusValue();
        IDictionary<string, string> getListOfChange();
        IDictionary<string, string> getProjectCombo();
        IDictionary<string, string> getPriorityValue();

        void IssuesQuery();
        void ProjectsQuery();
        void JournalsQuery(string issueID, IJournalsForm _jf);
        void UsersListQuery(string projID, IUsersListForm _UForm);        
        void ProjDetailsQuery(string projID, IProjectDetails _detailsForm);        

        void stopThread();
        void ProjectConboInit();
    }
}
