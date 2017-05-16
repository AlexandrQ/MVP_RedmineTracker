using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RedmineRestApi.RedmineData
{
    [DataContract(Name = "user")]    
    public class User
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "login")]
        public string Login { get; set; }

        [DataMember(Name = "firstname")]
        public string Firstname { get; set; }

        [DataMember(Name = "lastname")]
        public string Lastname { get; set; }

        [DataMember(Name = "mail")]
        public string Mail { get; set; }

        [DataMember(Name = "created_on")]
        public string Created_on { get; set; }

        [DataMember(Name = "last_login_on")]
        public string Last_login_on { get; set; }

        [DataMember(Name = "api_key")]
        public string Api_key { get; set; } 

        [DataMember(Name = "memberships")]
        public Membership[] Memberships { get; set; }



        public User(string id , string name, string login, string firstname, string lastname, string mail, string created_on, string last_login_on, string api_key, Membership[] memberships)
        {
             this.ID = id;
             this.Name = name;        
             this.Login = login;
             this.Firstname = firstname;
             this.Lastname = lastname;
             this.Mail = mail;
             this.Created_on = created_on;
             this.Last_login_on = last_login_on;
             this.Api_key = api_key;
             this.Memberships = memberships;            
        }

        public override string ToString()
        {
            return  MyReturnStr();/*"User id: " + ID + ", Name: " + Name + ", login: " + Login + ", Firstname: " + Firstname + ", Lastname: " + Lastname
                + ", Mail: " + Mail + ", Created_on: " + Created_on + ", Last login on" + Last_login_on + ", Api key: " + Api_key;*/
        }

        public string MyReturnStr()
        {
            string myStr = "";

            if (ID != null) myStr += "\tUser id: " + ID +"\n\t";
            if (Name != null) myStr += "Name: " + Name + "\n\t";
            if (Login != null) myStr += "login: " + Login + "\n\t";
            if (Firstname != null) myStr += "Firstname: " + Firstname + "\n\t";
            if (Lastname != null) myStr += "Lastname: " + Lastname + "\n\t";
            if (Mail != null) myStr += "Mail: " + Mail + "\n\t";
            if (Created_on != null) myStr += "Created_on: " + Created_on + "\n\t";
            if (Last_login_on != null) myStr += "Last login on" + Last_login_on + "\n\t";
            if (Api_key != null) myStr += "Api key: " + Api_key;
            if (Memberships[0] != null)
            {
                myStr += "\n";
                for (int i = 0; i < Memberships.Count(); i++)
                {
                    myStr += "Memberships: " + Memberships[i] + "\n";
                }
            };

            return myStr;
        }
    }
}
