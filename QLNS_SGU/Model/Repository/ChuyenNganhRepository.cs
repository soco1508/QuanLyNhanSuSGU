using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class ChuyenNganhRepository : Repository<ChuyenNganh>
    {
        public ChuyenNganhRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public int GetIdChuyenNganh(string chuyennganh)
        {
            return _db.ChuyenNganhs.Where(x => x.tenChuyenNganh == chuyennganh).Select(y => y.idChuyenNganh).FirstOrDefault();
        }

        public IList<ChuyenNganh> GetListChuyenNganh()
        {
            return _db.ChuyenNganhs.ToList();
        }

        public void DeleteById(int id)
        {
            ChuyenNganh chuyenNganh = _db.ChuyenNganhs.Where(x => x.idChuyenNganh == id).First();
            _db.ChuyenNganhs.Remove(chuyenNganh);
        }

        public bool CheckExistById(int idRowFocused)
        {
            bool b = _db.ChuyenNganhs.Any(x => x.idChuyenNganh == idRowFocused);
            return b;
        }

        public void Create(string chuyennganh, int idnganhdaotao)
        {
            _db.ChuyenNganhs.Add(new ChuyenNganh { tenChuyenNganh = chuyennganh, idNganhDaoTao = idnganhdaotao });
            _db.SaveChanges();
        }

        public void Update(int id, string chuyennganh, int idnganhdaotao)
        {
            ChuyenNganh chuyenNganh = _db.ChuyenNganhs.Where(x => x.idChuyenNganh == id).First();
            chuyenNganh.tenChuyenNganh = chuyennganh;
            chuyenNganh.idNganhDaoTao = idnganhdaotao;
            _db.SaveChanges();
        }

        public List<string> GetListChuyenNganhForImport()
        {
            return _db.ChuyenNganhs.Select(x => x.tenChuyenNganh).ToList();
        }

        public int GetIdNganhDaoTaoByIdChuyenNganh(int idnganhday)
        {
            return _db.ChuyenNganhs.Where(x => x.idChuyenNganh == idnganhday).Select(y => y.idNganhDaoTao).FirstOrDefault();
        }

        public List<ChuyenNganh> GetListChuyenNganhByIdNganhDaoTao(int idnganhdaotao)
        {
            return _db.ChuyenNganhs.Where(x => x.idNganhDaoTao == idnganhdaotao).ToList();
        }
    }
}
