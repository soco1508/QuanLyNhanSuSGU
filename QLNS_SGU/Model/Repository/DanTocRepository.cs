using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class DanTocRepository : Repository<DanToc>
    {
        public DanTocRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public int GetIdByName(string dantoc)
        {
            return _db.DanTocs.Where(x => x.tenDanToc == dantoc).Select(y => y.idDanToc).FirstOrDefault();
        }

        public List<DanToc> GetListDanToc()
        {
            return _db.DanTocs.ToList();
        }

        public object SelectIdEmptyValue()
        {
            return _db.DanTocs.Where(x => x.tenDanToc == "").Select(y => y.idDanToc).FirstOrDefault();
        }
    }
}
