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
    partial class ProjectDetailsForm : Form, IProjectDetails
    {
        private string projectID;
        private readonly IModel _model;

        public ProjectDetailsForm(string projID, IModel _md)
        {
            InitializeComponent();
            this.projectID = projID;
            this._model = _md;
        }

        public void OpenView()
        {
            this.Show();
            this._model.ProjDetailsQuery(projectID, this);
        }

        public void CloseView()
        {
            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.CloseView();
        }

        public void ShowDetails()
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Rows.Add(_model.getProjectDetails().project.ID, _model.getProjectDetails().project.Name,
                _model.getProjectDetails().project.Description, _model.getProjectDetails().project.Status,
                _model.getProjectDetails().project.Created_on, _model.getProjectDetails().project.Updated_on);
        }
    }
}
