using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class LoaiHocHamHocViRepository : Repository<LoaiHocHamHocVi>
    {
        public LoaiHocHamHocViRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public int GetIdLoaiHocHamHocViByTenLoaiHocHamHocVi(string loaihochamhocvi)
        {
            if(loaihochamhocvi != string.Empty)
                return _db.LoaiHocHamHocVis.Where(x => x.tenLoaiHocHamHocVi == loaihochamhocvi).Select(y => y.idLoaiHocHamHocVi).FirstOrDefault();
            return _db.LoaiHocHamHocVis.Where(x => x.tenLoaiHocHamHocVi == "Khác").Select(y => y.idLoaiHocHamHocVi).FirstOrDefault();
        }

        public IList<LoaiHocHamHocVi> GetListLoaiHocHamHocVi()
        {
            return _db.LoaiHocHamHocVis.ToList();
        }

        public void Update(int id, string loaihochamhocvi, int? phancap)
        {
            LoaiHocHamHocVi _loaihochamhocvi = _db.LoaiHocHamHocVis.Where(x => x.idLoaiHocHamHocVi == id).FirstOrDefault();
            _loaihochamhocvi.tenLoaiHocHamHocVi = loaihochamhocvi;
            _loaihochamhocvi.phanCap = phancap;
            _db.SaveChanges();
        }

        public void Create(string loaihochamhocvi, int? phancap)
        {
            _db.LoaiHocHamHocVis.Add(new LoaiHocHamHocVi { tenLoaiHocHamHocVi = loaihochamhocvi , phanCap = phancap});
            _db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            LoaiHocHamHocVi _loaihochamhocvi = _db.LoaiHocHamHocVis.Where(x => x.idLoaiHocHamHocVi == id).First();
            _db.LoaiHocHamHocVis.Remove(_loaihochamhocvi);
        }

        public bool CheckExistById(int idRowFocused)
        {
            if (_db.LoaiHocHamHocVis.Any(x => x.idLoaiHocHamHocVi == idRowFocused))
            {
                return true;
            }
            return false;
        }

        public List<string> GetListTenLoaiHocHamHocVi()
        {
            return _db.LoaiHocHamHocVis.Select(x => x.tenLoaiHocHamHocVi).ToList();
        }
    }
}
