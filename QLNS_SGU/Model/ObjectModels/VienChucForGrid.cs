using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class VienChucForGrid
    {
        public int idVienChuc { get; set; }
        public string maVienChuc { get; set; }
        public string ho { get; set; }
        public string ten { get; set; }
        public string sDT { get; set; }
        public string gioiTinh { get; set; }
        public DateTime? ngaySinh { get; set; }
        public string noiSinh { get; set; }
        public string queQuan { get; set; }
        public string danToc { get; set; }
        public string tonGiao { get; set; }
        public string hoKhauThuongTru { get; set; }
        public string noiOHienNay { get; set; }
        public bool? laDangVien { get; set; }
        public DateTime? ngayVaoDang { get; set; }
        public DateTime? ngayThamGiaCongTac { get; set; }
        public DateTime? ngayVaoNganh { get; set; }
        public DateTime? ngayVeTruong { get; set; }
        public string vanHoa { get; set; }
        public string quanLyNhaNuoc { get; set; }
        public string chinhTri { get; set; }
        public string ghiChu { get; set; }
        public byte[] anh { get; set; }
        public VienChucForGrid()
        {
            idVienChuc = -1;
            maVienChuc = "";
            ho = "";
            ten = "";
            sDT = "";
            gioiTinh = "";
            ngaySinh = null;
            noiSinh = "";
            queQuan = "";
            danToc = "";
            tonGiao = "";
            hoKhauThuongTru = "";
            noiOHienNay = "";
            laDangVien = false;
            ngayVaoDang = null;
            ngayThamGiaCongTac = null;
            ngayVaoNganh = null;
            ngayVeTruong = null;
            vanHoa = "";
            quanLyNhaNuoc = "";
            chinhTri = "";
            ghiChu = "";
            anh = null;
        }        
    }
}
