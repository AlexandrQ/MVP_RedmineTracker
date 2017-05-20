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
    public partial class NewIssueForm : Form, INewIssueForm
    {
        //public event Action Init;

        public NewIssueForm()
        {
            InitializeComponent();
        }

        public void OpenView()
        {
            this.Show();
            
        }

        public void CloseView()
        {
            //this.Hide();
            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {            
            this.CloseView();
        }
    }
}
