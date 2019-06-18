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
        public string IdLoaiChungChi { get; set; }
        public string LoaiChungChi { get; set; }
        public string TenChungChi { get; set; }
        public string CapDo { get; set; }
        public DateTime? NgayCapChungChi { get; set; }
        public string CoSoDaoTao { get; set; }
        public string LinkVanBanDinhKem { get; set; }
        public string GhiChu { get; set; }
        public ChungChiForView()
        {
            Id = -1;
            IdLoaiChungChi = string.Empty;
            LoaiChungChi = string.Empty;
            TenChungChi = string.Empty;
            CapDo = string.Empty;
            NgayCapChungChi = Convert.ToDateTime("01/01/1900");
            CoSoDaoTao = string.Empty;
            LinkVanBanDinhKem = string.Empty;
            GhiChu = string.Empty;
        }
    }
}
