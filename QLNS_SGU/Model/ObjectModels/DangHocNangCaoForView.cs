using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class DangHocNangCaoForView
    {
        public int Id { get; set; }
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
        public DangHocNangCaoForView()
        {
            Id = -1;
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
