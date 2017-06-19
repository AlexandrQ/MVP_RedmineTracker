using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedmineTracker.Interfaces
{
    interface IMainForm:IView
    {
        event Action NewIssue;
        event Action IssueUpdate;
        event Action ApplyFilter;
        //event Action ChangeStatus;        
        event Action ShowProjects;
        event Action showJournals;
        event Action CloseMainView;
        event Action MainFormInitialized;        

        string getSelectedIssueID();
        string getSelectedStatusID();
        IDictionary<string, string> getFilter();
    }
}
