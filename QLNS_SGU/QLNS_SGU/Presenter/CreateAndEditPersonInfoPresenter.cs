using System;
using QLNS_SGU.View;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Diagnostics;
using System.Configuration.Install;

namespace QLNS_SGU.Presenter
{
    public interface ICreateAndEditPersonInfoPresenter : IPresenterArgumentForCreateAndEditPersonalInFo
    {
        void LoadForm();
        void FormClosing(object sender, FormClosingEventArgs e);
    }
    public class CreateAndEditPersonInfoPresenter : ICreateAndEditPersonInfoPresenter
    {
        private static CreateAndEditPersonInfoForm _view;
        private string _maVienChucInMainForm = string.Empty;
        private int _tabOrderInRightViewMainForm = -1;
        public int rowFocusFormMainForm = -1;
        public bool checkClickGrid = false;
        public object UI => _view;
        public CreateAndEditPersonInfoPresenter(CreateAndEditPersonInfoForm view) => _view = view;
        public static void CloseForm()
        {
            _view.Close();
        }
        public void Initialize(string maVienChucInMainForm, int tabOrderInRightViewMainForm)
        {
            _view.Attach(this);
            _maVienChucInMainForm = maVienChucInMainForm;
            _tabOrderInRightViewMainForm = tabOrderInRightViewMainForm;      
        }
        public void LoadForm()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            var tabPageThongTinCaNhanPresenter = new TabPageThongTinCaNhanPresenter(new TabPageThongTinCaNhan());
            tabPageThongTinCaNhanPresenter.Initialize(_maVienChucInMainForm);
            Form frmThongTinCaNhan = (Form)tabPageThongTinCaNhanPresenter.UI;
            InitForm(frmThongTinCaNhan);
            var tabPageQuaTrinhCongTacPresenter = new TabPageQuaTrinhCongTacPresenter(new TabPageQuaTrinhCongTac1());
            tabPageQuaTrinhCongTacPresenter.Initialize(_maVienChucInMainForm);
            tabPageQuaTrinhCongTacPresenter.rowFocusFromCreateAndEditPersonalInfoForm = rowFocusFormMainForm;
            Form frmQuaTrinhCongTac = (Form)tabPageQuaTrinhCongTacPresenter.UI;
            InitForm(frmQuaTrinhCongTac);
            var tabPageQuaTrinhLuongPresenter = new TabPageQuaTrinhLuongPresenter(new TabPageQuaTrinhLuong());
            tabPageQuaTrinhLuongPresenter.Initialize(_maVienChucInMainForm);
            tabPageQuaTrinhLuongPresenter.rowFocusFromCreateAndEditPersonalInfoForm = rowFocusFormMainForm;
            Form frmQuaTrinhLuong = (Form)tabPageQuaTrinhLuongPresenter.UI;
            InitForm(frmQuaTrinhLuong);
            var tabPageChuyenMonPresenter = new TabPageChuyenMonPresenter(new TabPageChuyenMon());
            tabPageChuyenMonPresenter.Initialize(_maVienChucInMainForm);
            tabPageChuyenMonPresenter.rowFocusFromCreateAndEditPersonalInfoForm = rowFocusFormMainForm;
            tabPageChuyenMonPresenter.checkClickGridForLoadForm = checkClickGrid;
            Form frmChuyenMon = (Form)tabPageChuyenMonPresenter.UI;
            InitForm(frmChuyenMon);
            var tabPageTrangThaiPresenter = new TabPageTrangThaiPresenter(new TabPageTrangThai());
            tabPageTrangThaiPresenter.Initialize(_maVienChucInMainForm);
            tabPageTrangThaiPresenter.rowFocusFromCreateAndEditPersonalInfoForm = rowFocusFormMainForm;
            Form frmTrangThai = (Form)tabPageTrangThaiPresenter.UI;
            InitForm(frmTrangThai);
            var tabPageBaoHiemXaHoiPresenter = new TabPageBaoHiemXaHoiPresenter(new TabPageBaoHiemXaHoi());
            tabPageBaoHiemXaHoiPresenter.Initialize(_maVienChucInMainForm);
            Form frmBaoHiemXaHoi = (Form)tabPageBaoHiemXaHoiPresenter.UI;
            InitForm(frmBaoHiemXaHoi);
            var tabPhuCapThamNienNhaGiaoPresenter = new TabPagePhuCapThamNienNhaGiaoPresenter(new TabPagePhuCapThamNienNhaGiao());
            tabPhuCapThamNienNhaGiaoPresenter.Initialize(_maVienChucInMainForm);
            Form frmPhuCapThamNienNhaGiao = (Form)tabPhuCapThamNienNhaGiaoPresenter.UI;
            InitForm(frmPhuCapThamNienNhaGiao);
            frmThongTinCaNhan.Activate();
            switch (_tabOrderInRightViewMainForm)
            {
                case 0:
                    frmThongTinCaNhan.Activate();
                    break;
                case 1:
                    frmQuaTrinhCongTac.Activate();
                    break;
                case 2:
                    frmQuaTrinhLuong.Activate();
                    break;
                case 3:
                    frmChuyenMon.Activate();
                    break;
                case 4:
                    frmTrangThai.Activate();
                    break;
                case 5:
                    frmChuyenMon.Activate();
                    tabPageChuyenMonPresenter.SelectTabChungChi();
                    break;
            }
            SplashScreenManager.CloseForm(false);
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        }
        private void InitForm(Form f)
        {
            f.TopLevel = false;
            f.MdiParent = _view;
            f.Dock = DockStyle.Fill;
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }

        public void FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TabPageQuaTrinhCongTacPresenter.idFileUploadQTCT != string.Empty)
            {
                TabPageQuaTrinhCongTacPresenter.RemoveFileIfNotSaveQTCT(TabPageQuaTrinhCongTacPresenter.idFileUploadQTCT);
            }
            if (TabPageQuaTrinhCongTacPresenter.idFileUploadHD != string.Empty)
            {
                TabPageQuaTrinhCongTacPresenter.RemoveFileIfNotSaveHD(TabPageQuaTrinhCongTacPresenter.idFileUploadHD);
            }
            if (TabPageQuaTrinhLuongPresenter.idFileUpload != string.Empty)
            {
                TabPageQuaTrinhLuongPresenter.RemoveFileIfNotSave(TabPageQuaTrinhLuongPresenter.idFileUpload);
            }
            if (TabPageChuyenMonPresenter.idFileUploadHHHV != string.Empty)
            {
                TabPageChuyenMonPresenter.RemoveFileIfNotSaveHHHV(TabPageChuyenMonPresenter.idFileUploadHHHV);
            }
            if (TabPageChuyenMonPresenter.idFileUploadCC != string.Empty)
            {
                TabPageChuyenMonPresenter.RemoveFileIfNotSaveCC(TabPageChuyenMonPresenter.idFileUploadCC);
            }
            if(TabPageChuyenMonPresenter.idFileUploadN != string.Empty)
            {
                TabPageChuyenMonPresenter.RemoveFileIfNotSaveN(TabPageChuyenMonPresenter.idFileUploadN);
            }
            if(TabPageChuyenMonPresenter.idFileUploadDHNC != string.Empty)
            {
                TabPageChuyenMonPresenter.RemoveFileIfNotSaveDHNC(TabPageChuyenMonPresenter.idFileUploadDHNC);
            }
            if(TabPageTrangThaiPresenter.idFileUpload != string.Empty)
            {
                TabPageTrangThaiPresenter.RemoveFileIfNotSave(TabPageTrangThaiPresenter.idFileUpload);
            }            
            if(TabPagePhuCapThamNienNhaGiaoPresenter.idFileUpload != string.Empty)
            {
                TabPagePhuCapThamNienNhaGiaoPresenter.RemoveFileIfNotSave(TabPagePhuCapThamNienNhaGiaoPresenter.idFileUpload);
            }
        }
    }
}
