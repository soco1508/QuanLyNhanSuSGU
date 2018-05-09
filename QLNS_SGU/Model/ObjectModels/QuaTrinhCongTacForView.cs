using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class QuaTrinhCongTacForView
    {
        public int Id { get; set; }
        public string ChucVu { get; set; }
        public string DonVi { get; set; }
        public string DiaDiem { get; set; }
        public string ToChuyenMon { get; set; }
        public string PhanLoaiCongTac { get; set; }
        public string CheckPhanLoaiCongTac { get; set; }
        public double? HeSoChucVu { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string LoaiThayDoi { get; set; }
        public string KiemNhiem { get; set; }
        public string LinkVanBanDinhKem { get; set; }
        public string NhanXet { get; set; }

        public QuaTrinhCongTacForView()
        {
            Id = -1;
            ChucVu = string.Empty;
            DonVi = string.Empty;
            DiaDiem = string.Empty;
            ToChuyenMon = string.Empty;
            PhanLoaiCongTac = string.Empty;
            CheckPhanLoaiCongTac = string.Empty;
            HeSoChucVu = -1;
            NgayBatDau = Convert.ToDateTime("01/01/1900");
            NgayKetThuc = Convert.ToDateTime("01/01/2500");
            LoaiThayDoi = string.Empty;
            KiemNhiem = string.Empty;
            LinkVanBanDinhKem = string.Empty;
            NhanXet = string.Empty;
        }
    }
}
