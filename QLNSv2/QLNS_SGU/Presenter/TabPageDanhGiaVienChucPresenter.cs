using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using Model;
using Model.Entities;
using Model.Helper;
using Model.ObjectModels;
using QLNS_SGU.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS_SGU.Presenter
{
    public interface ITabPageDanhGiaVienChucPresenter : IPresenterArgument
    {
        void LoadForm();
        void KhoangThoiGianChanged(object sender, EventArgs e);
        void MucDoDanhGiaChanged(object sender, EventArgs e);
        void NgayBatDauChanged(object sender, EventArgs e);
        void NgayKetThucChanged(object sender, EventArgs e);
        void ClickRowAndShowInfo();
        void Save();
        void Refresh();
        void Add();
        void Delete();
        void ExportExcel();
        void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e);
        void UploadFileToLocal();
        void UploadFileToGoogleDrive();
        void DownloadFileToDevice();
    }
    public class TabPageDanhGiaVienChucPresenter : ITabPageDanhGiaVienChucPresenter
    {
        private TabPageDanhGiaVienChuc _view;
        private bool checkAddNew = true;
        private bool khoangThoiGianChanged = false;
        private bool ngayBatDauChanged = false;
        private bool ngayKetThucChanged = false;
        private bool mucDoDanhGiaChanged = false;        
        public static string idFileUpload = string.Empty;
        public static string maVienChucForGetListLinkVanBanDinhKem = string.Empty;
        public static string maVienChucFromTabPageThongTinCaNhan = string.Empty;
        private static CreateAndEditPersonInfoForm _createAndEditPersonInfoForm = new CreateAndEditPersonInfoForm();
        private string GenerateCode() => Guid.NewGuid().ToString("N");
        public object UI => _view;
        public TabPageDanhGiaVienChucPresenter(TabPageDanhGiaVienChuc view)
        {
            _view = view;
        }
        public void Initialize(string mavienchuc)
        {
            _view.Attach(this);
            _view.TXTMaVienChuc.Text = mavienchuc;
            
        }

        private void LoadCbxData()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());

                List<DanhMucThoiGian> listDanhMucThoiGian = unitOfWorks.DanhMucThoiGianRepository.GetListDanhMucThoiGian();
                _view.CBXKhoangThoiGian.Properties.DataSource = listDanhMucThoiGian;
                _view.CBXKhoangThoiGian.Properties.DisplayMember = "tenDanhMucThoiGian";
                _view.CBXKhoangThoiGian.Properties.ValueMember = "idDanhMucThoiGian";
                _view.CBXKhoangThoiGian.Properties.DropDownRows = listDanhMucThoiGian.Count;
                _view.CBXKhoangThoiGian.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idDanhMucThoiGian", string.Empty));
                _view.CBXKhoangThoiGian.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenDanhMucThoiGian", string.Empty));
                _view.CBXKhoangThoiGian.Properties.Columns[0].Visible = false;

                List<MucDoDanhGia> listMucDoDanhGia = unitOfWorks.MucDoDanhGiaRepository.GetListMucDoDanhGia();
                _view.CBXMucDoDanhGia.Properties.DataSource = listMucDoDanhGia;
                _view.CBXMucDoDanhGia.Properties.DisplayMember = "tenMucDoDanhGia";
                _view.CBXMucDoDanhGia.Properties.ValueMember = "idMucDoDanhGia";
                _view.CBXMucDoDanhGia.Properties.DropDownRows = listMucDoDanhGia.Count;
                _view.CBXMucDoDanhGia.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idMucDoDanhGia", string.Empty));
                _view.CBXMucDoDanhGia.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenMucDoDanhGia", string.Empty));
                _view.CBXMucDoDanhGia.Properties.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void LoadGridTabPageDanhGiaVienChuc(string mavienchuc)
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                List<QuaTrinhDanhGiaVienChucForView> list = unitOfWorks.QuaTrinhDanhGiaVienChucRepository.GetListQuaTrinhDanhGia(mavienchuc);
                _view.GCDanhGiaVienChuc.DataSource = list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void SetDefaultValueControl()
        {
            checkAddNew = true;
            _view.CBXKhoangThoiGian.Text = string.Empty;
            _view.DTNgayBatDau.Text = string.Empty;
            _view.DTNgayKetThuc.Text = string.Empty;
            _view.CBXMucDoDanhGia.Text = string.Empty;
        }
        private void InsertData()
        {
            try
            {
                string mavienchuc = _view.TXTMaVienChuc.Text;
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                unitOfWorks.QuaTrinhDanhGiaVienChucRepository.Insert(new QuaTrinhDanhGiaVienChuc
                {
                    idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                    idDanhMucThoiGian = Convert.ToInt32(_view.CBXKhoangThoiGian.EditValue),
                    ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayBatDau.Text),
                    ngayKetThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayKetThuc.Text),
                    idMucDoDanhGia = Convert.ToInt32(_view.CBXMucDoDanhGia.EditValue)
                });
                unitOfWorks.Save();
                LoadGridTabPageDanhGiaVienChuc(_view.TXTMaVienChuc.Text);
                XtraMessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetDefaultValueControl();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void UpdateData()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            try
            {
                int id = Convert.ToInt32(_view.GVDanhGiaVienChuc.GetFocusedRowCellDisplayText("Id"));
                DateTime? ngaybatdau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayBatDau.Text);
                DateTime? ngayketthuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayKetThuc.Text);
                QuaTrinhDanhGiaVienChuc quaTrinhDanhGia = unitOfWorks.QuaTrinhDanhGiaVienChucRepository.GetObjectById(id);
                if (khoangThoiGianChanged)
                {
                    quaTrinhDanhGia.idDanhMucThoiGian = Convert.ToInt32(_view.CBXKhoangThoiGian.EditValue);
                    khoangThoiGianChanged = false;
                }
                if (ngayBatDauChanged)
                {
                    quaTrinhDanhGia.ngayBatDau = ngaybatdau;
                    ngayBatDauChanged = false;
                }
                if (ngayKetThucChanged)
                {
                    quaTrinhDanhGia.ngayKetThuc = ngayketthuc;
                    ngayKetThucChanged = false;
                }
                if (mucDoDanhGiaChanged)
                {
                    quaTrinhDanhGia.idMucDoDanhGia = Convert.ToInt32(_view.CBXMucDoDanhGia.EditValue);
                    mucDoDanhGiaChanged = false;
                }
                unitOfWorks.Save();
                LoadGridTabPageDanhGiaVienChuc(_view.TXTMaVienChuc.Text);
                XtraMessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void LoadForm()
        {
            try
            {
                LoadCbxData();
                string mavienchuc = _view.TXTMaVienChuc.Text;
                if (mavienchuc != string.Empty)
                    LoadGridTabPageDanhGiaVienChuc(mavienchuc);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void KhoangThoiGianChanged(object sender, EventArgs e)
        {
            khoangThoiGianChanged = true;
        }

        public void MucDoDanhGiaChanged(object sender, EventArgs e)
        {
            mucDoDanhGiaChanged = true;
        }

        public void NgayBatDauChanged(object sender, EventArgs e)
        {
            ngayBatDauChanged = true;
        }

        public void NgayKetThucChanged(object sender, EventArgs e)
        {
            ngayKetThucChanged = true;
        }

        public void ClickRowAndShowInfo()
        {
            checkAddNew = false;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            try
            {
                int row_handle = _view.GVDanhGiaVienChuc.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    string khoangthoigian = _view.GVDanhGiaVienChuc.GetFocusedRowCellDisplayText("KhoangThoiGian").ToString();
                    string mucdodanhgia = _view.GVDanhGiaVienChuc.GetFocusedRowCellDisplayText("MucDoDanhGia").ToString();
                    _view.CBXKhoangThoiGian.EditValue = unitOfWorks.DanhMucThoiGianRepository.GetIdByName(khoangthoigian);
                    _view.CBXMucDoDanhGia.EditValue = unitOfWorks.MucDoDanhGiaRepository.GetIdByName(mucdodanhgia);
                    _view.DTNgayBatDau.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVDanhGiaVienChuc.GetFocusedRowCellDisplayText("NgayBatDau"));
                    _view.DTNgayKetThuc.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVDanhGiaVienChuc.GetFocusedRowCellDisplayText("NgayKetThuc"));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Save()
        {
            try
            {
                if (checkAddNew)
                {
                    if (_view.TXTMaVienChuc.Text != string.Empty && maVienChucFromTabPageThongTinCaNhan == string.Empty)
                    {
                        InsertData();
                    }
                    else if (_view.TXTMaVienChuc.Text == string.Empty && maVienChucFromTabPageThongTinCaNhan != string.Empty)
                    {
                        _view.TXTMaVienChuc.Text = maVienChucFromTabPageThongTinCaNhan;
                        InsertData();
                    }
                    else XtraMessageBox.Show("Vui lòng thêm thông tin viên chức trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int row_handle = _view.GVDanhGiaVienChuc.FocusedRowHandle;
                    if (row_handle >= 0)
                        UpdateData();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Refresh() => SetDefaultValueControl();

        public void Add() => SetDefaultValueControl();

        public void Delete()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVDanhGiaVienChuc.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVDanhGiaVienChuc.GetFocusedRowCellDisplayText("Id"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.QuaTrinhDanhGiaVienChucRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVDanhGiaVienChuc.DeleteRow(row_handle);
                        Refresh();
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Quá trình đánh giá này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            try
            {
                _view.SaveFileDialog.FileName = string.Empty;
                _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
                _view.GVDanhGiaVienChuc.ExportToXlsx(_view.SaveFileDialog.FileName);
                XtraMessageBox.Show("Xuất Excel thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        public void UploadFileToLocal()
        {
            if (_view.GVDanhGiaVienChuc.FocusedRowHandle >= 0)
            {
                string mavienchuc = _view.TXTMaVienChuc.Text;
                _view.FolderBrowserDialog.SelectedPath = string.Empty;
                _view.OpenFileDialog.FileName = string.Empty;
                _view.OpenFileDialog.Filter = "Pdf Files|*.pdf";
                if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
                if (_view.FolderBrowserDialog.ShowDialog() == DialogResult.Cancel) return;

                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
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
                    idFileUpload = saveFileName;
                    maVienChucForGetListLinkVanBanDinhKem = mavienchuc;
                    _view.TXTLinkVanBanDinhKem.Text = string.Empty;
                    _view.TXTLinkVanBanDinhKem.Text = saveFileName;
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

        public void UploadFileToGoogleDrive()
        {
            if (_view.GVDanhGiaVienChuc.FocusedRowHandle >= 0)
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
                        idFileUpload = id;
                        maVienChucForGetListLinkVanBanDinhKem = mavienchuc;
                        _view.TXTLinkVanBanDinhKem.Text = string.Empty;
                        _view.TXTLinkVanBanDinhKem.Text = "https://drive.google.com/open?id=" + id + "";
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

        public void DownloadFileToDevice()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            try
            {
                SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), true, true, false);
                SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin xuống thiết bị....");
                string linkvanbandinhkem = _view.TXTLinkVanBanDinhKem.Text.Trim();
                var tuple = unitOfWorks.GoogleDriveFileRepository.DoDownLoadAndReturnMessage(linkvanbandinhkem);
                SplashScreenManager.CloseForm();
                if (tuple.Item2 == 64)
                    XtraMessageBox.Show(tuple.Item1, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (tuple.Item2 == 16)
                    XtraMessageBox.Show(tuple.Item1, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
