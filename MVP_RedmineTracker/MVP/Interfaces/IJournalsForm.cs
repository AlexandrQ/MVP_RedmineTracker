using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedmineTracker.Interfaces
{
    public interface IJournalsForm : IView
    {
        //event Action ShowProjects;
        void ShowJournals();
    }
}
