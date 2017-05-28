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
    

    partial class NewIssueForm : Form, INewIssueForm
    {
        private IDictionary<string, string> NewIssueData = new Dictionary<string, string>();
        private NewIssue newIssue = new NewIssue();
        //public event Action Init;

        public event Action CreateNewIssue;

        private readonly IModel _model;

        public NewIssueForm(IModel _md)
        {
            InitializeComponent();
            _model = _md;
        }

        public void OpenView()
        {
            fillProjectCombo();
            this.Show();
        }

        public void CloseView()
        {
            this.Close();
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

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.CloseView();
        }

        public string getSelectedProjID()
        {
            string projID = projectComboBox.SelectedItem.ToString();

            foreach (KeyValuePair<string, string> myPair in _model.getProjectComboValue())
            {
                if (myPair.Key == projID)
                {
                    projID = myPair.Value;
                    break;
                }
            }
            return projID;
        }

        public void FillAssignee()
        {
            assigneeComboBox.Items.Clear();
            assigneeComboBox.Items.Add("");

            watchersCheckedListBox.Items.Clear();

            foreach (Membership memberships in _model.getMemberships().memberships)
            {
                assigneeComboBox.Items.Add(memberships.User.Name);
                watchersCheckedListBox.Items.Add(memberships.User.Name);
            }
        }


        private void projectComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (projectComboBox.SelectedItem != null)
            {
                if (projectComboBox.SelectedItem.ToString() != "")
                {
                    //projectComboBoxSelected.Invoke();    
                    _model.UsersListComboQuery(getSelectedProjID(), this);
                }
            }
        }


        public IDictionary<string, string> getIssueData()
        {
            return NewIssueData;
        }


        public NewIssue getNewIssue()
        {
            return newIssue;
        }


        private void CollectDataFromForm()
        {
            NewIssueData.Clear();

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
                            newIssue.status_id = myPair.Value;
                            break;
                        }
                    }
                    NewIssueData.Add(queryName, queryValue);
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
                            newIssue.priority_id = myPair.Value;
                            break;
                        }
                    }
                    NewIssueData.Add(queryName, queryValue);
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
                            newIssue.project_id = myPair.Value;
                            break;
                        }
                    }
                    NewIssueData.Add(queryName, queryValue);
                }
            }

            //assigned to
            if (assigneeComboBox.SelectedItem != null)
            {
                if (assigneeComboBox.SelectedItem.ToString() != "")
                {
                    string queryValue = assigneeComboBox.SelectedItem.ToString();
                    string queryName = "assigned_to";

                    foreach (Membership memberships in _model.getMemberships().memberships)
                    {
                        if (queryValue == memberships.User.Name)
                        {
                            queryValue = memberships.User.ID;
                            newIssue.assigned_to_id = memberships.User.ID;
                            break;
                        }
                    }

                    NewIssueData.Add(queryName, queryValue);
                }
            }

            //watchers
            /*if (watchersCheckedListBox.SelectedItems.Count != 0)
            {
                //string queryValue = "";
                //string queryName = "watcher_user_ids";

                foreach (string str in watchersCheckedListBox.SelectedItems)
                {
                    int i;
                    foreach (Membership memberships in _model.getMemberships().memberships)
                    {
                        
                        if (str == memberships.User.Name)
                        {
                            //queryValue += memberships.User.ID + " ";
                            
                        }
                    }
                }
                
                //filter.Add(queryName, queryValue);

            } */

            //subject
            if (subjectTextBox.Text != "")
            {
                NewIssueData.Add("subject", subjectTextBox.Text);
                newIssue.subject = subjectTextBox.Text;
            }

            //description
            if (descriptionTextBox.Text != "")
            {
                NewIssueData.Add("description", descriptionTextBox.Text);
                newIssue.description = descriptionTextBox.Text;
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            CollectDataFromForm();
            CreateNewIssue.Invoke();
            this.CloseView();
        }
    }
}
