using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class QuanLyNhaNuocRepository : Repository<QuanLyNhaNuoc>
    {
        public QuanLyNhaNuocRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public int GetIdByName(string quanlynhanuoc)
        {
            if(quanlynhanuoc != string.Empty)
            {
                return 2;
            }
            return 1;
        }

        public List<QuanLyNhaNuoc> GetListQuanLyNhaNuoc()
        {
            return _db.QuanLyNhaNuocs.ToList();
        }

        public object SelectIdEmptyValue()
        {
            return _db.QuanLyNhaNuocs.Where(x => x.tenQuanLyNhaNuoc == "").Select(y => y.idQuanLyNhaNuoc).FirstOrDefault();
        }
    }
}
