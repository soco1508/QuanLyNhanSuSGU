using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class LoaiDonViRepository : Repository<LoaiDonVi>
    {
        public LoaiDonViRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public int GetIdLoaiDonVi(string donvi)
        {
            string[] temp_donvi = donvi.Split(' ');
            if (temp_donvi[0].Contains("Phòng"))
            {
                return 1;
            }
            else if (temp_donvi[0].Contains("Khoa"))
            {
                return 2;
            }
            else if (temp_donvi[0].Contains("Trung tâm"))
            {
                return 3;
            }
            else if (temp_donvi[0].Contains("Viện"))
            {
                return 4;
            }
            else if (temp_donvi[0].Contains("Ban"))
            {
                return 5;
            }
            else
            {
                return 6;
            }
        }

        public IList<LoaiDonVi> GetListLoaiDonVi()
        {
            return _db.LoaiDonVis.ToList();
        }

        public void Update(int id, string loaidonvi)
        {
            LoaiDonVi loaiDonVi = _db.LoaiDonVis.Where(x => x.idLoaiDonVi == id).First();
            loaiDonVi.tenLoaiDonVi = loaidonvi;
            _db.SaveChanges();
        }

        public void Create(string loaidonvi)
        {
            _db.LoaiDonVis.Add(new LoaiDonVi { tenLoaiDonVi = loaidonvi });
            _db.SaveChanges();
        }

        public void DeleteById(int id)
        {
            LoaiDonVi loaiDonVi = _db.LoaiDonVis.Where(x => x.idLoaiDonVi == id).First();
            _db.LoaiDonVis.Remove(loaiDonVi);
        }

        public bool CheckExistById(int idRowFocused)
        {
            if (_db.LoaiDonVis.Any(x => x.idLoaiDonVi == idRowFocused))
            {
                return true;
            }
            return false;
        }
    }
}
