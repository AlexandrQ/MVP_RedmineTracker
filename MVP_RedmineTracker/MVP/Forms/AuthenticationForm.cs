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
    partial class AuthenticationForm : Form, IAuthenticationForm
    {
        private readonly IModel _model;
        private readonly MainForm main;
        public event Action checkAuthentication;

        public AuthenticationForm(IModel _Model, MainForm Main)
        {
            InitializeComponent();
            this._model = _Model;
            this.main = Main;
            _model.AuthenticationPassed += () => AuthenticationPassed();
            _model.AuthenticationFailed += () => AuthenticationFailed();
            _model.NoInternetConnection += () => NoInternet();
        }

        public void OpenView()
        {
            this.Show();            
        }

        public void CloseView()
        {
            this.Close();
        }

        public string getLogin()
        {
            return loginTextBox.Text;
        }

        public string getPassword()
        {
            return passwordTextBox.Text;
        }

        public void AuthenticationPassed()
        {
            Program.Context.MainForm = main;
            this.CloseView();            
        }

        public void AuthenticationFailed()
        {
            MessageBox.Show("Authentication Failed!", "Error!");
        }

        public void NoInternet()
        {
            MessageBox.Show("No Internet connection", "Error!");
        }


        private void submitButton_Click(object sender, EventArgs e)
        {
            checkAuthentication.Invoke();
        }
    }
}
