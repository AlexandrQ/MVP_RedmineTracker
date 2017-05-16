using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RedmineRestApi.RedmineData
{
    [DataContract (Name = "journals")]
    public class Journals
    { 
        [DataMember(Name = "journals")]
        public Journal[] journals { get; set; }

        [DataMember(Name = "journal")]
        public Journal journal { get; set; }
    }
}
