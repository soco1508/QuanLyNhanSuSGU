using System;
using QLNS_SGU.View;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Diagnostics;
using System.Configuration.Install;
using DevExpress.XtraTabbedMdi;
using System.IO;
using System.Collections.Generic;
using Model;
using Model.Entities;
using System.Linq;

namespace QLNS_SGU.Presenter
{
    public interface ICreateAndEditPersonInfoPresenter : IPresenterArgumentForCreateAndEditPersonalInFo
    {
        void LoadForm();
        void FormClosing(object sender, FormClosingEventArgs e);
        void MouseDown(object sender, MouseEventArgs e);
    }
    public class CreateAndEditPersonInfoPresenter : ICreateAndEditPersonInfoPresenter
    {
        UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
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
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
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
            var tabDanhGiaVienChucPresenter = new TabPageDanhGiaVienChucPresenter(new TabPageDanhGiaVienChuc());
            tabDanhGiaVienChucPresenter.Initialize(_maVienChucInMainForm);
            Form frmDanhGiaVienChuc = (Form)tabDanhGiaVienChucPresenter.UI;
            InitForm(frmDanhGiaVienChuc);
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
            //stopwatch.Stop();
            //TimeSpan ts = stopwatch.Elapsed;
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //ts.Hours, ts.Minutes, ts.Seconds,
            //ts.Milliseconds / 10);
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
            if (TabPageQuaTrinhCongTacPresenter.idFileUploadQTCT != string.Empty && TabPageQuaTrinhCongTacPresenter.maVienChucForGetListLinkVanBanDinhKemQTCT != string.Empty)
            {
                List<string> listLinkVanBanDinhKem = unitOfWorks.ChucVuDonViVienChucRepository.GetListLinkVanBanDinhKem(TabPageQuaTrinhCongTacPresenter.maVienChucForGetListLinkVanBanDinhKemQTCT);
                RemoveFileIfNotSave(TabPageQuaTrinhCongTacPresenter.idFileUploadQTCT, listLinkVanBanDinhKem);
            }
            if (TabPageQuaTrinhCongTacPresenter.idFileUploadHD != string.Empty && TabPageQuaTrinhCongTacPresenter.maVienChucForGetListLinkVanBanDinhKemHD != string.Empty)
            {
                List<string> listLinkVanBanDinhKem = unitOfWorks.HopDongVienChucRepository.GetListLinkVanBanDinhKem(TabPageQuaTrinhCongTacPresenter.maVienChucForGetListLinkVanBanDinhKemHD);
                RemoveFileIfNotSave(TabPageQuaTrinhCongTacPresenter.idFileUploadHD, listLinkVanBanDinhKem);
            }
            if (TabPageQuaTrinhLuongPresenter.idFileUpload != string.Empty && TabPageQuaTrinhLuongPresenter.maVienChucForGetListLinkVanBanDinhKem != string.Empty)
            {
                List<string> listLinkVanBanDinhKem = unitOfWorks.QuaTrinhLuongRepository.GetListLinkVanBanDinhKem(TabPageQuaTrinhLuongPresenter.maVienChucForGetListLinkVanBanDinhKem);
                RemoveFileIfNotSave(TabPageQuaTrinhLuongPresenter.idFileUpload, listLinkVanBanDinhKem);
            }
            if (TabPageChuyenMonPresenter.idFileUploadHHHV != string.Empty && TabPageChuyenMonPresenter.maVienChucForGetListLinkVanBanDinhKemHHHV != string.Empty)
            {
                List<string> listLinkVanBanDinhKem = unitOfWorks.HocHamHocViVienChucRepository.GetListLinkVanBanDinhKem(TabPageChuyenMonPresenter.maVienChucForGetListLinkVanBanDinhKemHHHV);
                RemoveFileIfNotSave(TabPageChuyenMonPresenter.idFileUploadHHHV, listLinkVanBanDinhKem);
            }
            if (TabPageChuyenMonPresenter.idFileUploadCC != string.Empty && TabPageChuyenMonPresenter.maVienChucForGetListLinkVanBanDinhKemCC != string.Empty)
            {
                List<string> listLinkVanBanDinhKem = unitOfWorks.ChungChiVienChucRepository.GetListLinkVanBanDinhKem(TabPageChuyenMonPresenter.maVienChucForGetListLinkVanBanDinhKemCC);
                RemoveFileIfNotSave(TabPageChuyenMonPresenter.idFileUploadCC, listLinkVanBanDinhKem);
            }
            if(TabPageChuyenMonPresenter.idFileUploadN != string.Empty && TabPageChuyenMonPresenter.maVienChucForGetListLinkVanBanDinhKemN != string.Empty)
            {
                List<string> listLinkVanBanDinhKem = unitOfWorks.NganhVienChucRepository.GetListLinkVanBanDinhKem(TabPageChuyenMonPresenter.maVienChucForGetListLinkVanBanDinhKemN);
                RemoveFileIfNotSave(TabPageChuyenMonPresenter.idFileUploadN, listLinkVanBanDinhKem);
            }
            if(TabPageChuyenMonPresenter.idFileUploadDHNC != string.Empty && TabPageChuyenMonPresenter.maVienChucForGetListLinkAnhQuyetDinh != string.Empty)
            {
                List<string> listLinkAnhQuyetDinh = unitOfWorks.DangHocNangCaoRepository.GetListLinkAnhQuyetDinh(TabPageChuyenMonPresenter.maVienChucForGetListLinkAnhQuyetDinh);
                RemoveFileIfNotSave(TabPageChuyenMonPresenter.idFileUploadDHNC, listLinkAnhQuyetDinh);
            }
            if(TabPageTrangThaiPresenter.idFileUpload != string.Empty && TabPageTrangThaiPresenter.maVienChucForGetListLinkVanBanDinhKem != string.Empty)
            {
                List<string> listLinkVanBanDinhKem = unitOfWorks.TrangThaiVienChucRepository.GetListLinkVanBanDinhKem(TabPageTrangThaiPresenter.maVienChucForGetListLinkVanBanDinhKem);
                RemoveFileIfNotSave(TabPageTrangThaiPresenter.idFileUpload, listLinkVanBanDinhKem);
            }            
            if(TabPagePhuCapThamNienNhaGiaoPresenter.idFileUpload != string.Empty && TabPagePhuCapThamNienNhaGiaoPresenter.maVienChucForGetListLinkVanBanDinhKem != string.Empty)
            {
                List<string> listLinkVanBanDinhKem = unitOfWorks.QuaTrinhPhuCapThamNienNhaGiaoRepository.GetListLinkVanBanDinhKem(TabPagePhuCapThamNienNhaGiaoPresenter.maVienChucForGetListLinkVanBanDinhKem);
                RemoveFileIfNotSave(TabPagePhuCapThamNienNhaGiaoPresenter.idFileUpload, listLinkVanBanDinhKem);
            }
            if (TabPageDanhGiaVienChucPresenter.idFileUpload != string.Empty && TabPageDanhGiaVienChucPresenter.maVienChucForGetListLinkVanBanDinhKem != string.Empty)
            {
                List<string> listLinkVanBanDinhKem = unitOfWorks.QuaTrinhDanhGiaVienChucRepository.GetListLinkVanBanDinhKem(TabPageDanhGiaVienChucPresenter.maVienChucForGetListLinkVanBanDinhKem);
                RemoveFileIfNotSave(TabPageDanhGiaVienChucPresenter.idFileUpload, listLinkVanBanDinhKem);
            }
        }

        public void MouseDown(object sender, MouseEventArgs e)
        {
            _view.Update();
            string name = _view.XtraTabbedMdiManager.SelectedPage.MdiChild.Name;
        }

        private void RemoveFileIfNotSave(string id, List<string> listLinkVanBanDinhKem)
        {
            if (listLinkVanBanDinhKem.Any())
            {
                if (id.Contains(':') && id.Contains('\\') && id.Contains('.'))
                {
                    if (!listLinkVanBanDinhKem.Any(x => x == id))
                        File.Delete(id);
                }
                else
                {
                    if (!listLinkVanBanDinhKem.Any(x => x == "https://drive.google.com/open?id=" + id + ""))
                        unitOfWorks.GoogleDriveFileRepository.DeleteFile(id);
                }
            }
        }
    }
}
