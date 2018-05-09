using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLNS_SGU.View;
using Model;
using Model.Entities;
using Model.ObjectModels;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace QLNS_SGU.Presenter
{
    public interface ITabPageQuaTrinhCongTacPresenter : IPresenterArgument
    {
        void LoadForm();
        void ExportExcelQTCT();
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
        void NhanXetChanged(object sender, EventArgs e);
        void RowIndicatorQTCT(object sender, RowIndicatorCustomDrawEventArgs e);
        void ShowTrangThaiHienTai();

        void ExportExcelHD();
        void SaveHD();
        void RefreshHD();
        void AddHD();
        void DeleteHD();
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
        public static bool checkEmptyRowQTCTGrid = false;
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
            string mavienchuc = _view.TXTMaVienChuc.Text;
            if(mavienchuc != string.Empty)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                bool check = unitOfWorks.TrangThaiVienChucRepository.CheckExistsAnyRow(mavienchuc);
                if (check)
                {
                    TrangThaiVienChuc trangThaiVienChuc = unitOfWorks.TrangThaiVienChucRepository.GetTrangThaiHienTai(mavienchuc);
                    if (trangThaiVienChuc != null)
                    {
                        DateTime currentDate = DateTime.Now.Date;
                        DateTime ngayKetThuc = trangThaiVienChuc.ngayKetThuc.Value.Date;
                        if (ngayKetThuc >= currentDate || trangThaiVienChuc.ngayKetThuc == null)
                        {
                            _view.LinkLBTrangThaiHienTai.Text = trangThaiVienChuc.TrangThai.tenTrangThai;
                        }
                        if (ngayKetThuc < currentDate)
                        {
                            _view.LinkLBTrangThaiHienTai.Text = "Đang làm";
                        }
                    }
                    else _view.LinkLBTrangThaiHienTai.Text = "Đang làm";
                }                
            }
            
        }
        private void Download(string linkvanbandinhkem)
        {
            if(linkvanbandinhkem != string.Empty)
            {
                string[] arr_linkvanbandinhkem = linkvanbandinhkem.Split('=');
                string idvanbandinhkem = arr_linkvanbandinhkem[1];
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                if (unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
                {
                    try
                    {
                        SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), true, true, false);
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
        private static string maVienChucForGetListLinkVanBanDinhKemQTCT = string.Empty;
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
        private bool nhanXetChanged = false;
        private void LoadGridTabPageQuaTrinhCongTac(string mavienchuc)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<QuaTrinhCongTacForView> listQuaTrinhCongTac = unitOfWorks.ChucVuDonViVienChucRepository.GetListQuaTrinhCongTacForEdit(mavienchuc);
            _view.GCTabPageQuaTrinhCongTac.DataSource = listQuaTrinhCongTac;
            if (_view.GVTabPageQuaTrinhCongTac.RowCount > 0)
            {
                checkEmptyRowQTCTGrid = true;
            }
            else checkEmptyRowQTCTGrid = false;
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
            _view.CBXToChuyenMon.EditValue = unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(string.Empty, string.Empty);

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
            checkAddNewQTCT = true;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            _view.CBXChucVu.ErrorText = string.Empty;
            _view.DTNgayBatDau.ErrorText = string.Empty;
            _view.CBXChucVu.EditValue = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(string.Empty);
            _view.CBXDonVi.EditValue = unitOfWorks.DonViRepository.GetIdDonVi(string.Empty);
            _view.CBXToChuyenMon.EditValue = unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(string.Empty, string.Empty);
            _view.CBXPhanLoai.EditValue = string.Empty;
            _view.TXTHeSoChucVu.Text = string.Empty;
            _view.CBXKiemNhiem.Text = string.Empty;
            _view.TXTPhanLoaiCongTac.Text = string.Empty;
            _view.CBXLoaiThayDoi.Text = string.Empty;
            _view.DTNgayBatDau.Text = string.Empty;
            _view.DTNgayKetThuc.Text = string.Empty;
            _view.TXTLinkVanBanDinhKem.Text = string.Empty;
            _view.TXTNhanXet.Text = string.Empty;
        }
        private void InsertDataQTCT()
        {
            string mavienchuc = _view.TXTMaVienChuc.Text;
            string checkphanloaicongtac = _view.CBXPhanLoai.Text;
            string kiemnhiem = _view.CBXKiemNhiem.Text;
            string loaithaydoi = _view.CBXLoaiThayDoi.Text;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            unitOfWorks.ChucVuDonViVienChucRepository.Insert(new ChucVuDonViVienChuc
            {
                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                idChucVu = Convert.ToInt32(_view.CBXChucVu.EditValue),
                idDonVi = Convert.ToInt32(_view.CBXDonVi.EditValue),
                idToChuyenMon = Convert.ToInt32(_view.CBXToChuyenMon.EditValue),
                checkPhanLoaiCongTac = unitOfWorks.ChucVuDonViVienChucRepository.HardCheckPhanLoaiCongTacToDatabase(checkphanloaicongtac),
                kiemNhiem = unitOfWorks.ChucVuDonViVienChucRepository.HardKiemNhiemToDatabase(kiemnhiem),
                loaiThayDoi = unitOfWorks.ChucVuDonViVienChucRepository.HardLoaiThayDoiToDatabase(loaithaydoi),
                linkVanBanDinhKem = _view.TXTLinkVanBanDinhKem.Text,
                ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayBatDau.Text),
                ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayKetThuc.Text),
                phanLoaiCongTac = _view.TXTPhanLoaiCongTac.Text,
                nhanXet = _view.TXTNhanXet.Text
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
                chucVuDonViVienChuc.ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayBatDau.Text);
                ngayBatDauQuaTrinhCongTacChanged = false;
            }
            if (ngayKetThucQuaTrinhCongTacChanged)
            {
                chucVuDonViVienChuc.ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayKetThuc.Text);
                ngayKetThucQuaTrinhCongTacChanged = false;
            }
            if (linkVanBanDinhKemQuaTrinhCongTacChanged)
            {
                chucVuDonViVienChuc.linkVanBanDinhKem = _view.TXTLinkVanBanDinhKem.Text;
                linkVanBanDinhKemQuaTrinhCongTacChanged = false;
            }
            if (nhanXetChanged)
            {
                chucVuDonViVienChuc.nhanXet = _view.TXTNhanXet.Text;
                nhanXetChanged = false;
            }
            unitOfWorks.Save();
            LoadGridTabPageQuaTrinhCongTac(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainPresenter.RefreshMainGridAndRightViewQuaTrinhCongTac();
        }

        public void SelectTabCongTac() => _view.XtraTabControl.SelectedTabPageIndex = 0;
        public void ExportExcelQTCT() => ExportExcel(_view.GVTabPageQuaTrinhCongTac);

        public static void RemoveFileIfNotSaveQTCT(string id)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<string> listLinkVanBanDinhKem = unitOfWorks.ChucVuDonViVienChucRepository.GetListLinkVanBanDinhKem(maVienChucForGetListLinkVanBanDinhKemQTCT);
            if (listLinkVanBanDinhKem.Any(x => x.Equals("https://drive.google.com/open?id=" + id + "")) == false)
            {
                unitOfWorks.GoogleDriveFileRepository.DeleteFile(id);
            }
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
                if (unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
                {
                    try
                    {
                        SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), false, false, true);
                        SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                        SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin lên Google Drive....");
                        string code = GenerateCode(); // code xac dinh file duy nhat
                        string filename = _view.OpenFileDialog.FileName;
                        string[] temp = filename.Split('\\');
                        string[] split_filename = filename.Split('.');
                        string new_filename = split_filename[0] + "-" + mavienchuc + "-" + code + "." + split_filename[1];
                        FileInfo fileInfo = new FileInfo(filename);
                        fileInfo.MoveTo(new_filename);
                        unitOfWorks.GoogleDriveFileRepository.UploadFile(new_filename);
                        FileInfo fileInfo1 = new FileInfo(new_filename); //doi lai filename cu~
                        fileInfo1.MoveTo(filename);
                        string id = unitOfWorks.GoogleDriveFileRepository.GetIdDriveFile(mavienchuc, code);
                        idFileUploadQTCT = id;
                        maVienChucForGetListLinkVanBanDinhKemQTCT = mavienchuc;
                        _view.TXTLinkVanBanDinhKem.Text = string.Empty;
                        _view.TXTLinkVanBanDinhKem.Text = "https://drive.google.com/open?id=" + id + "";
                        SplashScreenManager.CloseForm();
                    }
                    catch
                    {
                        XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn dòng cần upload!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DownloadFileToDeviceQTCT()
        {
            string linkvanbandinhkem = _view.TXTLinkVanBanDinhKem.ToString().Trim();
            Download(linkvanbandinhkem);
        }

        public void ClickRowAndShowInfoQTCT()
        {
            checkAddNewQTCT = false;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
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
                string nhanxet = _view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("NhanXet").ToString();
                _view.CBXChucVu.EditValue = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(chucvu);
                _view.CBXDonVi.EditValue = unitOfWorks.DonViRepository.GetIdDonVi(donvi);
                _view.CBXToChuyenMon.EditValue = unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(donvi, tochuyenmon);
                _view.CBXPhanLoai.EditValue = checkphanloaicongtac;
                _view.CBXKiemNhiem.EditValue = kiemnhiem;
                _view.CBXLoaiThayDoi.EditValue = loaithaydoi;
                _view.TXTHeSoChucVu.Text = hesochucvu;
                _view.TXTPhanLoaiCongTac.Text = phanloaicongtac;
                _view.DTNgayBatDau.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("NgayBatDau"));
                _view.DTNgayKetThuc.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVTabPageQuaTrinhCongTac.GetFocusedRowCellDisplayText("NgayKetThuc"));
                _view.TXTLinkVanBanDinhKem.Text = linkvanbandinhkem;
                _view.TXTNhanXet.Text = nhanxet;
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
                    {
                        InsertDataQTCT();
                    }
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
                    {
                        InsertDataQTCT();
                    }
                }
                else XtraMessageBox.Show("Vui lòng thêm thông tin viên chức trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int row_handle = _view.GVTabPageQuaTrinhCongTac.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    if (_view.DTNgayBatDau.Text != string.Empty)
                    {
                        UpdateDataQTCT();
                    }
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
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
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
            chucVuChanged = true;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int idchucvu = Convert.ToInt32(_view.CBXChucVu.EditValue);
            string hesochucvu = unitOfWorks.ChucVuRepository.GetHeSoChucVuByIdChucVu(idchucvu);
            _view.TXTHeSoChucVu.Text = hesochucvu;
        }

        public void DonViChanged(object sender, EventArgs e)
        {
            donViChanged = true;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int iddonvi = Convert.ToInt32(_view.CBXDonVi.EditValue);
            _view.CBXToChuyenMon.Properties.DataSource = null;
            List<ToChuyenMon> listToChuyenMon = unitOfWorks.ToChuyenMonRepository.GetListToChuyenMonByDonVi(iddonvi);
            _view.CBXToChuyenMon.Properties.DataSource = listToChuyenMon;
            _view.CBXToChuyenMon.Properties.DisplayMember = "tenToChuyenMon";
            _view.CBXToChuyenMon.Properties.ValueMember = "idToChuyenMon";
            _view.CBXToChuyenMon.Properties.DropDownRows = listToChuyenMon.Count;
            _view.CBXToChuyenMon.Properties.Columns[0].Visible = false;
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

        public void NhanXetChanged(object sender, EventArgs e)
        {
            nhanXetChanged = true;
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
        private static string maVienChucForGetListLinkVanBanDinhKemHD = string.Empty;
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
            _view.CBXLoaiHopDong.Properties.DisplayMember = "tenLoaiHopDong";
            _view.CBXLoaiHopDong.Properties.ValueMember = "idLoaiHopDong";
            _view.CBXLoaiHopDong.Properties.DropDownRows = listLoaiHopDong.Count;
            _view.CBXLoaiHopDong.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idLoaiHopDong", string.Empty));
            _view.CBXLoaiHopDong.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenLoaiHopDong", string.Empty));
            _view.CBXLoaiHopDong.Properties.Columns[0].Visible = false;
        }
        private void InsertDataHD()
        {
            string mavienchuc = _view.TXTMaVienChuc.Text;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            unitOfWorks.HopDongVienChucRepository.Insert(new HopDongVienChuc
            {
                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                idLoaiHopDong = Convert.ToInt32(_view.CBXLoaiHopDong.EditValue),
                ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayBatDauHD.Text),
                ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayKetThucHD.Text),
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
            DateTime? ngaybatdau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayBatDauHD.Text);
            DateTime? ngayketthuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayKetThucHD.Text);
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
                {
                    UpdateDataHD();
                }
            }
        }

        public void DeleteHD()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
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
            checkAddNewHD = false;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
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

        public static void RemoveFileIfNotSaveHD(string id)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<string> listLinkVanBanDinhKem = unitOfWorks.HopDongVienChucRepository.GetListLinkVanBanDinhKem(maVienChucForGetListLinkVanBanDinhKemHD);
            if (listLinkVanBanDinhKem.Any(x => x.Equals("https://drive.google.com/open?id=" + id + "")) == false)
            {
                unitOfWorks.GoogleDriveFileRepository.DeleteFile(id);
            }
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
                if (unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
                {
                    try
                    {
                        SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), false, false, true);
                        SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                        SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin lên Google Drive....");
                        string code = GenerateCode(); // code xac dinh file duy nhat
                        string filename = _view.OpenFileDialog.FileName;
                        string[] temp = filename.Split('\\');
                        string[] split_filename = filename.Split('.');
                        string new_filename = split_filename[0] + "-" + mavienchuc + "-" + code + "." + split_filename[1];
                        FileInfo fileInfo = new FileInfo(filename);
                        fileInfo.MoveTo(new_filename);
                        unitOfWorks.GoogleDriveFileRepository.UploadFile(new_filename);
                        FileInfo fileInfo1 = new FileInfo(new_filename); //doi lai filename cu~
                        fileInfo1.MoveTo(filename);
                        string id = unitOfWorks.GoogleDriveFileRepository.GetIdDriveFile(mavienchuc, code);
                        idFileUploadHD = id;
                        maVienChucForGetListLinkVanBanDinhKemHD = mavienchuc;
                        _view.TXTLinkVanBanDinhKemHD.Text = string.Empty;
                        _view.TXTLinkVanBanDinhKemHD.Text = "https://drive.google.com/open?id=" + id + "";
                        SplashScreenManager.CloseForm();
                    }
                    catch
                    {
                        XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn dòng cần upload!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DownloadFileToDeviceHD()
        {
            string linkvanbandinhkem = _view.TXTLinkVanBanDinhKemHD.ToString().Trim();
            Download(linkvanbandinhkem);
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
