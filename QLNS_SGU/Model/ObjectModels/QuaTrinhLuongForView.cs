using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class QuaTrinhLuongForView
    {
        public int Id { get; set; }
        public string MaNgach { get; set; }
        public string TenNgach { get; set; }
        public int? Bac { get; set; }
        public double? HeSoBac { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayLenLuong { get; set; }
        public bool? DangHuongLuong { get; set; }
        public int? TruocHan { get; set; }
        public double? HeSoVuotKhung { get; set; }
        public string LinkVanBanDinhKem { get; set; }
        public QuaTrinhLuongForView()
        {
            Id = -1;
            MaNgach = string.Empty;
            TenNgach = string.Empty;
            Bac = -1;
            HeSoBac = -1;
            NgayBatDau = Convert.ToDateTime("01/01/1900");
            NgayLenLuong = Convert.ToDateTime("01/01/1900");
            DangHuongLuong = false;
            TruocHan = -1;
            HeSoVuotKhung = -1;
            LinkVanBanDinhKem = string.Empty;
        }
    }
}
