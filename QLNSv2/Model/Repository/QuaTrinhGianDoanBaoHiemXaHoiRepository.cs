using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class QuaTrinhGianDoanBaoHiemXaHoiRepository : Repository<QuaTrinhGianDoanBaoHiemXaHoi>
    {
        public QuaTrinhGianDoanBaoHiemXaHoiRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public BindingList<QuaTrinhGianDoanBaoHiemXaHoi> GetListQuaTrinhGianDoan(string maVienChuc)
        {
            BindingList<QuaTrinhGianDoanBaoHiemXaHoi> bdlist = new BindingList<QuaTrinhGianDoanBaoHiemXaHoi>();
            VienChucRepository vienChucRepository = new VienChucRepository(_db);
            int idvienchuc = vienChucRepository.GetIdVienChuc(maVienChuc);
            var anonymlist = _db.QuaTrinhGianDoanBaoHiemXaHois.Where(x => x.idVienChuc == idvienchuc).Select(y => new
            { y.idQuaTrinhGianDoan, y.idVienChuc, y.lyDo, y.ngayBatDau, y.ngayKetThuc }).ToList();
            for(int i = anonymlist.Count - 1; i >= 0; i--)
            {
                bdlist.Add(new QuaTrinhGianDoanBaoHiemXaHoi
                {
                    idQuaTrinhGianDoan = anonymlist[i].idQuaTrinhGianDoan,
                    idVienChuc = anonymlist[i].idVienChuc,
                    lyDo = anonymlist[i].lyDo,
                    ngayBatDau = anonymlist[i].ngayBatDau,
                    ngayKetThuc = anonymlist[i].ngayKetThuc
                });
            }
            return bdlist;
        }

        public void DeleteById(int id)
        {
            var a = _db.QuaTrinhGianDoanBaoHiemXaHois.Where(x => x.idQuaTrinhGianDoan == id).FirstOrDefault();
            _db.QuaTrinhGianDoanBaoHiemXaHois.Remove(a);
        }

        public QuaTrinhGianDoanBaoHiemXaHoi GetObjectById(int id)
        {
            return _db.QuaTrinhGianDoanBaoHiemXaHois.Where(x => x.idQuaTrinhGianDoan == id).FirstOrDefault();
        }
    }
}
