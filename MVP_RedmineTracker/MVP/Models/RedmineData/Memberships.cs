using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RedmineRestApi.RedmineData
{
    [DataContract(Name = "memberships")]
    public class Memberships
    { 
        [DataMember(Name = "memberships")]
        public Membership[] memberships { get; set; }

        [DataMember(Name = "membership")]
        public Membership membership { get; set; }
    }
    
}
