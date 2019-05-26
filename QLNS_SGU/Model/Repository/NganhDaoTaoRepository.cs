using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class NganhDaoTaoRepository : Repository<NganhDaoTao>
    {
        public NganhDaoTaoRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public int GetIdNganhDaoTao(string nganhdaotao)
        {
            int idNganhDaoTao = _db.NganhDaoTaos.Where(x => x.tenNganhDaoTao.ToLower() == nganhdaotao.ToLower()).Select(y => y.idNganhDaoTao).FirstOrDefault();
            if (idNganhDaoTao > 0)
                return idNganhDaoTao;
            return _db.NganhDaoTaos.Where(x => x.tenNganhDaoTao == string.Empty).Select(y => y.idNganhDaoTao).FirstOrDefault();
        }

        public List<string> GetListNganhDaoTaoForImport()
        {
            return _db.NganhDaoTaos.Select(x => x.tenNganhDaoTao).ToList();
        }

        public IList<NganhDaoTao> GetListNganhDaoTao()
        {
            return _db.NganhDaoTaos.ToList();
        }

        public void DeleteById(int id)
        {
            NganhDaoTao nganhDaoTao = _db.NganhDaoTaos.Where(x => x.idNganhDaoTao == id).FirstOrDefault();
            _db.NganhDaoTaos.Remove(nganhDaoTao);
        }

        public bool CheckExistById(int idRowFocused)
        {
            bool b = _db.NganhDaoTaos.Any(x => x.idNganhDaoTao == idRowFocused);
            return b;
        }

        public void Create(int idnganhdaotao, string nganhdaotao, int idloainganh)
        {
            _db.NganhDaoTaos.Add(new NganhDaoTao { idNganhDaoTao = idnganhdaotao, tenNganhDaoTao = nganhdaotao, idLoaiNganh = idloainganh });
            _db.SaveChanges();
        }

        public void Update(int id, string nganhdaotao, int idloainganh)
        {
            NganhDaoTao nganhDaoTao = _db.NganhDaoTaos.Where(x => x.idNganhDaoTao == id).FirstOrDefault();
            nganhDaoTao.tenNganhDaoTao = nganhdaotao;
            nganhDaoTao.idLoaiNganh = idloainganh;
            _db.SaveChanges();
        }

        public int GetIdLoaiNganhByIdNganhDaoTao(int idnganhdaotao)
        {
            return _db.NganhDaoTaos.Where(x => x.idNganhDaoTao == idnganhdaotao).Select(y => y.idLoaiNganh).FirstOrDefault();
        }

        public List<NganhDaoTao> GetListNganhDaoTaoByIdLoaiNganh(int idloainganh)
        {
            return _db.NganhDaoTaos.Where(x => x.idLoaiNganh == idloainganh).ToList();
        }

        public int GetIdNganhDaoTaoByIdChuyenNganh(int idchuyennganh)
        {
            return _db.ChuyenNganhs.Where(x => x.idChuyenNganh == idchuyennganh).Select(y => y.idNganhDaoTao).FirstOrDefault();
        }

        /// <summary>
        /// nganhdaotao + loainganh
        /// </summary>
        /// <returns></returns>
        public List<string> GetListTenNganhDaoTaoVaLoaiNganh()
        {
            var rows = _db.NganhDaoTaos.Select(x => new { x.tenNganhDaoTao, x.LoaiNganh.tenLoaiNganh });
            var list = new List<string>();
            foreach(var row in rows)
            {
                string temp = row.tenNganhDaoTao + row.tenLoaiNganh;
                list.Add(temp);
            }
            return list;
        }
    }
}
