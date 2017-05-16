using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RedmineRestApi.RedmineData
{
    [DataContract(Name = "author")]    
    public class Author
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public Author(string id, string name)
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

            if (ID != null) myStr += "Author id: " + ID;
            if (Name != null) myStr += ",Author Name: " + Name;            

            return myStr;
        }
    }
}
