using System;
using System.Collections.Generic;
using System.Linq;
using QLNS_SGU.View;
using Model;
using Model.Entities;
using Model.ObjectModels;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Model.Helper;

namespace QLNS_SGU.Presenter
{
    public interface ITabPageQuaTrinhCongTacPresenter : IPresenterArgument
    {
        void LoadForm();
        void ExportExcelQTCT();
        void UploadFileToLocalQTCT();
        void UploadFileToGoogleDriveQTCT();
        void DownloadFileToDeviceQTCT();
        void ClickRowAndShowInfoQTCT();
        void SaveQTCT();
        void RefreshQTCT();
        void AddQTCT();
        void DeleteQTCT();
        void ChucVuChanged(object sender, EventArgs e);        
        void DonViChanged(object sender, EventArgs e);
        void ToChuyenMonChanged(object sender, EventArgs e);
        void PhanLoaiCongTacChanged(object sender, EventArgs e);
        void NgayBatDauQuaTrinhCongTacChanged(object sender, EventArgs e);
        void NgayKetThucQuaTrinhCongTacChanged(object sender, EventArgs e);
        void LoaiThayDoiChanged(object sender, EventArgs e);
        void PhanLoaiChanged(object sender, EventArgs e);
        void KiemNhiemChanged(object sender, EventArgs e);
        void LinkVanBanDinhKemQuaTrinhCongTacChanged(object sender, EventArgs e);
        void RowIndicatorQTCT(object sender, RowIndicatorCustomDrawEventArgs e);
        void ShowTrangThaiHienTai();

        void ExportExcelHD();
        void SaveHD();
        void RefreshHD();
        void AddHD();
        void DeleteHD();
        void UploadFileToLocalHD();
        void UploadFileToGoogleDriveHD();
        void DownloadFileToDeviceHD();
        void ClickRowAndShowInfoHD();
        void LoaiHopDongChanged(object sender, EventArgs e);
        void NgayBatDauHopDongChanged(object sender, EventArgs e);
        void NgayKetThucHopDongChanged(object sender, EventArgs e);
        void GhiChuHopDongChanged(object sender, EventArgs e);
        void LinkVanBanDinhKemHopDongChanged(object sender, EventArgs e);
        void RowIndicatorHD(object sender, RowIndicatorCustomDrawEventArgs e);        
    }
    public class TabPageQuaTrinhCongTacPresenter : ITabPageQuaTrinhCongTacPresenter
    {        
        //public static bool checkEmptyRowQTCTGrid = false;
        public static string maVienChucFromTabPageThongTinCaNhan = string.Empty;
        public int rowFocusFromCreateAndEditPersonalInfoForm = -1;
        private TabPageQuaTrinhCongTac1 _view;
        private static CreateAndEditPersonInfoForm _createAndEditPersonInfoForm = new CreateAndEditPersonInfoForm();
        private string GenerateCode() => Guid.NewGuid().ToString("N");
        public object UI => _view;
        public TabPageQuaTrinhCongTacPresenter(TabPageQuaTrinhCongTac1 view) => _view = view;          
        public void Initialize(string mavienchuc)
        {
            _view.Attach(this);
            _view.TXTMaVienChuc.Text = mavienchuc;
            _view.GVTabPageQuaTrinhCongTac.IndicatorWidth = 50;
            _view.GVTabPageHopDong.IndicatorWidth = 50;
        }
        private void CheckTrangThaiHienTai()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string mavienchuc = _view.TXTMaVienChuc.Text;
            if(mavienchuc != string.Empty)
            {                
                bool check = unitOfWorks.TrangThaiVienChucRepository.CheckExistsAnyRow(mavienchuc);
                if (check)
                {
                    TrangThaiVienChuc trangThaiVienChuc = unitOfWorks.TrangThaiVienChucRepository.GetTrangThaiHienTai(mavienchuc);
                    if (trangThaiVienChuc != null)
                    {
                        DateTime currentDate = DateTime.Now.Date;
                        DateTime ngayKetThuc = trangThaiVienChuc.ngayKetThuc.Value.Date;
                        if (ngayKetThuc >= currentDate || trangThaiVienChuc.ngayKetThuc == null)
                            _view.LinkLBTrangThaiHienTai.Text = trangThaiVienChuc.TrangThai.tenTrangThai;
                        if (ngayKetThuc < currentDate)
                            _view.LinkLBTrangThaiHienTai.Text = "Đang làm";
                    }
                    else
                        _view.LinkLBTrangThaiHienTai.Text = "Đang làm";
                }                
            }
            
        }        
        private void ExportExcel(GridView gv)
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            gv.ExportToXlsx(_view.SaveFileDialog.FileName);
            XtraMessageBox.Show("Xuất Excel thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void LoadForm()
        {
            LoadCbxDataQTCT();
            LoadCbxDataHD();
            _view.XtraTabControl.SelectedTabPageIndex = 0;
            string mavienchuc = _view.TXTMaVienChuc.Text;
            if(mavienchuc != string.Empty)
            {
                CheckTrangThaiHienTai();
                LoadGridTabPageQuaTrinhCongTac(mavienchuc);
                LoadGridTabPageHopDong(mavienchuc);
                if(rowFocusFromCreateAndEditPersonalInfoForm >= 0)
                {
                    _view.GVTabPageQuaTrinhCongTac.FocusedRowHandle = rowFocusFromCreateAndEditPersonalInfoForm;
                    ClickRowAndShowInfoQTCT();
                }                
            }
        }

        #region QTCT
        public static string idFileUploadQTCT = string.Empty;
        public static string maVienChucForGetListLinkVanBanDinhKemQTCT = string.Empty;
        private bool checkAddNewQTCT = true;
        private bool chucVuChanged = false;
        private bool donViChanged = false;
        private bool toChuyenMonChanged = false;
        private bool phanLoaiCongTacChanged = false;
        private bool ngayBatDauQuaTrinhCongTacChanged = false;
        private bool ngayKetThucQuaTrinhCongTacChanged = false;
        private bool loaiThayDoiChanged = false;
        private bool phanLoaiChanged = false;
        private bool kiemNhiemChanged = false;
        private bool linkVanBanDinhKemQuaTrinhCongTacChanged = false;
        
        private void LoadGridTabPageQuaTrinhCongTac(string mavienchuc)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<QuaTrinhCongTacForView> listQuaTrinhCongTac = unitOfWorks.ChucVuDonViVienChucRepository.GetListQuaTrinhCongTacForEdit(mavienchuc);
            _view.GCTabPageQuaTrinhCongTac.DataSource = listQuaTrinhCongTac;
            //if (_view.GVTabPageQuaTrinhCongTac.RowCount > 0)
            //    checkEmptyRowQTCTGrid = true;
            //else
            //    checkEmptyRowQTCTGrid = false;
        }
        private void LoadCbxDataQTCT()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<ChucVu> listChucVu = unitOfWorks.ChucVuRepository.GetListChucVu().ToList();
            _view.CBXChucVu.Properties.DataSource = listChucVu;
            _view.CBXChucVu.Properties.DisplayMember = "tenChucVu";
            _view.CBXChucVu.Properties.ValueMember = "idChucVu";
            _view.CBXChucVu.Properties.DropDownRows = listChucVu.Count;
            _view.CBXChucVu.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idChucVu", string.Empty));
            _view.CBXChucVu.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenChucVu", string.Empty));
            _view.CBXChucVu.Properties.Columns[0].Visible = false;
            List<DonVi> listDonVi = unitOfWorks.DonViRepository.GetListDonVi().ToList();
            _view.CBXDonVi.Properties.DataSource = listDonVi;
            _view.CBXDonVi.Properties.DisplayMember = "tenDonVi";
            _view.CBXDonVi.Properties.ValueMember = "idDonVi";
            _view.CBXDonVi.Properties.DropDownRows = listDonVi.Count;
            _view.CBXDonVi.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idDonVi", string.Empty));
            _view.CBXDonVi.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenDonVi", string.Empty));
            _view.CBXDonVi.Properties.Columns[0].Visible = false;
            List<ToChuyenMon> listToChuyenMon = unitOfWorks.ToChuyenMonRepository.GetListToChuyenMon().ToList();
            _view.CBXToChuyenMon.Properties.DataSource = listToChuyenMon;
            _view.CBXToChuyenMon.Properties.DisplayMember = "tenToChuyenMon";
            _view.CBXToChuyenMon.Properties.ValueMember = "idToChuyenMon";
            _view.CBXToChuyenMon.Properties.DropDownRows = listToChuyenMon.Count;
            _view.CBXToChuyenMon.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idToChuyenMon", string.Empty));
            _view.CBXToChuyenMon.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenToChuyenMon", string.Empty));
            _view.CBXToChuyenMon.Properties.Columns[0].Visible = false;
            //_view.CBXToChuyenMon.EditValue = unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(string.Empty, string.Empty);

            List<string> listPhanLoaiCongTac = unitOfWorks.ChucVuDonViVienChucRepository.GetListPhanLoaiCongTac();
            AutoCompleteStringCollection phanLoaiCongTacSource = new AutoCompleteStringCollection();
            listPhanLoaiCongTac.ForEach(x => phanLoaiCongTacSource.Add(x)); // autocompleteStringCollection if add null value app will be crashed
            _view.TXTPhanLoaiCongTac.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _view.TXTPhanLoaiCongTac.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _view.TXTPhanLoaiCongTac.MaskBox.AutoCompleteCustomSource = phanLoaiCongTacSource;

            List<string> listPhanLoai = new List<string>() { "Chức vụ quá khứ", "Một chức vụ hiện tại", "Nhiều chức vụ hiện tại", string.Empty };
            _view.CBXPhanLoai.Properties.DataSource = listPhanLoai;
            _view.CBXPhanLoai.Properties.DropDownRows = listPhanLoai.Count;
            List<string> listKiemNhiem = new List<string>() { "Có", "Không", string.Empty };
            _view.CBXKiemNhiem.Properties.DataSource = listKiemNhiem;
            _view.CBXKiemNhiem.Properties.DropDownRows = listKiemNhiem.Count;
            List<string> listLoaiThayDoi = new List<string>() { "Chưa thay đổi", "Thay đổi chức vụ", "Thay đổi đơn vị", "Thay đổi tổ bộ môn", string.Empty };
            _view.CBXLoaiThayDoi.Properties.DataSource = listLoaiThayDoi;
            _view.CBXLoaiThayDoi.Properties.DropDownRows = listLoaiThayDoi.Count;
            _view.CBXDonVi.EditValue = unitOfWorks.DonViRepository.GetIdDonVi(string.Empty);
        }
        private void SetDefaultValueControlQTCT()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            checkAddNewQTCT = true;            
            _view.CBXChucVu.ErrorText = string.Empty;
            _view.DTNgayBatDau.ErrorText = string.Empty;
            _view.CBXChucVu.EditValue = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(string.Empty);
            _view.CBXDonVi.EditValue = unitOfWorks.DonViRepository.GetIdDonVi(string.Empty);
            _view.CBXToChuyenMon.Text = string.Empty;
            _view.CBXPhanLoai.EditValue = string.Empty;
            _view.TXTHeSoChucVu.Text = string.Empty;
            _view.CBXKiemNhiem.Text = string.Empty;
            _view.TXTPhanLoaiCongTac.Text = string.Empty;
            _view.CBXLoaiThayDoi.Text = string.Empty;
            _view.DTNgayBatDau.Text = string.Empty;
            _view.DTNgayKetThuc.Text = string.Empty;
            _view.TXTLinkVanBanDinhKemQTCT.Text = string.Empty;
            
        }
        private void InsertDataQTCT()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string mavienchuc = _view.TXTMaVienChuc.Text;
            string checkphanloaicongtac = _view.CBXPhanLoai.Text;
            string kiemnhiem = _view.CBXKiemNhiem.Text;
            string loaithaydoi = _view.CBXLoaiThayDoi.Text;            
            unitOfWorks.ChucVuDonViVienChucRepository.Insert(new ChucVuDonViVienChuc
            {
                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                idChucVu = Convert.ToInt32(_view.CBXChucVu.EditValue),
                idDonVi = Convert.ToInt32(_view.CBXDonVi.EditValue),
                idToChuyenMon = _view.CBXToChuyenMon.Text != "" ? Convert.ToInt32(_view.CBXToChuyenMon.EditValue) : -1,
                checkPhanLoaiCongTac = unitOfWorks.ChucVuDonViVienChucRepository.HardCheckPhanLoaiCongTacToDatabase(checkphanloaicongtac),
                kiemNhiem = unitOfWorks.ChucVuDonViVienChucRepository.HardKiemNhiemToDatabase(kiemnhiem),
                loaiThayDoi = unitOfWorks.ChucVuDonViVienChucRepository.HardLoaiThayDoiToDatabase(loaithaydoi),
                linkVanBanDinhKem = _view.TXTLinkVanBanDinhKemQTCT.Text,
                ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayBatDau.Text),
                ngayKetThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayKetThuc.Text),
                phanLoaiCongTac = _view.TXTPhanLoaiCongTac.Text               
            });
            unitOfWorks.Save();
            LoadGridTabPageQuaTrinhCongTac(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetDefaultValueControlQTCT();
            MainPresenter.RefreshMainGridAndRightViewQuaTrinhCongTac();            
        }
        private void UpdateDataQTCT()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string checkphanloaicongtac = _view.CBXPhanLoai.EditValue.ToString();
            string kiemnhiem = _view.CBXKiemNhiem.EditValue.ToString();
            string loaithaydoi = _view.CBXLoaiThayDoi.EditValue.ToString();
            int idchucvudonvivienchuc = Convert.ToInt32(_view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("Id"));
            ChucVuDonViVienChuc chucVuDonViVienChuc = unitOfWorks.ChucVuDonViVienChucRepository.GetObjectById(idchucvudonvivienchuc);
            if (chucVuChanged)
            {
                chucVuDonViVienChuc.idChucVu = Convert.ToInt32(_view.CBXChucVu.EditValue);
                chucVuChanged = false;
            }
            if (donViChanged)
            {
                chucVuDonViVienChuc.idDonVi = Convert.ToInt32(_view.CBXDonVi.EditValue);
                donViChanged = false;
            }
            if (toChuyenMonChanged)
            {
                chucVuDonViVienChuc.idToChuyenMon = Convert.ToInt32(_view.CBXToChuyenMon.EditValue);
                toChuyenMonChanged = false;
            }
            if (phanLoaiChanged)
            {
                chucVuDonViVienChuc.checkPhanLoaiCongTac = unitOfWorks.ChucVuDonViVienChucRepository.HardCheckPhanLoaiCongTacToDatabase(checkphanloaicongtac);
                phanLoaiChanged = false;
            }
            if (kiemNhiemChanged)
            {
                chucVuDonViVienChuc.kiemNhiem = unitOfWorks.ChucVuDonViVienChucRepository.HardKiemNhiemToDatabase(kiemnhiem);
                kiemNhiemChanged = false;
            }
            if (phanLoaiCongTacChanged)
            {
                chucVuDonViVienChuc.phanLoaiCongTac = _view.TXTPhanLoaiCongTac.Text;
                phanLoaiCongTacChanged = false;
            }
            if (loaiThayDoiChanged)
            {
                chucVuDonViVienChuc.loaiThayDoi = unitOfWorks.ChucVuDonViVienChucRepository.HardLoaiThayDoiToDatabase(loaithaydoi);
                loaiThayDoiChanged = false;
            }
            if (ngayBatDauQuaTrinhCongTacChanged)
            {
                chucVuDonViVienChuc.ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayBatDau.Text);
                ngayBatDauQuaTrinhCongTacChanged = false;
            }
            if (ngayKetThucQuaTrinhCongTacChanged)
            {
                chucVuDonViVienChuc.ngayKetThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayKetThuc.Text);
                ngayKetThucQuaTrinhCongTacChanged = false;
            }
            if (linkVanBanDinhKemQuaTrinhCongTacChanged)
            {
                chucVuDonViVienChuc.linkVanBanDinhKem = _view.TXTLinkVanBanDinhKemQTCT.Text;
                linkVanBanDinhKemQuaTrinhCongTacChanged = false;
            }
            unitOfWorks.Save();
            LoadGridTabPageQuaTrinhCongTac(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainPresenter.RefreshMainGridAndRightViewQuaTrinhCongTac();
        }

        public void SelectTabCongTac() => _view.XtraTabControl.SelectedTabPageIndex = 0;
        public void ExportExcelQTCT() => ExportExcel(_view.GVTabPageQuaTrinhCongTac);

        public void UploadFileToLocalQTCT()
        {
            if (_view.GVTabPageQuaTrinhCongTac.FocusedRowHandle >= 0)
            {
                string mavienchuc = _view.TXTMaVienChuc.Text;
                _view.FolderBrowserDialog.SelectedPath = string.Empty;
                _view.OpenFileDialog.FileName = string.Empty;
                _view.OpenFileDialog.Filter = "Pdf Files|*.pdf";
                if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
                if (_view.FolderBrowserDialog.ShowDialog() == DialogResult.Cancel) return;

                FileInfo replaceOldFileName = null;
                string filename = string.Empty;
                try
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), false, false, true);
                    SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                    SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin lên Hard Drive....");
                    filename = _view.OpenFileDialog.FileName;
                    string selectedPath = _view.FolderBrowserDialog.SelectedPath.ToString();
                    string saveFileName = unitOfWorks.HardDriveFileRepository.DoUploadAndReturnLinkFileHardDrive(GenerateCode(), filename, mavienchuc, replaceOldFileName, selectedPath);
                    idFileUploadQTCT = saveFileName;
                    maVienChucForGetListLinkVanBanDinhKemQTCT = mavienchuc;
                    _view.TXTLinkVanBanDinhKemQTCT.Text = string.Empty;
                    _view.TXTLinkVanBanDinhKemQTCT.Text = saveFileName;
                    SplashScreenManager.CloseForm();
                    XtraMessageBox.Show("Tải lên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    replaceOldFileName.MoveTo(filename);  //doi lai filename cu~
                    XtraMessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                XtraMessageBox.Show("Bạn chưa chọn dòng cần upload!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void UploadFileToGoogleDriveQTCT()
        {
            if (_view.GVTabPageQuaTrinhCongTac.FocusedRowHandle >= 0)
            {
                string mavienchuc = _view.TXTMaVienChuc.Text;
                _view.OpenFileDialog.FileName = string.Empty;
                _view.OpenFileDialog.Filter = "Pdf Files|*.pdf";
                if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                FileInfo replaceOldFileName = null;
                string filename = string.Empty;
                if (unitOfWorks.GoogleDriveFileRepository.InternetAvailable())
                {
                    try
                    {
                        SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), false, false, true);
                        SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                        SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin lên Google Drive....");
                        string code = GenerateCode(); // code xac dinh file duy nhat
                        filename = _view.OpenFileDialog.FileName;
                        string id = unitOfWorks.GoogleDriveFileRepository.DoUpLoadAndReturnIdFile(GenerateCode(), filename, mavienchuc, replaceOldFileName);
                        idFileUploadQTCT = id;
                        maVienChucForGetListLinkVanBanDinhKemQTCT = mavienchuc;
                        _view.TXTLinkVanBanDinhKemQTCT.Text = string.Empty;
                        _view.TXTLinkVanBanDinhKemQTCT.Text = "https://drive.google.com/open?id=" + id + "";
                        SplashScreenManager.CloseForm();
                        XtraMessageBox.Show("Tải lên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        replaceOldFileName.MoveTo(filename);
                        XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                XtraMessageBox.Show("Bạn chưa chọn dòng cần upload!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void DownloadFileToDeviceQTCT()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), true, true, false);
            SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
            SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin xuống thiết bị....");
            string linkvanbandinhkem = _view.TXTLinkVanBanDinhKemQTCT.Text.Trim();
            var tuple = unitOfWorks.GoogleDriveFileRepository.DoDownLoadAndReturnMessage(linkvanbandinhkem);
            SplashScreenManager.CloseForm();
            if (tuple.Item2 == 64)
                XtraMessageBox.Show(tuple.Item1, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (tuple.Item2 == 16)
                XtraMessageBox.Show(tuple.Item1, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ClickRowAndShowInfoQTCT()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            checkAddNewQTCT = false;            
            int row_handle = _view.GVTabPageQuaTrinhCongTac.FocusedRowHandle;
            if (row_handle >= 0)
            {
                string chucvu = _view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("ChucVu").ToString();
                string donvi = _view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("DonVi").ToString();
                string tochuyenmon = _view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("ToChuyenMon").ToString();
                string checkphanloaicongtac = _view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("CheckPhanLoaiCongTac").ToString();
                string hesochucvu = _view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("HeSoChucVu").ToString();
                string kiemnhiem = _view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("KiemNhiem").ToString();
                string phanloaicongtac = _view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("PhanLoaiCongTac").ToString();
                string loaithaydoi = _view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("LoaiThayDoi").ToString();
                string linkvanbandinhkem = _view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("LinkVanBanDinhKem").ToString();
                
                _view.CBXChucVu.EditValue = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(chucvu);
                _view.CBXDonVi.EditValue = unitOfWorks.DonViRepository.GetIdDonVi(donvi);
                _view.CBXToChuyenMon.EditValue = unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(tochuyenmon);
                _view.CBXPhanLoai.EditValue = checkphanloaicongtac;
                _view.CBXKiemNhiem.EditValue = kiemnhiem;
                _view.CBXLoaiThayDoi.EditValue = loaithaydoi;
                _view.TXTHeSoChucVu.Text = hesochucvu;
                _view.TXTPhanLoaiCongTac.Text = phanloaicongtac;
                _view.DTNgayBatDau.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("NgayBatDau"));
                _view.DTNgayKetThuc.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("NgayKetThuc"));
                _view.TXTLinkVanBanDinhKemQTCT.Text = linkvanbandinhkem;                
            }
        }

        public void SaveQTCT()
        {
            if (checkAddNewQTCT)
            {
                if (_view.TXTMaVienChuc.Text != string.Empty && maVienChucFromTabPageThongTinCaNhan == string.Empty)
                {                   
                    if (_view.CBXChucVu.Text == string.Empty)
                    {
                        _view.CBXChucVu.ErrorText = "Vui lòng chọn chức vụ.";
                        _view.CBXChucVu.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.DTNgayBatDau.Text == string.Empty)
                    {
                        _view.DTNgayBatDau.ErrorText = "Vui lòng chọn ngày bắt đầu.";
                        _view.DTNgayBatDau.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.CBXChucVu.Text != string.Empty && _view.DTNgayBatDau.Text != string.Empty)
                        InsertDataQTCT();
                }
                else if (_view.TXTMaVienChuc.Text == string.Empty && maVienChucFromTabPageThongTinCaNhan != string.Empty)
                {
                    _view.TXTMaVienChuc.Text = maVienChucFromTabPageThongTinCaNhan;
                    if (_view.CBXChucVu.Text == string.Empty)
                    {
                        _view.CBXChucVu.ErrorText = "Vui lòng chọn chức vụ.";
                        _view.CBXChucVu.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.DTNgayBatDau.Text == string.Empty)
                    {
                        _view.DTNgayBatDau.ErrorText = "Vui lòng chọn ngày bắt đầu.";
                        _view.DTNgayBatDau.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.CBXChucVu.Text != string.Empty && _view.DTNgayBatDau.Text != string.Empty)
                        InsertDataQTCT();
                }
                else
                    XtraMessageBox.Show("Vui lòng thêm thông tin viên chức trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int row_handle = _view.GVTabPageQuaTrinhCongTac.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    if (_view.DTNgayBatDau.Text != string.Empty)
                        UpdateDataQTCT();
                    else
                    {
                        _view.DTNgayBatDau.ErrorText = "Vui lòng chọn ngày bắt đầu.";
                        _view.DTNgayBatDau.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                }
            }            
        }

        public void RefreshQTCT() => SetDefaultValueControlQTCT();

        public void AddQTCT() => SetDefaultValueControlQTCT();

        public void DeleteQTCT()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            try
            {                
                int row_handle = _view.GVTabPageQuaTrinhCongTac.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("Id"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.ChucVuDonViVienChucRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVTabPageQuaTrinhCongTac.DeleteRow(row_handle);
                        RefreshQTCT();
                        MainPresenter.RefreshMainGridAndRightViewQuaTrinhCongTac();
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Công tác này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ChucVuChanged(object sender, EventArgs e)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            chucVuChanged = true;            
            int idchucvu = Convert.ToInt32(_view.CBXChucVu.EditValue);
            string hesochucvu = unitOfWorks.ChucVuRepository.GetHeSoChucVuByIdChucVu(idchucvu);
            _view.TXTHeSoChucVu.Text = hesochucvu;
        }

        public void DonViChanged(object sender, EventArgs e)
        {
            donViChanged = true;            
        }

        public void ToChuyenMonChanged(object sender, EventArgs e)
        {
            toChuyenMonChanged = true;
        }

        public void PhanLoaiCongTacChanged(object sender, EventArgs e)
        {
            phanLoaiCongTacChanged = true;            
        }

        public void NgayBatDauQuaTrinhCongTacChanged(object sender, EventArgs e)
        {
            ngayBatDauQuaTrinhCongTacChanged = true;
        }

        public void NgayKetThucQuaTrinhCongTacChanged(object sender, EventArgs e)
        {
            ngayKetThucQuaTrinhCongTacChanged = true;
        }

        public void LoaiThayDoiChanged(object sender, EventArgs e)
        {
            loaiThayDoiChanged = true;
        }

        public void PhanLoaiChanged(object sender, EventArgs e)
        {
            phanLoaiChanged = true;
        }

        public void KiemNhiemChanged(object sender, EventArgs e)
        {
            kiemNhiemChanged = true;
        }

        public void LinkVanBanDinhKemQuaTrinhCongTacChanged(object sender, EventArgs e)
        {
            linkVanBanDinhKemQuaTrinhCongTacChanged = true;
        }

        public void RowIndicatorQTCT(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        public void ShowTrangThaiHienTai()
        {
            if(_view.LinkLBTrangThaiHienTai.Text != string.Empty)
            {
                string mavienchuc = _view.TXTMaVienChuc.Text;
                var presenter = new TrangThaiHienTaiPresenter(new TrangThaiHienTaiForm());
                presenter.Initialize(mavienchuc);
                Form f = (Form)presenter.UI;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();
            }
        }
        #endregion
        #region HD
        public static string maVienChucForGetListLinkVanBanDinhKemHD = string.Empty;
        public static string idFileUploadHD = string.Empty;
        private bool checkAddNewHD = true;
        private bool loaiHopDongChanged = false;
        private bool ngayBatDauHopDongChanged = false;
        private bool ngayKetThucHopDongChanged = false;
        private bool ghiChuHopDongChanged = false;
        private bool linkVanBanDinhKemHopDongChanged = false;
        private void SetDefaultValueControlHD()
        {
            checkAddNewHD = true;
            _view.CBXLoaiHopDong.ErrorText = string.Empty;
            _view.CBXLoaiHopDong.Text = string.Empty;
            _view.DTNgayBatDauHD.Text = string.Empty;
            _view.DTNgayKetThucHD.Text = string.Empty;
            _view.TXTGhiChuHD.Text = string.Empty;
            _view.TXTLinkVanBanDinhKemHD.Text = string.Empty;
        }
        private void LoadGridTabPageHopDong(string mavienchuc)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<HopDongForView> listHopDong = unitOfWorks.HopDongVienChucRepository.GetListHopDongVienChuc(mavienchuc);
            _view.GCTabPageHopDong.DataSource = listHopDong;
        }
        private void LoadCbxDataHD()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<LoaiHopDong> listLoaiHopDong = unitOfWorks.LoaiHopDongRepository.GetListLoaiHopDong().ToList();
            _view.CBXLoaiHopDong.Properties.DataSource = listLoaiHopDong;
            _view.CBXLoaiHopDong.Properties.DisplayMember = "maLoaiHopDong";
            _view.CBXLoaiHopDong.Properties.ValueMember = "idLoaiHopDong";
            _view.CBXLoaiHopDong.Properties.DropDownRows = listLoaiHopDong.Count;
            _view.CBXLoaiHopDong.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idLoaiHopDong", string.Empty));
            _view.CBXLoaiHopDong.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("maLoaiHopDong", string.Empty));
            _view.CBXLoaiHopDong.Properties.Columns[0].Visible = false;
        }
        private void InsertDataHD()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string mavienchuc = _view.TXTMaVienChuc.Text;            
            unitOfWorks.HopDongVienChucRepository.Insert(new HopDongVienChuc
            {
                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                idLoaiHopDong = Convert.ToInt32(_view.CBXLoaiHopDong.EditValue),
                ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayBatDauHD.Text),
                ngayKetThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayKetThucHD.Text),
                moTa = _view.TXTGhiChuHD.Text,
                linkVanBanDinhKem = _view.TXTLinkVanBanDinhKemHD.Text
            });
            unitOfWorks.Save();
            LoadGridTabPageHopDong(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetDefaultValueControlHD();
            MainPresenter.SetValueLbHopDong();            
        }
        private void UpdateDataHD()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int idhopdongvienchuc = Convert.ToInt32(_view.GVTabPageHopDong.GetFocusedRowCellDisplayText("Id"));
            DateTime? ngaybatdau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayBatDauHD.Text);
            DateTime? ngayketthuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayKetThucHD.Text);
            HopDongVienChuc hopDongVienChuc = unitOfWorks.HopDongVienChucRepository.GetObjectById(idhopdongvienchuc);
            if (loaiHopDongChanged)
            {
                hopDongVienChuc.idLoaiHopDong = Convert.ToInt32(_view.CBXLoaiHopDong.EditValue);
                loaiHopDongChanged = false;
            }
            if (ngayBatDauHopDongChanged)
            {
                hopDongVienChuc.ngayBatDau = ngaybatdau;
                ngayBatDauHopDongChanged = false;
            }
            if (ngayKetThucHopDongChanged)
            {
                hopDongVienChuc.ngayKetThuc = ngayketthuc;
                ngayKetThucHopDongChanged = false;
            }
            if (ghiChuHopDongChanged)
            {
                hopDongVienChuc.moTa = _view.TXTGhiChuHD.Text;
                ghiChuHopDongChanged = false;
            }
            if (linkVanBanDinhKemHopDongChanged)
            {
                hopDongVienChuc.linkVanBanDinhKem = _view.TXTLinkVanBanDinhKemHD.Text;
                linkVanBanDinhKemHopDongChanged = false;
            }
            unitOfWorks.Save();
            XtraMessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadGridTabPageHopDong(_view.TXTMaVienChuc.Text);
        }

        public void SelectTabHopDong() => _view.XtraTabControl.SelectedTabPageIndex = 1;

        public void ExportExcelHD() => ExportExcel(_view.GVTabPageHopDong);

        public void RefreshHD() => SetDefaultValueControlHD();

        public void AddHD() => SetDefaultValueControlHD();

        public void SaveHD()
        {
            if (checkAddNewHD)
            {
                if (_view.TXTMaVienChuc.Text != string.Empty && maVienChucFromTabPageThongTinCaNhan == string.Empty)
                {
                    if (_view.CBXLoaiHopDong.Text != string.Empty)
                    {
                        InsertDataHD();
                    }
                    else
                    {
                        _view.CBXLoaiHopDong.ErrorText = "Vui lòng chọn hợp đồng.";
                        _view.CBXLoaiHopDong.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                }
                else if (_view.TXTMaVienChuc.Text == string.Empty && maVienChucFromTabPageThongTinCaNhan != string.Empty)
                {
                    _view.TXTMaVienChuc.Text = maVienChucFromTabPageThongTinCaNhan;
                    if (_view.CBXLoaiHopDong.Text != string.Empty)
                    {
                        InsertDataHD();
                    }
                    else
                    {
                        _view.CBXLoaiHopDong.ErrorText = "Vui lòng chọn hợp đồng.";
                        _view.CBXLoaiHopDong.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                }
                else XtraMessageBox.Show("Vui lòng thêm thông tin viên chức trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int row_handle = _view.GVTabPageHopDong.FocusedRowHandle;
                if (row_handle >= 0)
                    UpdateDataHD();
            }
        }

        public void DeleteHD()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            try
            {                
                int row_handle = _view.GVTabPageHopDong.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVTabPageHopDong.GetFocusedRowCellDisplayText("Id"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.HopDongVienChucRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVTabPageHopDong.DeleteRow(row_handle);
                        RefreshHD();
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Hợp đồng này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ClickRowAndShowInfoHD()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            checkAddNewHD = false;            
            int row_handle = _view.GVTabPageHopDong.FocusedRowHandle;
            if (row_handle >= 0)
            {
                string loaihopdong = _view.GVTabPageHopDong.GetFocusedRowCellDisplayText("LoaiHopDong").ToString();
                string ghichu = _view.GVTabPageHopDong.GetFocusedRowCellDisplayText("GhiChu").ToString();
                string linkvanbandinhkem = _view.GVTabPageHopDong.GetFocusedRowCellDisplayText("LinkVanBanDinhKem").ToString();
                _view.CBXLoaiHopDong.EditValue = unitOfWorks.LoaiHopDongRepository.GetIdLoaiHopDong(loaihopdong);
                _view.DTNgayBatDauHD.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVTabPageHopDong.GetFocusedRowCellDisplayText("NgayBatDau"));
                _view.DTNgayKetThucHD.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVTabPageHopDong.GetFocusedRowCellDisplayText("NgayKetThuc"));
                _view.TXTGhiChuHD.Text = ghichu;
                _view.TXTLinkVanBanDinhKemHD.Text = linkvanbandinhkem;
            }
        }

        public void UploadFileToLocalHD()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            if (_view.GVTabPageHopDong.FocusedRowHandle >= 0)
            {
                string mavienchuc = _view.TXTMaVienChuc.Text;
                _view.FolderBrowserDialog.SelectedPath = string.Empty;
                _view.OpenFileDialog.FileName = string.Empty;
                _view.OpenFileDialog.Filter = "Pdf Files|*.pdf";
                if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
                if (_view.FolderBrowserDialog.ShowDialog() == DialogResult.Cancel) return;
                FileInfo replaceOldFileName = null;
                string filename = string.Empty;
                try
                {
                    SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), false, false, true);
                    SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                    SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin lên Hard Drive....");
                    filename = _view.OpenFileDialog.FileName;
                    string selectedPath = _view.FolderBrowserDialog.SelectedPath.ToString();
                    string saveFileName = unitOfWorks.HardDriveFileRepository.DoUploadAndReturnLinkFileHardDrive(GenerateCode(), filename, mavienchuc, replaceOldFileName, selectedPath);
                    idFileUploadHD = saveFileName;
                    maVienChucForGetListLinkVanBanDinhKemHD = mavienchuc;
                    _view.TXTLinkVanBanDinhKemHD.Text = string.Empty;
                    _view.TXTLinkVanBanDinhKemHD.Text = saveFileName;
                    SplashScreenManager.CloseForm();
                    XtraMessageBox.Show("Tải lên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    replaceOldFileName.MoveTo(filename);  //doi lai filename cu~
                    XtraMessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                XtraMessageBox.Show("Bạn chưa chọn dòng cần upload!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void UploadFileToGoogleDriveHD()
        {
            if (_view.GVTabPageHopDong.FocusedRowHandle >= 0)
            {
                string mavienchuc = _view.TXTMaVienChuc.Text;
                _view.OpenFileDialog.FileName = string.Empty;
                _view.OpenFileDialog.Filter = "Pdf Files|*.pdf";
                if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                FileInfo replaceOldFileName = null;
                string filename = string.Empty;
                if (unitOfWorks.GoogleDriveFileRepository.InternetAvailable())
                {
                    try
                    {
                        SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), false, false, true);
                        SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                        SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin lên Google Drive....");
                        string code = GenerateCode(); // code xac dinh file duy nhat
                        filename = _view.OpenFileDialog.FileName;
                        string id = unitOfWorks.GoogleDriveFileRepository.DoUpLoadAndReturnIdFile(GenerateCode(), filename, mavienchuc, replaceOldFileName);
                        idFileUploadHD = id;
                        maVienChucForGetListLinkVanBanDinhKemHD = mavienchuc;
                        _view.TXTLinkVanBanDinhKemHD.Text = string.Empty;
                        _view.TXTLinkVanBanDinhKemHD.Text = "https://drive.google.com/open?id=" + id + "";
                        SplashScreenManager.CloseForm();
                        XtraMessageBox.Show("Tải lên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        replaceOldFileName.MoveTo(filename);
                        XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                XtraMessageBox.Show("Bạn chưa chọn dòng cần upload!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void DownloadFileToDeviceHD()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), true, true, false);
            SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
            SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin xuống thiết bị....");
            string linkvanbandinhkem = _view.TXTLinkVanBanDinhKemHD.Text.Trim();
            var tuple = unitOfWorks.GoogleDriveFileRepository.DoDownLoadAndReturnMessage(linkvanbandinhkem);
            SplashScreenManager.CloseForm();
            if (tuple.Item2 == 64)
                XtraMessageBox.Show(tuple.Item1, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (tuple.Item2 == 16)
                XtraMessageBox.Show(tuple.Item1, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void LoaiHopDongChanged(object sender, EventArgs e)
        {
            loaiHopDongChanged = true;
        }

        public void NgayBatDauHopDongChanged(object sender, EventArgs e)
        {
            ngayBatDauHopDongChanged = true;
        }

        public void NgayKetThucHopDongChanged(object sender, EventArgs e)
        {
            ngayKetThucHopDongChanged = true;
        }

        public void GhiChuHopDongChanged(object sender, EventArgs e)
        {
            ghiChuHopDongChanged = true;
        }

        public void LinkVanBanDinhKemHopDongChanged(object sender, EventArgs e)
        {
            linkVanBanDinhKemHopDongChanged = true;
        }

        public void RowIndicatorHD(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        #endregion
    }
}
