using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class DangHocNangCaoForExport
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
        public string SoQuyetDinh { get; set; }
        public string LinkAnhQuyetDinh { get; set; }
        public string LoaiHocHamHocVi { get; set; }
        public string Loai { get; set; }
        public string TenHocHamHocVi { get; set; }
        public string CoSoDaoTao { get; set; }
        public string NgonNguDaoTao { get; set; }
        public string HinhThucDaoTao { get; set; }
        public string NuocCapBang { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public DangHocNangCaoForExport()
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
            LoaiHocHamHocVi = "";
            SoQuyetDinh = "";
            LinkAnhQuyetDinh = "";
            TenHocHamHocVi = "";
            CoSoDaoTao = "";
            NgonNguDaoTao = "";
            HinhThucDaoTao = "";
            NuocCapBang = "";
            NgayBatDau = null;
            NgayKetThuc = null;
            Loai = "";
        }
    }
}
