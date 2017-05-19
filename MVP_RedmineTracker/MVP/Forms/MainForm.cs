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
        public event Action Initialize;
        //public event Action Timer_Tick;
        public event Action ShowProjects;
        public event Action NewIssue;


        private readonly IModel _model;
        //private Timer MyTimer = new Timer();

        public MainForm(Model ms)
        {
            InitializeComponent();
            this._model = ms;

            /*MyTimer.Enabled = true;
            MyTimer.Interval = 5000;
            MyTimer.Tick += MyTimer_Tick;*/

            this._model.IssuesUpdated += () => this.ShowmIss();
            this._model.NewIssuesAppeared += () => this.NewIssues();
            this._model.IssueChanged += () => this.NewIssueChanged();

        }

       /* public void MyTimer_Tick(object sender, EventArgs e)
        {
            Timer_Tick.Invoke();
        }*/

       

        public void OpenView()
        {
            Initialize.Invoke();
            Application.Run(this);            
        }

        public void CloseView()
        {
            this.Close();
        }

        private void ShowmIss()
        {
            dataGridView1.Rows.Clear();
            foreach (Issue issue in _model.getMyIssuesObj().issues)
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

        public void NewIssues()
        {
            MessageBox.Show("У Вас новые задачи!");
        }

        public void NewIssueChanged()
        {
            foreach (KeyValuePair<string, string> myPair in this._model.getListOfChange())
            {
                MessageBox.Show(myPair.Value);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ShowIssues.Invoke();
        }

        private void ShowProjectsButton_Click(object sender, EventArgs e)
        {
            ShowProjects.Invoke();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewIssue.Invoke();
        }
    }
}
