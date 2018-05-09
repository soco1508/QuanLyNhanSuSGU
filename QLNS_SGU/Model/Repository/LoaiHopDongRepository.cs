using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class LoaiHopDongRepository : Repository<LoaiHopDong>
    {
        public LoaiHopDongRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public string GetTenLoaiHopDong(string maloaihopdong)
        {
            switch (maloaihopdong)
            {
                case "BC":
                    return "Biên chế";
                case "CTH":
                    return "Có thời hạn";
                case "K":
                    return "Khoáng";
                case "HĐ":
                    return "Hợp đồng";
                case "H68":
                    return "H68";
                default:
                    return "";
            }
        }

        public void DeleteDuplicateRowInList(List<LoaiHopDong> listLoaiHopDong)
        {
            var rows1 = listLoaiHopDong.GroupBy(x => new { x.maLoaiHopDong }).Select(y => y.Min(a => a.idLoaiHopDong));
            var rows2 = listLoaiHopDong.Select(x => x)
                                       .Where(x => !(rows1.Contains(x.idLoaiHopDong)));
            foreach (var row in rows2.ToList())
            {
                listLoaiHopDong.Remove(row);
            }
        }

        public int GetIdLoaiHopDong(string loaihopdong)
        {
            return _db.LoaiHopDongs.Where(x => x.tenLoaiHopDong == loaihopdong).Select(y => y.idLoaiHopDong).FirstOrDefault();
        }

        public IList<LoaiHopDong> GetListLoaiHopDong()
        {
            return _db.LoaiHopDongs.ToList();
        }

        public void Update(int id, string maloaihopdong, string loaihopdong)
        {
            LoaiHopDong _loaihopdong = _db.LoaiHopDongs.Where(x => x.idLoaiHopDong == id).First();
            _loaihopdong.maLoaiHopDong = maloaihopdong;
            _loaihopdong.tenLoaiHopDong = loaihopdong;
            _db.SaveChanges();
        }

        public void Create(string maloaihopdong, string loaihopdong)
        {
            _db.LoaiHopDongs.Add(new LoaiHopDong { tenLoaiHopDong = loaihopdong, maLoaiHopDong = maloaihopdong });
            _db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            LoaiHopDong loaihopdong = _db.LoaiHopDongs.Where(x => x.idLoaiHopDong == id).First();
            _db.LoaiHopDongs.Remove(loaihopdong);
        }

        public bool CheckExistById(int idRowFocused)
        {
            if (_db.LoaiHopDongs.Any(x => x.idLoaiHopDong == idRowFocused))
            {
                return true;
            }
            return false;
        }

        public string GenerateMaLoaiHopDong(string loaihopdong)
        {
            switch (loaihopdong)
            {
                case "Không thời hạn":
                    return "KTH";
                case "Có thời hạn":
                    return "CTH";
                case "Khoán":
                    return "K";
                case "Kéo dài công tác":
                    return "KDCT";
                case "Hợp đồng 68":
                    return "HĐ68";
                default:
                    return "";
            }
        }
    }
}
