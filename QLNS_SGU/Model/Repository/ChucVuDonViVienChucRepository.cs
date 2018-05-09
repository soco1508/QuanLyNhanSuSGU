using Model.Entities;
using Model.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class ChucVuDonViVienChucRepository : Repository<ChucVuDonViVienChuc>
    {
        public ChucVuDonViVienChucRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public List<QuaTrinhCongTacForView> GetListQuaTrinhCongTacForView(string mavienchuc)
        {
            int idvienchuc = _db.VienChucs.Where(x => x.maVienChuc == mavienchuc).Select(y => y.idVienChuc).FirstOrDefault();
            var rows = from a in _db.ChucVuDonViVienChucs
                       where a.idVienChuc == idvienchuc
                       select a; 
            var rows1 = from a in rows
                        group a by a.idDonVi into g
                        from p in g
                        where p.ChucVu.heSoChucVu == g.Max(t => t.ChucVu.heSoChucVu)
                        select p;
            List<ChucVuDonViVienChuc> listChucVuDonViVienChuc = rows1.ToList();
            List<QuaTrinhCongTacForView> listQuaTrinhCongTac = new List<QuaTrinhCongTacForView>();
            for (int i = listChucVuDonViVienChuc.Count - 1; i >= 0; i--)
            {
                listQuaTrinhCongTac.Add(new QuaTrinhCongTacForView
                {
                    Id = listChucVuDonViVienChuc[i].idViTriDonViVienChuc,
                    ChucVu = listChucVuDonViVienChuc[i].ChucVu.tenChucVu,
                    DonVi = listChucVuDonViVienChuc[i].DonVi.tenDonVi,
                    ToChuyenMon = listChucVuDonViVienChuc[i].ToChuyenMon.tenToChuyenMon,
                    DiaDiem = listChucVuDonViVienChuc[i].DonVi.diaDiem,
                    NgayBatDau = listChucVuDonViVienChuc[i].ngayBatDau,
                    NgayKetThuc = listChucVuDonViVienChuc[i].ngayKetThuc,
                    PhanLoaiCongTac = listChucVuDonViVienChuc[i].phanLoaiCongTac,
                    HeSoChucVu = listChucVuDonViVienChuc[i].ChucVu.heSoChucVu,
                    CheckPhanLoaiCongTac = HardCheckPhanLoaiCongTacToGrid(listChucVuDonViVienChuc[i].checkPhanLoaiCongTac),
                    LoaiThayDoi = HardLoaiThayDoiToGrid(listChucVuDonViVienChuc[i].loaiThayDoi),
                    KiemNhiem = HardKiemNhiemToGrid(listChucVuDonViVienChuc[i].kiemNhiem),
                    LinkVanBanDinhKem = listChucVuDonViVienChuc[i].linkVanBanDinhKem
                });
            }
            return listQuaTrinhCongTac;
        }

        public List<QuaTrinhCongTacForView> GetListQuaTrinhCongTacForEdit(string mavienchuc)
        {
            int idvienchuc = _db.VienChucs.Where(x => x.maVienChuc == mavienchuc).Select(y => y.idVienChuc).FirstOrDefault();
            List<ChucVuDonViVienChuc> listChucVuDonViVienChuc = _db.ChucVuDonViVienChucs.Where(x => x.idVienChuc == idvienchuc).ToList();
            List<QuaTrinhCongTacForView> listQuaTrinhCongTac = new List<QuaTrinhCongTacForView>();
            for (int i = listChucVuDonViVienChuc.Count - 1; i >= 0; i--)
            {
                listQuaTrinhCongTac.Add(new QuaTrinhCongTacForView
                {
                    Id = listChucVuDonViVienChuc[i].idViTriDonViVienChuc,
                    ChucVu = listChucVuDonViVienChuc[i].ChucVu.tenChucVu,
                    DonVi = listChucVuDonViVienChuc[i].DonVi.tenDonVi,
                    ToChuyenMon = listChucVuDonViVienChuc[i].ToChuyenMon.tenToChuyenMon,
                    DiaDiem = listChucVuDonViVienChuc[i].DonVi.diaDiem,
                    NgayBatDau = listChucVuDonViVienChuc[i].ngayBatDau,
                    NgayKetThuc = listChucVuDonViVienChuc[i].ngayKetThuc,
                    PhanLoaiCongTac = listChucVuDonViVienChuc[i].phanLoaiCongTac,
                    HeSoChucVu = listChucVuDonViVienChuc[i].ChucVu.heSoChucVu,
                    CheckPhanLoaiCongTac = HardCheckPhanLoaiCongTacToGrid(listChucVuDonViVienChuc[i].checkPhanLoaiCongTac),
                    LoaiThayDoi = HardLoaiThayDoiToGrid(listChucVuDonViVienChuc[i].loaiThayDoi),
                    KiemNhiem = HardKiemNhiemToGrid(listChucVuDonViVienChuc[i].kiemNhiem),
                    LinkVanBanDinhKem = listChucVuDonViVienChuc[i].linkVanBanDinhKem,
                    NhanXet = listChucVuDonViVienChuc[i].nhanXet
                });
            }
            return listQuaTrinhCongTac;
        }

        //public void UpdateNgayBatDau()
        //{
        //    List<ChucVuDonViVienChuc> list = _db.ChucVuDonViVienChucs.ToList();
        //    for(int i = 0; i < list.Count; i++)
        //    {
        //        int id = list[i].idViTriDonViVienChuc; // phải gán biến trước nếu ko sẽ bị lỗi recognize linq khi gán trực tiếp
        //        ChucVuDonViVienChuc cv = _db.ChucVuDonViVienChucs.Where(x => x.idViTriDonViVienChuc == id).FirstOrDefault();
        //        cv.ngayBatDau = Convert.ToDateTime("01/01/2000");
        //    }
        //}

        public void Update(int id, string linkfiledinhkem)
        {
            ChucVuDonViVienChuc chucVuDonViVienChuc = _db.ChucVuDonViVienChucs.Where(x => x.idViTriDonViVienChuc == id).FirstOrDefault();
            chucVuDonViVienChuc.linkVanBanDinhKem = linkfiledinhkem;
        }

        public ChucVuDonViVienChuc GetCongTacByIdVienChucAndDuration(int idVienChuc, DateTime dtFromDuration, DateTime dtToDuration)
        {
            var rows = _db.ChucVuDonViVienChucs.Where(x => x.idVienChuc == idVienChuc);
            List<ChucVuDonViVienChuc> listChucVuDonViVienChuc = new List<ChucVuDonViVienChuc>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau >= dtFromDuration && row.ngayKetThuc <= dtToDuration)
                    {
                        listChucVuDonViVienChuc.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau >= dtFromDuration)
                    {
                        listChucVuDonViVienChuc.Add(row);
                    }
                }
            }
            if(listChucVuDonViVienChuc.Count > 0)
                return listChucVuDonViVienChuc.OrderByDescending(x => x.ChucVu.heSoChucVu).FirstOrDefault();
            return null;
        }

        public ChucVuDonViVienChuc GetCongTacByIdVienChucAndTimeline(int idVienChuc, DateTime dtTimeline)
        {
            var rows = _db.ChucVuDonViVienChucs.Where(x => x.idVienChuc == idVienChuc);
            List<ChucVuDonViVienChuc> listChucVuDonViVienChuc = new List<ChucVuDonViVienChuc>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau <= dtTimeline && row.ngayKetThuc >= dtTimeline)
                    {
                        listChucVuDonViVienChuc.Add(new ChucVuDonViVienChuc(row));
                    }
                }
                if (row.ngayKetThuc == null)
                {
                    if (row.ngayBatDau <= dtTimeline)
                    {
                        listChucVuDonViVienChuc.Add(new ChucVuDonViVienChuc(row));
                    }
                }
            }
            if(listChucVuDonViVienChuc.Count > 0)
                return listChucVuDonViVienChuc.OrderByDescending(x => x.ChucVu.heSoChucVu).FirstOrDefault();
            return null;
        }

        public List<ChucVuDonViVienChuc> GetListCongTacByIdVienChucAndTimeline(int idVienChuc, DateTime dtTimeline)
        {
            var rows = _db.ChucVuDonViVienChucs.Where(x => x.idVienChuc == idVienChuc);
            List<ChucVuDonViVienChuc> listChucVuDonViVienChuc = new List<ChucVuDonViVienChuc>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau <= dtTimeline && row.ngayKetThuc >= dtTimeline)
                    {
                        listChucVuDonViVienChuc.Add(new ChucVuDonViVienChuc(row));
                    }
                }
                if (row.ngayKetThuc == null)
                {
                    if (row.ngayBatDau <= dtTimeline)
                    {
                        listChucVuDonViVienChuc.Add(new ChucVuDonViVienChuc(row));
                    }
                }
            }
            return listChucVuDonViVienChuc;
        }

        public List<ChucVuDonViVienChuc> GetListCongTacByIdVienChucAndDuration(int idVienChuc, DateTime dtFromDuration, DateTime dtToDuration)
        {
            var rows = _db.ChucVuDonViVienChucs.Where(x => x.idVienChuc == idVienChuc);
            List<ChucVuDonViVienChuc> listChucVuDonViVienChuc = new List<ChucVuDonViVienChuc>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau >= dtFromDuration && row.ngayKetThuc <= dtToDuration)
                    {
                        listChucVuDonViVienChuc.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau >= dtFromDuration)
                    {
                        listChucVuDonViVienChuc.Add(row);
                    }
                }
            }
            return listChucVuDonViVienChuc;
        }

        public double? GetHeSoChucVu(string hesochucvu)
        {
            if (hesochucvu != string.Empty)
            {
                double a = Math.Round(Convert.ToDouble(hesochucvu), 2);
                return a;
            }
            return 0;
        }

        public string HardCheckPhanLoaiCongTacToGrid(int? checkphanloaicongtac)
        {
            switch (checkphanloaicongtac)
            {
                case 0:
                    return "Chức vụ quá khứ";
                case 1:
                    return "Một chức vụ hiện tại";
                case 2:
                    return "Nhiều chức vụ hiện tại";
                case 3:
                    return string.Empty;
                default:
                    return string.Empty;
            }
        }
        public string HardKiemNhiemToGrid(int? kiemnhiem)
        {
            switch (kiemnhiem)
            {
                case 0:
                    return "Không";
                case 1:
                    return "Có";
                case 2:
                    return string.Empty;
                default:
                    return string.Empty;
            }
        }

        public List<string> GetListPhanLoaiCongTac()
        {
            return _db.ChucVuDonViVienChucs.Where(x => x.phanLoaiCongTac != null).Select(x => x.phanLoaiCongTac).Distinct().ToList();
        }

        public string HardLoaiThayDoiToGrid(int? loaithaydoi)
        {
            switch (loaithaydoi)
            {
                case 0:
                    return "Chưa thay đổi";
                case 1:
                    return "Thay đổi chức vụ";
                case 2:
                    return "Thay đổi đơn vị";
                case 3:
                    return "Thay đổi tổ bộ môn";
                case 4:
                    return string.Empty;
                default:
                    return string.Empty;
            }
        }
        public int? HardCheckPhanLoaiCongTacToDatabase(string checkphanloaicongtac)
        {
            switch (checkphanloaicongtac)
            {
                case "Chức vụ quá khứ":
                    return 0;
                case "Một chức vụ hiện tại":
                    return 1;
                case "Nhiều chức vụ hiện tại":
                    return 2;
                case "":
                    return 3;
                default:
                    return 3;
            }
        }

        public int? HardKiemNhiemToDatabase(string kiemnhiem)
        {
            switch (kiemnhiem)
            {
                case "Không":
                    return 0;
                case "Có":
                    return 1;
                case "":
                    return 2;
                default:
                    return 2;
            }
        }

        public int? HardLoaiThayDoiToDatabase(string loaithaydoi)
        {
            switch (loaithaydoi)
            {
                case "Chưa thay đổi":
                    return 0;
                case "Thay đổi chức vụ":
                    return 1;
                case "Thay đổi đơn vị":
                    return 2;
                case "Thay đổi tổ bộ môn":
                    return 3;
                case "":
                    return 4;
                default:
                    return 4;
            }
        }

        public ChucVuDonViVienChuc GetObjectById(int idchucvudonvivienchuc)
        {
            return _db.ChucVuDonViVienChucs.Where(x => x.idViTriDonViVienChuc == idchucvudonvivienchuc).FirstOrDefault();
        }

        public void DeleteById(int id)
        {
            ChucVuDonViVienChuc chucVuDonViVienChuc = _db.ChucVuDonViVienChucs.Where(x => x.idViTriDonViVienChuc == id).FirstOrDefault();
            _db.ChucVuDonViVienChucs.Remove(chucVuDonViVienChuc);
        }

        public void UpdateNgayKetThuc()
        {
            List<ChucVuDonViVienChuc> list = _db.ChucVuDonViVienChucs.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                int id = list[i].idViTriDonViVienChuc; // phải gán biến trước nếu ko sẽ bị lỗi recognize linq khi gán trực tiếp
                ChucVuDonViVienChuc cv = _db.ChucVuDonViVienChucs.Where(x => x.idViTriDonViVienChuc == id).FirstOrDefault();
                cv.ngayKetThuc = Convert.ToDateTime("01/01/2020");
            }
        }

        public List<ChucVuDonViVienChuc> GetObjectByIdVienChuc(int idVienChuc)
        {
            return _db.ChucVuDonViVienChucs.Where(x => x.idVienChuc == idVienChuc).Select(x => new ChucVuDonViVienChuc { ChucVu = x.ChucVu, phanLoaiCongTac = x.phanLoaiCongTac, kiemNhiem = x.kiemNhiem }).ToList();
        }

        public List<string> GetListLinkVanBanDinhKem(string maVienChucForGetListLinkVanBanDinhKemQTCT)
        {
            return _db.ChucVuDonViVienChucs.Where(x => x.VienChuc.maVienChuc == maVienChucForGetListLinkVanBanDinhKemQTCT).Select(y => y.linkVanBanDinhKem).ToList();
        }
    }
}
