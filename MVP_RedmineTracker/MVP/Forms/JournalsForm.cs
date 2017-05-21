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
    partial class JournalsForm : Form, IJournalsForm
    {
        private readonly IModel _model;
        private string issueID;

        public JournalsForm(IModel imodel, string issID)
        {
            this.InitializeComponent();
            this._model = imodel;
           // this._model.JournalsReceived += () => ShowJournals();
            this.issueID = issID;
        }

        public void OpenView()
        {
            this.Show();
            this._model.JournalsQuery(issueID, this);
        }

        public void ShowJournals()
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < _model.getMyJournals().issue.Journals.Count(); i++)
            {
                for (int j = 0; j < _model.getMyJournals().issue.Journals[i].Details.Count(); j++)
                {
                    this.dataGridView1.Rows.Add(_model.getMyJournals().issue.ID, _model.getMyJournals().issue.Journals[i].ID,
                        _model.getMyJournals().issue.Journals[i].User.Name, _model.getMyJournals().issue.Journals[i].Notes,
                        _model.getMyJournals().issue.Journals[i].Created_on, _model.getMyJournals().issue.Journals[i].Details[j].Name,
                        _model.getMyJournals().issue.Journals[i].Details[j].Old_value, _model.getMyJournals().issue.Journals[i].Details[j].New_value);
                }
            }
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
