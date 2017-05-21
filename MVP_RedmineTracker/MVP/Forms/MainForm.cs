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

    partial class MainForm : Form , IMainForm
    {        
        public event Action MainFormInitialized;
        public event Action CloseMainView;
        public event Action ShowProjects;
        public event Action NewIssue;
        public event Action showJournals;

        private readonly IModel _model;        

        public MainForm(IModel ms)
        {
            InitializeComponent();
            this._model = ms;
            
            this._model.NewIssuesAppeared += () => this.NewIssues();
            this._model.IssueChanged += () => this.NewIssueChanged();
            

        }
       
        public void OpenView()
        {
            MainFormInitialized.Invoke();
            this.ShowmIss();
            Application.Run(this);            
        }

        

        public void CloseView()
        {
            this.Close();
        }

        private void ShowmIss()
        {                        
            dataGridView1.Rows.Clear();
            foreach (Issue issue in _model.getMyIssues().issues)
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
            ShowmIss();
        }

        public void NewIssueChanged()
        {
            foreach (KeyValuePair<string, string> myPair in this._model.getListOfChange())
            {
                MessageBox.Show(myPair.Value);
            }
            ShowmIss();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ShowmIss();            
        }

        private void ShowProjectsButton_Click(object sender, EventArgs e)
        {
            ShowProjects.Invoke();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewIssue.Invoke();
        }        

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseMainView.Invoke();            
        }

        public string getSelectedIssueID()
        {            
            int x = dataGridView1.SelectedCells[0].RowIndex;            
            return dataGridView1.Rows[x].Cells[0].Value.ToString();
        }

        private void journalsButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                showJournals.Invoke();
            }
        }
    }
}
