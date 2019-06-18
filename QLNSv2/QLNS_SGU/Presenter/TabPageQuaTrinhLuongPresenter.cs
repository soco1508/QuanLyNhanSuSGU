using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLNS_SGU.View;
using DevExpress.XtraEditors;
using Model;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.IO;
using Model.Entities;
using Model.ObjectModels;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;
using Model.Helper;

namespace QLNS_SGU.Presenter
{
    public interface ITabPageQuaTrinhLuongPresenter : IPresenterArgument
    {
        //void LoadForm();
        void ClickRowAndShowInfo();
        void UploadFileToLocal();
        void UploadFileToGoogleDrive();
        void DownloadFileToDevice();
        void Save();
        void Refresh();
        void Add();
        void Delete();
        void BacChanged(object sender, EventArgs e);
        void ExportExcel();
        void MaNgachChanged(object sender, EventArgs e);
        void TenNgachChanged(object sender, EventArgs e);
        void NgayBatDauChanged(object sender, EventArgs e);
        void NgayLenLuongChanged(object sender, EventArgs e);
        void DangHuongLuongChanged(object sender, EventArgs e);
        void TruocHanChanged(object sender, EventArgs e);
        void HeSoVuotKhungChanged(object sender, EventArgs e);
        void LinkVanBanDinhKemChanged(object sender, EventArgs e);
        void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e);        
    }
    public class TabPageQuaTrinhLuongPresenter : ITabPageQuaTrinhLuongPresenter
    {
        UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
        public static string idFileUpload = string.Empty;
        public static string maVienChucFromTabPageThongTinCaNhan = string.Empty;
        public static string maVienChucForGetListLinkVanBanDinhKem = string.Empty;
        private bool checkAddNew = true;
        private bool maNgachOrTenNgachOrBacChanged = false;
        private bool ngayBatDauChanged = false;
        private bool ngayLenLuongChanged = false;
        private bool dangHuongLuongChanged = false;
        private bool truocHanChanged = false;
        private bool heSoVuotKhungChanged = false;
        private bool linkVanBanDinhKemChanged = false;
        public int rowFocusFromCreateAndEditPersonalInfoForm = -1;
        private static CreateAndEditPersonInfoForm _createAndEditPersonInfoForm = new CreateAndEditPersonInfoForm();
        private TabPageQuaTrinhLuong _view;       
        private string GenerateCode() => Guid.NewGuid().ToString("N");

        public object UI => _view;
        public TabPageQuaTrinhLuongPresenter(TabPageQuaTrinhLuong view) => _view = view;
        public void Initialize(string mavienchuc)
        {
            _view.Attach(this);
            _view.TXTMaVienChuc.Text = mavienchuc;
            _view.GVTabPageQuaTrinhLuong.IndicatorWidth = 50;
        }
        private void LoadGridTabPageQuaTrinhLuong(string mavienchuc)
        {            
            List<QuaTrinhLuongForView> listQuaTrinhLuong = unitOfWorks.QuaTrinhLuongRepository.GetListQuaTrinhLuong(mavienchuc);
            _view.GCTabPageQuaTrinhLuong.DataSource = listQuaTrinhLuong;
        }
        private void LoadCbxData()
        {
            _view.CBXMaNgach.Properties.DataSource = null;
            _view.CBXMaNgach.Properties.Columns.Clear();
            
            List<Ngach> listNgach = unitOfWorks.NgachRepository.GetListNgach().ToList();            
            _view.CBXMaNgach.Properties.DisplayMember = "maNgach";
            _view.CBXMaNgach.Properties.ValueMember = "idNgach";
            _view.CBXMaNgach.Properties.DataSource = listNgach;
            _view.CBXMaNgach.Properties.DropDownRows = listNgach.Count;
            _view.CBXMaNgach.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idNgach", string.Empty));
            _view.CBXMaNgach.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("maNgach", string.Empty));
            _view.CBXMaNgach.Properties.Columns[0].Visible = false;
            List<int> listBac = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            _view.CBXBac.Properties.DataSource = listBac;
            _view.CBXBac.Properties.DropDownRows = listBac.Count;

            AutoCompleteStringCollection tenNgachSource = new AutoCompleteStringCollection();
            listNgach.ForEach(x => tenNgachSource.Add(x.tenNgach)); // autocompleteStringCollection if add null value app will be crashed
            _view.TXTTenNgach.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _view.TXTTenNgach.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _view.TXTTenNgach.MaskBox.AutoCompleteCustomSource = tenNgachSource;
        }
        private void SetDefaultValueControl()
        {
            checkAddNew = true;
            _view.CBXMaNgach.ErrorText = string.Empty;
            _view.TXTTenNgach.Text = string.Empty;
            _view.CBXBac.ErrorText = string.Empty;
            _view.CBXMaNgach.Text = string.Empty;
            _view.CBXBac.Text = string.Empty;
            _view.DTNgayBatDau.Text = string.Empty;
            _view.DTNgayLenLuong.Text = string.Empty;
            _view.CHKDangHuongLuong.Checked = false;
            _view.TXTHeSoBac.EditValue = 0;
            _view.TXTTruocHan.EditValue = 0;
            _view.TXTHeSoVuotKhung.EditValue = 0;
            _view.TXTLinkVanBanDinhKem.Text = string.Empty;
        }
        private void InsertData()
        {
            string mavienchuc = _view.TXTMaVienChuc.Text;            
            int idngach = Convert.ToInt32(_view.CBXMaNgach.EditValue);
            int bac = Convert.ToInt32(_view.CBXBac.EditValue);
            unitOfWorks.QuaTrinhLuongRepository.Insert(new QuaTrinhLuong
            {
                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                idBac = unitOfWorks.BacRepository.GetIdBac(bac, idngach),
                ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayBatDau.Text),
                ngayLenLuong = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayLenLuong.Text),                
                dangHuongLuong = _view.CHKDangHuongLuong.Checked,
                truocHan = Convert.ToInt32(_view.TXTTruocHan.EditValue),
                heSoVuotKhung = Convert.ToDouble(_view.TXTHeSoVuotKhung.EditValue),
                linkVanBanDinhKem = _view.TXTLinkVanBanDinhKem.Text
            });
            unitOfWorks.Save();
            LoadGridTabPageQuaTrinhLuong(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainPresenter.RefreshRightViewQuaTrinhLuong();
            SetDefaultValueControl();
        }
        private void UpdateData()
        {
            
            int idquatrinhluong = Convert.ToInt32(_view.GVTabPageQuaTrinhLuong.GetFocusedRowCellDisplayText("Id"));
            DateTime? ngaybatdau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayBatDau.Text);
            DateTime? ngaylenluong = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayLenLuong.Text);
            int idngach = Convert.ToInt32(_view.CBXMaNgach.EditValue);
            int bac = Convert.ToInt32(_view.CBXBac.EditValue);
            QuaTrinhLuong quaTrinhLuong = unitOfWorks.QuaTrinhLuongRepository.GetObjectById(idquatrinhluong);
            if (maNgachOrTenNgachOrBacChanged)
            {
                quaTrinhLuong.idBac = unitOfWorks.BacRepository.GetIdBac(bac, idngach);
                maNgachOrTenNgachOrBacChanged = false;
            }
            if (ngayBatDauChanged)
            {
                quaTrinhLuong.ngayBatDau = ngaybatdau;
                ngayBatDauChanged = false;
            }
            if (ngayLenLuongChanged)
            {
                quaTrinhLuong.ngayLenLuong = ngaylenluong;
                ngayLenLuongChanged = false;
            }
            if (dangHuongLuongChanged)
            {
                quaTrinhLuong.dangHuongLuong = _view.CHKDangHuongLuong.Checked;
                dangHuongLuongChanged = false;
            }
            if (truocHanChanged)
            {
                quaTrinhLuong.truocHan = Convert.ToInt32(_view.TXTTruocHan.EditValue);
                truocHanChanged = false;
            }
            if (heSoVuotKhungChanged)
            {
                quaTrinhLuong.heSoVuotKhung = Convert.ToDouble(_view.TXTHeSoVuotKhung.EditValue);
                heSoVuotKhungChanged = false;
            }
            if (linkVanBanDinhKemChanged)
            {
                quaTrinhLuong.linkVanBanDinhKem = _view.TXTLinkVanBanDinhKem.Text;
                linkVanBanDinhKemChanged = false;
            }
            unitOfWorks.Save();
            LoadGridTabPageQuaTrinhLuong(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainPresenter.RefreshRightViewQuaTrinhLuong();       
        }        

        public void Refresh() => SetDefaultValueControl();

        public void Add() => SetDefaultValueControl();

        public void Save()
        {
            if (checkAddNew)
            {
                if (_view.TXTMaVienChuc.Text != string.Empty && maVienChucFromTabPageThongTinCaNhan == string.Empty)
                {                    
                    if (_view.CBXMaNgach.Text == string.Empty)
                    {
                        _view.CBXMaNgach.ErrorText = "Vui lòng chọn ngạch.";
                        _view.CBXMaNgach.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.CBXBac.Text == string.Empty)
                    {
                        _view.CBXBac.ErrorText = "Vui lòng chọn bậc.";
                        _view.CBXBac.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.CBXMaNgach.Text != string.Empty && _view.CBXBac.Text != string.Empty)
                    {
                        InsertData();
                    }
                }
                else if (_view.TXTMaVienChuc.Text == string.Empty && maVienChucFromTabPageThongTinCaNhan != string.Empty)
                {
                    _view.TXTMaVienChuc.Text = maVienChucFromTabPageThongTinCaNhan;                    
                    if (_view.CBXMaNgach.Text == string.Empty)
                    {
                        _view.CBXMaNgach.ErrorText = "Vui lòng chọn ngạch.";
                        _view.CBXMaNgach.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.CBXBac.Text == string.Empty)
                    {
                        _view.CBXBac.ErrorText = "Vui lòng chọn bậc.";
                        _view.CBXBac.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.CBXMaNgach.Text != string.Empty && _view.CBXBac.Text != string.Empty)
                    {
                        InsertData();
                    }
                }
                else XtraMessageBox.Show("Vui lòng thêm thông tin viên chức trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int row_handle = _view.GVTabPageQuaTrinhLuong.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    UpdateData();
                }
            }
        }

        public void Delete()
        {
            try
            {
                
                int row_handle = _view.GVTabPageQuaTrinhLuong.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVTabPageQuaTrinhLuong.GetFocusedRowCellDisplayText("Id"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.QuaTrinhLuongRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVTabPageQuaTrinhLuong.DeleteRow(row_handle);
                        Refresh();
                        MainPresenter.RefreshRightViewQuaTrinhLuong();
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Quá trình lương này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }               

        public void ClickRowAndShowInfo()
        {
            checkAddNew = false;
            
            int row_handle = _view.GVTabPageQuaTrinhLuong.FocusedRowHandle;
            if (row_handle >= 0)
            {
                string mangach = _view.GVTabPageQuaTrinhLuong.GetFocusedRowCellDisplayText("MaNgach").ToString();
                string hesobac = _view.GVTabPageQuaTrinhLuong.GetFocusedRowCellDisplayText("HeSoBac").ToString();
                bool danghuongluong = Convert.ToBoolean(_view.GVTabPageQuaTrinhLuong.GetRowCellValue(row_handle, "DangHuongLuong"));
                string truochan = _view.GVTabPageQuaTrinhLuong.GetFocusedRowCellDisplayText("TruocHan");
                string hesovuotkhung = _view.GVTabPageQuaTrinhLuong.GetFocusedRowCellDisplayText("HeSoVuotKhung");
                string linkvanbandinhkem = _view.GVTabPageQuaTrinhLuong.GetFocusedRowCellDisplayText("LinkVanBanDinhKem").ToString();
                _view.CBXMaNgach.Text = mangach;
                _view.CBXBac.EditValue = Convert.ToInt32(_view.GVTabPageQuaTrinhLuong.GetFocusedRowCellDisplayText("Bac"));
                _view.DTNgayBatDau.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVTabPageQuaTrinhLuong.GetFocusedRowCellDisplayText("NgayBatDau"));
                _view.DTNgayLenLuong.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVTabPageQuaTrinhLuong.GetFocusedRowCellDisplayText("NgayLenLuong"));
                _view.TXTHeSoBac.EditValue = hesobac;
                _view.CHKDangHuongLuong.Checked = danghuongluong;
                if(truochan != string.Empty) _view.TXTTruocHan.EditValue = Convert.ToInt32(truochan);
                if(hesovuotkhung != string.Empty) _view.TXTHeSoVuotKhung.EditValue = Convert.ToDouble(hesovuotkhung);
                _view.TXTLinkVanBanDinhKem.Text = linkvanbandinhkem;
            }
        }

        public void UploadFileToLocal()
        {
            if (_view.GVTabPageQuaTrinhLuong.FocusedRowHandle >= 0)
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
            if (_view.GVTabPageQuaTrinhLuong.FocusedRowHandle >= 0)
            {
                string mavienchuc = _view.TXTMaVienChuc.Text;
                _view.OpenFileDialog.FileName = string.Empty;
                _view.OpenFileDialog.Filter = "Pdf Files|*.pdf";
                if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;

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

        public void LoadForm()
        {
            LoadCbxData();
            string mavienchuc = _view.TXTMaVienChuc.Text;
            if (mavienchuc != string.Empty)
            {
                LoadGridTabPageQuaTrinhLuong(mavienchuc);
                if(rowFocusFromCreateAndEditPersonalInfoForm >= 0)
                {
                    _view.GVTabPageQuaTrinhLuong.FocusedRowHandle = rowFocusFromCreateAndEditPersonalInfoForm;
                    ClickRowAndShowInfo();
                }
            }
        }        

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVTabPageQuaTrinhLuong.ExportToXlsx(_view.SaveFileDialog.FileName);
            XtraMessageBox.Show("Xuất Excel thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MaNgachChanged(object sender, EventArgs e)
        {
            maNgachOrTenNgachOrBacChanged = true;
            
            int idngach = Convert.ToInt32(_view.CBXMaNgach.EditValue);
            _view.TXTTenNgach.Text = unitOfWorks.NgachRepository.GetTenNgachByIdNgach(idngach);
        }

        public void TenNgachChanged(object sender, EventArgs e)
        {
            maNgachOrTenNgachOrBacChanged = true;
            
            List<string> listTenNgach = unitOfWorks.NgachRepository.GetListTenNgach();
            string tenngach = _view.TXTTenNgach.Text;
            if(tenngach != string.Empty)
            {
                if (listTenNgach.Any(x => x == tenngach) == false)
                {
                    _view.TXTTenNgach.ErrorText = "Ngạch không tồn tại. Vui lòng nhập lại.";
                    _view.TXTTenNgach.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                else
                {
                    _view.CBXMaNgach.Properties.DataSource = null;
                    _view.CBXMaNgach.Properties.Columns.Clear();
                    _view.TXTTenNgach.ErrorText = string.Empty;
                    List<Ngach> listNgach = unitOfWorks.NgachRepository.GetListNgachByTenNgach(tenngach).ToList();
                    _view.CBXMaNgach.Properties.DisplayMember = "maNgach";
                    _view.CBXMaNgach.Properties.ValueMember = "idNgach";
                    _view.CBXMaNgach.Properties.DataSource = listNgach;
                    _view.CBXMaNgach.Properties.DropDownRows = listNgach.Count;
                    _view.CBXMaNgach.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idNgach", string.Empty));
                    _view.CBXMaNgach.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("maNgach", string.Empty));
                    _view.CBXMaNgach.Properties.Columns[0].Visible = false;
                }
            }
            else
            {
                LoadCbxData();
            }
        }

        public void BacChanged(object sender, EventArgs e)
        {
            maNgachOrTenNgachOrBacChanged = true;
            
            string bac = _view.CBXBac.Text;
            string mangach = _view.CBXMaNgach.Text;
            if (bac != string.Empty && mangach != string.Empty)
            {
                int tempBac = Convert.ToInt32(bac);
                int idngach = Convert.ToInt32(_view.CBXMaNgach.EditValue);
                _view.TXTHeSoBac.EditValue = unitOfWorks.BacRepository.GetHeSoBac(idngach, tempBac);
            }       
        }

        public void NgayBatDauChanged(object sender, EventArgs e)
        {
            ngayBatDauChanged = true;
        }

        public void NgayLenLuongChanged(object sender, EventArgs e)
        {
            ngayLenLuongChanged = true;
        }

        public void DangHuongLuongChanged(object sender, EventArgs e)
        {
            dangHuongLuongChanged = true;
        }

        public void LinkVanBanDinhKemChanged(object sender, EventArgs e)
        {
            linkVanBanDinhKemChanged = true;
        }

        public void TruocHanChanged(object sender, EventArgs e)
        {
            truocHanChanged = true;
        }

        public void HeSoVuotKhungChanged(object sender, EventArgs e)
        {
            heSoVuotKhungChanged = true;
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
