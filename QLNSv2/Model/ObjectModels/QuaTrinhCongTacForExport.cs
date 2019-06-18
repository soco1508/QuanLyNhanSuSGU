using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class QuaTrinhCongTacForExport
    {
        public string MaVienChuc { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string SoDienThoai { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string NoiSinh { get; set; }
        public string QueQuan { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string HoKhauThuongTru { get; set; }
        public string NoiOHienNay { get; set; }
        public bool? LaDangVien { get; set; }
        public DateTime? NgayVaoDang { get; set; }
        public DateTime? NgayThamGiaCongTac { get; set; }
        public DateTime? NgayVaoNganh { get; set; }
        public DateTime? NgayVeTruong { get; set; }
        public string VanHoa { get; set; }
        public string QuanLyNhaNuoc { get; set; }
        public string ChinhTri { get; set; }
        public string GhiChu { get; set; }
        public string LoaiChucVu { get; set; }
        public string ChucVu { get; set; }
        public double? HeSoChucVu { get; set; }
        public string LoaiDonVi { get; set; }
        public string DonVi { get; set; }
        public string DiaDiem { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoaiDonVi { get; set; }
        public string ToChuyenMon { get; set; }
        public string PhanLoaiCongTac { get; set; }
        public string CheckPhanLoaiCongTac { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string LoaiThayDoi { get; set; }
        public string KiemNhiem { get; set; }
        public string LinkVanBanDinhKem { get; set; }
        public QuaTrinhCongTacForExport()
        {
            MaVienChuc = "";
            Ho = "";
            Ten = "";
            SoDienThoai = "";
            GioiTinh = "";
            NgaySinh = null;
            NoiSinh = "";
            QueQuan = "";
            DanToc = "";
            TonGiao = "";
            HoKhauThuongTru = "";
            NoiOHienNay = "";
            LaDangVien = false;
            NgayVaoDang = null;
            NgayThamGiaCongTac = null;
            NgayVaoNganh = null;
            NgayVeTruong = null;
            VanHoa = "";
            QuanLyNhaNuoc = "";
            ChinhTri = "";
            GhiChu = "";
            LoaiChucVu = "";
            ChucVu = "";
            HeSoChucVu = 0;
            LoaiDonVi = "";
            DonVi = "";
            DiaDiem = "";
            DiaChi = "";
            SoDienThoaiDonVi = "";
            ToChuyenMon = "";
            PhanLoaiCongTac = "";
            CheckPhanLoaiCongTac = "";
            NgayBatDau = null;
            NgayKetThuc = null;
            LoaiThayDoi = "";
            KiemNhiem = "";
            LinkVanBanDinhKem = "";
        }
    }
}
