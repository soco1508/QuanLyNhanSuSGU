﻿using DevExpress.XtraEditors;
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
    public interface ITabPageTrangThaiPresenter : IPresenterArgument
    {
        void ClickRowAndShowInfo();
        void UploadFileToLocal();
        void UploadFileToGoogleDrive();
        void DownloadFileToDevice();
        void Save();
        void Refresh();
        void Add();
        void Delete();
        void LoadForm();
        void ExportExcel();
        void TrangThaiChanged(object sender, EventArgs e);
        void MoTaChanged(object sender, EventArgs e);
        void DiaDiemChanged(object sender, EventArgs e);
        void NgayBatDauChanged(object sender, EventArgs e);
        void NgayKetThucChanged(object sender, EventArgs e);
        void LinkVanBanDinhKemChanged(object sender, EventArgs e);
        void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e);
    }
    public class TabPageTrangThaiPresenter : ITabPageTrangThaiPresenter
    {
        UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
        public static string idFileUpload = string.Empty;
        public static string maVienChucForGetListLinkVanBanDinhKem = string.Empty;
        public static string maVienChucFromTabPageThongTinCaNhan = string.Empty;
        private bool checkAddNew = true;
        private bool trangThaiChanged = false;
        private bool moTaChanged = false;
        private bool diaDiemChanged = false;
        private bool ngayBatDauChanged = false;
        private bool ngayKetThucChanged = false;        
        private bool linkVanBanDinhKemChanged = false;
        public int rowFocusFromCreateAndEditPersonalInfoForm = -1;
        private static CreateAndEditPersonInfoForm _createAndEditPersonInfoForm = new CreateAndEditPersonInfoForm();
        private TabPageTrangThai _view;
        public object UI => _view;
        public TabPageTrangThaiPresenter(TabPageTrangThai view) => _view = view;
        private string GenerateCode() => Guid.NewGuid().ToString("N");
        public void Initialize(string mavienchuc)
        {
            _view.Attach(this);
            _view.TXTMaVienChuc.Text = mavienchuc;
            _view.GVTabPageTrangThai.IndicatorWidth = 50;
        }
        private void LoadGridTabPageTrangThai(string mavienchuc)
        {
            List<TrangThaiForView> listTrangThai = unitOfWorks.TrangThaiVienChucRepository.GetListTrangThaiVienChuc(mavienchuc);
            _view.GCTabPageTrangThai.DataSource = listTrangThai;
            //_view.GCTabPageTrangThai.DataSource = unitOfWorks.TrangThaiVienChucRepository.GetListTrangThaiVienChuc(mavienchuc).GetEnumerator();
        }
        private void LoadCbxData()
        {
            List<TrangThai> listTrangThai = unitOfWorks.TrangThaiRepository.GetListTrangThai().ToList();
            _view.CBXTrangThai.Properties.DataSource = listTrangThai;
            _view.CBXTrangThai.Properties.DisplayMember = "tenTrangThai";
            _view.CBXTrangThai.Properties.ValueMember = "idTrangThai";
            _view.CBXTrangThai.Properties.DropDownRows = listTrangThai.Count;
            _view.CBXTrangThai.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idTrangThai", string.Empty));
            _view.CBXTrangThai.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenTrangThai", string.Empty));
            _view.CBXTrangThai.Properties.Columns[0].Visible = false;
        }
        private void SetDefaultValueControl()
        {
            checkAddNew = true;
            _view.CBXTrangThai.ErrorText = string.Empty;
            _view.CBXTrangThai.Text = string.Empty;
            _view.DTNgayBatDau.Text = string.Empty;
            _view.DTNgayKetThuc.Text = string.Empty;
            _view.TXTMoTa.Text = string.Empty;
            _view.TXTDiaDiem.Text = string.Empty;
            _view.TXTLinkVanBanDinhKem.Text = string.Empty;
        }
        private void InsertData()
        {
            string mavienchuc = _view.TXTMaVienChuc.Text;
            unitOfWorks.TrangThaiVienChucRepository.Insert(new TrangThaiVienChuc
            {
                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                idTrangThai = Convert.ToInt32(_view.CBXTrangThai.EditValue),
                ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayBatDau.Text),
                ngayKetThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayKetThuc.Text),
                moTa = _view.TXTMoTa.Text,
                diaDiem = _view.TXTDiaDiem.Text,
                linkVanBanDinhKem = _view.TXTLinkVanBanDinhKem.Text
            });
            unitOfWorks.Save();
            LoadGridTabPageTrangThai(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainPresenter.RefreshRightViewTrangThai();
            SetDefaultValueControl();
        }
        private void UpdateData()
        {
            int idtrangthaivienchuc = Convert.ToInt32(_view.GVTabPageTrangThai.GetFocusedRowCellDisplayText("Id"));
            DateTime? ngaybatdau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayBatDau.Text);
            DateTime? ngayketthuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayKetThuc.Text);
            TrangThaiVienChuc trangThaiVienChuc = unitOfWorks.TrangThaiVienChucRepository.GetObjectById(idtrangthaivienchuc);
            if (trangThaiChanged)
            {
                trangThaiVienChuc.idTrangThai = Convert.ToInt32(_view.CBXTrangThai.EditValue);
                trangThaiChanged = false;
            }
            if (moTaChanged)
            {
                trangThaiVienChuc.moTa = _view.TXTMoTa.Text;
                moTaChanged = false;
            }
            if (diaDiemChanged)
            {
                trangThaiVienChuc.diaDiem = _view.TXTDiaDiem.Text;
                diaDiemChanged = false;
            }
            if (ngayBatDauChanged)
            {
                trangThaiVienChuc.ngayBatDau = ngaybatdau;
                ngayBatDauChanged = false;
            }
            if (ngayKetThucChanged)
            {
                trangThaiVienChuc.ngayKetThuc = ngayketthuc;
                ngayKetThucChanged = false;
            }
            if (linkVanBanDinhKemChanged)
            {
                trangThaiVienChuc.linkVanBanDinhKem = _view.TXTLinkVanBanDinhKem.Text;
                linkVanBanDinhKemChanged = false;
            }
            unitOfWorks.Save();
            LoadGridTabPageTrangThai(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainPresenter.RefreshRightViewTrangThai();
        }

        private void InsertFirstRowDefault(string mavienchuc)
        {
            unitOfWorks.TrangThaiVienChucRepository.InsertFirstRowDefault(mavienchuc);
            try
            {
                unitOfWorks.Save();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Vui lòng nhập dữ liệu trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public void Add() => SetDefaultValueControl();

        public void ClickRowAndShowInfo()
        {
            checkAddNew = false;
            int row_handle = _view.GVTabPageTrangThai.FocusedRowHandle;
            if (row_handle >= 0)
            {
                string trangthai = _view.GVTabPageTrangThai.GetFocusedRowCellDisplayText("TrangThai").ToString();
                string mota = _view.GVTabPageTrangThai.GetFocusedRowCellDisplayText("MoTa").ToString();
                string diadiem = _view.GVTabPageTrangThai.GetFocusedRowCellDisplayText("DiaDiem").ToString();
                string linkvanbandinhkem = _view.GVTabPageTrangThai.GetFocusedRowCellDisplayText("LinkVanBanDinhKem").ToString();
                _view.CBXTrangThai.EditValue = unitOfWorks.TrangThaiRepository.GetIdTrangThai(trangthai);
                _view.DTNgayBatDau.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVTabPageTrangThai.GetFocusedRowCellDisplayText("NgayBatDau"));
                _view.DTNgayKetThuc.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVTabPageTrangThai.GetFocusedRowCellDisplayText("NgayKetThuc"));
                _view.TXTMoTa.Text = mota;
                _view.TXTDiaDiem.Text = diadiem;
                _view.TXTLinkVanBanDinhKem.Text = linkvanbandinhkem;
            }
        }

        public void Delete()
        {
            try
            {
                int row_handle = _view.GVTabPageTrangThai.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVTabPageTrangThai.GetFocusedRowCellDisplayText("Id"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.TrangThaiVienChucRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVTabPageTrangThai.DeleteRow(row_handle);
                        Refresh();
                        MainPresenter.RefreshRightViewTrangThai();
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Trạng thái này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        public void Save()
        {
            if (checkAddNew)
            {
                if (_view.TXTMaVienChuc.Text != string.Empty && maVienChucFromTabPageThongTinCaNhan == string.Empty)
                {
                    if (_view.CBXTrangThai.Text != string.Empty)
                        InsertData();
                    else
                    {
                        _view.CBXTrangThai.ErrorText = "Vui lòng chọn trạng thái.";
                        _view.CBXTrangThai.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                }
                else if (_view.TXTMaVienChuc.Text == string.Empty && maVienChucFromTabPageThongTinCaNhan != string.Empty)
                {
                    _view.TXTMaVienChuc.Text = maVienChucFromTabPageThongTinCaNhan;
                    if (_view.CBXTrangThai.Text != string.Empty)
                        InsertData();
                    else
                    {
                        _view.CBXTrangThai.ErrorText = "Vui lòng chọn trạng thái.";
                        _view.CBXTrangThai.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                }
                else
                    XtraMessageBox.Show("Vui lòng thêm thông tin viên chức trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int row_handle = _view.GVTabPageTrangThai.FocusedRowHandle;
                if (row_handle >= 0)
                    UpdateData();
            }
        }     

        public void Refresh() => SetDefaultValueControl();

        public void UploadFileToLocal()
        {
            if (_view.GVTabPageTrangThai.FocusedRowHandle >= 0)
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
                catch(Exception ex)
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
            if (_view.GVTabPageTrangThai.FocusedRowHandle >= 0)
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
                LoadGridTabPageTrangThai(mavienchuc);
                if(_view.GVTabPageTrangThai.RowCount == 0)
                {
                    if (TabPageQuaTrinhCongTacPresenter.checkEmptyRowQTCTGrid)
                        InsertFirstRowDefault(mavienchuc);
                }
                if(rowFocusFromCreateAndEditPersonalInfoForm >= 0)
                {
                    _view.GVTabPageTrangThai.FocusedRowHandle = rowFocusFromCreateAndEditPersonalInfoForm;
                    ClickRowAndShowInfo();
                }
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVTabPageTrangThai.ExportToXlsx(_view.SaveFileDialog.FileName);
            XtraMessageBox.Show("Xuất Excel thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void TrangThaiChanged(object sender, EventArgs e)
        {
            trangThaiChanged = true;
        }

        public void MoTaChanged(object sender, EventArgs e)
        {
            moTaChanged = true;
        }

        public void DiaDiemChanged(object sender, EventArgs e)
        {
            diaDiemChanged = true;
        }

        public void NgayBatDauChanged(object sender, EventArgs e)
        {
            ngayBatDauChanged = true;
        }

        public void NgayKetThucChanged(object sender, EventArgs e)
        {
            ngayKetThucChanged = true;
        }

        public void LinkVanBanDinhKemChanged(object sender, EventArgs e)
        {
            linkVanBanDinhKemChanged = true;
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
