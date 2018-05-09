using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLNS_SGU.View;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace QLNS_SGU.Presenter
{
    public interface IContainerPresenter : IPresenterArgument
    {
        void Logout();
        void Exit();
        void NaviLoaiNganh();
        void NaviNganhDaoTao();
        void NaviMain();
        void NaviImportData();
        void OpenCreateAndEditPersonInfoForm();
        void NaviChuyenNganh();
        void NaviLoaiDonVi();
        void NaviDonVi();
        void NaviToChuyenMon();
        void NaviNgach();
        void NaviBac();
        void NaviLoaiChucVu();
        void NaviChucVu();
        void NaviLoaiHopDong();
        void NaviLoaiHocHamHocVi();
        void NaviLoaiChungChi();
        void NaviTrangThai();
        void NaviExportDataMultiDomain();
        void NaviExportDataOneDomain();
    }
    public class ContainerPresenter : IContainerPresenter
    {
        private ContainerForm _view;
        public ContainerPresenter(ContainerForm view) => _view = view;
        public object UI => _view;

        public void Exit() => Application.Exit();

        private Form CheckExistChildForm(Type fType)
        {
            foreach (Form f in _view.MdiChildren)
            {
                if (f.GetType() == fType) // Neu Form duoc truyen vao da duoc mo
                {
                    return f;
                }
            }
            return null;
        }            

        public void Initialize(string name)
        {
            _view.Attach(this);
            _view.XinChao = "Xin chào, " + name + "   ";
            _view.WindowState = FormWindowState.Maximized;
            var presenter = new MainPresenter(new MainForm());
            //var presenter = new ExportDataPresenter(new ExportDataForm());            
            presenter.Initialize();
            Form f = (Form)presenter.UI;
            f.MdiParent = _view;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }

        public void Logout()
        {
            _view.Hide();
            var loginpresenter = new LoginPresenter(new LoginForm());
            loginpresenter.Initialize();
            Form f = (Form)loginpresenter.UI;
            f.Show();
        }        

        public void NaviMain()
        {
            Form frm = CheckExistChildForm(typeof(MainForm));
            if (frm == null)
            {
                var mainpresenter = new MainPresenter(new MainForm());
                mainpresenter.Initialize();
                Form f = (Form)mainpresenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviImportData()
        {
            Form frm = CheckExistChildForm(typeof(ImportDataForm));
            if (frm == null)
            {
                var mainpresenter = new ImportDataPresenter(new ImportDataForm());
                mainpresenter.Initialize();
                Form f = (Form)mainpresenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void OpenCreateAndEditPersonInfoForm()
        {
            string mavienchuc = "";
            var createAndEditPersonInfoPresenter = new CreateAndEditPersonInfoPresenter(new CreateAndEditPersonInfoForm());
            createAndEditPersonInfoPresenter.Initialize(mavienchuc, 0);
            Form f = (Form)createAndEditPersonInfoPresenter.UI;
            f.Height = Screen.PrimaryScreen.WorkingArea.Height;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
        }

        public void NaviLoaiNganh()
        {
            Form frm = CheckExistChildForm(typeof(LoaiNganhForm));
            if (frm == null)
            {
                var loainganhpresenter = new LoaiNganhPresenter(new LoaiNganhForm());
                loainganhpresenter.Initialize();
                Form f = (Form)loainganhpresenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviNganhDaoTao()
        {
            Form frm = CheckExistChildForm(typeof(NganhDaoTaoForm));
            if (frm == null)
            {
                var nganhdaotaopresenter = new NganhDaoTaoPresenter(new NganhDaoTaoForm());
                nganhdaotaopresenter.Initialize();
                Form f = (Form)nganhdaotaopresenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviChuyenNganh() 
        {
            Form frm = CheckExistChildForm(typeof(ChuyenNganhForm));
            if (frm == null)
            {
                var presenter = new ChuyenNganhPresenter(new ChuyenNganhForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviLoaiDonVi()
        {
            Form frm = CheckExistChildForm(typeof(LoaiDonViForm));
            if (frm == null)
            {
                var presenter = new LoaiDonViPresenter(new LoaiDonViForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviDonVi()
        {
            Form frm = CheckExistChildForm(typeof(DonViForm));
            if (frm == null)
            {
                var presenter = new DonViPresenter(new DonViForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviToChuyenMon()
        {
            Form frm = CheckExistChildForm(typeof(ToChuyenMonForm));
            if (frm == null)
            {
                var presenter = new ToChuyenMonPresenter(new ToChuyenMonForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviNgach()
        {
            Form frm = CheckExistChildForm(typeof(NgachForm));
            if (frm == null)
            {
                var presenter = new NgachPresenter(new NgachForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviBac()
        {
            Form frm = CheckExistChildForm(typeof(BacForm));
            if (frm == null)
            {
                var presenter = new BacPresenter(new BacForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviLoaiChucVu()
        {
            Form frm = CheckExistChildForm(typeof(LoaiChucVuForm));
            if (frm == null)
            {
                var presenter = new LoaiChucVuPresenter(new LoaiChucVuForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviChucVu()
        {
            Form frm = CheckExistChildForm(typeof(ChucVuForm));
            if (frm == null)
            {
                var presenter = new ChucVuPresenter(new ChucVuForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviLoaiHopDong()
        {
            Form frm = CheckExistChildForm(typeof(LoaiHopDongForm));
            if (frm == null)
            {
                var presenter = new LoaiHopDongPresenter(new LoaiHopDongForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviLoaiHocHamHocVi()
        {
            Form frm = CheckExistChildForm(typeof(LoaiHocHamHocViForm));
            if (frm == null)
            {
                var presenter = new LoaiHocHamHocViPresenter(new LoaiHocHamHocViForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviLoaiChungChi()
        {
            Form frm = CheckExistChildForm(typeof(LoaiChungChiForm));
            if (frm == null)
            {
                var presenter = new LoaiChungChiPresenter(new LoaiChungChiForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviTrangThai()
        {
            Form frm = CheckExistChildForm(typeof(TrangThaiForm));
            if (frm == null)
            {
                var presenter = new TrangThaiPresenter(new TrangThaiForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviExportDataMultiDomain()
        {
            Form frm = CheckExistChildForm(typeof(ExportDataMultiDomainForm));
            if (frm == null)
            {
                var presenter = new ExportDataMultiDomainPresenter(new ExportDataMultiDomainForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }

        public void NaviExportDataOneDomain()
        {
            Form frm = CheckExistChildForm(typeof(ExportDataOneDomainForm));
            if (frm == null)
            {
                var presenter = new ExportDataOneDomainPresenter(new ExportDataOneDomainForm());
                presenter.Initialize();
                Form f = (Form)presenter.UI;
                f.MdiParent = _view;
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.Activate();
            }
        }
    }
}