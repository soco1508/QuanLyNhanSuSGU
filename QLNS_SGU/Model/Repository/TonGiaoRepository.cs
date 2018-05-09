using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class TonGiaoRepository : Repository<TonGiao>
    {
        public TonGiaoRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public int GetIdByName(string tongiao)
        {
            return _db.TonGiaos.Where(x => x.tenTonGiao == tongiao).Select(y => y.idTonGiao).FirstOrDefault();
        }

        public List<TonGiao> GetListTonGiao()
        {
            return _db.TonGiaos.ToList();
        }

        public int SelectIdEmptyValue(Func<TonGiao, bool> p)
        {
            return _db.TonGiaos.Where(x => x.tenTonGiao == "").Select(y => y.idTonGiao).FirstOrDefault();
        }
    }
}
