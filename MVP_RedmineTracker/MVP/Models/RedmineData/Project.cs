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
    public class Project
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "created_on")]
        public string Created_on { get; set; }

        [DataMember(Name = "updated_on")]
        public string Updated_on { get; set; }



        public Project(string id, string name, string description, string status, string created_on, string updated_on)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.Status = status;
            this.Created_on = created_on;
            this.Updated_on = updated_on;
        }

        public override string ToString()
        {
            return "Project id: " + ID + ", name: " + Name + 
                ", description: " + Description + ", status" + Status + 
                ", Created on: " + Created_on + ", updated on" + Updated_on;
        }
    }
}
