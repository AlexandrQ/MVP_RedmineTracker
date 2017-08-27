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
        delegate void StringArgReturningVoidDelegate();

        public event Action MainFormInitialized;
        public event Action CloseMainView;
        public event Action ShowProjects;
        public event Action NewIssue;
        public event Action NewProject;
        public event Action showJournals;
        public event Action IssueUpdate;
        public event Action ApplyFilter;
        //public event Action ChangeStatus;

        private readonly IModel _model;
        private IDictionary<string, string> filter = new Dictionary<string, string>();

        public MainForm(IModel ms)
        {
            InitializeComponent();
            this._model = ms;
            
            this._model.NewIssuesAppeared += () => NewIssues();
            this._model.IssueChanged += () => NewIssueChanged();
            this._model.ProjectsReceived += () => fillCombo();
            this._model.FilterApplied += () => ShowmFilterIss();

        }

       
        public void OpenView()
        {
            MainFormInitialized.Invoke();
            this.ShowmIss();
            this.Show();
            //Application.Run(this);            
        }

        
        private void fillCombo()
        {
            projectComboBox.Items.Clear();
            projectComboBox.Items.Add("");
            foreach (KeyValuePair<string, string> myPair in _model.getProjectComboValue())
            {
                projectComboBox.Items.Add(myPair.Key);
            }            
        }    
        

        public void CloseView()
        {
            this.Close();
        }


        private void ShowmIss()
        {
            if (this.dataGridView1.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(ShowmIss);
                this.Invoke(d, new object[] {});
            }
            else
            { 
                this.dataGridView1.Rows.Clear();
                foreach (Issue issue in _model.getMyIssues().issues)
                {
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
                }
            }
        }


        private void ShowmFilterIss()
        {
            dataGridView1.Rows.Clear();
            foreach (Issue issue in _model.getMyFilterIssues().issues)
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
            //ShowmIss();
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

        public string getSelectedStatusID()
        {
            string statusName = statusComboBox.SelectedItem.ToString();
            string statusID = "";

            foreach (KeyValuePair<string, string> myPair in _model.getStatusValue())
            {
                if (myPair.Key == statusName)
                {
                     statusID = myPair.Value;
                    break;
                }
            }

            return statusID;
        }


        public IDictionary<string, string> getFilter()
        {
            return filter;
        }


        private void journalsButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                showJournals.Invoke();
            }
        }


        private void filterButton_Click(object sender, EventArgs e)
        {
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
                }
            }

            //created_on
            if (createdOnComboBox.SelectedItem != null)
            {
                if (createdOnComboBox.SelectedItem.ToString() != "")
                {
                    string queryValue = createdOnComboBox.SelectedItem.ToString();
                    string queryName = "created_on";

                    queryValue += dateTimePicker1.Value.Year + "-";
                    if (dateTimePicker1.Value.Month.ToString().Length == 1)
                    {
                        queryValue += "0" + dateTimePicker1.Value.Month + "-";
                    }
                    else queryValue += dateTimePicker1.Value.Month + "-";
                    if (dateTimePicker1.Value.Day.ToString().Length == 1)
                    {
                        queryValue += "0" + dateTimePicker1.Value.Day;
                    }
                    else queryValue += dateTimePicker1.Value.Day;

                    filter.Add(queryName, queryValue);
                }
            }

            ApplyFilter.Invoke();
        }       

        private void issueUpdateButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                IssueUpdate.Invoke();
            }
        }

        private void newProjectButton_Click(object sender, EventArgs e)
        {
            NewProject.Invoke();
        }
    }
}
