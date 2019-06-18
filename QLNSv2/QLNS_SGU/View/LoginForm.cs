using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLNS_SGU.Presenter;
using DevExpress.XtraSplashScreen;

namespace QLNS_SGU.View
{
    public interface ILoginForm : IView<LoginPresenter>
    {
        TextEdit TaiKhoan { get; set; }
        TextEdit MatKhau { get; }
        CheckEdit NhoMatKhau { get; set; }
        SplashScreenManager SplashScreenManager { get; set; }
    }
    public partial class LoginForm : XtraForm, ILoginForm
    {
        public LoginForm()
        {
            InitializeComponent();           
        }
    
        public TextEdit TaiKhoan
        {
            get
            {
                return txtTaiKhoan;
            }
            set
            {
                txtTaiKhoan = value;
            }
        }
    
        public TextEdit MatKhau
        {
            get
            {
                return txtMatKhau;
            }
            set
            {
                txtMatKhau = value;
            }
        }

        public CheckEdit NhoMatKhau
        {
            get
            {
                return chkBNhoMatKhau;
            }
            set
            {
                chkBNhoMatKhau = value;
            }
        }

        public SplashScreenManager SplashScreenManager
        {
            get => splashScreenManager1;
            set => splashScreenManager1 = value;
        }

        public void Attach(LoginPresenter presenter)
        {
            btnDangNhap.Click += (sender, e) => presenter.Login();
            btnHuyBo.Click += (sender, e) => presenter.Cancel();
            TaiKhoan.KeyPress += new KeyPressEventHandler(presenter.CheckEnterKeyPress);
            MatKhau.KeyPress += new KeyPressEventHandler(presenter.CheckEnterKeyPress);
            chkBNhoMatKhau.KeyPress += new KeyPressEventHandler(presenter.CheckEnterKeyPress);
            TaiKhoan.EditValueChanged += (sender, e) => presenter.EditTaiKhoanChanged();
            MatKhau.EditValueChanged += (sender, e) => presenter.EditMatKhauChanged();            
        }       
    }
}