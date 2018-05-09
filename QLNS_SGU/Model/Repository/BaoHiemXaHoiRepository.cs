using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class BaoHiemXaHoiRepository : Repository<BaoHiemXaHoi>
    {
        public BaoHiemXaHoiRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public BaoHiemXaHoi GetObjectByMaVienChuc(string mavienchuc)
        {
            VienChucRepository vienChucRepository = new VienChucRepository(_db);
            int idvienchuc = vienChucRepository.GetIdVienChuc(mavienchuc);
            return _db.BaoHiemXaHois.Where(x => x.idVienChuc == idvienchuc).FirstOrDefault();
        }

        public int GetIdBaoHiemXaHoiByMaVienChuc(string maVienChuc)
        {
            VienChucRepository vienChucRepository = new VienChucRepository(_db);
            int idvienchuc = vienChucRepository.GetIdVienChuc(maVienChuc);
            return _db.BaoHiemXaHois.Where(x => x.idVienChuc == idvienchuc).Select(y => y.idBaoHiemXaHoi).FirstOrDefault();
        }
    }
}
