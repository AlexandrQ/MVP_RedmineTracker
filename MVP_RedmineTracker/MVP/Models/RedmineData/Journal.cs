using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RedmineRestApi.RedmineData
{
    [DataContract(Name = "journal")]    
    public class Journal
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "notes")]
        public string Notes { get; set; }

        [DataMember(Name = "created_on")]
        public string Created_on { get; set; }

        [DataMember(Name = "details")]
        public Detail[] Details { get; set; }

        public Journal(string id, User user, string notes, string created_on, Detail[] details)
        {
            this.ID = id;
            this.User = user;
            this.Notes = notes;
            this.Created_on = created_on;
            this.Details = details;
        }

        public override string ToString()
        {
            return MyReturnStr();
        }

        public string MyReturnStr()
        {
            string myStr = "";

            if (ID != null) myStr += "id: " + ID;
            if (User != null) myStr += ",User: " + User;
            if (Notes != null) myStr += ",Notes: " + Notes;
            if (Created_on != null) myStr += ",Created_on: " + Created_on;
            if (Details != null) myStr += ",Details: " + Details;

            return myStr;
        }
    }
}
