using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using QLNS_SGU.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS_SGU.View
{
    public interface ITabPageDanhGiaVienChuc : IView<ITabPageDanhGiaVienChucPresenter>
    {
        FolderBrowserDialog FolderBrowserDialog { get; set; }
        OpenFileDialog OpenFileDialog { get; set; }
        SaveFileDialog SaveFileDialog { get; set; }
        GridControl GCDanhGiaVienChuc { get; set; }
        GridView GVDanhGiaVienChuc { get; set; }
        LookUpEdit CBXKhoangThoiGian { get; set; }
        LookUpEdit CBXMucDoDanhGia { get; set; }
        DateEdit DTNgayBatDau { get; set; }
        DateEdit DTNgayKetThuc { get; set; }
        TextEdit TXTMaVienChuc { get; set; }
        TextEdit TXTLinkVanBanDinhKem { get; set; }
    }
    public partial class TabPageDanhGiaVienChuc : XtraForm, ITabPageDanhGiaVienChuc
    {
        public TabPageDanhGiaVienChuc()
        {
            InitializeComponent();
        }
        #region Controls
        public FolderBrowserDialog FolderBrowserDialog { get => folderBrowserDialog1; set => folderBrowserDialog1 = value; }
        public OpenFileDialog OpenFileDialog { get => openFileDialog1; set => openFileDialog1 = value; }
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public GridControl GCDanhGiaVienChuc { get => gcDanhGiaVienChuc; set => gcDanhGiaVienChuc = value; }
        public GridView GVDanhGiaVienChuc { get => gvDanhGiaVienChuc; set => gvDanhGiaVienChuc = value; }
        public LookUpEdit CBXKhoangThoiGian { get => cbxKhoangThoiGian; set => cbxKhoangThoiGian = value; }
        public LookUpEdit CBXMucDoDanhGia { get => cbxMucDoDanhGia; set => cbxMucDoDanhGia = value; }
        public DateEdit DTNgayBatDau { get => dtNgayBatDau; set => dtNgayBatDau = value; }
        public DateEdit DTNgayKetThuc { get => dtNgayKetThuc; set => dtNgayKetThuc = value; }
        public TextEdit TXTMaVienChuc { get => txtMaVienChuc; set => txtMaVienChuc = value; }
        public TextEdit TXTLinkVanBanDinhKem { get => txtLinkVanBanDinhKem; set => txtLinkVanBanDinhKem = value; }
        #endregion
        public void Attach(ITabPageDanhGiaVienChucPresenter presenter)
        {
            Load += (s, e) => presenter.LoadForm();
            cbxKhoangThoiGian.EditValueChanged += new EventHandler(presenter.KhoangThoiGianChanged);           
            cbxMucDoDanhGia.EditValueChanged += new EventHandler(presenter.MucDoDanhGiaChanged);
            dtNgayBatDau.DateTimeChanged += new EventHandler(presenter.NgayBatDauChanged);
            dtNgayKetThuc.DateTimeChanged += new EventHandler(presenter.NgayKetThucChanged);
            gvDanhGiaVienChuc.Click += (s, e) => presenter.ClickRowAndShowInfo();
            btnSave.Click += (s, e) => presenter.Save();
            btnRefresh.Click += (s, e) => presenter.Refresh();
            btnAdd.Click += (s, e) => presenter.Add();
            btnDelete.Click += (s, e) => presenter.Delete();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gvDanhGiaVienChuc.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
            btnUploadLocal.Click += (s, e) => presenter.UploadFileToLocal();
            btnUpload.Click += (s, e) => presenter.UploadFileToGoogleDrive();
            btnDownload.Click += (s, e) => presenter.DownloadFileToDevice();
        }
    }
}
