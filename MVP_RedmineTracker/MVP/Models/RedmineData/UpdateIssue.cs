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
    public class UpdateIssue
    {
        [DataMember(Name = "project_id")]
        public string project_id { get; set; }

        [DataMember(Name = "notes")]
        public string notes { get; set; }

        [DataMember(Name = "status_id")]
        public string status_id { get; set; }

        [DataMember(Name = "priority_id")]
        public string priority_id { get; set; }

        [DataMember(Name = "assigned_to_id")]
        public string assigned_to_id { get; set; }

        [DataMember(Name = "done_ratio")]
        public string done_ratio { get; set; }

        public UpdateIssue() { } 

        public UpdateIssue(string project_id, string notes, string done_ratio, string status_id, string assigned_to_id, string priority_id)//, string watcher_user_ids)
        {
            this.project_id = project_id;
            this.notes = notes;
            this.done_ratio = done_ratio;
            this.status_id = status_id;
            this.assigned_to_id = assigned_to_id;
            this.priority_id = priority_id;
            //this.watcher_user_ids[watcher_user_ids.Count()+1] = watcher_user_ids;



        }

        public override string ToString()
        {
            return MyReturnStr();  
        }

        public string MyReturnStr()
        {
            string myStr = "";

            if (project_id != null) myStr += "Project_id: " + project_id + "\n\t";
            if (notes != null) myStr += "notes: " + notes + "\n\t";
            if (done_ratio != null) myStr += "done_ratio: " + done_ratio + "\n\t";
            if (status_id != null) myStr += "Status_id: " + status_id + "\n\t";
            if (assigned_to_id != null) myStr += "Assigned_to_id: " + assigned_to_id + "\n\t";
            if (priority_id != null) myStr += "Priority_id: " + priority_id + "\n\t";



            return myStr;
        }       
    }
}
