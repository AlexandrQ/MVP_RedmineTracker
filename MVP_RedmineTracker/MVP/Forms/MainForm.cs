using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RedmineTracker.Interfaces;
using RedmineRestApi.RedmineData;
using RedmineTracker.MVP;

namespace MVP_RedmineTracker.MVP.Forms
{   

    public partial class MainForm : Form , IMainForm
    {
        public event Action ShowIssues;

        private Model ms;


        public MainForm(Model ms)
        {
            InitializeComponent();
            this.ms = ms;

            this.ms.IssuesUpdated += () => this.ShowmIss();
        }

        public void OpenView()
        {
            Application.Run(this);
        }

        public void CloseView()
        {
            this.Close();
        }

        private void ShowmIss()
        {
            foreach (Issue issue in ms.myIssues.issues)
            {
                dataGridView1.Rows.Add(issue.ID, issue.Project.Name,
                    issue.Status.Name,
                    issue.Priority.Name,
                    issue.Author.Name,
                    issue.Assigned_to.Name,
                    issue.Subject,
                    issue.Description,
                    issue.Start_date,
                    issue.Done_ratio,
                    issue.Created_on,
                    issue.Updated_on);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowIssues.Invoke();
        }
    }
}
