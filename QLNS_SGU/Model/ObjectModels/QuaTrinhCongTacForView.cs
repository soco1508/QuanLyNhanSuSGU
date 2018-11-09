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
        public string ToChuyenMon { get; set; }
        public string PhanLoaiCongTac { get; set; }
        public string CheckPhanLoaiCongTac { get; set; }
        public double? HeSoChucVu { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string LoaiThayDoi { get; set; }
        public string KiemNhiem { get; set; }
        public string LinkVanBanDinhKem { get; set; }

        public QuaTrinhCongTacForView()
        {
            Id = -1;
            ChucVu = string.Empty;
            DonVi = string.Empty;
            ToChuyenMon = string.Empty;
            PhanLoaiCongTac = string.Empty;
            CheckPhanLoaiCongTac = string.Empty;
            HeSoChucVu = -1;
            NgayBatDau = null;
            NgayKetThuc = null;
            LoaiThayDoi = string.Empty;
            KiemNhiem = string.Empty;
            LinkVanBanDinhKem = string.Empty;           
        }
    }
}
