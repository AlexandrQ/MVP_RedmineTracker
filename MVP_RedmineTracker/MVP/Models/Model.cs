using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RedmineTracker.Interfaces;
using RedmineRestApi.RedmineData;
using RedmineRestApi.HttpRest;

using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Collections.Specialized;
using System.Web;

namespace RedmineTracker.MVP
{
    public class Model : IModel
    {
        public event Action IssuesUpdated;

        public Issues myIssues;
        public Issues myOldIssues, myNewIssues;
        public Timer MyTimer = new Timer();



        public void getMyIssues()
        {
            myIssues = RequestIssues.Run();
            IssuesUpdated.Invoke();
        }

        public void RunTimer()
        {
            myOldIssues = RequestIssues.Run();
            
            MyTimer.Enabled = true;
            MyTimer.Interval = 5000;
            MyTimer.Tick += MyTimer_Tick;

        }

        public void MyTimer_Tick(object sender, EventArgs e)
        {
            //CompareTwoIssues();
            
        }

        private void CompareTwoIssues()
        {            
            myNewIssues = RequestIssues.Run();

            IDictionary<string, string> listOfChanges = new Dictionary<string, string>();

            if (!Issues.IssuesCount(myOldIssues, myNewIssues))
            {              
                
                MessageBox.Show("У Вас новые задачи!");
            }

            listOfChanges = Issues.IssuesChanges(myOldIssues, myNewIssues);


            if (listOfChanges.Count != 0)
            {                

                foreach (KeyValuePair<string, string> myPair in listOfChanges)
                {
                    MessageBox.Show(myPair.Value);
                }
            }

        }

    }
}
