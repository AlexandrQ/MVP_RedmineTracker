using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RedmineRestApi.RedmineData
{
    [DataContract (Name = "details")]
    public class Details
    { 
        [DataMember(Name = "details")]
        public Detail[] details { get; set; }

        [DataMember(Name = "detail")]
        public Detail detail { get; set; }
    }
}
