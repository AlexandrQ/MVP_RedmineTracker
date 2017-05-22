using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedmineTracker.Interfaces
{
    public interface INewIssueForm : IView
    {
        event Action projectComboBoxSelected;

        void fillAssigneeComboBox();
        string getSelectedProjID();
    }
}
