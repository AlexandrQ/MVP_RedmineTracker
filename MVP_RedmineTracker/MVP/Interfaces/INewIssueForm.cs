using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedmineTracker.Interfaces
{
    interface INewIssueForm : IView
    {
        //event Action ShowProjects;

        event Action Init;

    }
}
