﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedmineTracker.Interfaces
{
    public interface IProjectDetails : IView
    {
        event Action ShowListParticipans;
        
        string getProjectID();

        void ShowDetails();
    }
}
