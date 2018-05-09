using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class LoaiChungChiRepository : Repository<LoaiChungChi>
    {
        public LoaiChungChiRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public List<string> GetListTenLoaiChungChi()
        {
            var list = from a in _db.LoaiChungChis
                       group a.tenLoaiChungChi by a.tenLoaiChungChi into g
                       select g.Key;
            return list.ToList();
        }

        public void Update(int id, string loaichungchi)
        {
            LoaiChungChi _loaichungchi = _db.LoaiChungChis.Where(x => x.idLoaiChungChi == id).First();
            _loaichungchi.tenLoaiChungChi = loaichungchi;
            _db.SaveChanges();
        }

        public void Create(string loaichungchi)
        {
            _db.LoaiChungChis.Add(new LoaiChungChi { tenLoaiChungChi = loaichungchi });
            _db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            LoaiChungChi loaichungchi = _db.LoaiChungChis.Where(x => x.idLoaiChungChi == id).First();
            _db.LoaiChungChis.Remove(loaichungchi);
        }

        public bool CheckExistById(int idRowFocused)
        {
            if (_db.LoaiChungChis.Any(x => x.idLoaiChungChi == idRowFocused))
            {
                return true;
            }
            return false;
        }

        public string GetFirstChar(string s)
        {
            if (s.Contains(","))
            {
                string[] words = s.Split(',');
                return words[0];
            }
            else
            {
                return s;
            }
        }

        public IList<LoaiChungChi> GetListLoaiChungChiForCRUD()
        {
            return _db.LoaiChungChis.ToList();
        }

        public int GetIdLoaiChungChi(string loaichungchi)
        {
            return _db.LoaiChungChis.Where(x => x.tenLoaiChungChi == loaichungchi).Select(y => y.idLoaiChungChi).FirstOrDefault();
        }

        //public List<string> GetListCapDoChungChiByTenLoaiChungChi(string tenloaichungchi)
        //{
        //    return _db.LoaiChungChis.Where(x => x.tenLoaiChungChi.Contains(tenloaichungchi)).Select(y => y.capDo).Distinct().ToList();
        //}

        //public List<string> GetListTenLoaiChungChiByCapDo(string capdo)
        //{
        //    return _db.LoaiChungChis.Where(x => x.capDo.Contains(capdo)).Select(y => y.tenLoaiChungChi).Distinct().ToList();
        //}

        public int GetIdLoaiChungChiByTen(string tenloaichungchi)
        {
            return _db.LoaiChungChis.Where(x => x.tenLoaiChungChi == tenloaichungchi).Select(y => y.idLoaiChungChi).FirstOrDefault();
        }
    }
}
