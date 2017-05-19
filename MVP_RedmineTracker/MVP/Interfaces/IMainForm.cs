using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedmineTracker.Interfaces
{
    interface IMainForm:IView
    {                
        event Action ShowIssues;
        event Action Initialize;
        //event Action Timer_Tick;
        event Action ShowProjects;
        event Action NewIssue;
    }
}
