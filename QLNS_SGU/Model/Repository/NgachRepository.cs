using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class NgachRepository : Repository<Ngach>
    {
        public NgachRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public int GetIdNgach(string mangach)
        {
            return _db.Ngaches.Where(x => x.maNgach == mangach).Select(y => y.idNgach).First();
        }

        public IList<Ngach> GetListNgach()
        {
            return _db.Ngaches.ToList();
        }

        public void Update(int id, string mangach, string ngach, int hesovuotkhungbanamdau, int hesovuotkhungtrenbanam, int thoihannangbac)
        {
            Ngach _ngach = _db.Ngaches.Where(x => x.idNgach == id).First();
            _ngach.maNgach = mangach;
            _ngach.tenNgach = ngach;
            _ngach.heSoVuotKhungBaNamDau = hesovuotkhungbanamdau;
            _ngach.heSoVuotKhungTrenBaNam = hesovuotkhungtrenbanam;
            _ngach.thoiHanNangBac = thoihannangbac;
            _db.SaveChanges();
        }

        public void Create(string mangach, string ngach, int hesovuotkhungbanamdau, int hesovuotkhungtrenbanam, int thoihannangbac)
        {
            _db.Ngaches.Add(new Ngach { maNgach = mangach, tenNgach = ngach, heSoVuotKhungBaNamDau = hesovuotkhungbanamdau, heSoVuotKhungTrenBaNam = hesovuotkhungtrenbanam, thoiHanNangBac = thoihannangbac });
            _db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            Ngach ngach = _db.Ngaches.Where(x => x.idNgach == id).First();
            _db.Ngaches.Remove(ngach);
        }

        public bool CheckExistById(int idRowFocused)
        {
            if (_db.Ngaches.Any(x => x.idNgach == idRowFocused))
            {
                return true;
            }
            return false;
        }

        public string GetTenNgachByIdNgach(int idngach)
        {
            return _db.Ngaches.Where(x => x.idNgach == idngach).Select(y => y.tenNgach).FirstOrDefault();
        }

        public List<string> GetListTenNgach()
        {
            return _db.Ngaches.Select(x => x.tenNgach).Distinct().ToList();
        }

        public IList<Ngach> GetListNgachByTenNgach(string tenngach)
        {
            return _db.Ngaches.Where(x => x.tenNgach == tenngach).ToList();
        }
    }
}
