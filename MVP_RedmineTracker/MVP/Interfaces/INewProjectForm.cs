using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RedmineRestApi.RedmineData;

namespace RedmineTracker.Interfaces

{
    public interface INewProjectForm : IView
    {
        event Action CreateNewProject;

        NewProject getNewProjectData();


    }
}
