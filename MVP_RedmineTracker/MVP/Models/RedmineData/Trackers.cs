using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RedmineRestApi.RedmineData
{
    [DataContract (Name ="trackers")]
    public class Trackers { 
        [DataMember(Name = "trackers")]
        public Tracker[] trackers { get; set; }

        [DataMember(Name = "tracker")]
        public Tracker tracker { get; set; }
    }
}
