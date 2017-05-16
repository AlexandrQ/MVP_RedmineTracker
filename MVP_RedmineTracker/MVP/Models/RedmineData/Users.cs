using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RedmineRestApi.RedmineData
{
    [DataContract (Name ="users")]
    public class Users { 
        [DataMember(Name = "users")]
        public User[] users { get; set; }

        [DataMember(Name = "user")]
        public User user { get; set; }
    }
}
