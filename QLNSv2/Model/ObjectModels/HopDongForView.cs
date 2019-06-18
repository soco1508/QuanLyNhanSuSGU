using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class HopDongForView
    {
        public int Id { get; set; }
        public string LoaiHopDong { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string LinkVanBanDinhKem { get; set; }
        public string GhiChu { get; set; }
        public HopDongForView()
        {
            Id = -1;
            LoaiHopDong = string.Empty;
            NgayBatDau = Convert.ToDateTime("01/01/1900");
            NgayKetThuc = Convert.ToDateTime("01/01/3000"); ;
            LinkVanBanDinhKem = string.Empty;
            GhiChu = string.Empty;
        }
    }
}
