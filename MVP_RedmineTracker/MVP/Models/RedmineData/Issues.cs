using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RedmineRestApi.RedmineData
{
    [DataContract (Name ="issues")]
    public class Issues { 
        [DataMember(Name = "issues")]
        public Issue[] issues { get; set; }

        [DataMember(Name = "issue")]
        public Issue issue { get; set; }

        public static bool operator ==(Issues s1, Issues s2)
        {        
            bool myBool = true;    
            foreach (Issue issue1 in s1.issues)
            {
                foreach (Issue issue2 in s2.issues)
                {
                    if (issue1 != issue2)
                    {
                        myBool = false;
                    }
                    else
                    {
                        myBool = true;
                        break;
                    }
                }
                if (myBool == false) return false; 
            }
            return true;
        }

        public static bool operator !=(Issues s1, Issues s2)
        {
            foreach (Issue issue1 in s1.issues)
            {
                foreach (Issue issue2 in s2.issues)
                {
                    if (issue1 == issue2) return false;
                }
            }
            return true;
        }



        public static bool IssuesCount(Issues oldIssues, Issues newIssues)
        {
            //возвращает true если количество issue одиннаковое
            int oldCount = 0, newCount = 0;
            foreach (Issue issue1 in oldIssues.issues)
            {
                oldCount++;
            }
            foreach (Issue issue2 in newIssues.issues)
            {
                newCount++;
            }
            if (oldCount != newCount) return false;
            else return true;
        }


        public static IDictionary<string, string> IssuesChanges(Issues oldIssues, Issues newIssues)
        {
            IDictionary<string, string> listOfChanges = new Dictionary<string, string>();
            int count = 0;

            foreach (Issue oldIss in oldIssues.issues)
            {
                foreach (Issue newIss in newIssues.issues)
                {
                    //если не равны issue с одинаковым id
                    if ((Issue.intToBool(oldIss.ID.CompareTo(newIss.ID))) & (oldIss != newIss))
                    {
                        //проверяем не изменились ли статусы задач
                        if (!Issue.intToBool(oldIss.Status.Name.CompareTo(newIss.Status.Name)))
                        {
                            listOfChanges.Add(count.ToString(), "Issue #" + oldIss.ID +
                                "Changed status from " + oldIss.Status.Name + " to " + newIss.Status.Name);
                            count++;
                        }
                        //проверяем не изменился ли процент выполнения задач
                        if (!Issue.intToBool(oldIss.Done_ratio.CompareTo(newIss.Done_ratio)))
                        {
                            listOfChanges.Add(count.ToString(), "Issue #" + oldIss.ID +
                                "Changed 'Done ratio' from " + oldIss.Done_ratio + " to " + newIss.Done_ratio);
                            count++;
                        }
                        /*
                        *
                        *добавить проверку на изменение других полей задачи
                        *
                        */
                    }

                }
            }

            return listOfChanges;

        }
    }

    
}
