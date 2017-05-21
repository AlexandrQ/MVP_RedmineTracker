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
    partial class ProjectForm : Form, IProjectForm
    {
        public event Action ShowProjects;
        public event Action ShowDetailsView;

        private readonly IModel _ms;

        public ProjectForm(IModel ms)
        {
            InitializeComponent();

            this._ms = ms;
            
            this._ms.ProjectsReceived += () => this.ShowProj();
        }

        public void OpenView()
        {
            this.Show();
            this.ShowProjects.Invoke();
        }

        public void CloseView()
        {
            this.Hide();
        }

        public void ShowProj()
        {
            dataGridView1.Rows.Clear();
            foreach (Membership membersip in _ms.getMyProjects().user.Memberships)
            {
                dataGridView1.Rows.Add(membersip.Project.ID, membersip.Project.Name);
            }
        }

        private void exitButton1_Click(object sender, EventArgs e)
        {
            CloseView();
        }

        public string getSelectedProjID()
        {
            int x = dataGridView1.SelectedCells[0].RowIndex;
            return dataGridView1.Rows[x].Cells[0].Value.ToString();
        }

        private void detailsButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                ShowDetailsView.Invoke();
            }
        }
    }
}
