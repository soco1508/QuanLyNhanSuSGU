using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class MucDoDanhGiaRepository : Repository<MucDoDanhGia>
    {
        public MucDoDanhGiaRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public List<MucDoDanhGia> GetListMucDoDanhGia()
        {
            return _db.MucDoDanhGias.ToList();
        }

        public int GetIdByName(string mucdodanhgia)
        {
            return _db.MucDoDanhGias.Where(x => x.tenMucDoDanhGia == mucdodanhgia).Select(y => y.idMucDoDanhGia).FirstOrDefault();
        }
    }
}
