using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using MVP_RedmineTracker.MVP.Forms;
using RedmineTracker.MVP;

namespace MVP_RedmineTracker
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        public static ApplicationContext Context { get; set; }


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            Model ms = new Model();
            MainForm mainForm = new MainForm(ms);
            
            AuthenticationForm aForm = new AuthenticationForm(ms, mainForm);
            Context = new ApplicationContext(aForm);
            

            Presenter presenter = new Presenter(mainForm, aForm, new ProjectForm(ms),  ms);
            presenter.Run();
            Application.Run(Context);

        }
    }
}
