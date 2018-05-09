using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class TrangThaiForView
    {
        public int Id { get; set; }
        public string TrangThai { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string MoTa { get; set; }
        public string DiaDiem { get; set; }
        public string LinkVanBanDinhKem { get; set; }
        public TrangThaiForView()
        {
            Id = -1;
            TrangThai = "";
            NgayBatDau = Convert.ToDateTime("01/01/1900");
            NgayKetThuc = Convert.ToDateTime("01/01/3000");
            MoTa = "";
            DiaDiem = "";
            LinkVanBanDinhKem = "";
        }
    }
}
