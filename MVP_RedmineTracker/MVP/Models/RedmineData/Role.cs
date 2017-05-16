using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace RedmineRestApi.RedmineData
{
    [DataContract(Name = "role")]
    public class Role
    {

        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }        

        public Role(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public override string ToString()
        {
            return "Role id: " + ID + ", Role name: " + Name;
        }
    }
}
