using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RedmineRestApi.RedmineData
{
    [DataContract (Name ="roles")]
    public class Roles { 

        [DataMember(Name = "roles")]
        public Role[] roles { get; set; }

        [DataMember(Name = "roles")]
        public Role role { get; set; }
    }
}
