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
        private UpdateIssue UpIsssue = new UpdateIssue();

        private IDictionary<string, string> filter = new Dictionary<string, string>();

        public event Action changeIssue;

        public UpdateIssuesForm(IModel imodel, string issID)
        {
            InitializeComponent();
            this._model = imodel;            
            this.issueID = issID;

            this.ShowmIss();
            this.fillProjectCombo();

            this._model.ProjectsReceived += () => fillProjectCombo();
            //this._model.IssueChanged += () => ShowmIss();
            
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


        public IDictionary<string, string> getFilter()
        {
            return filter;
        }


        public UpdateIssue getUpdatedIssue()
        {
            return UpIsssue;
        }


        public string getIssueID()
        {
            return issueID;
        }


        private void updateButton_Click(object sender, EventArgs e)
        {
            //тут нужен не фильтр-словарь, а объект класса Issue или NewIssue
            //null-знаяения брать из текущих issue
            filter.Clear();

            //status
            if (statusComboBox.SelectedItem != null)
            {
                if (statusComboBox.SelectedItem.ToString() != "")
                {
                    string queryValue = statusComboBox.SelectedItem.ToString();
                    string queryName = "status_id";

                    foreach (KeyValuePair<string, string> myPair in _model.getStatusValue())
                    {
                        if (myPair.Key == queryValue)
                        {
                            queryValue = myPair.Value;
                            break;
                        }
                    }
                    filter.Add(queryName, queryValue);

                    UpIsssue.status_id = queryValue;
                }
            }

            //priority
            if (priorityComboBox.SelectedItem != null)
            {
                if (priorityComboBox.SelectedItem.ToString() != "")
                {
                    string queryValue = priorityComboBox.SelectedItem.ToString();
                    string queryName = "priority_id";

                    foreach (KeyValuePair<string, string> myPair in _model.getPriorityValue())
                    {
                        if (myPair.Key == queryValue)
                        {
                            queryValue = myPair.Value;
                            break;
                        }
                    }
                    filter.Add(queryName, queryValue);

                    UpIsssue.priority_id = queryValue;
                }
            }

            //project
            if (projectComboBox.SelectedItem != null)
            {
                if (projectComboBox.SelectedItem.ToString() != "")
                {
                    string queryValue = projectComboBox.SelectedItem.ToString();
                    string queryName = "project_id";

                    foreach (KeyValuePair<string, string> myPair in _model.getProjectComboValue())
                    {
                        if (myPair.Key == queryValue)
                        {
                            queryValue = myPair.Value;
                            break;
                        }
                    }
                    filter.Add(queryName, queryValue);

                    UpIsssue.project_id = queryValue;
                }
            }

            //done_ratio
            if (doneRatioComboBox.SelectedItem != null)
            {
                if (doneRatioComboBox.SelectedItem.ToString() != "")
                {
                    string queryValue = doneRatioComboBox.SelectedItem.ToString();
                    string queryName = "done_ratio";

                    filter.Add(queryName, queryValue);

                    UpIsssue.done_ratio = queryValue;
                }
            }

            //notes
            if (notesTextBox.Text != null)
            {                
                string queryValue = notesTextBox.Text;
                string queryName = "notes";

                filter.Add(queryName, queryValue);

                UpIsssue.notes = queryValue;                
            }




            //assigned_to
            if (assignedToComboBox.SelectedItem != null)
            {
                if (assignedToComboBox.SelectedItem.ToString() != "")
                {
                    string queryValue = assignedToComboBox.SelectedItem.ToString();
                    string queryName = "assigned_to_id";

                    /*foreach (KeyValuePair<string, string> myPair in _model.getProjectComboValue())
                    {
                        if (myPair.Key == queryValue)
                        {
                            queryValue = myPair.Value;
                            break;
                        }
                    }*/
                    for (int i = 0; i < _model.getMemberships().memberships.Count(); i++)
                    {
                        if (queryValue == _model.getMemberships().memberships[i].User.Name)
                        {
                            queryValue = _model.getMemberships().memberships[i].User.ID;
                            filter.Add(queryName, queryValue);

                            UpIsssue.assigned_to_id = queryValue;
                            break;
                        }                        
                    }                    
                }
            }

            changeIssue.Invoke();
            CloseView();
        }
    }
}
