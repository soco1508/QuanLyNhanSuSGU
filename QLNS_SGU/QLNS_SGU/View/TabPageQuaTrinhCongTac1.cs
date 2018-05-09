using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLNS_SGU.Presenter;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraLayout;
using DevExpress.XtraRichEdit;
using DevExpress.XtraTab;

namespace QLNS_SGU.View
{
    public interface ITabPageQuaTrinhCongTac : IView<ITabPageQuaTrinhCongTacPresenter>
    {
        XtraTabControl XtraTabControl { get; set; }
        OpenFileDialog OpenFileDialog { get; set; }
        SaveFileDialog SaveFileDialog { get; set; }
        //QTCT
        SimpleButton BTNExportExcelQTCT { get; set; }
        GridControl GCTabPageQuaTrinhCongTac { get; set; }
        GridView GVTabPageQuaTrinhCongTac { get; set; }
        TextEdit TXTMaVienChuc { get; set; }
        TextEdit TXTLinkVanBanDinhKem { get; set; }
        TextEdit TXTHeSoChucVu { get; set; }
        LookUpEdit CBXChucVu { get; set; }
        LookUpEdit CBXDonVi { get; set; }
        LookUpEdit CBXToChuyenMon { get; set; }
        TextEdit TXTPhanLoaiCongTac { get; set; }
        LookUpEdit CBXPhanLoai { get; set; }
        LookUpEdit CBXKiemNhiem { get; set; }
        LookUpEdit CBXLoaiThayDoi { get; set; }
        DateEdit DTNgayBatDau { get; set; }
        DateEdit DTNgayKetThuc { get; set; }
        MemoEdit TXTNhanXet { get; set; }
        HyperlinkLabelControl LinkLBTrangThaiHienTai { get; set; }
        //HD
        SimpleButton BTNExportExcelHD { get; set; }
        GridControl GCTabPageHopDong { get; set; }
        GridView GVTabPageHopDong { get; set; }
        LookUpEdit CBXLoaiHopDong { get; set; }
        DateEdit DTNgayBatDauHD { get; set; }
        DateEdit DTNgayKetThucHD { get; set; }
        MemoEdit TXTGhiChuHD { get; set; }
        TextEdit TXTLinkVanBanDinhKemHD { get; set; }
    }
    public partial class TabPageQuaTrinhCongTac1 : XtraForm, ITabPageQuaTrinhCongTac
    {
        public TabPageQuaTrinhCongTac1()
        {
            InitializeComponent();
        }
        #region Controls
        public XtraTabControl XtraTabControl { get => xtraTabControl1; set => xtraTabControl1 = value; }
        public OpenFileDialog OpenFileDialog { get => openFileDialog1; set => openFileDialog1 = value; }
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        //QTCT
        public SimpleButton BTNExportExcelQTCT { get => btnExportExcelQTCT; set => btnExportExcelQTCT = value; }
        public GridControl GCTabPageQuaTrinhCongTac { get => gcTabPageQuaTrinhCongTac; set => gcTabPageQuaTrinhCongTac = value; }
        public GridView GVTabPageQuaTrinhCongTac { get => gvTabPageQuaTrinhCongTac; set => gvTabPageQuaTrinhCongTac = value; }
        public TextEdit TXTMaVienChuc { get => txtMaVienChuc; set => txtMaVienChuc = value; }
        public TextEdit TXTLinkVanBanDinhKem { get => txtLinkVanBanDinhKem; set => txtLinkVanBanDinhKem = value; }
        public TextEdit TXTHeSoChucVu { get => txtHeSoChucVu; set => txtHeSoChucVu = value; }
        public LookUpEdit CBXChucVu { get => cbxChucVu; set => cbxChucVu = value; }
        public LookUpEdit CBXDonVi { get => cbxDonVi; set => cbxDonVi = value; }
        public LookUpEdit CBXToChuyenMon { get => cbxToChuyenMon; set => cbxToChuyenMon = value; }
        public TextEdit TXTPhanLoaiCongTac { get => txtPhanLoaiCongTac; set => txtPhanLoaiCongTac = value; }
        public LookUpEdit CBXPhanLoai { get => cbxCheckPhanLoaiCongTac; set => cbxCheckPhanLoaiCongTac = value; }
        public LookUpEdit CBXKiemNhiem { get => cbxKiemNhiem; set => cbxKiemNhiem = value; }
        public LookUpEdit CBXLoaiThayDoi { get => cbxLoaiThayDoi; set => cbxLoaiThayDoi = value; }
        public DateEdit DTNgayBatDau { get => dtNgayBatDau; set => dtNgayBatDau = value; }
        public DateEdit DTNgayKetThuc { get => dtNgayKetThuc; set => dtNgayKetThuc = value; }
        public MemoEdit TXTNhanXet { get => txtNhanXet; set => txtNhanXet = value; }
        public HyperlinkLabelControl LinkLBTrangThaiHienTai { get => linklbTrangThaiHienTai; set => linklbTrangThaiHienTai = value; }
        //HD
        public SimpleButton BTNExportExcelHD { get => btnExportExcelHD; set => btnExportExcelHD = value; }
        public GridControl GCTabPageHopDong { get => gcHopDong; set => gcHopDong = value; }
        public GridView GVTabPageHopDong { get => gvHopDong; set => gvHopDong = value; }
        public LookUpEdit CBXLoaiHopDong { get => cbxLoaiHopDong; set => cbxLoaiHopDong = value; }
        public DateEdit DTNgayBatDauHD { get => dtNgayBatDauHD; set => dtNgayBatDauHD = value; }
        public DateEdit DTNgayKetThucHD { get => dtNgayKetThucHD; set => dtNgayKetThucHD = value; }
        public MemoEdit TXTGhiChuHD { get => txtGhiChuHD; set => txtGhiChuHD = value; }
        public TextEdit TXTLinkVanBanDinhKemHD { get => txtLinkVanBanDinhKemHD; set => txtLinkVanBanDinhKemHD = value; }
        #endregion
        public void Attach(ITabPageQuaTrinhCongTacPresenter presenter)
        {
            Load += (s, e) => presenter.LoadForm();
            //QTCT           
            gvTabPageQuaTrinhCongTac.Click += (s, e) => presenter.ClickRowAndShowInfoQTCT();
            btnUploadQTCT.Click += (s, e) => presenter.UploadFileToGoogleDriveQTCT();
            btnDownloadQTCT.Click += (s, e) => presenter.DownloadFileToDeviceQTCT();
            btnEditQTCT.Click += (s, e) => presenter.SaveQTCT();
            btnRefreshQTCT.Click += (s, e) => presenter.RefreshQTCT();
            btnAddQTCT.Click += (s, e) => presenter.AddQTCT();
            btnDeleteQTCT.Click += (s, e) => presenter.DeleteQTCT();
            btnExportExcelQTCT.Click += (s, e) => presenter.ExportExcelQTCT();
            cbxChucVu.EditValueChanged += new EventHandler(presenter.ChucVuChanged);
            cbxDonVi.EditValueChanged += new EventHandler(presenter.DonViChanged);
            cbxToChuyenMon.EditValueChanged += new EventHandler(presenter.ToChuyenMonChanged);
            txtPhanLoaiCongTac.EditValueChanged += new EventHandler(presenter.PhanLoaiCongTacChanged);
            dtNgayBatDau.DateTimeChanged += new EventHandler(presenter.NgayBatDauQuaTrinhCongTacChanged);
            dtNgayKetThuc.DateTimeChanged += new EventHandler(presenter.NgayKetThucQuaTrinhCongTacChanged);
            cbxLoaiThayDoi.EditValueChanged += new EventHandler(presenter.LoaiThayDoiChanged);
            cbxCheckPhanLoaiCongTac.EditValueChanged += new EventHandler(presenter.PhanLoaiChanged);
            cbxKiemNhiem.EditValueChanged += new EventHandler(presenter.KiemNhiemChanged);
            txtLinkVanBanDinhKem.TextChanged += new EventHandler(presenter.LinkVanBanDinhKemQuaTrinhCongTacChanged);
            txtNhanXet.TextChanged += new EventHandler(presenter.NhanXetChanged);
            gvTabPageQuaTrinhCongTac.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicatorQTCT);
            linklbTrangThaiHienTai.Click += (s, e) => presenter.ShowTrangThaiHienTai();
            //HD
            gvHopDong.Click += (s, e) => presenter.ClickRowAndShowInfoHD();
            btnUploadHD.Click += (s, e) => presenter.UploadFileToGoogleDriveHD();
            btnDownloadHD.Click += (s, e) => presenter.DownloadFileToDeviceHD();
            btnEditHD.Click += (s, e) => presenter.SaveHD();
            btnRefreshHD.Click += (s, e) => presenter.RefreshHD();
            btnAddHD.Click += (s, e) => presenter.AddHD();
            btnDeleteHD.Click += (s, e) => presenter.DeleteHD();
            btnExportExcelHD.Click += (s, e) => presenter.ExportExcelHD();
            cbxLoaiHopDong.EditValueChanged += new EventHandler(presenter.LoaiHopDongChanged);
            dtNgayBatDauHD.DateTimeChanged += new EventHandler(presenter.NgayBatDauHopDongChanged);
            dtNgayBatDauHD.DateTimeChanged += new EventHandler(presenter.NgayKetThucHopDongChanged);
            txtGhiChuHD.TextChanged += new EventHandler(presenter.GhiChuHopDongChanged);
            txtLinkVanBanDinhKemHD.TextChanged += new EventHandler(presenter.LinkVanBanDinhKemHopDongChanged);
            gvHopDong.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicatorHD);
        }
    }
}