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
    public partial class NewProjectForm : Form, INewProjectForm
    {
        private NewProject myNewProject = new NewProject();

        public event Action CreateNewProject;

        public NewProjectForm()
        {
            InitializeComponent();
        }

        public void OpenView()
        {            
            this.Show();
        }

        public void CloseView()
        {
            this.Close();
        }


        public NewProject getNewProjectData()
        {
            return myNewProject;
        }


        private void CollectDataFromForm()
        {
            if (nameTextBox.Text.Length > 0)
            {
                if (identifierTextBox.Text.Length > 0)
                {
                    myNewProject.name = nameTextBox.Text;
                    myNewProject.identifier = identifierTextBox.Text;
                    if (descriptionTextBox.Text != null)
                    {
                        myNewProject.description = descriptionTextBox.Text;
                    }                    
                }
                else MessageBox.Show("Identifier length should be grather then 1");
            }
            else MessageBox.Show("Name length should be grather then 1");
        }


        private void createButton_Click(object sender, EventArgs e)
        {
            CollectDataFromForm();
            CreateNewProject.Invoke();
            CloseView();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            CloseView();
        }
    }
}
