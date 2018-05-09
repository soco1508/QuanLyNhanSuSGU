using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class ChungChiForView
    {
        public int Id { get; set; }
        public int IdLoaiChungChi { get; set; }
        public string LoaiChungChi { get; set; }
        public string CapDo { get; set; }
        public DateTime? NgayCapChungChi { get; set; }
        public string LinkVanBanDinhKem { get; set; }
        public string GhiChu { get; set; }
        public ChungChiForView()
        {
            Id = -1;
            LoaiChungChi = "";
            CapDo = "";
            NgayCapChungChi = Convert.ToDateTime("01/01/1900");
            LinkVanBanDinhKem = "";
            GhiChu = "";
        }
    }
}
