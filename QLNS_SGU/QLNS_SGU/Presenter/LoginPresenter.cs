using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLNS_SGU.View;
using Model;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.LookAndFeel;

namespace QLNS_SGU.Presenter
{
    public interface ILoginPresenter : IPresenter
    {
        void Login();
        void Cancel();
        void CheckEnterKeyPress(object sender, KeyPressEventArgs e);
        void EditTaiKhoanChanged();
        void EditMatKhauChanged();
    }
    public class LoginPresenter : ILoginPresenter
    {
        private LoginForm _view;

        public LoginPresenter(LoginForm view)
        {
            _view = view;
        }

        public object UI
        {
            get
            {
                return _view;
            }
        }        

        public void Initialize()
        {
            _view.Attach(this);
            if (Properties.Settings.Default.NhoMatKhau)
            {
                _view.TaiKhoan.Text = Properties.Settings.Default.TaiKhoan;
                _view.MatKhau.Text = Properties.Settings.Default.MatKhau;
                _view.NhoMatKhau.Checked = Properties.Settings.Default.NhoMatKhau;
            }
        }

        public void Cancel()
        {
            Application.Exit();
        }        

        private void CheckLogin()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new Model.Entities.QLNSSGU_1Entities());
            string taikhoan = _view.TaiKhoan.Text.Trim();
            string matkhau = _view.MatKhau.Text;
            if (taikhoan == String.Empty && matkhau != string.Empty)
            {
                _view.TaiKhoan.ErrorText = "Vui lòng nhập tài khoản.";
                _view.TaiKhoan.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            }
            else if (taikhoan != String.Empty && matkhau == string.Empty)
            {
                _view.MatKhau.ErrorText = "Vui lòng nhập mật khẩu.";
                _view.MatKhau.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            }
            else if (taikhoan == String.Empty && matkhau == string.Empty)
            {
                _view.TaiKhoan.ErrorText = "Vui lòng nhập tài khoản.";
                _view.MatKhau.ErrorText = "Vui lòng nhập mật khẩu.";
                _view.TaiKhoan.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                _view.MatKhau.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            }
            else
            {
                _view.SplashScreenManager.ShowWaitForm();
                _view.SplashScreenManager.SetWaitFormCaption("Vui lòng chờ");
                _view.SplashScreenManager.SetWaitFormDescription("Đang kiểm tra tài khoản....");
                bool check = unitOfWorks.QuanTriVienRepository.CheckLogin(taikhoan, matkhau);
                switch (check)
                {
                    case true:
                        RememberPassword(taikhoan, matkhau);
                        _view.SplashScreenManager.CloseWaitForm();
                        _view.Hide();
                        var containerpresenter = new ContainerPresenter(new ContainerForm());
                        containerpresenter.Initialize(_view.TaiKhoan.Text);
                        Form f = (Form)containerpresenter.UI;
                        f.Show();
                        break;
                    case false:
                        _view.SplashScreenManager.CloseWaitForm();
                        XtraMessageBox.Show("Sai tài khoản hoặc mật khẩu! Vui lòng kiểm tra lại.");
                        break;
                    default:
                        break;
                }
            }
        }

        public void Login()
        {
            CheckLogin();
        }

        public void CheckEnterKeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Return)
            {
                CheckLogin();
            }
        }

        public void EditTaiKhoanChanged()
        {
            _view.TaiKhoan.ErrorText = null;
        }

        public void EditMatKhauChanged()
        {
            _view.MatKhau.ErrorText = null;
        }

        private void RememberPassword(string taikhoan, string matkhau)
        {
            if (_view.NhoMatKhau.Checked)
            {
                Properties.Settings.Default.TaiKhoan = taikhoan;
                Properties.Settings.Default.MatKhau = matkhau;
                Properties.Settings.Default.NhoMatKhau = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.TaiKhoan = string.Empty;
                Properties.Settings.Default.MatKhau = string.Empty;
                Properties.Settings.Default.NhoMatKhau = false;
                Properties.Settings.Default.Save();
            }                       
        }
    }
}
