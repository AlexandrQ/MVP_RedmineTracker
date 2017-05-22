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
        //public event Action Init;

        public event Action projectComboBoxSelected;

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

        public void fillAssigneeComboBox()
        {
            assigneeComboBox.Items.Clear();
            assigneeComboBox.Items.Add("");            
            foreach (Membership memberships in _model.getMemberships().memberships)
            {
                assigneeComboBox.Items.Add(memberships.User.Name);
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

    }
}
