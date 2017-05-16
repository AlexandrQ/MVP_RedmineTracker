using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RedmineRestApi.RedmineData
{
    [DataContract(Name = "detail")]    
    public class Detail
    {
        [DataMember(Name = "property")]
        public string Property { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "old_value")]
        public string Old_value { get; set; }

        [DataMember(Name = "new_value")]
        public string New_value { get; set; }

        public Detail(string property, string name, string old_value, string new_value)
        {
            this.Property = property;
            this.Name = name;
            this.Old_value = old_value;
            this.New_value = new_value;
        }

        public override string ToString()
        {
            return MyReturnStr();
        }

        public string MyReturnStr()
        {
            string myStr = "";

            if (Property != null) myStr += "property: " + Property;
            if (Name != null) myStr += ",Name: " + Name;
            if (Old_value != null) myStr += ",Old_value: " + Old_value;
            if (New_value != null) myStr += ",New_value: " + New_value;

            return myStr;
        }
    }
}
