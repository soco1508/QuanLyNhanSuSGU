﻿using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class QuanTriVienRepository : Repository<QuanTriVien>
    {
        public QuanTriVienRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public bool CheckLogin(string taikhoan, string matkhau)
        {
            int check = _db.QuanTriViens.Where(x => x.taikhoan == taikhoan && x.matkhau == matkhau).Select(y => y.id).SingleOrDefault();
            return check > 0 ? true : false;
        }

        public int? GetRoleByUsername(string taikhoan)
        {
            return _db.QuanTriViens.Where(x => x.taikhoan == taikhoan).Select(y => y.phanQuyen).FirstOrDefault();
        }
    }
}
