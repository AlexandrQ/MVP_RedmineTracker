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
        event Action FilterApplied;
        event Action ProjectsReceived;
        event Action NewIssuesAppeared;
        event Action AuthenticationPassed;
        event Action AuthenticationFailed;
        event Action NoInternetConnection;

        Issues getMyIssues();
        Users getMyProjects();
        Issues getMyJournals();
        Issues getMyFilterIssues();
        Projects getProjectDetails();
        Memberships getMemberships();
        IDictionary<string, string> getStatusValue();
        IDictionary<string, string> getListOfChange();        
        IDictionary<string, string> getPriorityValue();
        IDictionary<string, string> getProjectComboValue();
        
        void IssuesQuery();
        void ProjectsQuery();        
        void CreateNewIssueQuery(NewIssue myQuery);
        void AuthenticationQuery(string L, string P);
        void FilterQuery(IDictionary<string, string> myQuery);
        void JournalsQuery(string issueID, IJournalsForm _jf);
        //void ChangeStatusQuery(string IssueID, string statusID);
        void UsersListQuery(string projID, IUsersListForm _UForm);
        void UsersListComboQuery(string projID, IUpdateIssuesForm _UIForm);
        void ProjDetailsQuery(string projID, IProjectDetails _detailsForm);
        void UsersListComboQuery(string projID, INewIssueForm _INewIssForm);
        void UpdateIssueQuery(string issID, UpdateIssue updatedIssue/*IDictionary<string, string> myFilter*/);

        void stopThread();        
    }
}
