using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class ToChuyenMonRepository : Repository<ToChuyenMon>
    {
        public ToChuyenMonRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public string GetToChuyenMon(int idtochuyenmon)
        {
            return _db.ToChuyenMons.Where(x => x.idToChuyenMon == idtochuyenmon).Select(y => y.tenToChuyenMon).First();
        }

        public void DeleteDuplicateRowInList(List<ToChuyenMon> listToChuyenMon)
        {
            var rows1 = listToChuyenMon.GroupBy(x => new { x.tenToChuyenMon, x.idDonVi }).Select(y => y.Min(a => a.idToChuyenMon));
            var rows2 = listToChuyenMon.Select(x => x)
                                       .Where(x => !(rows1.Contains(x.idToChuyenMon)));
            foreach (var row in rows2.ToList())
            {
                listToChuyenMon.Remove(row);
            }
        }

        public int GetIdToChuyenMon(string donvi, string tochuyenmon)
        {
            if(tochuyenmon != string.Empty)
            {
                int idDonVi = _db.DonVis.Where(x => x.tenDonVi == donvi).Select(y => y.idDonVi).FirstOrDefault();
                int id = _db.ToChuyenMons.Where(x => x.idDonVi == idDonVi && x.tenToChuyenMon == tochuyenmon).Select(y => y.idToChuyenMon).FirstOrDefault();
                return id;
            }
            return 1;
        }

        public IList<ToChuyenMon> GetListToChuyenMon()
        {
            return _db.ToChuyenMons.Where(x => x.tenToChuyenMon != "").ToList();
        }

        public void DeleteById(int id)
        {
            ToChuyenMon toChuyenMon = _db.ToChuyenMons.Where(x => x.idToChuyenMon == id).First();
            _db.ToChuyenMons.Remove(toChuyenMon);
        }

        public bool CheckExistById(int idRowFocused)
        {
            bool b = _db.ToChuyenMons.Any(x => x.idToChuyenMon == idRowFocused);
            return b;
        }

        public void Create(string tochuyenmon, int iddonvi)
        {
            _db.ToChuyenMons.Add(new ToChuyenMon { tenToChuyenMon = tochuyenmon, idDonVi = iddonvi });
            _db.SaveChanges();
        }

        public void Update(int id, string ToChuyenMon, int iddonvi)
        {
            ToChuyenMon toChuyenMon = _db.ToChuyenMons.Where(x => x.idToChuyenMon == id).First();
            toChuyenMon.tenToChuyenMon = ToChuyenMon;
            toChuyenMon.idDonVi = iddonvi;
            _db.SaveChanges();
        }

        public List<ToChuyenMon> GetListToChuyenMonByDonVi(int iddonvi)
        {
            return _db.ToChuyenMons.Where(x => x.idDonVi == iddonvi).ToList();
        }

        /// <summary>
        /// lấy danh sách tổ chuyên môn, còn đơn vị để ktra nếu trùng
        /// [0]: tổ chuyên môn, [1]: đơn vị, [2]: địa điểm 
        /// </summary>
        /// <returns></returns>
        public List<string> GetListTenToChuyenMonVaDonVi()
        {
            List<string> listTenToChuyenMonVaDonVi = new List<string>();
            var rows = _db.ToChuyenMons.Select(x => new { x.tenToChuyenMon, x.DonVi.tenDonVi });
            foreach (var row in rows)
            {                
                listTenToChuyenMonVaDonVi.Add(row.tenToChuyenMon + row.tenDonVi);
            }
            return listTenToChuyenMonVaDonVi;
        }

        public int GetIdByIdDonViVaTenToChuyenMon(int iddonvi, string tochuyenmon)
        {
            int id = _db.ToChuyenMons.Where(x => x.idDonVi == iddonvi && x.tenToChuyenMon == tochuyenmon).Select(y => y.idToChuyenMon).FirstOrDefault();
            if (id > 0)
                return id;
            else
            {
                int idDonVi = _db.DonVis.Where(x => x.tenDonVi == string.Empty && x.diaDiem == string.Empty).Select(y => y.idDonVi).FirstOrDefault();
                return _db.ToChuyenMons.Where(x => x.idDonVi == idDonVi && x.tenToChuyenMon == string.Empty).Select(y => y.idToChuyenMon).FirstOrDefault();
            }
        }
    }
}
