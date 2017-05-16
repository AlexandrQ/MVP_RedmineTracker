using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RedmineRestApi.RedmineData
{
    [DataContract (Name = "authors")]
    public class Authors
    { 
        [DataMember(Name = "authors")]
        public Author[] authors { get; set; }

        [DataMember(Name = "author")]
        public Author author { get; set; }
    }
}
