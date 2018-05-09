using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;
using Model.ObjectModels;

namespace Model.Repository
{
    public class DangHocNangCaoRepository : Repository<DangHocNangCao>
    {
        public DangHocNangCaoRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public List<DangHocNangCaoForView> GetListDangHocNangCao(string mavienchuc)
        {
            int idvienchuc = _db.VienChucs.Where(x => x.maVienChuc == mavienchuc).Select(y => y.idVienChuc).FirstOrDefault();
            List<DangHocNangCao> listDangHocNangCao = _db.DangHocNangCaos.Where(x => x.idVienChuc == idvienchuc).ToList();
            List<DangHocNangCaoForView> listDangHocNangCaoForView = new List<DangHocNangCaoForView>();
            for (int i = 0; i < listDangHocNangCao.Count; i++)
            {
                listDangHocNangCaoForView.Add(new DangHocNangCaoForView
                {
                    Id = listDangHocNangCao[i].idDangHocNangCao,
                    LoaiHocHamHocVi = HardCodeLoaiHocHamHocViToGrid(listDangHocNangCao[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi),
                    SoQuyetDinh = listDangHocNangCao[i].soQuyetDinh,
                    LinkAnhQuyetDinh = listDangHocNangCao[i].linkAnhQuyetDinh,
                    TenHocHamHocVi = HardCodeHocNangCao(listDangHocNangCao[i].tenHocHamHocVi),
                    CoSoDaoTao = listDangHocNangCao[i].coSoDaoTao,
                    NgonNguDaoTao = listDangHocNangCao[i].ngonNguDaoTao,
                    HinhThucDaoTao = listDangHocNangCao[i].hinhThucDaoTao,
                    NuocCapBang = listDangHocNangCao[i].nuocCapBang,
                    NgayBatDau = listDangHocNangCao[i].ngayBatDau,
                    NgayKetThuc = listDangHocNangCao[i].ngayKetThuc,
                    Loai = HardCodeLoaiToGrid(listDangHocNangCao[i].loai)
                });
            }
            return listDangHocNangCaoForView;
        }

        public string HardCodeHocNangCao(string hocnangcao)
        {
            string rs = "";
            if (hocnangcao.Contains("TS ") && hocnangcao.Contains("ThS ") == false)
            {
                rs = hocnangcao.Replace("TS ", "Nghiên cứu sinh ");
            }
            else if (hocnangcao.Contains("TS ") == false && hocnangcao.Contains("ThS "))
            {
                rs = hocnangcao.Replace("ThS ", "Cao học ");
            }
            return rs;
        }

        public string HardCodeLoaiHocHamHocViToGrid(string tenLoaiHocHamHocVi)
        {
            switch (tenLoaiHocHamHocVi)
            {
                case "Thạc sĩ":
                    return "Cao học";
                case "Tiến sĩ":
                    return "Nghiên cứu sinh";
                default:
                    return "";
            }
        }

        public string HardCodeLoaiToGrid(int? loai)
        {
            switch (loai)
            {
                case 0:
                    return "Đang học";
                case 1:
                    return "Đã xong";
                case 2:
                    return "Gia hạn";
                case 3:
                    return "Hết hạn";
                default:
                    return "";
            }
        }

        public DateTime? ParseDatetimeMatchDatetimeDatabase(string s)
        {
            if(s != string.Empty)
            {
                if (s.Contains("/"))
                {
                    return Convert.ToDateTime(s);
                }
                else
                {
                    return Convert.ToDateTime("01/01/" + s); // s = 2017
                }
            }
            return null;
        }

        public string ConcatString(string trinhdo, string chuyennganh)
        {
            switch (trinhdo)
            {
                case "cao học":
                    return "ThS " + chuyennganh;
                case "nghiên cứu sinh":
                    return "TS " + chuyennganh;
                default:
                    return "";
            }
        }

        public int? HardCodeLoaiToDatabase(string loai)
        {
            switch (loai)
            {
                case "Đang học":
                    return 0;
                case "Đã xong":
                    return 1;
                case "Gia hạn":
                    return 2;
                case "Hết hạn":
                    return 3;
                default:
                    return 0;
            }
        }

        public DangHocNangCao GetObjectById(int iddanghocnangcao)
        {
            return _db.DangHocNangCaos.Where(x => x.idDangHocNangCao == iddanghocnangcao).FirstOrDefault();
        }

        public void DeleteById(int id)
        {
            var a = _db.DangHocNangCaos.Where(x => x.idDangHocNangCao == id).FirstOrDefault();
            _db.DangHocNangCaos.Remove(a);
        }

        public DangHocNangCao GetMaxLoaiHocHamHocVi(List<DangHocNangCao> list)
        {
            DangHocNangCao dangHocNangCao = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi.Equals("Giáo sư"))
                {
                    dangHocNangCao = new DangHocNangCao(list[i]);
                    break;
                }
                if (list[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi.Equals("Phó giáo sư"))
                {
                    dangHocNangCao = new DangHocNangCao(list[i]);
                    break;
                }
                if (list[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi.Equals("Tiến sĩ"))
                {
                    dangHocNangCao = new DangHocNangCao(list[i]);
                    break;
                }
                if (list[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi.Equals("Thạc sĩ"))
                {
                    dangHocNangCao = new DangHocNangCao(list[i]);
                    break;
                }
                if (list[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi.Equals("Đại học"))
                {
                    dangHocNangCao = new DangHocNangCao(list[i]);
                    break;
                }
                if (list[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi.Equals("Cao đẳng"))
                {
                    dangHocNangCao = new DangHocNangCao(list[i]);
                    break;
                }
                if (list[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi.Equals("Trung cấp"))
                {
                    dangHocNangCao = new DangHocNangCao(list[i]);
                    break;
                }
                if (list[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi.Equals("Phổ thông"))
                {
                    dangHocNangCao = new DangHocNangCao(list[i]);
                    break;
                }
                if (list[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi.Equals("Khác"))
                {
                    dangHocNangCao = new DangHocNangCao(list[i]);
                    break;
                }
            }
            return dangHocNangCao;
        }

        public DangHocNangCao GetDangHocNangCaoByIdVienChucAndTimelineForExportOne(int idVienChuc, DateTime dtTimeline)
        {
            var rows = _db.DangHocNangCaos.Where(x => x.idVienChuc == idVienChuc);
            List<DangHocNangCao> listDangHocNangCao = new List<DangHocNangCao>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau <= dtTimeline && row.ngayKetThuc >= dtTimeline)
                    {
                        listDangHocNangCao.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau <= dtTimeline)
                    {
                        listDangHocNangCao.Add(row);
                    }
                }
            }
            if(listDangHocNangCao.Count > 0)
            {
                DangHocNangCao dangHocNangCao = GetMaxLoaiHocHamHocVi(listDangHocNangCao);
                return dangHocNangCao;
            }
            return null;            
        }

        public DangHocNangCao GetDangHocNangCaoByIdVienChucAndDurationForExportOne(int idVienChuc, DateTime dtFromDuration, DateTime dtToDuration)
        {
            var rows = _db.DangHocNangCaos.Where(x => x.idVienChuc == idVienChuc);
            List<DangHocNangCao> listDangHocNangCao = new List<DangHocNangCao>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau >= dtFromDuration && row.ngayKetThuc <= dtToDuration)
                    {
                        listDangHocNangCao.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau >= dtFromDuration)
                    {
                        listDangHocNangCao.Add(row);
                    }
                }
            }
            if (listDangHocNangCao.Count > 0)
            {
                DangHocNangCao dangHocNangCao = GetMaxLoaiHocHamHocVi(listDangHocNangCao);
                return dangHocNangCao;
            }
            return null;
        }

        public List<string> GetListCoSoDaoTao()
        {
            return _db.DangHocNangCaos.Select(x => x.coSoDaoTao).Distinct().ToList();
        }

        public List<string> GetListHinhThucDaoTao()
        {
            return _db.DangHocNangCaos.Select(x => x.hinhThucDaoTao).Distinct().ToList();
        }

        public List<string> GetListNgonNguDaoTao()
        {
            return _db.DangHocNangCaos.Select(x => x.ngonNguDaoTao).Distinct().ToList();
        }

        public List<string> GetListNuocCapBang()
        {
            return _db.DangHocNangCaos.Select(x => x.nuocCapBang).Distinct().ToList();
        }

        public List<string> GetListLinkAnhQuyetDinh(string maVienChucForGetListLinkAnhQuyetDinh)
        {
            return _db.DangHocNangCaos.Where(x => x.VienChuc.maVienChuc == maVienChucForGetListLinkAnhQuyetDinh).Select(y => y.linkAnhQuyetDinh).ToList();
        }

        public List<DangHocNangCao> GetListDangHocNangCaoByIdVienChucAndDurationForExportFull(int idVienChuc, DateTime dtFromDuration, DateTime dtToDuration)
        {
            var rows = _db.DangHocNangCaos.Where(x => x.idVienChuc == idVienChuc);
            List<DangHocNangCao> listDangHocNangCao = new List<DangHocNangCao>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau >= dtFromDuration && row.ngayKetThuc <= dtToDuration)
                    {
                        listDangHocNangCao.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau >= dtFromDuration)
                    {
                        listDangHocNangCao.Add(row);
                    }
                }
            }
            return listDangHocNangCao;
        }

        public List<DangHocNangCao> GetListDangHocNangCaoByIdVienChucAndTimelineForExportFull(int idVienChuc, DateTime dtTimeline)
        {
            var rows = _db.DangHocNangCaos.Where(x => x.idVienChuc == idVienChuc);
            List<DangHocNangCao> listDangHocNangCao = new List<DangHocNangCao>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau <= dtTimeline && row.ngayKetThuc >= dtTimeline)
                    {
                        listDangHocNangCao.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau <= dtTimeline)
                    {
                        listDangHocNangCao.Add(row);
                    }
                }
            }
            return listDangHocNangCao;
        }
    }
}
