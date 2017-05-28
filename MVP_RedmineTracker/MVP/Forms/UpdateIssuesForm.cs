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

namespace MVP_RedmineTracker.MVP.Forms
{
    partial class UpdateIssuesForm : Form, IUpdateIssuesForm
    {
        private readonly IModel _model;
        private string issueID;
        private string projectID;

        public UpdateIssuesForm(IModel imodel, string issID)
        {
            InitializeComponent();
            this._model = imodel;            
            this.issueID = issID;
            this.ShowmIss();
            this.fillProjectCombo();
            this._model.ProjectsReceived += () => fillProjectCombo();
            this._model.IssueChanged += () => ShowmIss();
            
        }


        private void ShowmIss()
        {
            this.dataGridView1.Rows.Clear();
            foreach (Issue issue in _model.getMyIssues().issues)
            {
                if (issue.ID == this.issueID)
                {
                    this.projectID = issue.Project.ID;
                    this.dataGridView1.Rows.Add(issue.ID, issue.Project.Name,
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
                    break;
                }
            }            
        }


        private void fillProjectCombo()
        {
            projectComboBox.Items.Clear();
            projectComboBox.Items.Add("");
            foreach (KeyValuePair<string, string> myPair in _model.getProjectComboValue())
            {
                projectComboBox.Items.Add(myPair.Key);
            }
        }

        public void fillUserComboBox()
        {
            this.assignedToComboBox.Items.Clear();
            this.assignedToComboBox.Items.Add("");
            for (int i = 0; i < _model.getMemberships().memberships.Count(); i++)
            {
                this.assignedToComboBox.Items.Add(_model.getMemberships().memberships[i].User.Name);
            }
        }


        public void OpenView()
        {            
            this.Show();
            _model.UsersListComboQuery(projectID, this);
        }

        public void CloseView()
        {
            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.CloseView();
        }
    }
}
