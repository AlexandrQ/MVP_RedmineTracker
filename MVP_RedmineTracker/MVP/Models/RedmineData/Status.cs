using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RedmineRestApi.RedmineData
{
    [DataContract(Name = "status")]    
    public class Status
    {
        

    [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public Status(string id, string name)
        {
             this.ID = id;
             this.Name = name;     
        }

        public override string ToString()
        {
            return MyReturnStr();
        }

        public string MyReturnStr()
        {
            string myStr = "";

            if (ID != null) myStr += "Status id: " + ID;
            if (Name != null) myStr += ",Status Name: " + Name;            

            return myStr;
        }

        public static bool operator ==(Status s1, Status s2)
        {
            if (intToBool(s1.ID.CompareTo(s2.ID)) & intToBool(s1.Name.CompareTo(s2.Name))) return true;
            else return false;
        }

        public static bool operator !=(Status s1, Status s2)
        {
            if (intToBool(s1.ID.CompareTo(s2.ID)) & intToBool(s1.Name.CompareTo(s2.Name))) return false;
            else return true;
        }

        public static bool intToBool(int var)
        {
            if (var == 0) return true;
            else return false;
        }
    }
}
