using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedmineTracker.Interfaces
{
    interface IProjectForm : IView
    {
        event Action ShowProjects;
        event Action ShowDetailsView;

        string getSelectedProjID();

    }
}
