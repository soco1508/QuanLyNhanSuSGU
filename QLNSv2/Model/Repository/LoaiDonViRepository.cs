using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class LoaiDonViRepository : Repository<LoaiDonVi>
    {
        public LoaiDonViRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        /// <summary>
        /// Get idLoaiDonVi = chữ đầu của đơn vị, vd: Phòng đào tạo => Phòng
        /// </summary>
        /// <param name="donvi"></param>
        /// <returns></returns>
        public int GetIdLoaiDonVi(string donvi)
        {
            int defaultId = _db.LoaiDonVis.Where(x => x.tenLoaiDonVi == "Khác").Select(y => y.idLoaiDonVi).FirstOrDefault();
            if (donvi != string.Empty)
            {
                string[] arrDonVi = donvi.Split(' ');
                switch (arrDonVi[0])
                {
                    case "Phòng":
                        return _db.LoaiDonVis.Where(x => x.tenLoaiDonVi == "Phòng").Select(y => y.idLoaiDonVi).FirstOrDefault();
                    case "Khoa":
                        return _db.LoaiDonVis.Where(x => x.tenLoaiDonVi == "Khoa").Select(y => y.idLoaiDonVi).FirstOrDefault();
                    case "Trung tâm":
                        return _db.LoaiDonVis.Where(x => x.tenLoaiDonVi == "Trung tâm").Select(y => y.idLoaiDonVi).FirstOrDefault();
                    case "Viện":
                        return _db.LoaiDonVis.Where(x => x.tenLoaiDonVi == "Viện").Select(y => y.idLoaiDonVi).FirstOrDefault();
                    case "Ban":
                        return _db.LoaiDonVis.Where(x => x.tenLoaiDonVi == "Ban").Select(y => y.idLoaiDonVi).FirstOrDefault();
                    default:
                        return defaultId;
                }
            }
            return defaultId;
        }

        public IList<LoaiDonVi> GetListLoaiDonVi()
        {
            return _db.LoaiDonVis.ToList();
        }

        public void Update(int id, string loaidonvi)
        {
            LoaiDonVi loaiDonVi = _db.LoaiDonVis.Where(x => x.idLoaiDonVi == id).First();
            loaiDonVi.tenLoaiDonVi = loaidonvi;
            _db.SaveChanges();
        }

        public void Create(string loaidonvi)
        {
            _db.LoaiDonVis.Add(new LoaiDonVi { tenLoaiDonVi = loaidonvi });
            _db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            LoaiDonVi loaiDonVi = _db.LoaiDonVis.Where(x => x.idLoaiDonVi == id).First();
            _db.LoaiDonVis.Remove(loaiDonVi);
        }

        public bool CheckExistById(int idRowFocused)
        {
            if (_db.LoaiDonVis.Any(x => x.idLoaiDonVi == idRowFocused))
            {
                return true;
            }
            return false;
        }

        public List<string> GetListTenLoaiDonVi()
        {
            return _db.LoaiDonVis.Select(x => x.tenLoaiDonVi).ToList();
        }
    }
}
