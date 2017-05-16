using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RedmineRestApi.RedmineData
{
    [DataContract (Name = "priorities")]
    public class Priorities
    { 
        [DataMember(Name = "priorities")]
        public Priority[] priorities { get; set; }

        [DataMember(Name = "priority")]
        public Priority priority { get; set; }
    }
}
