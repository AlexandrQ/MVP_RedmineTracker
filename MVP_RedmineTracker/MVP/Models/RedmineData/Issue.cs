using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace RedmineRestApi.RedmineData
{
    [DataContract(Name = "issue")]
    public class Issue
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember (Name = "project")]
        public Project Project { get; set; }

        [DataMember(Name = "tracker")]
        public Tracker Tracker { get; set; }

        [DataMember(Name = "status")]
        public Status Status { get; set; }

        [DataMember(Name = "priority")]
        public Priority Priority { get; set; }

        [DataMember(Name = "author")]
        public Author Author { get; set; }
        
        [DataMember(Name = "assigned_to")]
        public Assigned_to Assigned_to { get; set; }

        [DataMember (Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "start_date")]
        public string Start_date { get; set; }

        [DataMember(Name = "done_ratio")]
        public string Done_ratio { get; set; }

        [DataMember(Name = "created_on")]
        public string Created_on { get; set; }

        [DataMember(Name = "updated_on")]
        public string Updated_on { get; set; }

        [DataMember(Name = "journals")]
        public Journal[] Journals { get; set; }

        public Issue(string id, Project project, Tracker tracker, Status status, Priority priority, Author author, Assigned_to assigned_to, string description, string subject, string start_date, string done_ratio, string created_on, string updated_on, Journal[] journals)
        {
            this.ID = id;
            this.Project = project;
            this.Tracker = tracker;
            this.Status = status;
            this.Priority = priority;
            this.Author = author;
            this.Assigned_to = assigned_to;
            this.Description = description;
            this.Subject = subject;
            this.Start_date = start_date;
            this.Done_ratio = done_ratio;
            this.Created_on = created_on;
            this.Updated_on = updated_on;
            this.Journals = journals;
        }

        public override string ToString()
        {
            return MyReturnStr();  
        }

        public string MyReturnStr()
        {
            string myStr = "";

            if (ID != null) myStr += "id: " + ID + "\n\t";
            if (Project != null) myStr += Project + "\n\t";
            if (Tracker != null) myStr += Tracker + "\n\t";
            if (Status != null) myStr += Status + "\n\t";
            if (Priority != null) myStr += Priority + "\n\t";
            if (Author != null) myStr += Author + "\n\t";
            if (Assigned_to != null) myStr += Assigned_to + "\n\t";
            if (Description != null) myStr += "Description: " + Description + "\n\t";
            if (Subject != null) myStr += "Subject: " + Subject + "\n\t";
            if (Start_date != null) myStr += "Start_date: " + Start_date + "\n\t";
            if (Done_ratio != null) myStr += "Done_ratio: " + Done_ratio + "\n\t";
            if (Created_on != null) myStr += "Created_on: " + Created_on + "\n\t";
            if (Updated_on != null) myStr += "Updated_on: " + Updated_on + "\n\t";
            if (Journals != null) myStr += "Journals: " + Journals;


            return myStr;
        }




       public static bool operator ==(Issue s1, Issue s2)
       {
            if (intToBool(s1.ID.CompareTo(s2.ID)) & (s1.Status == s2.Status) &
                (s1.Priority == s2.Priority) & intToBool(s1.Done_ratio.CompareTo(s2.Done_ratio)) &
                intToBool(s1.Updated_on.CompareTo(s2.Updated_on))) return true;
            else return false;
        }

        public static bool operator !=(Issue s1, Issue s2)
        {
            if (intToBool(s1.ID.CompareTo(s2.ID)) & (s1.Status == s2.Status) &
                (s1.Priority == s2.Priority) & intToBool(s1.Done_ratio.CompareTo(s2.Done_ratio)) &
                intToBool(s1.Updated_on.CompareTo(s2.Updated_on))) return false;
            else return true;
        }

        public static bool intToBool(int var)
        {
            if (var == 0) return true;
            else return false;
        }
    }
}
