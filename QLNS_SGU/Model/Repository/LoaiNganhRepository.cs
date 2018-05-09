using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class LoaiNganhRepository : Repository<LoaiNganh>
    {
        public LoaiNganhRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public int GetIdLoaiNganhByNganhDaoTao(string nganhdaotao)
        {
            return _db.NganhDaoTaos.Where(x => x.tenNganhDaoTao == nganhdaotao).Select(y => y.idLoaiNganh).FirstOrDefault();
        }

        public List<LoaiNganh> GetListLoaiNganh()
        {
            return _db.LoaiNganhs.ToList();
        }

        public void Update(int id, string loainganh)
        {
            LoaiNganh loaiNganh = _db.LoaiNganhs.Where(x => x.idLoaiNganh == id).First();
            loaiNganh.tenLoaiNganh = loainganh;
            _db.SaveChanges();
        }

        public void Create(string loainganh)
        {
            _db.LoaiNganhs.Add(new LoaiNganh { tenLoaiNganh = loainganh });
            _db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            LoaiNganh loaiNganh = _db.LoaiNganhs.Where(x => x.idLoaiNganh == id).First();
            _db.LoaiNganhs.Remove(loaiNganh);
        }

        public bool CheckExistById(int idRowFocused)
        {
            if(_db.LoaiNganhs.Any(x => x.idLoaiNganh == idRowFocused))
            {
                return true;
            }
            return false;
        }

        public int GetIdLoaiNganh(string tenloainganh)
        {
            return _db.LoaiNganhs.Where(x => x.tenLoaiNganh == tenloainganh).Select(y => y.idLoaiNganh).FirstOrDefault();
        }

        public int GetIdLoaiNganhByIdNganhDaoTao(int idnganhdaotao)
        {
            return _db.NganhDaoTaos.Where(x => x.idNganhDaoTao == idnganhdaotao).Select(y => y.idLoaiNganh).FirstOrDefault();
        }

        public int GetIdLoaiNganhEmpty()
        {
            return _db.LoaiNganhs.Where(x => x.tenLoaiNganh == string.Empty).Select(y => y.idLoaiNganh).FirstOrDefault();
        }
    }
}
