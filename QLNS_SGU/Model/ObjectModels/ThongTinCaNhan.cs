using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ThongTinCaNhan
    {
        public string maTheVC { get; set; }
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
        public DateTime? ngayThamGiaCongTac { get; set; }
        public bool? laDangVien { get; set; }
        public DateTime? ngayVaoDang { get; set; }        
        public DateTime? ngayVaoNganh { get; set; }
        public DateTime? ngayVeTruong { get; set; }
        public string vanHoa { get; set; }
        public string soCMND { get; set; }
        public DateTime? ngayTuyenDungChinhThuc { get; set; }
        public string email { get; set; }
        public int? gioChuan { get; set; }
        public string ghiChu { get; set; }
        public byte[] anh { get; set; }

        public ThongTinCaNhan()
        {
            maTheVC = string.Empty;
            ho = string.Empty;
            ten = string.Empty;
            sDT = string.Empty;
            gioiTinh = string.Empty;
            ngaySinh = null;
            noiSinh = string.Empty;
            queQuan = string.Empty;
            danToc = string.Empty;
            tonGiao = string.Empty;
            hoKhauThuongTru = string.Empty;
            noiOHienNay = string.Empty;
            ngayThamGiaCongTac = null;
            laDangVien = false;
            ngayVaoDang = null;
            ngayVaoNganh = null;
            ngayVeTruong = null;
            vanHoa = string.Empty;
            ngayTuyenDungChinhThuc = null;
            soCMND = string.Empty;
            email = string.Empty;
            gioChuan = 0;
            ghiChu = string.Empty;
            anh = null;
        }        
    }
}
