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

namespace MVP_RedmineTracker.MVP.Forms
{
    partial class UsersListForm : Form, IUsersListForm
    {        
        private readonly IModel _model;
        private string projectID; 
        public UsersListForm(string projID, IModel _md)
        {
            InitializeComponent();
            this._model = _md;
            projectID = projID;
            //this._model.MembershipsUpdated += () => showUserList();
        }

        public void OpenView()
        {
            this.Show();
            _model.UsersListQuery(projectID, this);
        }

        public void CloseView()
        {
            this.Close();
        }

        public void showUserList()
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < _model.getMemberships().memberships.Count(); i++)
            {
                this.dataGridView1.Rows.Add(_model.getMemberships().memberships[i].User.ID,
                    _model.getMemberships().memberships[i].User.Name,
                    _model.getMemberships().memberships[i].Roles[0].Name);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.CloseView();
        }
    }
}
