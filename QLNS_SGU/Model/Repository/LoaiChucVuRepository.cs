using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class LoaiChucVuRepository : Repository<LoaiChucVu>
    {
        public LoaiChucVuRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public int GetIdLoaiChucVuByName(string name)
        {            
            return _db.LoaiChucVus.Where(x => x.tenLoaiChucVu == name).Select(y => y.idLoaiChucVu).FirstOrDefault();
        }

        public IList<LoaiChucVu> GetListLoaiChucVu()
        {
            return _db.LoaiChucVus.ToList();
        }

        public void Update(int id, string loaichucvu)
        {
            LoaiChucVu loaiChucVu = _db.LoaiChucVus.Where(x => x.idLoaiChucVu == id).First();
            loaiChucVu.tenLoaiChucVu = loaichucvu;
            _db.SaveChanges();
        }

        public void Create(string loaichucvu)
        {
            _db.LoaiChucVus.Add(new LoaiChucVu { tenLoaiChucVu = loaichucvu });
            _db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            LoaiChucVu loaichucvu = _db.LoaiChucVus.Where(x => x.idLoaiChucVu == id).First();
            _db.LoaiChucVus.Remove(loaichucvu);
        }

        public bool CheckExistById(int idRowFocused)
        {
            if (_db.LoaiChucVus.Any(x => x.idLoaiChucVu == idRowFocused))
            {
                return true;
            }
            return false;
        }

        public List<string> GetListTenLoaiChucVu()
        {
            return _db.LoaiChucVus.Select(x => x.tenLoaiChucVu).ToList();
        }
    }
}
