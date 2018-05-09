using System;
using System.Collections.Generic;
using QLNS_SGU.View;
using Model.Models;
using Model;
using Model.Entities;
using DevExpress.XtraSplashScreen;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using DevExpress.Utils;
using Model.ObjectModels;
using System.IO;
using System.ComponentModel;

namespace QLNS_SGU.Presenter
{
    public interface IMainPresenter : IPresenter
    {            
        void ViewPersonDetails();
        void ClosePersonDetails();
        void MouseWheelGVThongTinCaNhan(object sender, MouseEventArgs e);
        void ClickRowAndChangeInfoAtRightLayout();
        void OpenStoreImage();
        void ExportExcelMainGrid();
        void EventArrowKeysInGVMain(object sender, KeyEventArgs e);
        void OpenEditForm();
        void OpenEditFormHasId();
        void RightClickMainGrid(object sender, MouseEventArgs e);
        void RightClickQuaTrinhCongTacGrid(object sender, MouseEventArgs e);
        void RightClickQuaTrinhLuongGrid(object sender, MouseEventArgs e);
        void RightClickHocHamHocViGrid(object sender, MouseEventArgs e);
        void RightClickChungChiGrid(object sender, MouseEventArgs e);
        void RightClickTrangThaiGrid(object sender, MouseEventArgs e);
        void DownloadFileQuaTrinhCongTac();
        void DownloadFileQuaTrinhLuong();
        void DownloadFileHocHamHocVi();
        void DownloadFileChungChi();
        void DownloadFileTrangThai();                             
        void ClickRowGVQuaTrinhCongTac();
        void ClickRowGVQuaTrinhLuong();
        void ClickRowGVHocHamHocVi();
        void ClickRowGVChungChi();
        void ClickRowGVTrangThai();
        void ClickLabelTrangThai();
        void ClickLabelChuyenMon();
        void ClickLabelQuaTrinhLuong();
        void ClickLabelQuaTrinhCongTac();
        void ClickLabelThongTinCaNhan();
        void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e);
        void LoadForm(object sender, EventArgs e);
        void ClosingForm(object sender, FormClosingEventArgs e);
    }
    public class MainPresenter : IMainPresenter
    {
        private bool clickGVQuaTrinhCongTac = false;
        private bool clickGVQuaTrinhLuong = false;
        private bool clickGVHocHamHocVi = false;
        private bool clickGVChungChi = false;
        private bool clickGVTrangThai = false;
        string filename = "d:\\layoutmainform.xml";
        private static MainForm _view;
        public MainPresenter(MainForm view) => _view = view;
        public object UI => _view;  
        public static void MoveRowManaging(string mavienchuc)
        {
            int rowIndex = -1;
            for (int i = 0; i < _view.GVMain.RowCount; i++)
            {
                if(_view.GVMain.GetRowCellDisplayText(i, _view.GVMain.Columns["MaVienChuc"]) == mavienchuc)
                {
                    rowIndex = i;
					break;
                }
            }
            _view.GVMain.FocusedRowHandle = rowIndex;
        }
        public static void RefreshMainGridAndRightViewQuaTrinhCongTac()
        {
            LoadDataToMainGrid();
            int rowFocus = Convert.ToInt32(_view.TXTRowIndex.Text);
            if (_view.LCIQuaTrinhCongTac.IsHidden == false)
            {
                ShowQuaTrinhCongTac(rowFocus);
                SetValueLbChucVuAndLbDonVi(rowFocus);
            }            
            _view.GVMain.FocusedRowHandle = rowFocus;
        }
        public static void RefreshRightViewQuaTrinhLuong()
        {
            if(_view.LCIQuaTrinhLuong.IsHidden == false)
            {
                int rowFocus = Convert.ToInt32(_view.TXTRowIndex.Text);
                ShowQuaTrinhLuong(rowFocus);
                _view.GVQuaTrinhLuong.FocusedRowHandle = rowFocus;
            }
        }
        public static void RefreshRightViewTrangThai()
        {
            if (_view.LCITrangThai.IsHidden == false)
            {
                int rowFocus = Convert.ToInt32(_view.TXTRowIndex.Text);
                ShowTrangThai(rowFocus);
            }
        }
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVMain.IndicatorWidth = 50;
            _view.LayoutControl.AllowCustomization = false;
            _view.LayoutControl.Hide();
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            LoadDataToMainGrid();
            SplashScreenManager.CloseForm(false);
        }
        public void LoadForm(object sender, EventArgs e)
        {
            _view.GCMain.ForceInitialize();
            if(File.Exists(filename)) _view.GCMain.MainView.RestoreLayoutFromXml(filename);
        }
        public void ClosingForm(object sender, FormClosingEventArgs e)
        {
            _view.GCMain.MainView.SaveLayoutToXml(filename);
            Application.Exit();
        }
        public static void LoadDataToMainGrid()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            //Dictionary<int, GridViewMainData> datasource = unitOfWorks.GridViewDataRepository.LoadDataToMainGrid1();
            BindingList<GridViewMainData> listGridViewMainData = new BindingList<GridViewMainData>(unitOfWorks.GridViewDataRepository.LoadDataToMainGrid());            
            _view.GCMain.DataSource = listGridViewMainData/*datasource.Values*/;
        }       
        public static void SetValueLbHopDong()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int rowFocus = Convert.ToInt32(_view.TXTRowIndex.Text);
            string mavienchuc = _view.GVMain.GetRowCellValue(rowFocus, "MaVienChuc").ToString();
            string hopdong = unitOfWorks.HopDongVienChucRepository.GetLoaiHopDongVienChucForLbHopDong(mavienchuc);
            _view.LBHopDong.Text = "Hợp đồng " + hopdong + "   ";
        }
        private static void SetValueLbChucVuAndLbDonVi(int rowFocus)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string chucvu = _view.GVMain.GetRowCellValue(rowFocus, "ChucVu").ToString();
            string donvi = _view.GVMain.GetRowCellValue(rowFocus, "DonVi").ToString();
            if(chucvu != string.Empty)
            {
                _view.LBChucVu.Text = chucvu;
            }
            else
            {
                _view.LBChucVu.Text = "Chưa có chức vụ";
            }           
            if(donvi != string.Empty)
            {
                _view.LBDonVi.Text = donvi;
            }
            else
            {
                _view.LBDonVi.Text = "Chưa có đơn vị";
            }           
        }
        private void ChangeInfoAtRightLayout(int rowFocus)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            try
            {
                string mavienchuc = _view.GVMain.GetRowCellValue(rowFocus, "MaVienChuc").ToString();
                string ho = _view.GVMain.GetRowCellValue(rowFocus, "Ho").ToString();
                string ten = _view.GVMain.GetRowCellValue(rowFocus, "Ten").ToString();             
                _view.LBHoVaTen.Text = ho + " " + ten;
                SetValueLbChucVuAndLbDonVi(rowFocus);
                SetValueLbHopDong();
                byte[] img = unitOfWorks.ThongTinCaNhanRepository.GetImage(mavienchuc);
                if (img == null)
                {
                    _view.PICVienChuc.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(img);
                    _view.PICVienChuc.Image = Image.FromStream(ms);
                }
            }
            catch { }
        }
        private void ShowThongTinCaNhan(int rowFocus)
        {            
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            try
            {
                string mavienchuc = _view.GVMain.GetRowCellValue(rowFocus, "MaVienChuc").ToString();
                ThongTinCaNhan thongTinCaNhan = unitOfWorks.ThongTinCaNhanRepository.GetThongTinCaNhan(mavienchuc);
                List<ThongTinCaNhan> list = new List<ThongTinCaNhan>();
                list.Add(thongTinCaNhan);
                _view.GCThongTinCaNhan.DataSource = list;
            }
            catch { }          
        }
        private static void ShowQuaTrinhCongTac(int rowFocus)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            try
            {
                string mavienchuc = _view.GVMain.GetRowCellValue(rowFocus, "MaVienChuc").ToString();
                List<QuaTrinhCongTacForView> listQuaTrinhCongTac = unitOfWorks.ChucVuDonViVienChucRepository.GetListQuaTrinhCongTacForView(mavienchuc);
                _view.GCQuaTrinhCongTac.DataSource = listQuaTrinhCongTac;
            }
            catch { }
        }
        private static void ShowQuaTrinhLuong(int rowFocus)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            try
            {
                string mavienchuc = _view.GVMain.GetRowCellValue(rowFocus, "MaVienChuc").ToString();
                List<QuaTrinhLuongForView> listQuaTrinhLuongForView = unitOfWorks.QuaTrinhLuongRepository.GetListQuaTrinhLuong(mavienchuc);
                _view.GCQuaTrinhLuong.DataSource = listQuaTrinhLuongForView;
            }
            catch { }
        }
        public static void LoadGridHocHamHocViAtRightViewInMainForm()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int rowFocus = Convert.ToInt32(_view.TXTRowIndex.Text);
            string mavienchuc = _view.GVMain.GetRowCellValue(rowFocus, "MaVienChuc").ToString();
            List<HocHamHocViGridAtRightViewInMainForm> listHocHamHocVi = unitOfWorks.HocHamHocViVienChucRepository.GetListHocHamHocViGridAtRightViewInMainForm(mavienchuc);
            _view.GCHocHamHocVi.DataSource = listHocHamHocVi;
        }
        public static void LoadGridChungChi()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int rowFocus = Convert.ToInt32(_view.TXTRowIndex.Text);
            string mavienchuc = _view.GVMain.GetRowCellValue(rowFocus, "MaVienChuc").ToString();
            List<ChungChiForView> listChungChiForView = unitOfWorks.ChungChiVienChucRepository.GetListChungChiVienChuc(mavienchuc);
            _view.GCChungChi.DataSource = listChungChiForView;
        }
        private void ShowChuyenMon()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            try
            {
                LoadGridHocHamHocViAtRightViewInMainForm();
                LoadGridChungChi();
            }
            catch { }
        }
        private static void ShowTrangThai(int rowFocus)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            try
            {
                string mavienchuc = _view.GVMain.GetRowCellValue(rowFocus, "MaVienChuc").ToString();
                List<TrangThaiForView> listTrangThaiForView = unitOfWorks.TrangThaiVienChucRepository.GetListTrangThaiVienChuc(mavienchuc);
                _view.GCTrangThai.DataSource = listTrangThaiForView;
            }
            catch { }
        }
        private void Download(string linkvanbandinhkem)
        {
            if(linkvanbandinhkem != "")
            {
                string[] arr_linkvanbandinhkem = linkvanbandinhkem.Split('=');
                string idvanbandinhkem = arr_linkvanbandinhkem[1];
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                if (unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
                {
                    try
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false);
                        SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                        SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin xuống thiết bị....");
                        unitOfWorks.GoogleDriveFileRepository.DownloadFile(idvanbandinhkem);
                        SplashScreenManager.CloseForm();
                    }
                    catch
                    {
                        XtraMessageBox.Show("Tải xuống thất bại. Vui lòng kiểm tra lại đường truyền hoặc làm mới dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Tải xuống thất bại. Vui lòng kiểm tra lại đường truyền hoặc làm mới dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else XtraMessageBox.Show("Không có văn bản đính kèm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ExportExcelMainGrid()
        {
            _view.SaveFileDialog.Filter = "Excel |*.xlsx;*.xls";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _view.GCMain.ExportToXlsx(_view.SaveFileDialog.FileName);
            }
        }

        public void OpenStoreImage()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int rowFocus = Convert.ToInt32(_view.TXTRowIndex.Text);
            string mavienchuc = _view.GVMain.GetRowCellValue(rowFocus, "MaVienChuc").ToString();
            var storeImagePresenter = new StoreImagePresenter(new StoreImageForm());
            storeImagePresenter.Initialize(mavienchuc);
            Form f = (Form)storeImagePresenter.UI;
            bool checkInternet = unitOfWorks.GoogleDriveFileRepository.InternetAvailable();
            if (checkInternet == true)
            {
                f.Show();
            }
            else
            {
                XtraMessageBox.Show("Kết nối đến Google Drive thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        public void ClickRowAndChangeInfoAtRightLayout()
        {
            int rowFocus = _view.GVMain.FocusedRowHandle;
            if(rowFocus >= 0)
            {
                ChangeInfoAtRightLayout(rowFocus);
                ShowThongTinCaNhan(rowFocus);
                ShowQuaTrinhCongTac(rowFocus);
                ShowQuaTrinhLuong(rowFocus);
                ShowChuyenMon();
                ShowTrangThai(rowFocus);
                _view.TXTRowIndex.Text = rowFocus.ToString();
            }           
        }

        public void EventArrowKeysInGVMain(object sender, KeyEventArgs e)
        {
            int rowFocus = _view.GVMain.FocusedRowHandle;
            if(rowFocus >= 0)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        int temp_rowFocus = rowFocus + 1;
                        ChangeInfoAtRightLayout(temp_rowFocus);
                        ShowThongTinCaNhan(temp_rowFocus);
                        ShowQuaTrinhCongTac(temp_rowFocus);
                        ShowQuaTrinhLuong(temp_rowFocus);
                        ShowChuyenMon();
                        ShowTrangThai(temp_rowFocus);
                        _view.TXTRowIndex.Text = temp_rowFocus.ToString();
                        break;
                    case Keys.Up:
                        int temp_rowFocus1 = rowFocus - 1;
                        ChangeInfoAtRightLayout(temp_rowFocus1);
                        ShowThongTinCaNhan(temp_rowFocus1);
                        ShowQuaTrinhCongTac(temp_rowFocus1);
                        ShowQuaTrinhLuong(temp_rowFocus1);
                        ShowChuyenMon();
                        ShowTrangThai(temp_rowFocus1);
                        _view.TXTRowIndex.Text = temp_rowFocus1.ToString();
                        break;
                }
            }            
        }

        public void ViewPersonDetails()
        {
            string rowFocusText = _view.TXTRowIndex.Text;
            if(rowFocusText != string.Empty)
            {
                int rowFocus = Convert.ToInt32(rowFocusText);
                if (rowFocus >= 0)
                {
                    SplashScreenManager.ShowForm(typeof(WaitForm1));
                    _view.LCIThongTinCaNhan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _view.LCIQuaTrinhCongTac.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _view.LCIQuaTrinhLuong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _view.LCIHocHamHocVi_DangHocNangCao.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _view.LCIChungChi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _view.LCITrangThai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _view.LBThongTinCaNhan.AppearanceItemCaption.ForeColor = Color.RoyalBlue;
                    _view.LBQuaTrinhCongTac.AppearanceItemCaption.ForeColor = Color.DimGray;
                    _view.LBQuaTrinhLuong.AppearanceItemCaption.ForeColor = Color.DimGray;
                    _view.LBChuyenMon.AppearanceItemCaption.ForeColor = Color.DimGray;
                    _view.LBTrangThai.AppearanceItemCaption.ForeColor = Color.DimGray;
                    _view.LayoutControl.Show();
                    ChangeInfoAtRightLayout(rowFocus);
                    ShowThongTinCaNhan(rowFocus);
                    SplashScreenManager.CloseForm();
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng khác. Dòng này không có dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }         
        }

        public void ClickLabelThongTinCaNhan()
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            _view.LCIThongTinCaNhan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            _view.LCIQuaTrinhCongTac.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIQuaTrinhLuong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIHocHamHocVi_DangHocNangCao.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIChungChi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCITrangThai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LBThongTinCaNhan.AppearanceItemCaption.ForeColor = Color.RoyalBlue;
            _view.LBQuaTrinhCongTac.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBQuaTrinhLuong.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBChuyenMon.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBTrangThai.AppearanceItemCaption.ForeColor = Color.DimGray;
            SplashScreenManager.CloseForm();
        }

        public void ClickLabelQuaTrinhCongTac()
        {            
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            _view.LCIThongTinCaNhan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIQuaTrinhCongTac.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            _view.LCIQuaTrinhLuong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIHocHamHocVi_DangHocNangCao.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIChungChi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCITrangThai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LBThongTinCaNhan.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBQuaTrinhCongTac.AppearanceItemCaption.ForeColor = Color.RoyalBlue;
            _view.LBQuaTrinhLuong.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBChuyenMon.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBTrangThai.AppearanceItemCaption.ForeColor = Color.DimGray;
            int rowFocus = Convert.ToInt32(_view.TXTRowIndex.Text);
            ShowQuaTrinhCongTac(rowFocus);
            SplashScreenManager.CloseForm();            
        }

        public void ClickLabelChuyenMon()
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            _view.LCIThongTinCaNhan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIQuaTrinhCongTac.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIQuaTrinhLuong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIHocHamHocVi_DangHocNangCao.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            _view.LCIChungChi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            _view.LCITrangThai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LBThongTinCaNhan.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBQuaTrinhCongTac.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBQuaTrinhLuong.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBChuyenMon.AppearanceItemCaption.ForeColor = Color.RoyalBlue;
            _view.LBTrangThai.AppearanceItemCaption.ForeColor = Color.DimGray;
            ShowChuyenMon();
            SplashScreenManager.CloseForm();           
        }

        public void ClickLabelQuaTrinhLuong()
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            _view.LCIThongTinCaNhan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIQuaTrinhCongTac.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIQuaTrinhLuong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            _view.LCIHocHamHocVi_DangHocNangCao.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIChungChi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCITrangThai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LBThongTinCaNhan.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBQuaTrinhCongTac.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBQuaTrinhLuong.AppearanceItemCaption.ForeColor = Color.RoyalBlue;
            _view.LBChuyenMon.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBTrangThai.AppearanceItemCaption.ForeColor = Color.DimGray;
            int rowFocus = Convert.ToInt32(_view.TXTRowIndex.Text);
            ShowQuaTrinhLuong(rowFocus);
            SplashScreenManager.CloseForm();
        }

        public void ClickLabelTrangThai()
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            _view.LCIThongTinCaNhan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIQuaTrinhCongTac.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIQuaTrinhLuong.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIHocHamHocVi_DangHocNangCao.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCIChungChi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            _view.LCITrangThai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            _view.LBThongTinCaNhan.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBQuaTrinhCongTac.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBQuaTrinhLuong.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBChuyenMon.AppearanceItemCaption.ForeColor = Color.DimGray;
            _view.LBTrangThai.AppearanceItemCaption.ForeColor = Color.RoyalBlue;
            int rowFocus = Convert.ToInt32(_view.TXTRowIndex.Text);
            ShowTrangThai(rowFocus);
            SplashScreenManager.CloseForm();
        }

        public void RightClickMainGrid(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = _view.GVMain.CalcHitInfo(e.Location);
                int row_index = hit.RowHandle;
                _view.TXTRowIndex.Text = row_index.ToString();
                _view.PopupMenuGVMain.ShowPopup(Cursor.Position);
            }
        }

        public void ClosePersonDetails()
        {
            _view.LayoutControl.Hide();
        }        

        public void MouseWheelGVThongTinCaNhan(object sender, MouseEventArgs e)
        {
            if (!_view.GVThongTinCaNhan.PanModeActive) _view.GVThongTinCaNhan.PanModeSwitch();
            (_view.GVThongTinCaNhan.GetViewInfo() as LayoutViewInfo).PanCardArea(0, e.Delta);
            (e as DXMouseEventArgs).Handled = true;
            _view.GVThongTinCaNhan.Refresh();
            if (_view.GVThongTinCaNhan.PanModeActive) _view.GVThongTinCaNhan.PanModeSwitch();
        }
                
        private void OpenEditFormByOrder(string mavienchuc, int order, int rowFocus, bool checkClickGrid)
        {
            var createAndEditPersonInfoPresenter = new CreateAndEditPersonInfoPresenter(new CreateAndEditPersonInfoForm());
            createAndEditPersonInfoPresenter.Initialize(mavienchuc, order);
            createAndEditPersonInfoPresenter.rowFocusFormMainForm = rowFocus;
            createAndEditPersonInfoPresenter.checkClickGrid = checkClickGrid;
            Form f = (Form)createAndEditPersonInfoPresenter.UI;
            f.Height = Screen.PrimaryScreen.WorkingArea.Height;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
        }

        public void OpenEditFormHasId()
        {
            string mavienchuc = _view.GVMain.GetFocusedRowCellDisplayText("MaVienChuc").ToString();      
            if(_view.LBThongTinCaNhan.AppearanceItemCaption.ForeColor == Color.RoyalBlue)
            {
                OpenEditFormByOrder(mavienchuc, 0, -1, false);
            }
            if(_view.LBQuaTrinhCongTac.AppearanceItemCaption.ForeColor == Color.RoyalBlue)
            {
                if (clickGVQuaTrinhCongTac)
                {
                    int rowFocusGrid = _view.GVQuaTrinhCongTac.FocusedRowHandle;
                    if (rowFocusGrid >= 0)
                    {
                        OpenEditFormByOrder(mavienchuc, 1, rowFocusGrid, false);
                    }
                    else OpenEditFormByOrder(mavienchuc, 1, -1, false);
                }
                else OpenEditFormByOrder(mavienchuc, 1, -1, false);
            }
            if(_view.LBQuaTrinhLuong.AppearanceItemCaption.ForeColor == Color.RoyalBlue)
            {
                if (clickGVQuaTrinhLuong)
                {
                    int rowFocusGrid = _view.GVQuaTrinhLuong.FocusedRowHandle;
                    if (rowFocusGrid >= 0)
                    {
                        OpenEditFormByOrder(mavienchuc, 2, rowFocusGrid, false);
                    }
                    else OpenEditFormByOrder(mavienchuc, 2, -1, false);
                }
                else OpenEditFormByOrder(mavienchuc, 2, -1, false);
            }
            if(_view.LBChuyenMon.AppearanceItemCaption.ForeColor == Color.RoyalBlue)
            {
                if (clickGVHocHamHocVi && clickGVChungChi == false)
                {
                    int rowFocusGridHHHV = _view.GVHocHamHocVi.FocusedRowHandle;
                    if(rowFocusGridHHHV >= 0)
                    {
                        OpenEditFormByOrder(mavienchuc, 3, rowFocusGridHHHV, false);
                    }
                    else OpenEditFormByOrder(mavienchuc, 3, -1, false);
                }
                else if(clickGVHocHamHocVi == false && clickGVChungChi)
                {
                    int rowFocusGridCC = _view.GVChungChi.FocusedRowHandle;
                    if(rowFocusGridCC >= 0)
                    {
                        OpenEditFormByOrder(mavienchuc, 5, rowFocusGridCC, true);
                    }
                    else OpenEditFormByOrder(mavienchuc, 3, -1, false);
                }
                else OpenEditFormByOrder(mavienchuc, 3, -1, false);
            }
            if(_view.LBTrangThai.AppearanceItemCaption.ForeColor == Color.RoyalBlue)
            {
                if (clickGVTrangThai)
                {
                    int rowFocusGrid = _view.GVTrangThai.FocusedRowHandle;
                    if (rowFocusGrid >= 0)
                    {
                        OpenEditFormByOrder(mavienchuc, 4, rowFocusGrid, false);
                    }
                    else OpenEditFormByOrder(mavienchuc, 4, -1, false);
                }
                else OpenEditFormByOrder(mavienchuc, 4, -1, false);
            }         
        }

        public void OpenEditForm()
        {           
            string mavienchuc = _view.GVMain.GetFocusedRowCellDisplayText("MaVienChuc").ToString();
            var createAndEditPersonInfoPresenter = new CreateAndEditPersonInfoPresenter(new CreateAndEditPersonInfoForm());
            createAndEditPersonInfoPresenter.Initialize(mavienchuc, 0);
            Form f = (Form)createAndEditPersonInfoPresenter.UI;
            f.Height = Screen.PrimaryScreen.WorkingArea.Height;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();           
        }

        public void RightClickQuaTrinhCongTacGrid(object sender, MouseEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (e.Button == MouseButtons.Right)
            {
                var hit = gridView.CalcHitInfo(e.Location);
                int row_index = hit.RowHandle;
                _view.TXTRowIndexRightView.Text = row_index.ToString();
                _view.PopupMenuGVQuaTrinhCongTac.ShowPopup(Cursor.Position);
            }
        }
        public void RightClickQuaTrinhLuongGrid(object sender, MouseEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (e.Button == MouseButtons.Right)
            {
                var hit = gridView.CalcHitInfo(e.Location);
                int row_index = hit.RowHandle;
                _view.TXTRowIndexRightView.Text = row_index.ToString();
                _view.PopupMenuGVQuaTrinhLuong.ShowPopup(Cursor.Position);
            }
        }
        public void RightClickHocHamHocViGrid(object sender, MouseEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (e.Button == MouseButtons.Right)
            {
                var hit = gridView.CalcHitInfo(e.Location);
                int row_index = hit.RowHandle;
                _view.TXTRowIndexRightView.Text = row_index.ToString();
                _view.PopupMenuGVHocHamHocVi.ShowPopup(Cursor.Position);
            }
        }
        public void RightClickChungChiGrid(object sender, MouseEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (e.Button == MouseButtons.Right)
            {
                var hit = gridView.CalcHitInfo(e.Location);
                int row_index = hit.RowHandle;
                _view.TXTRowIndexRightView.Text = row_index.ToString();
                _view.PopupMenuGVChungChi.ShowPopup(Cursor.Position);
            }
        }
        public void RightClickTrangThaiGrid(object sender, MouseEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (e.Button == MouseButtons.Right)
            {
                var hit = gridView.CalcHitInfo(e.Location);
                int row_index = hit.RowHandle;
                _view.TXTRowIndexRightView.Text = row_index.ToString();
                _view.PopupMenuGVTrangThai.ShowPopup(Cursor.Position);
            }
        }

        public void DownloadFileQuaTrinhCongTac()
        {
            string linkvanbandinhkem = _view.GVQuaTrinhCongTac.GetFocusedRowCellDisplayText("LinkVanBanDinhKem").ToString().Trim();
            Download(linkvanbandinhkem);
        }        
        public void DownloadFileQuaTrinhLuong()
        {
            string linkvanbandinhkem = _view.GVQuaTrinhLuong.GetFocusedRowCellDisplayText("LinkVanBanDinhKem").ToString().Trim();
            Download(linkvanbandinhkem);
        }
        public void DownloadFileHocHamHocVi()
        {
            string linkvanbandinhkem = _view.GVHocHamHocVi.GetFocusedRowCellDisplayText("LinkVanBanDinhKem").ToString().Trim();
            Download(linkvanbandinhkem);
        }
        public void DownloadFileChungChi()
        {
            string linkvanbandinhkem = _view.GVChungChi.GetFocusedRowCellDisplayText("LinkVanBanDinhKem").ToString().Trim();
            Download(linkvanbandinhkem);
        }
        public void DownloadFileTrangThai()
        {
            string linkvanbandinhkem = _view.GVTrangThai.GetFocusedRowCellDisplayText("LinkVanBanDinhKem").ToString().Trim();
            Download(linkvanbandinhkem);
        }

        public void ClickRowGVQuaTrinhCongTac() => clickGVQuaTrinhCongTac = true;
        public void ClickRowGVQuaTrinhLuong() => clickGVQuaTrinhLuong = true;
        public void ClickRowGVTrangThai() => clickGVTrangThai = true;
        public void ClickRowGVHocHamHocVi()
        {
            clickGVHocHamHocVi = true;
            clickGVChungChi = false;
        }
        public void ClickRowGVChungChi()
        {
            clickGVHocHamHocVi = false;
            clickGVChungChi = true;
        }
    }
}
