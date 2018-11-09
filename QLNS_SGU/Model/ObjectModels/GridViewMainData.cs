using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class GridViewMainData
    {
        public int Order { get; set; }
        public string MaVienChuc { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string Ho { get; set; }
        public string Ten { get; set; }
        public string GioiTinh { get; set; }
        public string ChucVu { get; set; }
        public string DonVi { get; set; }
        public string TrinhDo { get; set; }
        public double? HeSo { get; set; }
        public string Color { get; set; }

        public GridViewMainData()
        {
            Order = -1;
            MaVienChuc = string.Empty;
            NgaySinh = Convert.ToDateTime("01/01/1900");
            Ho = string.Empty;
            Ten = string.Empty;
            GioiTinh = string.Empty;
            ChucVu = string.Empty;
            DonVi = string.Empty;
            TrinhDo = string.Empty;
            HeSo = -1;
            Color = string.Empty;
        }
    }
}
