using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class NganhForView
    {
        public int Id { get; set; }
        public int IdHocHamHocViVienChuc { get; set; }
        public string LoaiNganh { get; set; }
        public string NganhDaoTao { get; set; }
        public string ChuyenNganh { get; set; }
        public string TenHocHamHocVi { get; set; }
        public string TrinhDoDay { get; set; }
        public string PhanLoai { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string LinkVanBanDinhKem { get; set; }
        public NganhForView()
        {
            Id = -1;
            IdHocHamHocViVienChuc = -1;
            LoaiNganh = string.Empty;
            NganhDaoTao = string.Empty;
            ChuyenNganh = string.Empty;
            TenHocHamHocVi = string.Empty;
            TrinhDoDay = string.Empty;
            PhanLoai = string.Empty;
            NgayBatDau = null;
            NgayKetThuc = null;
            LinkVanBanDinhKem = string.Empty;
        }
    }
}
