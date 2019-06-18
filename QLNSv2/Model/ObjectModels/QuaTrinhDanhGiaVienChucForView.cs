using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class QuaTrinhDanhGiaVienChucForView
    {
        public int Id { get; set; }
        public string KhoangThoiGian { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string MucDoDanhGia { get; set; }

        public QuaTrinhDanhGiaVienChucForView()
        {
            Id = -1;
            KhoangThoiGian = string.Empty;
            NgayBatDau = null;
            NgayKetThuc = null;
            MucDoDanhGia = string.Empty;
        }
    }
}
