using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLNS_SGU.Presenter;
using QLNS_SGU.View;

namespace QLNS_SGU
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.UserSkins.BonusSkins.Register();
            //var presenter = new LoginPresenter(new LoginForm());
            //presenter.Initialize();
            //Application.Run((Form)presenter.UI);
            var a = new ContainerPresenter(new ContainerForm());
            a.Initialize("DONG");
            Application.Run((Form)a.UI);
        }
    }
}
