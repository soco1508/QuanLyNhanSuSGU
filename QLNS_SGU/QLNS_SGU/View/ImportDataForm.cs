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
using DevExpress.XtraTab;
using DevExpress.XtraBars;

namespace QLNS_SGU.View
{
    public interface IImportDataForm : IView<IImportDataPresenter>
    {
        OpenFileDialog OpenFileDialog { get; set; }
        BarStaticItem TxtPathFile { get; set; }
        BarEditItem CbxListSheets { get; set; }
        XtraTabControl XtraTabControl { get; set; }
        XtraTabPage TabPageThongTinVienChuc { get; set; }
        XtraTabPage TabPageTrangThai { get; set; }
        XtraTabPage TabPageChucVuDonVi { get; set; }
        XtraTabPage TabPageHopDong { get; set; }
        XtraTabPage TabPageNgachBacLuong { get; set; }
        XtraTabPage TabPageNganh { get; set; }
        XtraTabPage TabPageHocHamHocVi { get; set; }
        XtraTabPage TabPageLinhVucDangHocNangCao { get; set; }
        XtraTabPage TabPageChungChi { get; set; }
    }
    public partial class ImportDataForm : XtraForm, IImportDataForm
    {
        public ImportDataForm()
        {
            InitializeComponent();
        }
        #region ---Controls---
        public OpenFileDialog OpenFileDialog { get => openFileDialog1; set => openFileDialog1 = value; }
        public BarStaticItem TxtPathFile { get => txtPathFile; set => txtPathFile = value; }
        public BarEditItem CbxListSheets { get => cbxContainListSheets; set => cbxContainListSheets = value; }
        public XtraTabControl XtraTabControl { get => xtraTabControl1; set => xtraTabControl1 = value; }
        public XtraTabPage TabPageThongTinVienChuc { get => tabThongTinCaNhan; set => tabThongTinCaNhan = value; }
        public XtraTabPage TabPageTrangThai { get => tabTrangThai; set => tabTrangThai = value; }
        public XtraTabPage TabPageChucVuDonVi { get => tabChucVuDonVi; set => tabChucVuDonVi = value; }
        public XtraTabPage TabPageHopDong { get => tabHopDong; set => tabHopDong = value; }
        public XtraTabPage TabPageNgachBacLuong { get => tabNgachBacLuong; set => tabNgachBacLuong = value; }
        public XtraTabPage TabPageNganh { get => tabNganh; set => tabNganh = value; }
        public XtraTabPage TabPageHocHamHocVi { get => tabHocHamHocVi; set => tabHocHamHocVi = value; }
        public XtraTabPage TabPageLinhVucDangHocNangCao { get => tabDangHocNangCao; set => tabDangHocNangCao = value; }
        public XtraTabPage TabPageChungChi { get => tabChungChi; set => tabChungChi = value; }
        #endregion
        public void Attach(IImportDataPresenter presenter)
        {
            btnChooseFile.ItemClick += (sender, e) => presenter.ChooseFile();
            navBarThongTinVienChuc.LinkPressed += (sender, e) => presenter.OpenTabThongTinVienChuc();
            navBarTrangThai.LinkPressed += (sender, e) => presenter.OpenTabTrangThai();
            navBarChucVuDonVi.LinkPressed += (sender, e) => presenter.OpenTabChucVuDonVi();
            navBarHopDong.LinkPressed += (sender, e) => presenter.OpenTabHopDong();
            navBarNgachBacLuong.LinkPressed += (sender, e) => presenter.OpenTabNgachBacLuong();
            navBarNganh.LinkPressed += (sender, e) => presenter.OpenTabNganh();
            navBarHocHamHocVi.LinkPressed += (sender, e) => presenter.OpenTabHocHamHocVi();
            navBarDangHocNangCao.LinkPressed += (sender, e) => presenter.OpenTabLinhVucDangHocNangCao();
            navBarChungChi.LinkPressed += (sender, e) => presenter.OpenTabChungChi();
            //Thong tin ca nhan
            btnImportVienChuc.Click += (sender, e) => presenter.ImportVienChuc();
            //btnImportTrangThai.Click += (sender, e) => presenter.ImportTrangThai();
            btnImportTrangThaiVienChuc.Click += (sender, e) => presenter.ImportTrangThaiVienChuc();
            //Qua trinh cong tac
            //btnImportLoaiChucVu.Click += (sender, e) => presenter.ImportLoaiChucVu();
            btnImportChucVu.Click += (sender, e) => presenter.ImportChucVu();
            //btnImportLoaiDonVi.Click += (sender, e) => presenter.ImportLoaiDonVi();
            btnImportDonVi.Click += (sender, e) => presenter.ImportDonVi();
            btnImportToChuyenMon.Click += (sender, e) => presenter.ImportToChuyenMon();
            btnImportChucVuDonViVienChuc.Click += (sender, e) => presenter.ImportChucVuDonViVienChuc();
            //Qua trinh luong
            btnImportNgach.Click += (sender, e) => presenter.ImportNgach();
            btnImportBac.Click += (sender, e) => presenter.ImportBac();
            btnImportQuaTrinhLuong.Click += (sender, e) => presenter.ImportQuaTrinhLuong();
            btnImportLoaiHopDong.Click += (sender, e) => presenter.ImportLoaiHopDong();
            btnImportHopDongVienChuc.Click += (sender, e) => presenter.ImportHopDongVienChuc();
            //Chuyen mon
            btnImportLoaiNganh.Click += (sender, e) => presenter.ImportLoaiNganh();
            btnImportNganhDaoTao.Click += (sender, e) => presenter.ImportNganhDaoTao();
            btnImportChuyenNganh.Click += (sender, e) => presenter.ImportChuyenNganh();
            btnImportNganhVienChuc.Click += (sender, e) => presenter.ImportNganhVienChuc();
            //btnImportLoaiHocHamHocVi.Click += (sender, e) => presenter.ImportLoaiHocHamHocVi();
            btnImportHocHamHocViVienChuc.Click += (sender, e) => presenter.ImportHocHamHocViVienChuc();
            btnImportDangHocNangCao.Click += (sender, e) => presenter.ImportDangHocNangCao();
            btnImportLoaiChungChi.Click += (sender, e) => presenter.ImportLoaiChungChi();
            btnImportChungChiVienChuc.Click += (sender, e) => presenter.ImportChungChiVienChuc();
        }
    }
}