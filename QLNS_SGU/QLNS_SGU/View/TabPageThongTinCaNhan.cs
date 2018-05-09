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
using DevExpress.XtraBars;

namespace QLNS_SGU.View
{
    public interface ITabPageThongTinCaNhan : IView<ITabPageThongTinCaNhanPresenter>
    {
        PictureEdit PICVienChuc { get; set; }
        TextEdit TXTMaVienChuc { get; set; }
        RadioGroup RADGioiTinh { get; set; }
        TextEdit TXTHo { get; set; }
        TextEdit TXTTen { get; set; }
        DateEdit DTNgaySinh { get; set; }
        DateEdit DTNgayThamGiaCongTac { get; set; }
        DateEdit DTNgayVaoNganh { get; set; }
        DateEdit DTNgayVeTruong { get; set; }
        TextEdit TXTSoDienThoai { get; set; }
        TextEdit TXTNoiSinh { get; set; }
        TextEdit TXTQueQuan { get; set; }
        LookUpEdit CBXDanToc { get; set; }
        LookUpEdit CBXTonGiao { get; set; }
        TextEdit TXTHoKhauThuongTru { get; set; }
        TextEdit TXTNoiOHienNay { get; set; }
        CheckEdit CHKLaDangVien { get; set; }
        DateEdit DTNgayVaoDang { get; set; }
        TextEdit TXTVanHoa { get; set; }
        LookUpEdit CBXQuanLyNhaNuoc { get; set; }
        TextEdit TXTSoCMND { get; set; }
        MemoEdit TXTGhiChu { get; set; }        
    }
    public partial class TabPageThongTinCaNhan : XtraForm, ITabPageThongTinCaNhan
    {
        public TabPageThongTinCaNhan()
        {
            InitializeComponent();
        }
        #region Controls
        public PictureEdit PICVienChuc { get => picVienChuc; set => picVienChuc = value; }
        public TextEdit TXTMaVienChuc { get => txtMaVienChuc; set => txtMaVienChuc = value; }
        public RadioGroup RADGioiTinh { get => radGioiTinh; set => radGioiTinh = value; }
        public TextEdit TXTHo { get => txtHo; set => txtHo = value; }
        public TextEdit TXTTen { get => txtTen; set => txtTen = value; }
        public DateEdit DTNgaySinh { get => dtNgaySinh; set => dtNgaySinh = value; }
        public DateEdit DTNgayThamGiaCongTac { get => dtNgayThamGiaCongTac; set => dtNgayThamGiaCongTac = value; }
        public DateEdit DTNgayVaoNganh { get => dtNgayVaoNganh; set => dtNgayVaoNganh = value; }
        public DateEdit DTNgayVeTruong { get => dtNgayVeTruong; set => dtNgayVeTruong = value; }
        public TextEdit TXTSoDienThoai { get => txtSoDienThoai; set => txtSoDienThoai = value; }
        public TextEdit TXTNoiSinh { get => txtNoiSinh; set => txtNoiSinh = value; }
        public TextEdit TXTQueQuan { get => txtQueQuan; set => txtQueQuan = value; }
        public LookUpEdit CBXDanToc { get => cbxDanToc; set => cbxDanToc = value; }
        public LookUpEdit CBXTonGiao { get => cbxTonGiao; set => cbxTonGiao = value; }
        public TextEdit TXTHoKhauThuongTru { get => txtHoKhauThuongTru; set => txtHoKhauThuongTru = value; }
        public TextEdit TXTNoiOHienNay { get => txtNoiOHienNay; set => txtNoiOHienNay = value; }
        public CheckEdit CHKLaDangVien { get => chkLaDangVien; set => chkLaDangVien = value; }
        public DateEdit DTNgayVaoDang { get => dtNgayVaoDang; set => dtNgayVaoDang = value; }
        public TextEdit TXTVanHoa { get => txtVanHoa; set => txtVanHoa = value; }
        public LookUpEdit CBXQuanLyNhaNuoc { get => cbxQuanLyNhaNuoc; set => cbxQuanLyNhaNuoc = value; }
        public TextEdit TXTSoCMND { get => txtSoCMND; set => txtSoCMND = value; }
        public MemoEdit TXTGhiChu { get => txtGhiChu; set => txtGhiChu = value; }
        #endregion
        public void Attach(ITabPageThongTinCaNhanPresenter presenter)
        {
            Load += (s, e) => presenter.LoadForm();
            //btnAddNew.ItemClick += (s, e) => presenter.AddNew();
            btnSave.ItemClick += (s, e) => presenter.Save();
            btnSaveAndClose.ItemClick += (s, e) => presenter.SaveAndClose();
            radGioiTinh.SelectedIndexChanged += new EventHandler(presenter.GioiTinhChanged);
            txtHo.TextChanged += new EventHandler(presenter.HoChanged);
            txtTen.TextChanged += new EventHandler(presenter.TenChanged);
            dtNgaySinh.DateTimeChanged += new EventHandler(presenter.NgaySinhChanged);
            dtNgayThamGiaCongTac.DateTimeChanged += new EventHandler(presenter.NgayThamGiaCongTacChanged);
            dtNgayVaoNganh.DateTimeChanged += new EventHandler(presenter.NgayVaoNganhChanged);
            dtNgayVeTruong.DateTimeChanged += new EventHandler(presenter.NgayVeTruongChanged);
            txtSoDienThoai.TextChanged += new EventHandler(presenter.SoDienThoaiChanged);
            txtNoiSinh.TextChanged += new EventHandler(presenter.NoiSinhChanged);
            txtQueQuan.TextChanged += new EventHandler(presenter.QueQuanChanged);
            cbxDanToc.EditValueChanged += new EventHandler(presenter.DanTocChanged);
            cbxTonGiao.EditValueChanged += new EventHandler(presenter.TonGiaoChanged);
            txtHoKhauThuongTru.TextChanged += new EventHandler(presenter.HoKhauThuongTruChanged);
            txtNoiOHienNay.TextChanged += new EventHandler(presenter.NoiOHienNay);
            chkLaDangVien.CheckedChanged += new EventHandler(presenter.LaDangVienChanged);
            dtNgayVaoDang.DateTimeChanged += new EventHandler(presenter.NgayVaoDangChanged);
            txtVanHoa.TextChanged += new EventHandler(presenter.VanHoaChanged);
            cbxQuanLyNhaNuoc.EditValueChanged += new EventHandler(presenter.QuanLyNhaNuocChanged);
            txtSoCMND.TextChanged += new EventHandler(presenter.SoCMNDChanged);
            txtGhiChu.TextChanged += new EventHandler(presenter.GhiChuChanged);
            picVienChuc.ImageChanged += new EventHandler(presenter.PicChanged);
        }
    }
}