using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class DonViRepository : Repository<DonVi>
    {
        public DonViRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public string GetDonVi(int iddonvi)
        {
            return _db.DonVis.Where(x => x.idDonVi == iddonvi).Select(y => y.tenDonVi).First();
        }

        public string GetDiaDiem(int iddonvi)
        {
            return _db.DonVis.Where(x => x.idDonVi == iddonvi).Select(y => y.diaDiem).First();
        }

        public int GetIdDonVi(string donvi)
        {
            int idDonVi = _db.DonVis.Where(x => x.tenDonVi.ToLower() == donvi.ToLower()).Select(y => y.idDonVi).FirstOrDefault();
            if (idDonVi > 0)
                return idDonVi;
            return _db.DonVis.Where(x => x.tenDonVi == string.Empty).Select(y => y.idDonVi).FirstOrDefault();
        }

        public void DeleteDuplicateRowInList(List<DonVi> listDonVi)
        {
            var rows1 = listDonVi.GroupBy(x => new { x.tenDonVi, x.diaDiem }).Select(y => y.Min(a => a.idDonVi));
            var rows2 = listDonVi.Select(x => x).Where(x => !(rows1.Contains(x.idDonVi)));
            foreach (var row in rows2.ToList())
            {
                listDonVi.Remove(row);
            }
            foreach (var row in listDonVi)
            {
                if (row.diaDiem.Equals("a"))
                {
                    row.diaDiem = null;
                }
            }
        }

        public IList<DonVi> GetListDonVi()
        {
            return _db.DonVis.ToList();
        }

        public void DeleteById(int id)
        {
            DonVi DonVi = _db.DonVis.Where(x => x.idDonVi == id).First();
            _db.DonVis.Remove(DonVi);
        }

        public bool CheckExistById(int idRowFocused)
        {
            bool b = _db.DonVis.Any(x => x.idDonVi == idRowFocused);
            return b;
        }

        public void Create(string donvi, string diadiem, string diachi, string sdt, int idloaidonvi)
        {
            _db.DonVis.Add(new DonVi { tenDonVi = donvi, diaDiem = diadiem, diaChi = diachi, sDT = sdt, idLoaiDonVi = idloaidonvi });
            _db.SaveChanges();
        }

        public void Update(int id, string donvi, string diadiem, string diachi, string sdt, int idloaidonvi)
        {
            DonVi donVi = _db.DonVis.Where(x => x.idDonVi == id).First();
            donVi.tenDonVi = donvi;
            donVi.diaDiem = diadiem;
            donVi.diaChi = diachi;
            donVi.sDT = sdt;
            donVi.idLoaiDonVi = idloaidonvi;
            _db.SaveChanges();
        }

        /// <summary>
        /// Lấy danh sách "tên đơn vị" + "địa điểm" để import
        /// </summary>
        public List<string> GetListTenDonVi()
        {
            return _db.DonVis.Select(x => x.tenDonVi).ToList();
        }
    }
}
