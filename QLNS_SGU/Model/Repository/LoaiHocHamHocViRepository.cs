using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class LoaiHocHamHocViRepository : Repository<LoaiHocHamHocVi>
    {
        public LoaiHocHamHocViRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public int GetIdLoaiHocHamHocViByTenLoaiHocHamHocVi(string loaihochamhocvi)
        {
            return _db.LoaiHocHamHocVis.Where(x => x.tenLoaiHocHamHocVi == loaihochamhocvi).Select(y => y.idLoaiHocHamHocVi).FirstOrDefault();
        }

        public IList<LoaiHocHamHocVi> GetListLoaiHocHamHocVi()
        {
            return _db.LoaiHocHamHocVis.ToList();
        }

        public void Update(int id, string loaihochamhocvi)
        {
            LoaiHocHamHocVi _loaihochamhocvi = _db.LoaiHocHamHocVis.Where(x => x.idLoaiHocHamHocVi == id).First();
            _loaihochamhocvi.tenLoaiHocHamHocVi = loaihochamhocvi;
            _db.SaveChanges();
        }

        public void Create(string loaihochamhocvi)
        {
            _db.LoaiHocHamHocVis.Add(new LoaiHocHamHocVi { tenLoaiHocHamHocVi = loaihochamhocvi });
            _db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            LoaiHocHamHocVi _loaihochamhocvi = _db.LoaiHocHamHocVis.Where(x => x.idLoaiHocHamHocVi == id).First();
            _db.LoaiHocHamHocVis.Remove(_loaihochamhocvi);
        }

        public bool CheckExistById(int idRowFocused)
        {
            if (_db.LoaiHocHamHocVis.Any(x => x.idLoaiHocHamHocVi == idRowFocused))
            {
                return true;
            }
            return false;
        }

        public int GetIdLoaiHocHamHocViForDangHocNangCao(string s)
        {
            switch (s)
            {
                case "cao học":
                    return _db.LoaiHocHamHocVis.Where(x => x.tenLoaiHocHamHocVi == "Thạc sĩ").Select(y => y.idLoaiHocHamHocVi).FirstOrDefault();
                case "nghiên cứu sinh":
                    return _db.LoaiHocHamHocVis.Where(x => x.tenLoaiHocHamHocVi == "Tiến sĩ").Select(y => y.idLoaiHocHamHocVi).FirstOrDefault();
                default:
                    return -1;
            }
        }

        public string ReturnFirstCharOfLoaiHocHamHocVi(string text)
        {
            string[] arrText = text.Split(' ');
            string result = string.Empty;
            for (int i = 0; i < arrText.Length; i++)
            {
                string temp = arrText[i];
                result += temp[0];
            }
            return result;
        }
    }
}
