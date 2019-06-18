using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class DanhMucThoiGianRepository : Repository<DanhMucThoiGian>
    {
        public DanhMucThoiGianRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public List<DanhMucThoiGian> GetListDanhMucThoiGian()
        {
            return _db.DanhMucThoiGians.ToList();
        }

        public int GetIdByName(string khoangthoigian)
        {
            return _db.DanhMucThoiGians.Where(x => x.tenDanhMucThoiGian == khoangthoigian).Select(y => y.idDanhMucThoiGian).FirstOrDefault();
        }
    }
}
