using Model.Entities;
using Model.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class QuaTrinhDanhGiaVienChucRepository : Repository<QuaTrinhDanhGiaVienChuc>
    {
        public QuaTrinhDanhGiaVienChucRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public List<QuaTrinhDanhGiaVienChucForView> GetListQuaTrinhDanhGia(string mavienchuc)
        {
            VienChucRepository vienChucRepository = new VienChucRepository(_db);
            int idvienchuc = vienChucRepository.GetIdVienChuc(mavienchuc);
            var anonymList = _db.QuaTrinhDanhGiaVienChucs.Where(x => x.idVienChuc == idvienchuc).Select(y => new
            { y.idQuaTrinhDanhGiaVienChuc, y.DanhMucThoiGian.tenDanhMucThoiGian, y.ngayBatDau, y.ngayKetThuc, y.MucDoDanhGia.tenMucDoDanhGia }).ToList();
            List<QuaTrinhDanhGiaVienChucForView> listQuaTrinh = new List<QuaTrinhDanhGiaVienChucForView>();
            for (int i = anonymList.Count - 1; i >= 0; i--)
            {
                listQuaTrinh.Add(new QuaTrinhDanhGiaVienChucForView
                {
                    Id = anonymList[i].idQuaTrinhDanhGiaVienChuc,
                    KhoangThoiGian = anonymList[i].tenMucDoDanhGia,
                    NgayBatDau = anonymList[i].ngayBatDau,
                    NgayKetThuc = anonymList[i].ngayKetThuc,
                    MucDoDanhGia = anonymList[i].tenMucDoDanhGia
                });
            }
            return listQuaTrinh;
        }

        public QuaTrinhDanhGiaVienChuc GetObjectById(int id)
        {
            return _db.QuaTrinhDanhGiaVienChucs.Where(x => x.idQuaTrinhDanhGiaVienChuc == id).FirstOrDefault();
        }

        public void DeleteById(int id)
        {
            var a = _db.QuaTrinhDanhGiaVienChucs.Where(x => x.idQuaTrinhDanhGiaVienChuc == id).FirstOrDefault();
            _db.QuaTrinhDanhGiaVienChucs.Remove(a);
        }
    }
}
