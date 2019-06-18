using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class BacRepository : Repository<Bac>
    {
        public BacRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public double ParseHeSoBacToDouble(string hesobac)
        {
            if (hesobac != string.Empty)
            {
                if (hesobac.Contains(','))
                    hesobac = hesobac.Replace(',', '.');
                return Math.Round(Convert.ToDouble(hesobac), 3);
            }
            return Math.Round(Convert.ToDouble(0), 3);
        }

        public int GetIdBac(int bac, int idngach)
        {
            return _db.Bacs.Where(x => x.bac1 == bac && x.idNgach == idngach).Select(y => y.idBac).FirstOrDefault();
        }

        public IList<Bac> GetListBac()
        {
            return _db.Bacs.ToList();
        }

        public void DeleteById(int id)
        {
            Bac bac = _db.Bacs.Where(x => x.idBac == id).First();
            _db.Bacs.Remove(bac);
        }

        public bool CheckExistById(int idRowFocused)
        {
            bool b = _db.Bacs.Any(x => x.idBac == idRowFocused);
            return b;
        }

        public void Update(int id, int bac, double hesobac, int idngach)
        {
            Bac _bac = _db.Bacs.Where(x => x.idBac == id).FirstOrDefault();
            _bac.heSoBac = hesobac;
            _bac.bac1 = bac;
            _bac.idNgach = idngach;
            _db.SaveChanges();
        }

        public double GetHeSoBac(int idngach, int bac)
        {
            return _db.Bacs.Where(x => x.idNgach == idngach && x.bac1 == bac).Select(y => y.heSoBac).FirstOrDefault();
        }

        public List<string> GetListBacVaNgach()
        {
            List<string> list = new List<string>();
            var rows = _db.Bacs.Select(x => new { x.bac1, x.Ngach.maNgach });
            foreach(var row in rows)
            {
                list.Add(row.bac1.ToString() + row.maNgach);
            }
            return list;
        }
    }
}
