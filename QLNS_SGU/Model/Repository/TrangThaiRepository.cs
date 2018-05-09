using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class TrangThaiRepository : Repository<TrangThai>
    {
        public TrangThaiRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public int GetIdTrangThai(string trangthai)
        {
            return _db.TrangThais.Where(x => x.tenTrangThai == trangthai).Select(y => y.idTrangThai).FirstOrDefault();
        }

        public IList<TrangThai> GetListTrangThai()
        {
            return _db.TrangThais.ToList();
        }

        public void Update(int id, string trangthai)
        {
            TrangThai trangThai = _db.TrangThais.Where(x => x.idTrangThai == id).First();
            trangThai.tenTrangThai = trangthai;
            _db.SaveChanges();
        }

        public void Create(string trangthai)
        {
            _db.TrangThais.Add(new TrangThai { tenTrangThai = trangthai });
            _db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            TrangThai trangthai = _db.TrangThais.Where(x => x.idTrangThai == id).First();
            _db.TrangThais.Remove(trangthai);
        }

        public bool CheckExistById(int idRowFocused)
        {
            if (_db.TrangThais.Any(x => x.idTrangThai == idRowFocused))
            {
                return true;
            }
            return false;
        }
    }
}
