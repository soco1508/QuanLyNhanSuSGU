using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class ChucVuRepository : Repository<ChucVu>
    {
        public ChucVuRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public string GetChucVu(int idchucvu)
        {
            string chucvu = _db.ChucVus.Where(x => x.idChucVu == idchucvu).Select(y => y.tenChucVu).First();
            return chucvu;
        }

        public void DeleteDuplicateRowInList(List<ChucVu> listChucVu)
        {
            var rows1 = listChucVu.GroupBy(x => new { x.tenChucVu }).Select(y => y.Min(a => a.idChucVu));
            var rows2 = listChucVu.Select(x => x).Where(x => !(rows1.Contains(x.idChucVu)));
            foreach (var row in rows2.ToList())
            {
                listChucVu.Remove(row);
            }
        }

        public int GetIdChucVuByTenChucVu(string chucvu)
        {
            return _db.ChucVus.Where(x => x.tenChucVu.Equals(chucvu)).Select(y => y.idChucVu).FirstOrDefault();
        }

        public IList<ChucVu> GetListChucVu()
        {
            return _db.ChucVus.ToList();
        }

        public void DeleteById(int id)
        {
            ChucVu ChucVu = _db.ChucVus.Where(x => x.idChucVu == id).First();
            _db.ChucVus.Remove(ChucVu);
        }

        public void Create(string chucvu, /*double hesochucvu,*/ int idloaichucvu)
        {
            _db.ChucVus.Add(new ChucVu { tenChucVu = chucvu, /*heSoChucVu = hesochucvu, */idLoaiChucVu = idloaichucvu });
            _db.SaveChanges();
        }

        public void Update(int id, string chucvu, /*double hesochucvu,*/ int idloaichucvu)
        {
            ChucVu _chucvu = _db.ChucVus.Where(x => x.idChucVu == id).First();
            //_chucvu.heSoChucVu = hesochucvu;
            _chucvu.tenChucVu = chucvu;
            _chucvu.idLoaiChucVu = idloaichucvu;
            _db.SaveChanges();
        }

        public string GetHeSoChucVuByIdChucVu(int idchucvu)
        {
            return (_db.ChucVus.Where(x => x.idChucVu == idchucvu).Select(y => y.heSoChucVu).FirstOrDefault()).ToString();
        }
    }
}
