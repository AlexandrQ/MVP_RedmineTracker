using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace RedmineRestApi.RedmineData
{
    [DataContract(Name = "project")]
    public class NewProject
    {
        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "identifier")]
        public string identifier { get; set; }

        [DataMember(Name = "description")]
        public string description { get; set; }


        public NewProject() { }


        public NewProject(string name, string identifier,  string description)
        {            
            this.name = name;
            this.description = description;
            this.identifier = identifier;            
        }        
    }
}
