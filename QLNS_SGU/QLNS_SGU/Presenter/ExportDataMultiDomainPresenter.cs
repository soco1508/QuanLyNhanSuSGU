using Model;
using QLNS_SGU.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;
using Model.ObjectModels;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraEditors;

namespace QLNS_SGU.Presenter
{
    public interface IExportDataPresenter : IPresenter
    {
        void ExportExcel();
        void CheckAllAndUncheckAll();
        void EnableFilterDatetime(object sender, EventArgs e);
        void Cancel();
        void ExportData();
        void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e);
        void CHKCongTacChanged(object sender, EventArgs e);
        void CHKQuaTrinhLuongChanged(object sender, EventArgs e);
        void CHKHopDongChanged(object sender, EventArgs e);
        void CHKTrangThaiChanged(object sender, EventArgs e);
        void CHKNganhHocChanged(object sender, EventArgs e);
        void CHKNganhDayChanged(object sender, EventArgs e);
        void CHKChungChiChanged(object sender, EventArgs e);
        void CHKDangHocNangCaoChanged(object sender, EventArgs e);
        void CHKThongTinCaNhanChanged(object sender, EventArgs e);
    }
    public class ExportDataMultiDomainPresenter : IExportDataPresenter
    {
        private ExportDataMultiDomainForm _view;
        private bool checkAllState = false;
        private string FilterCurrent = "";
        private List<bool> listCheckBoxValue = new List<bool> { false, false, false, false, false, false, false, false, false };
        public object UI => _view;
        public ExportDataMultiDomainPresenter(ExportDataMultiDomainForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVCustom.IndicatorWidth = 50;
        }  
        private void UncheckAll()
        {
            _view.CHKThongTinCaNhan.Checked = false;
            _view.CHKCongTac.Checked = false;
            _view.CHKQuaTrinhLuong.Checked = false;
            _view.CHKHopDong.Checked = false;
            _view.CHKTrangThai.Checked = false;
            _view.CHKNganhHoc.Checked = false;
            _view.CHKNganhDay.Checked = false;
            _view.CHKChungChi.Checked = false;
            _view.CHKDangHocNangCao.Checked = false;
            checkAllState = false;
        }
        private void FormatDate(string fieldname)
        {
            _view.GVCustom.Columns[fieldname].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            _view.GVCustom.Columns[fieldname].DisplayFormat.FormatString = "dd/MM/yyyy";
        }
        private void SetCaptionColumn()
        {
            //Default
            _view.GVCustom.Columns["MaVienChuc"].Caption = "Mã viên chức";
            _view.GVCustom.Columns["Ho"].Caption = "Họ";
            _view.GVCustom.Columns["Ten"].Caption = "Tên";
            _view.GVCustom.Columns["GioiTinh"].Caption = "Giới tính";
            _view.GVCustom.Columns["DonVi"].Caption = "Đơn vị";
            _view.GVCustom.Columns["TrangThai"].Caption = "Trạng thái";
            //Vien chuc
            _view.GVCustom.Columns["SoDienThoai"].Caption = "Số điện thoại";           
            _view.GVCustom.Columns["NgaySinh"].Caption = "Ngày sinh";
            FormatDate("NgaySinh");
            _view.GVCustom.Columns["NoiSinh"].Caption = "Nơi sinh";
            _view.GVCustom.Columns["QueQuan"].Caption = "Quê quán";
            _view.GVCustom.Columns["DanToc"].Caption = "Dân tộc";
            _view.GVCustom.Columns["TonGiao"].Caption = "Tôn giáo";
            _view.GVCustom.Columns["HoKhauThuongTru"].Caption = "Hộ khẩu thường trú";
            _view.GVCustom.Columns["NoiOHienNay"].Caption = "Nơi ở hiện nay";
            _view.GVCustom.Columns["LaDangVien"].Caption = "Là đảng viên";
            _view.GVCustom.Columns["NgayVaoDang"].Caption = "Ngày vào đảng";
            FormatDate("NgayVaoDang");
            _view.GVCustom.Columns["NgayThamGiaCongTac"].Caption = "Ngày tham gia công tác";
            FormatDate("NgayThamGiaCongTac");
            _view.GVCustom.Columns["NgayVaoNganh"].Caption = "Ngày vào ngành";
            FormatDate("NgayVaoNganh");
            _view.GVCustom.Columns["NgayVeTruong"].Caption = "Ngày về trường";
            FormatDate("NgayVeTruong");
            _view.GVCustom.Columns["VanHoa"].Caption = "Văn hóa";
            _view.GVCustom.Columns["QuanLyNhaNuoc"].Caption = "Quản lý nhà nước";            
            //Cong tac
            _view.GVCustom.Columns["LoaiChucVu"].Caption = "Loại chức vụ";
            _view.GVCustom.Columns["ChucVu"].Caption = "Chức vụ";
            _view.GVCustom.Columns["HeSoChucVu"].Caption = "Hệ số chức vụ";
            _view.GVCustom.Columns["LoaiDonVi"].Caption = "Loại đơn vị";            
            _view.GVCustom.Columns["DiaDiemCT"].Caption = "Địa điểm (Công tác)";
            _view.GVCustom.Columns["DiaChi"].Caption = "Địa chỉ";
            _view.GVCustom.Columns["SoDienThoaiDonVi"].Caption = "Số điện thoại đơn vị";
            _view.GVCustom.Columns["ToChuyenMon"].Caption = "Tổ chuyên môn";
            _view.GVCustom.Columns["PhanLoaiCongTac"].Caption = "Phân loại công tác";
            _view.GVCustom.Columns["CheckPhanLoaiCongTac"].Caption = "Phân loại (Chức vụ)";
            _view.GVCustom.Columns["NgayBatDauCT"].Caption = "Ngày bắt đầu (Công tác)";
            FormatDate("NgayBatDauCT");
            _view.GVCustom.Columns["NgayKetThucCT"].Caption = "Ngày kết thúc (Công tác)";
            FormatDate("NgayKetThucCT");
            _view.GVCustom.Columns["LoaiThayDoi"].Caption = "Loại thay đổi";
            _view.GVCustom.Columns["KiemNhiem"].Caption = "Kiêm nhiệm";
            _view.GVCustom.Columns["LinkVanBanDinhKemCT"].Caption = "Link văn bản đính kèm (Công tác)";
            //Qua trinh luong
            _view.GVCustom.Columns["MaNgach"].Caption = "Mã ngạch";
            _view.GVCustom.Columns["TenNgach"].Caption = "Tên ngạch";
            _view.GVCustom.Columns["HeSoVuotKhungBaNamDau"].Caption = "Hệ số vượt khung ba năm đầu";
            _view.GVCustom.Columns["HeSoVuotKhungTrenBaNam"].Caption = "Hệ số vượt khung trên ba năm";
            _view.GVCustom.Columns["ThoiHanNangBac"].Caption = "Thời hạn nâng bậc";
            _view.GVCustom.Columns["Bac"].Caption = "Bậc";
            _view.GVCustom.Columns["HeSoBac"].Caption = "Hệ số";
            _view.GVCustom.Columns["NgayBatDauQTL"].Caption = "Ngày bắt đầu (Quá trình lương)";
            FormatDate("NgayBatDauQTL");
            _view.GVCustom.Columns["NgayLenLuong"].Caption = "Ngày lên lương";
            FormatDate("NgayLenLuong");
            _view.GVCustom.Columns["LinkVanBanDinhKemQTL"].Caption = "Link văn bản đính kèm (Quá trình lương)";
            //Hop dong
            _view.GVCustom.Columns["MaHopDong"].Caption = "Mã hợp đồng";
            _view.GVCustom.Columns["TenHopDong"].Caption = "Hợp đồng";
            _view.GVCustom.Columns["NgayBatDauHD"].Caption = "Ngày bắt đầu (Hợp đồng)";
            FormatDate("NgayBatDauHD");
            _view.GVCustom.Columns["NgayKetThucHD"].Caption = "Ngày kết thúc (Hợp đồng)";
            FormatDate("NgayKetThucHD");
            _view.GVCustom.Columns["MoTaHD"].Caption = "Mô tả (Hợp đồng)";
            _view.GVCustom.Columns["LinkVanBanDinhKemHD"].Caption = "Link văn bản đính kèm (Hợp đồng)";
            //Trang thai            
            _view.GVCustom.Columns["MoTaTT"].Caption = "Mô tả (Trạng thái)";
            _view.GVCustom.Columns["DiaDiemTT"].Caption = "Địa điểm (Trạng thái)";
            _view.GVCustom.Columns["NgayBatDauTT"].Caption = "Ngày bắt đầu (Trạng thái)";
            FormatDate("NgayBatDauTT");
            _view.GVCustom.Columns["NgayKetThucTT"].Caption = "Ngày kết thúc (Trạng thái)";
            FormatDate("NgayKetThucTT");
            _view.GVCustom.Columns["LinkVanBanDinhKemTT"].Caption = "Link văn bản đính kèm (Trạng thái)";
            //Nganh hoc
            _view.GVCustom.Columns["LoaiNganhNH"].Caption = "Loại ngành (Ngành học)";
            _view.GVCustom.Columns["NganhDaoTaoNH"].Caption = "Ngành đào tạo (Ngành học)";
            _view.GVCustom.Columns["ChuyenNganhNH"].Caption = "Chuyên ngành (Ngành học)";
            _view.GVCustom.Columns["LoaiHocHamHocViNH"].Caption = "Trình độ (Ngành học)";
            _view.GVCustom.Columns["TenHocHamHocViNH"].Caption = "Học hàm/Học vị (Ngành học)";
            _view.GVCustom.Columns["CoSoDaoTaoNH"].Caption = "Cơ sở đào tạo (Ngành học)";
            _view.GVCustom.Columns["NgonNguDaoTaoNH"].Caption = "Ngôn ngữ đào tạo (Ngành học)";
            _view.GVCustom.Columns["HinhThucDaoTaoNH"].Caption = "Hình thức đào tạo";
            _view.GVCustom.Columns["NuocCapBangNH"].Caption = "Nước cấp bằng (Ngành học)";
            _view.GVCustom.Columns["NgayCapBangNH"].Caption = "Ngày cấp bằng (Ngành học)";
            FormatDate("NgayCapBangNH");
            _view.GVCustom.Columns["LinkVanBanDinhKemHHHV_NH"].Caption = "Link văn bản đính kèm (Ngành học - Học hàm/Học vị)";
            _view.GVCustom.Columns["PhanLoaiNH"].Caption = "Phân loại (Ngành học)";
            _view.GVCustom.Columns["LinkVanBanDinhKemNH"].Caption = "Link văn bản đính kèm (Ngành học)";
            //Nganh day
            _view.GVCustom.Columns["LoaiNganhND"].Caption = "Loại ngành (Ngành dạy)";
            _view.GVCustom.Columns["NganhDaoTaoND"].Caption = "Ngành đào tạo (Ngành dạy)";
            _view.GVCustom.Columns["ChuyenNganhND"].Caption = "Chuyên ngành  (Ngành dạy)";
            _view.GVCustom.Columns["LoaiHocHamHocViND"].Caption = "Trình độ  (Ngành dạy)";
            _view.GVCustom.Columns["TenHocHamHocViND"].Caption = "Học hàm/Học vị (Ngành dạy)";
            _view.GVCustom.Columns["CoSoDaoTaoND"].Caption = "Cơ sở đào tạo (Ngành dạy)";
            _view.GVCustom.Columns["NgonNguDaoTaoND"].Caption = "Ngôn ngữ đào tạo (Ngành dạy)";
            _view.GVCustom.Columns["HinhThucDaoTaoND"].Caption = "Hình thức đào tạo (Ngành dạy)";
            _view.GVCustom.Columns["NuocCapBangND"].Caption = "Nước cấp bằng (Ngành dạy)";
            _view.GVCustom.Columns["NgayCapBangND"].Caption = "Ngày cấp bằng (Ngành dạy)";
            FormatDate("NgayCapBangND");
            _view.GVCustom.Columns["LinkVanBanDinhKemHHHV_ND"].Caption = "Link văn bản đính kèm (Ngành dạy - Học hàm/Học vị)";
            _view.GVCustom.Columns["PhanLoaiND"].Caption = "Phân loại (Ngành dạy)";
            _view.GVCustom.Columns["NgayBatDauND"].Caption = "Ngày bắt đầu (Ngành dạy)";
            FormatDate("NgayBatDauND");
            _view.GVCustom.Columns["NgayKetThucND"].Caption = "Ngày kết thúc (Ngành dạy)";
            FormatDate("NgayKetThucND");
            _view.GVCustom.Columns["LinkVanBanDinhKemND"].Caption = "Link văn bản đính kèm (Ngành dạy)";
            //Chung chi
            _view.GVCustom.Columns["NgoaiNgu"].Caption = "Ngoại ngữ";
            _view.GVCustom.Columns["TinHoc"].Caption = "Tin học";
            _view.GVCustom.Columns["LiLuanChinhTri"].Caption = "Lí luận chính trị";            
            _view.GVCustom.Columns["ChungChiChuyenMon"].Caption = "Chứng chỉ chuyên môn";
            //Dang hoc nang cao
            _view.GVCustom.Columns["SoQuyetDinh"].Caption = "Số quyết định";
            _view.GVCustom.Columns["LinkAnhQuyetDinh"].Caption = "Link ảnh quyết định";
            _view.GVCustom.Columns["LoaiHocHamHocViDHNC"].Caption = "Trình độ (Đang học nâng cao)";
            _view.GVCustom.Columns["Loai"].Caption = "Loại (Đang học nâng cao)";
            _view.GVCustom.Columns["TenHocHamHocViDHNC"].Caption = "Học hàm/Học vị (Đang học nâng cao)";
            _view.GVCustom.Columns["CoSoDaoTaoDHNC"].Caption = "Cơ sở đào tạo (Đang học nâng cao)";
            _view.GVCustom.Columns["NgonNguDaoTaoDHNC"].Caption = "Ngôn ngữ đào tạo (Đang học nâng cao)";
            _view.GVCustom.Columns["HinhThucDaoTaoDHNC"].Caption = "Hình thức đào tạo (Đang học nâng cao)";
            _view.GVCustom.Columns["NuocCapBangDHNC"].Caption = "Nước cấp bằng (Đang học nâng cao)";
            _view.GVCustom.Columns["NgayBatDauDHNC"].Caption = "Ngày bắt đầu (Đang học nâng cao)";
            FormatDate("NgayBatDauDHNC");
            _view.GVCustom.Columns["NgayKetThucDHNC"].Caption = "Ngày kết thúc (Đang học nâng cao)";
            FormatDate("NgayKetThucDHNC");
            // index to add row for export one domain
            _view.GVCustom.Columns["Index"].Visible = false;
        }
        private void GetDataDuration()
        {
            FilterCurrent = _view.GVCustom.ActiveFilterString;
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            DateTime dtFromDuration = _view.DTFromDuration.DateTime;
            DateTime dtToDuration = _view.DTToDuration.DateTime;
            List<ExportObjects> listFieldsDefault = unitOfWorks.VienChucRepository.GetListFieldsDefaultByDuration(dtFromDuration, dtToDuration);
            if (_view.CHKThongTinCaNhan.Checked)
            {
                foreach (var row in listFieldsDefault)
                {
                    VienChuc vienChuc = unitOfWorks.VienChucRepository.GetVienChucByIdVienChuc(row.IdVienChuc);
                    if (vienChuc != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.SoDienThoai = vienChuc.sDT;
                        exportObjects.NgaySinh = vienChuc.ngaySinh;
                        exportObjects.NoiSinh = vienChuc.noiSinh;
                        exportObjects.QueQuan = vienChuc.queQuan;
                        exportObjects.DanToc = vienChuc.DanToc.tenDanToc;
                        exportObjects.TonGiao = vienChuc.TonGiao.tenTonGiao;
                        exportObjects.HoKhauThuongTru = vienChuc.hoKhauThuongTru;
                        exportObjects.NoiOHienNay = vienChuc.noiOHienNay;
                        exportObjects.LaDangVien = unitOfWorks.VienChucRepository.ReturnLaDangVienToGrid(vienChuc.laDangVien);
                        exportObjects.NgayVaoDang = vienChuc.ngayVaoDang;
                        exportObjects.NgayThamGiaCongTac = vienChuc.ngayThamGiaCongTac;
                        exportObjects.NgayVaoNganh = vienChuc.ngayVaoNganh;
                        exportObjects.NgayVeTruong = vienChuc.ngayVeTruong;
                        exportObjects.VanHoa = vienChuc.vanHoa;
                        exportObjects.QuanLyNhaNuoc = vienChuc.QuanLyNhaNuoc.tenQuanLyNhaNuoc;
                        exportObjects.GhiChu = vienChuc.ghiChu;
                    }
                }
            }
            if (_view.CHKCongTac.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    ChucVuDonViVienChuc chucVuDonViVienChuc = unitOfWorks.ChucVuDonViVienChucRepository.GetCongTacByIdVienChucAndDuration(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (chucVuDonViVienChuc != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.LoaiChucVu = chucVuDonViVienChuc.ChucVu.LoaiChucVu.tenLoaiChucVu;
                        exportObjects.ChucVu = chucVuDonViVienChuc.ChucVu.tenChucVu;
                        exportObjects.HeSoChucVu = chucVuDonViVienChuc.ChucVu.heSoChucVu;
                        exportObjects.LoaiDonVi = chucVuDonViVienChuc.DonVi.LoaiDonVi.tenLoaiDonVi;
                        exportObjects.DonVi = chucVuDonViVienChuc.DonVi.tenDonVi;
                        exportObjects.DiaDiemCT = chucVuDonViVienChuc.DonVi.diaDiem;
                        exportObjects.DiaChi = chucVuDonViVienChuc.DonVi.diaChi;
                        exportObjects.SoDienThoaiDonVi = chucVuDonViVienChuc.DonVi.sDT;
                        exportObjects.ToChuyenMon = chucVuDonViVienChuc.ToChuyenMon.tenToChuyenMon;
                        exportObjects.PhanLoaiCongTac = chucVuDonViVienChuc.phanLoaiCongTac;
                        exportObjects.CheckPhanLoaiCongTac = unitOfWorks.ChucVuDonViVienChucRepository.HardCheckPhanLoaiCongTacToGrid(chucVuDonViVienChuc.checkPhanLoaiCongTac);
                        exportObjects.NgayBatDauCT = chucVuDonViVienChuc.ngayBatDau;
                        exportObjects.NgayKetThucCT = chucVuDonViVienChuc.ngayKetThuc;
                        exportObjects.LoaiThayDoi = unitOfWorks.ChucVuDonViVienChucRepository.HardLoaiThayDoiToGrid(chucVuDonViVienChuc.loaiThayDoi);
                        exportObjects.KiemNhiem = unitOfWorks.ChucVuDonViVienChucRepository.HardKiemNhiemToGrid(chucVuDonViVienChuc.kiemNhiem);
                        exportObjects.LinkVanBanDinhKemCT = chucVuDonViVienChuc.linkVanBanDinhKem;
                    }
                }
            }
            if (_view.CHKQuaTrinhLuong.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    QuaTrinhLuong quaTrinhLuong = unitOfWorks.QuaTrinhLuongRepository.GetQuaTrinhLuongByIdVienChucAndDurationForExportOne(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (quaTrinhLuong != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.MaNgach = quaTrinhLuong.Bac.Ngach.maNgach;
                        exportObjects.TenNgach = quaTrinhLuong.Bac.Ngach.tenNgach;
                        exportObjects.HeSoVuotKhungBaNamDau = quaTrinhLuong.Bac.Ngach.heSoVuotKhungBaNamDau;
                        exportObjects.HeSoVuotKhungTrenBaNam = quaTrinhLuong.Bac.Ngach.heSoVuotKhungTrenBaNam;
                        exportObjects.ThoiHanNangBac = quaTrinhLuong.Bac.Ngach.thoiHanNangBac;
                        exportObjects.Bac = quaTrinhLuong.Bac.bac1;
                        exportObjects.HeSoBac = quaTrinhLuong.Bac.heSoBac;
                        exportObjects.NgayBatDauQTL = quaTrinhLuong.ngayBatDau;
                        exportObjects.NgayLenLuong = quaTrinhLuong.ngayLenLuong;
                        exportObjects.LinkVanBanDinhKemQTL = quaTrinhLuong.linkVanBanDinhKem;
                    }
                }
            }
            if (_view.CHKHopDong.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    HopDongVienChuc hopDong = unitOfWorks.HopDongVienChucRepository.GetListHopDongByIdVienChucAndDurationForExportOne(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (hopDong != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.MaHopDong = hopDong.LoaiHopDong.maLoaiHopDong;
                        exportObjects.TenHopDong = hopDong.LoaiHopDong.tenLoaiHopDong;
                        exportObjects.NgayBatDauHD = hopDong.ngayBatDau;
                        exportObjects.NgayKetThucHD = hopDong.ngayKetThuc;
                        exportObjects.MoTaHD = hopDong.moTa;
                        exportObjects.LinkVanBanDinhKemHD = hopDong.linkVanBanDinhKem;
                    }
                }
            }
            if (_view.CHKTrangThai.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    TrangThaiVienChuc trangThai = unitOfWorks.TrangThaiVienChucRepository.GetTrangThaiByIdVienChucAndDuration(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (trangThai != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.TrangThai = trangThai.TrangThai.tenTrangThai;
                        exportObjects.MoTaTT = trangThai.moTa;
                        exportObjects.DiaDiemTT = trangThai.diaDiem;
                        exportObjects.NgayBatDauTT = trangThai.ngayBatDau;
                        exportObjects.NgayKetThucTT = trangThai.ngayKetThuc;
                        exportObjects.LinkVanBanDinhKemTT = trangThai.linkVanBanDinhKem;
                    }
                }
            }
            if (_view.CHKNganhHoc.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    NganhVienChuc nganhHoc = unitOfWorks.NganhVienChucRepository.GetNganhHocByIdVienChucAndDurationForExportOne(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (nganhHoc != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.LoaiNganhNH = nganhHoc.LoaiNganh.tenLoaiNganh;
                        exportObjects.NganhDaoTaoNH = nganhHoc.NganhDaoTao.tenNganhDaoTao;
                        exportObjects.ChuyenNganhNH = nganhHoc.ChuyenNganh.tenChuyenNganh;
                        exportObjects.LoaiHocHamHocViNH = nganhHoc.HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi;
                        exportObjects.TenHocHamHocViNH = nganhHoc.HocHamHocViVienChuc.tenHocHamHocVi;
                        exportObjects.CoSoDaoTaoNH = nganhHoc.HocHamHocViVienChuc.coSoDaoTao;
                        exportObjects.NgonNguDaoTaoNH = nganhHoc.HocHamHocViVienChuc.ngonNguDaoTao;
                        exportObjects.HinhThucDaoTaoNH = nganhHoc.HocHamHocViVienChuc.hinhThucDaoTao;
                        exportObjects.NuocCapBangNH = nganhHoc.HocHamHocViVienChuc.nuocCapBang;
                        exportObjects.NgayCapBangNH = nganhHoc.HocHamHocViVienChuc.ngayCapBang;
                        exportObjects.LinkVanBanDinhKemHHHV_NH = nganhHoc.HocHamHocViVienChuc.linkVanBanDinhKem;
                        exportObjects.PhanLoaiNH = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(nganhHoc.phanLoai);
                        exportObjects.LinkVanBanDinhKemNH = nganhHoc.linkVanBanDinhKem;
                    }
                }
            }
            if (_view.CHKNganhDay.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    NganhVienChuc nganhDay = unitOfWorks.NganhVienChucRepository.GetNganhDayByIdVienChucAndDuration(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (nganhDay != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.LoaiNganhND = nganhDay.LoaiNganh.tenLoaiNganh;
                        exportObjects.NganhDaoTaoND = nganhDay.NganhDaoTao.tenNganhDaoTao;
                        exportObjects.ChuyenNganhND = nganhDay.ChuyenNganh.tenChuyenNganh;
                        exportObjects.LoaiHocHamHocViND = nganhDay.HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi;
                        exportObjects.TenHocHamHocViND = nganhDay.HocHamHocViVienChuc.tenHocHamHocVi;
                        exportObjects.CoSoDaoTaoND = nganhDay.HocHamHocViVienChuc.coSoDaoTao;
                        exportObjects.NgonNguDaoTaoND = nganhDay.HocHamHocViVienChuc.ngonNguDaoTao;
                        exportObjects.HinhThucDaoTaoND = nganhDay.HocHamHocViVienChuc.hinhThucDaoTao;
                        exportObjects.NuocCapBangND = nganhDay.HocHamHocViVienChuc.nuocCapBang;
                        exportObjects.NgayCapBangND = nganhDay.HocHamHocViVienChuc.ngayCapBang;
                        exportObjects.LinkVanBanDinhKemHHHV_ND = nganhDay.HocHamHocViVienChuc.linkVanBanDinhKem;
                        exportObjects.PhanLoaiND = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(nganhDay.phanLoai);
                        exportObjects.NgayBatDauND = nganhDay.ngayBatDau;
                        exportObjects.NgayKetThucND = nganhDay.ngayKetThuc;
                        exportObjects.LinkVanBanDinhKemND = nganhDay.linkVanBanDinhKem;
                    }
                }
            }
            if (_view.CHKChungChi.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    var ngoaiNgu = unitOfWorks.ChungChiVienChucRepository.GetChungChiNgoaiNguByIdVienChucAndDuration(row.IdVienChuc, dtFromDuration, dtToDuration);
                    var tinHoc = unitOfWorks.ChungChiVienChucRepository.GetChungChiTinHocByIdVienChucAndDuration(row.IdVienChuc, dtFromDuration, dtToDuration);
                    var nghiepVuSuPham = unitOfWorks.ChungChiVienChucRepository.GetChungChiNghiepVuSuPhamByIdVienChucAndDuration(row.IdVienChuc, dtFromDuration, dtToDuration);
                    var liLuanChinhTri = unitOfWorks.ChungChiVienChucRepository.GetChungChiLiLuanChinhTriByIdVienChucAndDuration(row.IdVienChuc, dtFromDuration, dtToDuration);
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                    if (ngoaiNgu != null)
                    {
                        exportObjects.NgoaiNgu = ngoaiNgu.LoaiChungChi.tenLoaiChungChi;
                        if (ngoaiNgu.capDoChungChi != null)
                            exportObjects.NgoaiNgu += ", " + ngoaiNgu.capDoChungChi;
                        exportObjects.ChungChiChuyenMon += ngoaiNgu.LoaiChungChi.tenLoaiChungChi + ", ";
                    }
                    if (tinHoc != null)
                    {
                        exportObjects.TinHoc = tinHoc.LoaiChungChi.tenLoaiChungChi;
                        if (tinHoc.capDoChungChi != null)
                            exportObjects.TinHoc += ", " + tinHoc.capDoChungChi;
                        exportObjects.ChungChiChuyenMon += tinHoc.LoaiChungChi.tenLoaiChungChi + ", ";
                    }
                    if (nghiepVuSuPham != null)
                    {
                        exportObjects.ChungChiChuyenMon += nghiepVuSuPham.LoaiChungChi.tenLoaiChungChi + ", ";
                    }
                    if (liLuanChinhTri != null)
                    {
                        exportObjects.LiLuanChinhTri = liLuanChinhTri.LoaiChungChi.tenLoaiChungChi;
                        if (liLuanChinhTri.capDoChungChi != null)
                            exportObjects.LiLuanChinhTri += ", " + liLuanChinhTri.capDoChungChi;
                        exportObjects.ChungChiChuyenMon += liLuanChinhTri;
                    }
                }
            }
            if (_view.CHKDangHocNangCao.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    DangHocNangCao dangHocNangCao = unitOfWorks.DangHocNangCaoRepository.GetDangHocNangCaoByIdVienChucAndDurationForExportOne(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (dangHocNangCao != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.LoaiHocHamHocViDHNC = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiHocHamHocViToGrid(dangHocNangCao.LoaiHocHamHocVi.tenLoaiHocHamHocVi);
                        exportObjects.SoQuyetDinh = dangHocNangCao.soQuyetDinh;
                        exportObjects.LinkAnhQuyetDinh = dangHocNangCao.linkAnhQuyetDinh;
                        exportObjects.TenHocHamHocViDHNC = dangHocNangCao.tenHocHamHocVi;
                        exportObjects.CoSoDaoTaoDHNC = dangHocNangCao.coSoDaoTao;
                        exportObjects.NgonNguDaoTaoDHNC = dangHocNangCao.ngonNguDaoTao;
                        exportObjects.HinhThucDaoTaoDHNC = dangHocNangCao.hinhThucDaoTao;
                        exportObjects.NuocCapBangDHNC = dangHocNangCao.nuocCapBang;
                        exportObjects.NgayBatDauTT = dangHocNangCao.ngayBatDau;
                        exportObjects.NgayKetThucTT = dangHocNangCao.ngayKetThuc;
                        exportObjects.Loai = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiToGrid(dangHocNangCao.loai);
                    }
                }
            }
            _view.GCCustom.DataSource = null;
            _view.GCCustom.DataSource = listFieldsDefault;
            _view.GVCustom.PopulateColumns();
            _view.GVCustom.Columns[0].Visible = false;
            SplashScreenManager.CloseForm(false);
            for (int i = 2; i < _view.GVCustom.Columns.Count; i++)
            {
                for (int j = 0; j < _view.GVCustom.RowCount; j++)
                {
                    if (_view.GVCustom.GetRowCellDisplayText(j, _view.GVCustom.Columns[i]) != "")
                    {
                        _view.GVCustom.Columns[i].Visible = true;
                        break;
                    }
                    else { _view.GVCustom.Columns[i].Visible = false; }
                }
            }
            SetCaptionColumn();
            if (_view.CHKSaveFilter.Checked)
                _view.GVCustom.ActiveFilterString = FilterCurrent;
        }
        private void GetDataTimeline()
        {
            FilterCurrent = _view.GVCustom.ActiveFilterString;
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            DateTime dtTimeline = _view.DTTimeline.DateTime;
            List<ExportObjects> listFieldsDefault = unitOfWorks.VienChucRepository.GetListFieldsDefaultByTimeline(dtTimeline);
            if (_view.CHKThongTinCaNhan.Checked)
            {
                foreach(var row in listFieldsDefault)
                {
                    VienChuc vienChuc = unitOfWorks.VienChucRepository.GetVienChucByIdVienChuc(row.IdVienChuc);
                    if (vienChuc != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.SoDienThoai = vienChuc.sDT;
                        exportObjects.NgaySinh = vienChuc.ngaySinh;
                        exportObjects.NoiSinh = vienChuc.noiSinh;
                        exportObjects.QueQuan = vienChuc.queQuan;
                        exportObjects.DanToc = vienChuc.DanToc.tenDanToc;
                        exportObjects.TonGiao = vienChuc.TonGiao.tenTonGiao;
                        exportObjects.HoKhauThuongTru = vienChuc.hoKhauThuongTru;
                        exportObjects.NoiOHienNay = vienChuc.noiOHienNay;
                        exportObjects.LaDangVien = unitOfWorks.VienChucRepository.ReturnLaDangVienToGrid(vienChuc.laDangVien);
                        exportObjects.NgayVaoDang = vienChuc.ngayVaoDang;
                        exportObjects.NgayThamGiaCongTac = vienChuc.ngayThamGiaCongTac;
                        exportObjects.NgayVaoNganh = vienChuc.ngayVaoNganh;
                        exportObjects.NgayVeTruong = vienChuc.ngayVeTruong;
                        exportObjects.VanHoa = vienChuc.vanHoa;
                        exportObjects.QuanLyNhaNuoc = vienChuc.QuanLyNhaNuoc.tenQuanLyNhaNuoc;
                        exportObjects.GhiChu = vienChuc.ghiChu;
                    }
                }
            }
            if (_view.CHKCongTac.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {                   
                    ChucVuDonViVienChuc congTac = unitOfWorks.ChucVuDonViVienChucRepository.GetCongTacByIdVienChucAndTimeline(row.IdVienChuc, dtTimeline);
                    if(congTac != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.LoaiChucVu = congTac.ChucVu.LoaiChucVu.tenLoaiChucVu;
                        exportObjects.ChucVu = congTac.ChucVu.tenChucVu;
                        exportObjects.HeSoChucVu = congTac.ChucVu.heSoChucVu;
                        exportObjects.LoaiDonVi = congTac.DonVi.LoaiDonVi.tenLoaiDonVi;
                        exportObjects.DiaDiemCT = congTac.DonVi.diaDiem;
                        exportObjects.DiaChi = congTac.DonVi.diaChi;
                        exportObjects.SoDienThoaiDonVi = congTac.DonVi.sDT;
                        exportObjects.ToChuyenMon = congTac.ToChuyenMon.tenToChuyenMon;
                        exportObjects.PhanLoaiCongTac = congTac.phanLoaiCongTac;
                        exportObjects.CheckPhanLoaiCongTac = unitOfWorks.ChucVuDonViVienChucRepository.HardCheckPhanLoaiCongTacToGrid(congTac.checkPhanLoaiCongTac);
                        exportObjects.NgayBatDauCT = congTac.ngayBatDau;
                        exportObjects.NgayKetThucCT = congTac.ngayKetThuc;
                        exportObjects.LoaiThayDoi = unitOfWorks.ChucVuDonViVienChucRepository.HardLoaiThayDoiToGrid(congTac.loaiThayDoi);
                        exportObjects.KiemNhiem = unitOfWorks.ChucVuDonViVienChucRepository.HardKiemNhiemToGrid(congTac.kiemNhiem);
                        exportObjects.LinkVanBanDinhKemCT = congTac.linkVanBanDinhKem;
                    }                        
                }
            }
            if (_view.CHKTrangThai.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {                    
                    TrangThaiVienChuc trangThai = unitOfWorks.TrangThaiVienChucRepository.GetTrangThaiByIdVienChucAndTimeline(row.IdVienChuc, dtTimeline);
                    if(trangThai != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.MoTaTT = trangThai.moTa;
                        exportObjects.DiaDiemTT = trangThai.diaDiem;
                        exportObjects.NgayBatDauTT = trangThai.ngayBatDau;
                        exportObjects.NgayKetThucTT = trangThai.ngayKetThuc;
                        exportObjects.LinkVanBanDinhKemTT = trangThai.linkVanBanDinhKem;
                    }
                }
            }
            if (_view.CHKQuaTrinhLuong.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    QuaTrinhLuong quaTrinhLuong = unitOfWorks.QuaTrinhLuongRepository.GetQuaTrinhLuongByIdVienChucAndTimelineForExportOne(row.IdVienChuc, dtTimeline);
                    if (quaTrinhLuong != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.MaNgach = quaTrinhLuong.Bac.Ngach.maNgach;
                        exportObjects.TenNgach = quaTrinhLuong.Bac.Ngach.tenNgach;
                        exportObjects.HeSoVuotKhungBaNamDau = quaTrinhLuong.Bac.Ngach.heSoVuotKhungBaNamDau;
                        exportObjects.HeSoVuotKhungTrenBaNam = quaTrinhLuong.Bac.Ngach.heSoVuotKhungTrenBaNam;
                        exportObjects.ThoiHanNangBac = quaTrinhLuong.Bac.Ngach.thoiHanNangBac;
                        exportObjects.Bac = quaTrinhLuong.Bac.bac1;
                        exportObjects.HeSoBac = quaTrinhLuong.Bac.heSoBac;
                        exportObjects.NgayBatDauQTL = quaTrinhLuong.ngayBatDau;
                        exportObjects.NgayLenLuong = quaTrinhLuong.ngayLenLuong;
                        exportObjects.LinkVanBanDinhKemQTL = quaTrinhLuong.linkVanBanDinhKem;
                    }
                }
            }
            if (_view.CHKHopDong.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    HopDongVienChuc hopDong = unitOfWorks.HopDongVienChucRepository.GetListHopDongByIdVienChucAndTimelineForExportOne(row.IdVienChuc, dtTimeline);
                    if (hopDong != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.MaHopDong = hopDong.LoaiHopDong.maLoaiHopDong;
                        exportObjects.TenHopDong = hopDong.LoaiHopDong.tenLoaiHopDong;
                        exportObjects.NgayBatDauHD = hopDong.ngayBatDau;
                        exportObjects.NgayKetThucHD = hopDong.ngayKetThuc;
                        exportObjects.MoTaHD = hopDong.moTa;
                        exportObjects.LinkVanBanDinhKemHD = hopDong.linkVanBanDinhKem;
                    }
                }
            }
            if (_view.CHKNganhHoc.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    NganhVienChuc nganhHoc = unitOfWorks.NganhVienChucRepository.GetNganhHocByIdVienChucAndTimelineForExportOne(row.IdVienChuc, dtTimeline);
                    if (nganhHoc != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.LoaiNganhNH = nganhHoc.LoaiNganh.tenLoaiNganh;
                        exportObjects.NganhDaoTaoNH = nganhHoc.NganhDaoTao.tenNganhDaoTao;
                        exportObjects.ChuyenNganhNH = nganhHoc.ChuyenNganh.tenChuyenNganh;
                        exportObjects.LoaiHocHamHocViNH = nganhHoc.HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi;
                        exportObjects.TenHocHamHocViNH = nganhHoc.HocHamHocViVienChuc.tenHocHamHocVi;
                        exportObjects.CoSoDaoTaoNH = nganhHoc.HocHamHocViVienChuc.coSoDaoTao;
                        exportObjects.NgonNguDaoTaoNH = nganhHoc.HocHamHocViVienChuc.ngonNguDaoTao;
                        exportObjects.HinhThucDaoTaoNH = nganhHoc.HocHamHocViVienChuc.hinhThucDaoTao;
                        exportObjects.NuocCapBangNH = nganhHoc.HocHamHocViVienChuc.nuocCapBang;
                        exportObjects.NgayCapBangNH = nganhHoc.HocHamHocViVienChuc.ngayCapBang;
                        exportObjects.LinkVanBanDinhKemHHHV_NH = nganhHoc.HocHamHocViVienChuc.linkVanBanDinhKem;
                        exportObjects.PhanLoaiNH = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(nganhHoc.phanLoai);
                        exportObjects.LinkVanBanDinhKemNH = nganhHoc.linkVanBanDinhKem;
                    }
                }
            }
            if (_view.CHKNganhDay.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    NganhVienChuc nganhDay = unitOfWorks.NganhVienChucRepository.GetNganhDayByIdVienChucAndTimeline(row.IdVienChuc, dtTimeline);
                    if (nganhDay != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.LoaiNganhND = nganhDay.LoaiNganh.tenLoaiNganh;
                        exportObjects.NganhDaoTaoND = nganhDay.NganhDaoTao.tenNganhDaoTao;
                        exportObjects.ChuyenNganhND = nganhDay.ChuyenNganh.tenChuyenNganh;
                        exportObjects.LoaiHocHamHocViND = nganhDay.HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi;
                        exportObjects.TenHocHamHocViND = nganhDay.HocHamHocViVienChuc.tenHocHamHocVi;
                        exportObjects.CoSoDaoTaoND = nganhDay.HocHamHocViVienChuc.coSoDaoTao;
                        exportObjects.NgonNguDaoTaoND = nganhDay.HocHamHocViVienChuc.ngonNguDaoTao;
                        exportObjects.HinhThucDaoTaoND = nganhDay.HocHamHocViVienChuc.hinhThucDaoTao;
                        exportObjects.NuocCapBangND = nganhDay.HocHamHocViVienChuc.nuocCapBang;
                        exportObjects.NgayCapBangND = nganhDay.HocHamHocViVienChuc.ngayCapBang;
                        exportObjects.LinkVanBanDinhKemHHHV_ND = nganhDay.HocHamHocViVienChuc.linkVanBanDinhKem;
                        exportObjects.PhanLoaiND = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(nganhDay.phanLoai);
                        exportObjects.NgayBatDauND = nganhDay.ngayBatDau;
                        exportObjects.NgayKetThucND = nganhDay.ngayKetThuc;
                        exportObjects.LinkVanBanDinhKemND = nganhDay.linkVanBanDinhKem;
                    }
                }
            }
            if (_view.CHKChungChi.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    var ngoaiNgu = unitOfWorks.ChungChiVienChucRepository.GetChungChiNgoaiNguByIdVienChucAndTimeline(row.IdVienChuc, dtTimeline);
                    var tinHoc = unitOfWorks.ChungChiVienChucRepository.GetChungChiTinHocByIdVienChucAndTimeline(row.IdVienChuc, dtTimeline);
                    var nghiepVuSuPham = unitOfWorks.ChungChiVienChucRepository.GetChungChiNghiepVuSuPhamByIdVienChucAndTimeline(row.IdVienChuc, dtTimeline);
                    var liLuanChinhTri = unitOfWorks.ChungChiVienChucRepository.GetChungChiLiLuanChinhTriByIdVienChucAndTimeline(row.IdVienChuc, dtTimeline);
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                    if (ngoaiNgu != null)
                    {
                        exportObjects.NgoaiNgu = ngoaiNgu.LoaiChungChi.tenLoaiChungChi;
                        if (ngoaiNgu.capDoChungChi != null)
                            exportObjects.NgoaiNgu += ", " + ngoaiNgu.capDoChungChi;
                        exportObjects.ChungChiChuyenMon += ngoaiNgu.LoaiChungChi.tenLoaiChungChi + ", ";
                    }
                    if(tinHoc != null)
                    {
                        exportObjects.TinHoc = tinHoc.LoaiChungChi.tenLoaiChungChi;
                        if (tinHoc.capDoChungChi != null)
                            exportObjects.TinHoc += ", " + tinHoc.capDoChungChi;
                        exportObjects.ChungChiChuyenMon += tinHoc.LoaiChungChi.tenLoaiChungChi + ", ";
                    }
                    if (nghiepVuSuPham != null)
                    {
                        exportObjects.ChungChiChuyenMon += nghiepVuSuPham.LoaiChungChi.tenLoaiChungChi + ", ";
                    }
                    if(liLuanChinhTri != null)
                    {
                        exportObjects.LiLuanChinhTri = liLuanChinhTri.LoaiChungChi.tenLoaiChungChi;
                        if (liLuanChinhTri.capDoChungChi != null)
                            exportObjects.LiLuanChinhTri += ", " + liLuanChinhTri.capDoChungChi;
                        exportObjects.ChungChiChuyenMon += liLuanChinhTri;
                    }
                }
            }
            if (_view.CHKDangHocNangCao.Checked)
            {
                foreach (var row in listFieldsDefault.ToList())
                {
                    DangHocNangCao dangHocNangCao = unitOfWorks.DangHocNangCaoRepository.GetDangHocNangCaoByIdVienChucAndTimelineForExportOne(row.IdVienChuc, dtTimeline);
                    if (dangHocNangCao != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.LoaiHocHamHocViDHNC = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiHocHamHocViToGrid(dangHocNangCao.LoaiHocHamHocVi.tenLoaiHocHamHocVi);
                        exportObjects.SoQuyetDinh = dangHocNangCao.soQuyetDinh;
                        exportObjects.LinkAnhQuyetDinh = dangHocNangCao.linkAnhQuyetDinh;
                        exportObjects.TenHocHamHocViDHNC = dangHocNangCao.tenHocHamHocVi;
                        exportObjects.CoSoDaoTaoDHNC = dangHocNangCao.coSoDaoTao;
                        exportObjects.NgonNguDaoTaoDHNC = dangHocNangCao.ngonNguDaoTao;
                        exportObjects.HinhThucDaoTaoDHNC = dangHocNangCao.hinhThucDaoTao;
                        exportObjects.NuocCapBangDHNC = dangHocNangCao.nuocCapBang;
                        exportObjects.NgayBatDauTT = dangHocNangCao.ngayBatDau;
                        exportObjects.NgayKetThucTT = dangHocNangCao.ngayKetThuc;
                        exportObjects.Loai = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiToGrid(dangHocNangCao.loai);
                    }
                }
            }
            _view.GCCustom.DataSource = null;
            _view.GCCustom.DataSource = listFieldsDefault;
            _view.GVCustom.PopulateColumns();
            _view.GVCustom.Columns[0].Visible = false;
            SplashScreenManager.CloseForm(false);
            for (int i = 2; i < _view.GVCustom.Columns.Count; i++)
            {
                for (int j = 0; j < _view.GVCustom.RowCount; j++)
                {
                    if (_view.GVCustom.GetRowCellDisplayText(j, _view.GVCustom.Columns[i]) != string.Empty)
                    {
                        _view.GVCustom.Columns[i].Visible = true;
                        break;
                    }
                    else _view.GVCustom.Columns[i].Visible = false;
                }
            }
            SetCaptionColumn();
            if (_view.CHKSaveFilter.Checked)
                _view.GVCustom.ActiveFilterString = FilterCurrent;
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVCustom.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void CheckAllAndUncheckAll()
        {            
            if(_view.CHKThongTinCaNhan.Checked == false && _view.CHKCongTac.Checked == false && 
               _view.CHKQuaTrinhLuong.Checked == false && _view.CHKHopDong.Checked == false &&
               _view.CHKTrangThai.Checked == false && _view.CHKNganhHoc.Checked == false &&
               _view.CHKNganhDay.Checked == false && _view.CHKChungChi.Checked == false && _view.CHKDangHocNangCao.Checked == false)
            {
                checkAllState = false;
            }
            if (checkAllState == false)
            {
                _view.CHKThongTinCaNhan.Checked = true;
                _view.CHKCongTac.Checked = true;
                _view.CHKQuaTrinhLuong.Checked = true;
                _view.CHKHopDong.Checked = true;
                _view.CHKTrangThai.Checked = true;
                _view.CHKNganhHoc.Checked = true;
                _view.CHKNganhDay.Checked = true;
                _view.CHKChungChi.Checked = true;
                _view.CHKDangHocNangCao.Checked = true;
                checkAllState = true;
            }
            else
            {
                UncheckAll();
            }
        }
        
        public void EnableFilterDatetime(object sender, EventArgs e)
        {
            if(_view.RADSelectTimeToFilter.SelectedIndex == 0)
            {
                _view.DTTimeline.Enabled = true;
                _view.DTFromDuration.Enabled = false;
                _view.DTToDuration.Enabled = false;
                _view.DTFromDuration.EditValue = null;
                _view.DTToDuration.EditValue = null;
            }
            else
            {
                _view.DTTimeline.Enabled = false;
                _view.DTFromDuration.Enabled = true;
                _view.DTToDuration.Enabled = true;
                _view.DTTimeline.EditValue = null;
            }
        }

        public void Cancel()
        {
            UncheckAll();
            _view.RADSelectTimeToFilter.SelectedIndex = 0;
            _view.DTTimeline.Enabled = true;
            _view.DTFromDuration.Enabled = false;
            _view.DTToDuration.Enabled = false;
            _view.DTTimeline.EditValue = null;
            _view.DTFromDuration.EditValue = null;
            _view.DTToDuration.EditValue = null;
            _view.DTTimeline.ErrorText = null;
            _view.DTFromDuration.ErrorText = null;
            _view.DTToDuration.ErrorText = null;
            _view.GCCustom.DataSource = null;
        }

        public void ExportData()
        {
            if (_view.RADSelectTimeToFilter.SelectedIndex == 0) //moc thoi gian
            {
                if (listCheckBoxValue.Any(x => x == true))
                {
                    if (_view.DTTimeline.Text != "")
                    {
                        GetDataTimeline();
                    }
                    else
                    {
                        _view.DTTimeline.ErrorText = "Vui lòng chọn thời gian.";
                        _view.DTTimeline.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                }
                else
                {
                    XtraMessageBox.Show("Vui lòng chọn Lĩnh vực.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else                                            //khoang thoi gian
            {
                if (listCheckBoxValue.Any(x => x == true))
                {
                    if (_view.DTFromDuration.Text != "")
                    {
                        GetDataDuration();
                    }
                    else
                    {
                        _view.DTFromDuration.ErrorText = "Vui lòng chọn thời gian bắt đầu.";
                        _view.DTFromDuration.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                }
                else
                {
                    XtraMessageBox.Show("Vui lòng chọn Lĩnh vực.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }  

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        public void CHKThongTinCaNhanChanged(object sender, EventArgs e)
        {
            if (_view.CHKThongTinCaNhan.Checked)
            {
                listCheckBoxValue[0] = _view.CHKThongTinCaNhan.Checked;
            }
            else
            {
                listCheckBoxValue[0] = _view.CHKThongTinCaNhan.Checked;
            }
        }

        public void CHKCongTacChanged(object sender, EventArgs e)
        {
            if (_view.CHKCongTac.Checked)
            {
                listCheckBoxValue[1] = _view.CHKCongTac.Checked;
            }
            else
            {
                listCheckBoxValue[1] = _view.CHKCongTac.Checked;
            }
        }

        public void CHKQuaTrinhLuongChanged(object sender, EventArgs e)
        {
            if (_view.CHKQuaTrinhLuong.Checked)
            {
                listCheckBoxValue[2] = _view.CHKQuaTrinhLuong.Checked;
            }
            else
            {
                listCheckBoxValue[2] = _view.CHKQuaTrinhLuong.Checked;
            }
        }

        public void CHKHopDongChanged(object sender, EventArgs e)
        {
            if (_view.CHKHopDong.Checked)
            {
                listCheckBoxValue[3] = _view.CHKHopDong.Checked;
            }
            else
            {
                listCheckBoxValue[3] = _view.CHKHopDong.Checked;
            }
        }

        public void CHKTrangThaiChanged(object sender, EventArgs e)
        {
            if (_view.CHKTrangThai.Checked)
            {
                listCheckBoxValue[4] = _view.CHKTrangThai.Checked;
            }
            else
            {
                listCheckBoxValue[4] = _view.CHKTrangThai.Checked;
            }
        }

        public void CHKNganhHocChanged(object sender, EventArgs e)
        {
            if (_view.CHKNganhHoc.Checked)
            {
                listCheckBoxValue[5] = _view.CHKNganhHoc.Checked;
            }
            else
            {
                listCheckBoxValue[5] = _view.CHKNganhHoc.Checked;
            }
        }

        public void CHKNganhDayChanged(object sender, EventArgs e)
        {
            if (_view.CHKNganhDay.Checked)
            {
                listCheckBoxValue[6] = _view.CHKNganhDay.Checked;
            }
            else
            {
                listCheckBoxValue[6] = _view.CHKNganhDay.Checked;
            }
        }

        public void CHKChungChiChanged(object sender, EventArgs e)
        {
            if (_view.CHKChungChi.Checked)
            {
                listCheckBoxValue[7] = _view.CHKChungChi.Checked;
            }
            else
            {
                listCheckBoxValue[7] = _view.CHKChungChi.Checked;
            }
        }

        public void CHKDangHocNangCaoChanged(object sender, EventArgs e)
        {
            if (_view.CHKDangHocNangCao.Checked)
            {
                listCheckBoxValue[8] = _view.CHKDangHocNangCao.Checked;
            }
            else
            {
                listCheckBoxValue[8] = _view.CHKDangHocNangCao.Checked;
            }
        }
    }
}
