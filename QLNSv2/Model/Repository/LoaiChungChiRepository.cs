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
            return _db.LoaiChungChis.Select(x => x.tenLoaiChungChi).ToList();
        }

        public void Update(string idloaichungchi, string tenloaichungchi)
        {
            LoaiChungChi _loaichungchi = _db.LoaiChungChis.Where(x => x.idLoaiChungChi == idloaichungchi).FirstOrDefault();
            _loaichungchi.tenLoaiChungChi = tenloaichungchi;
        }

        public void Create(string idloaichungchi, string tenloaichungchi)
        {
            _db.LoaiChungChis.Add(new LoaiChungChi { idLoaiChungChi = idloaichungchi, tenLoaiChungChi = tenloaichungchi });
        }

        public void DeleteById(string id)
        {
            LoaiChungChi loaichungchi = _db.LoaiChungChis.Where(x => x.idLoaiChungChi == id).FirstOrDefault();
            _db.LoaiChungChis.Remove(loaichungchi);
        }

        public bool CheckExistById(int idRowFocused)
        {
            //if (_db.LoaiChungChis.Any(x => x.idLoaiChungChi == idRowFocused))
            //{
            //    return true;
            //}
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

        public string GetIdLoaiChungChi(string tenloaichungchi)
        {
            return _db.LoaiChungChis.Where(x => x.tenLoaiChungChi == tenloaichungchi).Select(y => y.idLoaiChungChi).FirstOrDefault();
        }

        //public List<string> GetListCapDoChungChiByTenLoaiChungChi(string tenloaichungchi)
        //{
        //    return _db.LoaiChungChis.Where(x => x.tenLoaiChungChi.Contains(tenloaichungchi)).Select(y => y.capDo).Distinct().ToList();
        //}

        //public List<string> GetListTenLoaiChungChiByCapDo(string capdo)
        //{
        //    return _db.LoaiChungChis.Where(x => x.capDo.Contains(capdo)).Select(y => y.tenLoaiChungChi).Distinct().ToList();
        //}

        public string GetIdLoaiChungChiByTen(string tenloaichungchi)
        {
            return _db.LoaiChungChis.Where(x => x.tenLoaiChungChi == tenloaichungchi).Select(y => y.idLoaiChungChi).FirstOrDefault();
        }

        public List<string> GetListIdLoaiChungChi()
        {
            return _db.LoaiChungChis.Select(x => x.idLoaiChungChi).ToList();
        }
    }
}
