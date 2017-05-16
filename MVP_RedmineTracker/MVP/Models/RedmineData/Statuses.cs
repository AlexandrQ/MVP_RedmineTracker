using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RedmineRestApi.RedmineData
{
    [DataContract (Name ="statuses")]
    public class Statuses { 
        [DataMember(Name = "statuses")]
        public Status[] statuses { get; set; }

        [DataMember(Name = "status")]
        public Status status { get; set; }
    }
}
