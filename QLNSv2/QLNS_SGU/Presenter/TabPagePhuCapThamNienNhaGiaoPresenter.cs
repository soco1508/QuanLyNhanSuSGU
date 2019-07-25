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
    public interface ITabPagePhuCapThamNienNhaGiaoPresenter : IPresenterArgument
    {
        void LoadForm();
        void ClickRowAndShowInfo();
        void UploadFileToLocal();
        void UploadFileToGoogleDrive();
        void DownloadFileToDevice();
        void Save();
        void Refresh();
        void Add();
        void Delete();
        void ExportExcel();
        void HeSoPhuCapChanged(object sender, EventArgs e);
        void NgayBatDauChanged(object sender, EventArgs e);
        void NgayNangPhuCapChanged(object sender, EventArgs e);
        void LinkVanBanDinhKemChanged(object sender, EventArgs e);
        void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e);        
    }
    public class TabPagePhuCapThamNienNhaGiaoPresenter : ITabPagePhuCapThamNienNhaGiaoPresenter
    {        
        public static string idFileUpload = string.Empty;
        public static string maVienChucForGetListLinkVanBanDinhKem = string.Empty;
        public static string maVienChucFromTabPageThongTinCaNhan = string.Empty;
        private bool checkAddNew = true;
        private bool heSoPhuCapChanged = false;
        private bool ngayBatDauChanged = false;
        private bool ngayNangPhuCapChanged = false;
        private bool linkVanBanDinhKemChanged = false;
        private static CreateAndEditPersonInfoForm _createAndEditPersonInfoForm = new CreateAndEditPersonInfoForm();
        private TabPagePhuCapThamNienNhaGiao _view;

        public object UI => _view;
        public TabPagePhuCapThamNienNhaGiaoPresenter(TabPagePhuCapThamNienNhaGiao view) => _view = view;
        private string GenerateCode() => Guid.NewGuid().ToString("N");
        public void Initialize(string mavienchuc)
        {
            _view.Attach(this);
            _view.TXTMaVienChuc.Text = mavienchuc;
            _view.GVPhuCapThamNienNhaGiao.IndicatorWidth = 50;
        }

        private void LoadGridTabPageQuaTrinhPhuCapThamNienNhaGiao(string mavienchuc)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<QuaTrinhPhuCapThamNienNhaGiao> list = unitOfWorks.QuaTrinhPhuCapThamNienNhaGiaoRepository.GetListQuaTrinhPhuCap(mavienchuc);
            _view.GCPhuCapThamNienNhaGiao.DataSource = list;
        }
        private void SetDefaultValueControl()
        {
            checkAddNew = true;
            _view.TXTHeSoPhuCap.Text = string.Empty;
            _view.DTNgayBatDau.Text = string.Empty;
            _view.DTNgayNangPhuCap.Text = string.Empty;
            _view.TXTLinkVanBanDinhKem.Text = string.Empty;
        }
        private void InsertData()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string mavienchuc = _view.TXTMaVienChuc.Text;            
            unitOfWorks.QuaTrinhPhuCapThamNienNhaGiaoRepository.Insert(new QuaTrinhPhuCapThamNienNhaGiao
            {
                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                heSoPhuCap = Convert.ToDouble(_view.TXTHeSoPhuCap.Text),
                ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayBatDau.Text),
                ngayNangPhuCap = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayNangPhuCap.Text),
                linkVanBanDinhKem = _view.TXTLinkVanBanDinhKem.Text
            });
            unitOfWorks.Save();
            LoadGridTabPageQuaTrinhPhuCapThamNienNhaGiao(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetDefaultValueControl();
        }
        private void UpdateData()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int idquatrinhphucap = Convert.ToInt32(_view.GVPhuCapThamNienNhaGiao.GetFocusedRowCellDisplayText("idQuaTrinhPhuCap"));
            DateTime? ngaybatdau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayBatDau.Text);
            DateTime? ngaynangphucap = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayNangPhuCap.Text);
            QuaTrinhPhuCapThamNienNhaGiao quaTrinhPhuCap = unitOfWorks.QuaTrinhPhuCapThamNienNhaGiaoRepository.GetObjectById(idquatrinhphucap);
            if (heSoPhuCapChanged)
            {
                quaTrinhPhuCap.heSoPhuCap = Convert.ToDouble(_view.TXTHeSoPhuCap.EditValue);
                heSoPhuCapChanged = false;
            }
            if (ngayBatDauChanged)
            {
                quaTrinhPhuCap.ngayBatDau = ngaybatdau;
                ngayBatDauChanged = false;
            }
            if (ngayNangPhuCapChanged)
            {
                quaTrinhPhuCap.ngayNangPhuCap = ngaynangphucap;
                ngayNangPhuCapChanged = false;
            }
            if (linkVanBanDinhKemChanged)
            {
                quaTrinhPhuCap.linkVanBanDinhKem = _view.TXTLinkVanBanDinhKem.Text;
                linkVanBanDinhKemChanged = false;
            }
            unitOfWorks.Save();
            LoadGridTabPageQuaTrinhPhuCapThamNienNhaGiao(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } 

        public void LoadForm()
        {
            string mavienchuc = _view.TXTMaVienChuc.Text;
            if (mavienchuc != string.Empty)
                LoadGridTabPageQuaTrinhPhuCapThamNienNhaGiao(mavienchuc);
        }

        public void ClickRowAndShowInfo()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            checkAddNew = false;            
            int row_handle = _view.GVPhuCapThamNienNhaGiao.FocusedRowHandle;
            if (row_handle >= 0)
            {
                string hesophucap = _view.GVPhuCapThamNienNhaGiao.GetFocusedRowCellDisplayText("heSoPhuCap").ToString();
                string linkvanbandinhkem = _view.GVPhuCapThamNienNhaGiao.GetFocusedRowCellDisplayText("linkVanBanDinhKem").ToString();
                if(hesophucap != string.Empty) _view.TXTHeSoPhuCap.EditValue = Convert.ToDouble(hesophucap);
                _view.DTNgayBatDau.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVPhuCapThamNienNhaGiao.GetFocusedRowCellDisplayText("ngayBatDau"));
                _view.DTNgayNangPhuCap.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVPhuCapThamNienNhaGiao.GetFocusedRowCellDisplayText("ngayNangPhuCap"));
                _view.TXTLinkVanBanDinhKem.Text = linkvanbandinhkem;
            }
        }

        public void UploadFileToLocal()
        {
            if (_view.GVPhuCapThamNienNhaGiao.FocusedRowHandle >= 0)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
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
            if (_view.GVPhuCapThamNienNhaGiao.FocusedRowHandle >= 0)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
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
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
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

        public void Save()
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
                int row_handle = _view.GVPhuCapThamNienNhaGiao.FocusedRowHandle;
                if (row_handle >= 0)
                    UpdateData();
            }
        }

        public void Refresh() => SetDefaultValueControl();

        public void Add() => SetDefaultValueControl();

        public void Delete()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            try
            {                
                int row_handle = _view.GVPhuCapThamNienNhaGiao.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVPhuCapThamNienNhaGiao.GetFocusedRowCellDisplayText("idQuaTrinhPhuCap"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.QuaTrinhPhuCapThamNienNhaGiaoRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVPhuCapThamNienNhaGiao.DeleteRow(row_handle);
                        Refresh();
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Quá trình phụ cấp này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVPhuCapThamNienNhaGiao.ExportToXlsx(_view.SaveFileDialog.FileName);
            XtraMessageBox.Show("Xuất Excel thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void HeSoPhuCapChanged(object sender, EventArgs e)
        {
            heSoPhuCapChanged = true;
        }

        public void NgayBatDauChanged(object sender, EventArgs e)
        {
            ngayBatDauChanged = true;
        }

        public void NgayNangPhuCapChanged(object sender, EventArgs e)
        {
            ngayNangPhuCapChanged = true;
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
