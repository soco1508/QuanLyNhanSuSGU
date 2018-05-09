using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;
using System.IO;

namespace Model.Repository
{
    public class ThongTinCaNhanRepository : Repository<ThongTinCaNhan>
    {
        public ThongTinCaNhanRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public ThongTinCaNhan GetThongTinCaNhan(string mavienchuc)
        {
            var vienChuc = _db.VienChucs.Where(x => x.maVienChuc == mavienchuc).FirstOrDefault();
            var thongTinCaNhan = new ThongTinCaNhan();
            thongTinCaNhan.maTheVC = vienChuc.maVienChuc;
            thongTinCaNhan.ho = vienChuc.ho;
            thongTinCaNhan.ten = vienChuc.ten;
            thongTinCaNhan.sDT = vienChuc.sDT;
            thongTinCaNhan.danToc = vienChuc.DanToc.tenDanToc;
            thongTinCaNhan.ghiChu = vienChuc.ghiChu;
            thongTinCaNhan.gioiTinh = ReturnGenderString(vienChuc.gioiTinh);
            thongTinCaNhan.hoKhauThuongTru = vienChuc.hoKhauThuongTru;
            thongTinCaNhan.laDangVien = vienChuc.laDangVien;
            thongTinCaNhan.ngaySinh = vienChuc.ngaySinh;
            thongTinCaNhan.ngayThamGiaCongTac = vienChuc.ngayThamGiaCongTac;
            thongTinCaNhan.ngayVaoDang = vienChuc.ngayVaoDang;
            thongTinCaNhan.ngayVaoNganh = vienChuc.ngayVaoNganh;
            thongTinCaNhan.ngayVeTruong = vienChuc.ngayVeTruong;
            thongTinCaNhan.noiOHienNay = vienChuc.noiOHienNay;
            thongTinCaNhan.noiSinh = vienChuc.noiSinh;
            thongTinCaNhan.quanLyNhaNuoc = vienChuc.QuanLyNhaNuoc.tenQuanLyNhaNuoc;
            thongTinCaNhan.queQuan = vienChuc.queQuan;
            thongTinCaNhan.tonGiao = vienChuc.TonGiao.tenTonGiao;
            thongTinCaNhan.vanHoa = vienChuc.vanHoa;
            return thongTinCaNhan;
        }        

        private string ReturnGenderString(bool? t)
        {
            if(t == true)
            {
                return "Nam";
            }
            return "Nữ";
        }

        public byte[] GetImage(string mavienchuc)
        {
            return _db.VienChucs.Where(x => x.maVienChuc == mavienchuc).Select(y => y.anh).FirstOrDefault();           
        }
    }
}
