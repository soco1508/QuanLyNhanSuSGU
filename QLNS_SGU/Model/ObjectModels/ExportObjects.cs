using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class ExportObjects
    {
        //Default
        public int IdVienChuc { get; set; }
        public string MaVienChuc { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string GioiTinh { get; set; }
        public string DonVi { get; set; }
        public string TrangThai { get; set; }
        //Thong tin ca nhan
        public string SoDienThoai { get; set; }       
        public DateTime? NgaySinh { get; set; }
        public string NoiSinh { get; set; }
        public string QueQuan { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string HoKhauThuongTru { get; set; }
        public string NoiOHienNay { get; set; }
        public string LaDangVien { get; set; }
        public DateTime? NgayVaoDang { get; set; }
        public DateTime? NgayThamGiaCongTac { get; set; }
        public DateTime? NgayVaoNganh { get; set; }
        public DateTime? NgayVeTruong { get; set; }
        public string VanHoa { get; set; }
        public string QuanLyNhaNuoc { get; set; }
        //public string ChinhTri { get; set; }
        public string GhiChu { get; set; }
        //Cong tac
        public string LoaiChucVu { get; set; }
        public string ChucVu { get; set; }
        public double? HeSoChucVu { get; set; }
        public string LoaiDonVi { get; set; }        
        public string DiaDiemCT { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoaiDonVi { get; set; }
        public string ToChuyenMon { get; set; }
        public string PhanLoaiCongTac { get; set; }
        public string CheckPhanLoaiCongTac { get; set; }
        public DateTime? NgayBatDauCT { get; set; }
        public DateTime? NgayKetThucCT { get; set; }
        public string LoaiThayDoi { get; set; }
        public string KiemNhiem { get; set; }
        public string LinkVanBanDinhKemCT { get; set; }
        //Qua trinh luong
        public string MaNgach { get; set; }
        public string TenNgach { get; set; }
        public int? HeSoVuotKhungBaNamDau { get; set; }
        public int? HeSoVuotKhungTrenBaNam { get; set; }
        public int? ThoiHanNangBac { get; set; }
        public int? Bac { get; set; }
        public double? HeSoBac { get; set; }
        public DateTime? NgayBatDauQTL { get; set; }
        public DateTime? NgayLenLuong { get; set; }
        public string LinkVanBanDinhKemQTL { get; set; }
        //Hop dong
        public string MaHopDong { get; set; }
        public string TenHopDong { get; set; }
        public DateTime? NgayBatDauHD { get; set; }
        public DateTime? NgayKetThucHD { get; set; }
        public string MoTaHD { get; set; }
        public string LinkVanBanDinhKemHD { get; set; }   
        //Trang thai       
        public string MoTaTT { get; set; }
        public string DiaDiemTT { get; set; }
        public DateTime? NgayBatDauTT { get; set; }
        public DateTime? NgayKetThucTT { get; set; }
        public string LinkVanBanDinhKemTT { get; set; }
        //Nganh hoc
        public string LoaiNganhNH { get; set; }
        public string NganhDaoTaoNH { get; set; }
        public string ChuyenNganhNH { get; set; }        
        public string LoaiHocHamHocViNH { get; set; }
        public string TenHocHamHocViNH { get; set; }
        public string CoSoDaoTaoNH { get; set; }
        public string NgonNguDaoTaoNH { get; set; }
        public string HinhThucDaoTaoNH { get; set; }
        public string NuocCapBangNH { get; set; }
        public DateTime? NgayCapBangNH { get; set; }
        public string LinkVanBanDinhKemHHHV_NH { get; set; }
        public string PhanLoaiNH { get; set; }
        public string LinkVanBanDinhKemNH { get; set; }
        //Nganh day
        public string LoaiNganhND { get; set; }
        public string NganhDaoTaoND { get; set; }
        public string ChuyenNganhND { get; set; }
        public string LoaiHocHamHocViND { get; set; }
        public string TenHocHamHocViND { get; set; }
        public string CoSoDaoTaoND { get; set; }
        public string NgonNguDaoTaoND { get; set; }
        public string HinhThucDaoTaoND { get; set; }
        public string NuocCapBangND { get; set; }
        public DateTime? NgayCapBangND { get; set; }
        public string LinkVanBanDinhKemHHHV_ND { get; set; }
        public string PhanLoaiND { get; set; }
        public DateTime? NgayBatDauND { get; set; }
        public DateTime? NgayKetThucND { get; set; }
        public string LinkVanBanDinhKemND { get; set; }
        //Chung chi
        public string NgoaiNgu { get; set; }
        public string TinHoc { get; set; }
        public string LiLuanChinhTri { get; set; }
        public string ChungChiChuyenMon { get; set; }
        public string ChungChi { get; set; }
        //Dang hoc nang cao
        public string SoQuyetDinh { get; set; }
        public string LinkAnhQuyetDinh { get; set; }
        public string LoaiHocHamHocViDHNC { get; set; }
        public string Loai { get; set; }
        public string TenHocHamHocViDHNC { get; set; }
        public string CoSoDaoTaoDHNC { get; set; }
        public string NgonNguDaoTaoDHNC { get; set; }
        public string HinhThucDaoTaoDHNC { get; set; }
        public string NuocCapBangDHNC { get; set; }
        public DateTime? NgayBatDauDHNC { get; set; }
        public DateTime? NgayKetThucDHNC { get; set; }
        //index to add row in export one domain
        public int Index { get; set; }
        public ExportObjects()
        {
            //Default
            MaVienChuc = "";
            Ho = "";
            Ten = "";            
            GioiTinh = "";
            DonVi = "";
            TrangThai = "";
            //Thong tin ca nhan
            SoDienThoai = "";
            NgaySinh = null;
            NoiSinh = "";
            QueQuan = "";
            DanToc = "";
            TonGiao = "";
            HoKhauThuongTru = "";
            NoiOHienNay = "";
            LaDangVien = "";
            NgayVaoDang = null;
            NgayThamGiaCongTac = null;
            NgayVaoNganh = null;
            NgayVeTruong = null;
            VanHoa = "";
            QuanLyNhaNuoc = "";
            //ChinhTri = "";
            GhiChu = "";
            //Cong tac
            LoaiChucVu = "";
            ChucVu = "";
            HeSoChucVu = null;
            LoaiDonVi = "";            
            DiaDiemCT = "";
            DiaChi = "";
            SoDienThoaiDonVi = "";
            ToChuyenMon = "";
            PhanLoaiCongTac = "";
            CheckPhanLoaiCongTac = "";
            NgayBatDauCT = null;
            NgayKetThucCT = null;
            LoaiThayDoi = "";
            KiemNhiem = "";
            LinkVanBanDinhKemCT = "";
            //Qua trinh luong
            MaNgach = "";
            TenNgach = "";
            HeSoVuotKhungBaNamDau = null;
            HeSoVuotKhungTrenBaNam = null;
            ThoiHanNangBac = null;
            Bac = null;
            HeSoBac = null;
            NgayBatDauQTL = null;
            NgayLenLuong = null;
            LinkVanBanDinhKemQTL = "";
            //Hop dong
            MaHopDong = "";
            TenHopDong = "";
            NgayBatDauHD = null;
            NgayKetThucHD = null;
            MoTaHD = "";
            LinkVanBanDinhKemHD = "";            
            //Trang thai            
            MoTaTT = "";
            DiaDiemTT = "";
            NgayBatDauTT = null;
            NgayKetThucTT = null;
            LinkVanBanDinhKemTT = "";
            //Nganh hoc
            LoaiNganhNH = "";
            NganhDaoTaoNH = "";
            ChuyenNganhNH = "";
            LoaiHocHamHocViNH = "";
            TenHocHamHocViNH = "";
            CoSoDaoTaoNH = "";
            NgonNguDaoTaoNH = "";
            HinhThucDaoTaoNH = "";
            NuocCapBangNH = "";
            NgayCapBangNH = null;
            LinkVanBanDinhKemHHHV_NH = "";
            PhanLoaiNH = "";
            LinkVanBanDinhKemNH = "";
            //Nganh day
            LoaiNganhND = "";
            NganhDaoTaoND = "";
            ChuyenNganhND = "";
            LoaiHocHamHocViND = "";
            TenHocHamHocViND = "";
            CoSoDaoTaoND = "";
            NgonNguDaoTaoND = "";
            HinhThucDaoTaoND = "";
            NuocCapBangND = "";
            NgayCapBangND = null;
            LinkVanBanDinhKemHHHV_ND = "";
            PhanLoaiND = "";
            NgayBatDauND = null;
            NgayKetThucND = null;
            LinkVanBanDinhKemND = "";
            //Chung chi
            NgoaiNgu = "";
            TinHoc = "";
            LiLuanChinhTri = "";
            ChungChiChuyenMon = "";
            //Dang hoc nang cao
            LoaiHocHamHocViDHNC = "";
            SoQuyetDinh = "";
            LinkAnhQuyetDinh = "";
            TenHocHamHocViDHNC = "";
            CoSoDaoTaoDHNC = "";
            NgonNguDaoTaoDHNC = "";
            HinhThucDaoTaoDHNC = "";
            NuocCapBangDHNC = "";
            NgayBatDauDHNC = null;
            NgayKetThucDHNC = null;
            Loai = "";
            //index to add row for export one domain
            Index = -1;
        }
    }
}
