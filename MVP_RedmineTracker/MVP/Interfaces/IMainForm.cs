using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedmineTracker.Interfaces
{
    interface IMainForm:IView
    {                        
        event Action MainFormInitialized;
        event Action CloseMainView;
        event Action ShowProjects;
        event Action NewIssue;
        event Action showJournals;

        string getSelectedIssueID();
        
    }
}
