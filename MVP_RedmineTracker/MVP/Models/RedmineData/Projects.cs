using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RedmineRestApi.RedmineData
{
    [DataContract]
    public class Projects
    {
        [DataMember(Name = "projects")]
        public Project[] projects { get; set; }

        [DataMember(Name = "project")]
        public Project project { get; set; }
    }
}


