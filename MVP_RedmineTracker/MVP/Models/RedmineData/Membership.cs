using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RedmineRestApi.RedmineData
{
    [DataContract(Name = "membership")]
    public class Membership
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "project")]
        public Project Project { get; set; }
        
        [DataMember(Name = "user")]
        public User User { get; set; }
        
        [DataMember(Name = "roles")]
        public Role[] Roles { get; set; }


        public Membership(string id, Project project, Role[]/*добавить вывод всех ролей в возможном массиве*/ roles, User user)
        {
            this.ID = id;
            this.Project = project;
            this.Roles = roles;
            this.User = user;
        }

        public override string ToString()
        {
            return "Membership id: " + ID + 
                "\n\t" + Project + "\n\t" + Roles[0]/*showRoles()*/ + "\n\t" + User;
        }


       /* public string showRoles()
        {
            string str = "";
            foreach (Role role in Roles1.roles)
            {
                str += role; 
            }

            return str;
        }*/
        /*
        public string strRole (Roles myRoles)
        {           

            String strRole = "";

            if (myRoles != null) {
                foreach (Role role in myRoles.roles)
                {
                    strRole += role.ToString();
                }
            }

            return strRole;
        } */
    }
 }

