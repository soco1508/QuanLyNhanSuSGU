using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class QuaTrinhLuongForExport
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
        public string MaNgach { get; set; }
        public string TenNgach { get; set; }
        public int? HeSoVuotKhungBaNamDau { get; set; }
        public int? HeSoVuotKhungTrenBaNam { get; set; }
        public int? ThoiHanNangBac { get; set; }
        public int? Bac { get; set; }
        public double? HeSoBac { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayLenLuong { get; set; }
        public bool? DangHuongLuong { get; set; }
        public string LinkVanBanDinhKem { get; set; }
        public QuaTrinhLuongForExport()
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
            MaNgach = "";
            TenNgach = "";
            HeSoVuotKhungBaNamDau = 0;
            HeSoVuotKhungTrenBaNam = 0;
            ThoiHanNangBac = -1;
            Bac = -1;
            HeSoBac = -1;
            NgayBatDau = null;
            NgayLenLuong = null;
            DangHuongLuong = false;
            LinkVanBanDinhKem = "";
        }
    }
}
