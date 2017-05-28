using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RedmineRestApi.RedmineData;

namespace RedmineTracker.Interfaces

{
    public interface INewIssueForm : IView
    {
        event Action CreateNewIssue;

        NewIssue getNewIssue();
        string getSelectedProjID();
        IDictionary<string, string> getIssueData();

        void FillAssignee();
    }
}
