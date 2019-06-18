using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class HocHamHocViForTabPageChuyenMon
    {
        public int Id { get; set; }
        public string LoaiHocHamHocVi { get; set; }
        public string LoaiNganh { get; set; }
        public string NganhDaoTao { get; set; }
        public string ChuyenNganh { get; set; }
        public string TenHocHamHocVi { get; set; }
        public string CoSoDaoTao { get; set; }
        public string NgonNguDaoTao { get; set; }
        public string HinhThucDaoTao { get; set; }
        public string NuocCapBang { get; set; }
        public DateTime? NgayCapBang { get; set; }
        public string LinkVanBanDinhKem { get; set; }
        public HocHamHocViForTabPageChuyenMon()
        {
            Id = -1;
            LoaiHocHamHocVi = "";
            LoaiNganh = "";
            NganhDaoTao = "";
            ChuyenNganh = "";
            TenHocHamHocVi = "";
            CoSoDaoTao = "";
            NgonNguDaoTao = "";
            HinhThucDaoTao = "";
            NuocCapBang = "";
            NgayCapBang = null;
            LinkVanBanDinhKem = "";
        }
    }
}
