using Model.Entities;
using Model.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class HocHamHocViVienChucRepository : Repository<HocHamHocViVienChuc>
    {
        public HocHamHocViVienChucRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        private int GetIdVienChuc(string mavienchuc)
        {
            return _db.VienChucs.Where(x => x.maVienChuc == mavienchuc).Select(y => y.idVienChuc).FirstOrDefault();
        }

        public List<HocHamHocViForTabPageChuyenMon> GetListHocHamHocViForTabPageChuyenMon(string mavienchuc)
        {
            int idvienchuc = GetIdVienChuc(mavienchuc);
            List<HocHamHocViVienChuc> listHocHamHocViVienChuc = _db.HocHamHocViVienChucs.Where(x => x.idVienChuc == idvienchuc).ToList();
            List<HocHamHocViForTabPageChuyenMon> listHocHamHocViForView = new List<HocHamHocViForTabPageChuyenMon>();
            for (int i = 0; i < listHocHamHocViVienChuc.Count; i++)
            {
                listHocHamHocViForView.Add(new HocHamHocViForTabPageChuyenMon
                {
                    Id = listHocHamHocViVienChuc[i].idHocHamHocViVienChuc,
                    LoaiHocHamHocVi = listHocHamHocViVienChuc[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi,
                    LoaiNganh = listHocHamHocViVienChuc[i].LoaiNganh.tenLoaiNganh,
                    NganhDaoTao = listHocHamHocViVienChuc[i].NganhDaoTao.tenNganhDaoTao,
                    ChuyenNganh = listHocHamHocViVienChuc[i].ChuyenNganh.tenChuyenNganh,
                    TenHocHamHocVi = listHocHamHocViVienChuc[i].tenHocHamHocVi,
                    CoSoDaoTao = listHocHamHocViVienChuc[i].coSoDaoTao,
                    NgonNguDaoTao = listHocHamHocViVienChuc[i].ngonNguDaoTao,
                    HinhThucDaoTao = listHocHamHocViVienChuc[i].hinhThucDaoTao,
                    NuocCapBang = listHocHamHocViVienChuc[i].nuocCapBang,
                    NgayCapBang = listHocHamHocViVienChuc[i].ngayCapBang,
                    LinkVanBanDinhKem = listHocHamHocViVienChuc[i].linkVanBanDinhKem
                });
            }
            return listHocHamHocViForView;
        }

        public int GetIdHocHamHocViVienChuc(int idvienchuc, int idloaihochamhocvi, int idloainganh, int idnganhdaotao, int idchuyennganh)
        {
            return _db.HocHamHocViVienChucs
                   .Where(x => x.idVienChuc == idvienchuc && x.idLoaiHocHamHocVi == idloaihochamhocvi && x.idLoaiNganh == idloainganh && x.idNganhDaoTao == idnganhdaotao && x.idChuyenNganh == idchuyennganh)
                   .Select(y => y.idHocHamHocViVienChuc).First();
        }

        public int? AssignBacHocHamHocVi(string trinhdo)
        {
            switch (trinhdo)
            {
                case "Phổ thông":
                    return 1;
                case "Trung cấp":
                    return 2;
                case "Cao đẳng":
                    return 3;
                case "Đại học":
                    return 4;
                case "Thạc sĩ":
                    return 5;
                case "Tiến sĩ":
                    return 6;
                case "Phó giáo sư":
                    return 7;
                case "Giáo sư":
                    return 8;
                case "Khác":
                    return 0;
                default:
                    return 0;
            }
        }

        public string ConcatString(string trinhdo, string nganhdaotao, string chuyennganh)
        {
            return trinhdo + " " + nganhdaotao + " - " + chuyennganh;
        }

        public int? HardCodeBacToDatabase(string loaihochamhocvi)
        {
            switch (loaihochamhocvi)
            {
                case "Phổ thông":
                    return 1;
                case "Trung cấp":
                    return 2;
                case "Cao đẳng":
                    return 3;
                case "Đại học":
                    return 4;
                case "Thạc sĩ":
                    return 5;
                case "Tiến sĩ":
                    return 6;
                case "Phó giáo sư":
                    return 7;
                case "Giáo sư":
                    return 8;
                case "Khác":
                    return 0;
                default:
                    return 0;
            }
        }

        public HocHamHocViVienChuc GetObjectById(int idhochamhocvi)
        {
            return _db.HocHamHocViVienChucs.Where(x => x.idHocHamHocViVienChuc == idhochamhocvi).FirstOrDefault();                
        }

        public void DeleteById(int id)
        {
            var a = _db.HocHamHocViVienChucs.Where(x => x.idHocHamHocViVienChuc == id).FirstOrDefault();
            _db.HocHamHocViVienChucs.Remove(a);
        }

        public List<HocHamHocViVienChuc> GetListTenHocHamHocViVienChuc(string mavienchuc)
        {
            return _db.HocHamHocViVienChucs.Where(x => x.VienChuc.maVienChuc == mavienchuc).ToList();
        }

        public List<HocHamHocViGridAtRightViewInMainForm> GetListHocHamHocViGridAtRightViewInMainForm(string mavienchuc)
        {
            int idvienchuc = GetIdVienChuc(mavienchuc);
            var listHocHamHocViVienChuc = _db.HocHamHocViVienChucs.Where(x => x.idVienChuc == idvienchuc).OrderByDescending(y => y.bacHocHamHocVi).ToList();
            List<HocHamHocViGridAtRightViewInMainForm> list = new List<HocHamHocViGridAtRightViewInMainForm>();
            listHocHamHocViVienChuc.ForEach(x =>
            {
                list.Add(new HocHamHocViGridAtRightViewInMainForm
                {
                    TenHocHamHocVi = x.tenHocHamHocVi,
                    LoaiHocHamHocVi = x.LoaiHocHamHocVi.tenLoaiHocHamHocVi,
                    NganhDaoTao = x.NganhDaoTao.tenNganhDaoTao,
                    ChuyenNganh = x.ChuyenNganh.tenChuyenNganh
                });
            });
            return list;
        }

        public List<string> GetListCoSoDaoTao()
        {
            return _db.HocHamHocViVienChucs.Where(x => x.coSoDaoTao != null).Select(y => y.coSoDaoTao).ToList();
        }

        public List<string> GetListHinhThucDaoTao()
        {
            return _db.HocHamHocViVienChucs.Where(x => x.hinhThucDaoTao != null).Select(y => y.hinhThucDaoTao).ToList();
        }

        public List<string> GetListNgonNguDaoTao()
        {
            return _db.HocHamHocViVienChucs.Where(x => x.ngonNguDaoTao != null).Select(y => y.ngonNguDaoTao).ToList();
        }

        public List<string> GetListNuocCapBang()
        {
            return _db.HocHamHocViVienChucs.Where(x => x.nuocCapBang != null).Select(y => y.nuocCapBang).ToList();
        }

        public List<string> GetListLinkVanBanDinhKem(string maVienChucForGetListLinkVanBanDinhKemHHHV)
        {
            return _db.HocHamHocViVienChucs.Where(x => x.VienChuc.maVienChuc == maVienChucForGetListLinkVanBanDinhKemHHHV).Select(y => y.linkVanBanDinhKem).ToList();
        }

        public int GetNewestIdHocHamHocViVienChuc()
        {
            return _db.HocHamHocViVienChucs.Max(m => m.idHocHamHocViVienChuc);
        }

        public int GetIdHocHamHocViVienChucEmpty()
        {
            return _db.HocHamHocViVienChucs.Where(x => x.tenHocHamHocVi == string.Empty).Select(y => y.idHocHamHocViVienChuc).FirstOrDefault();
        }
    }
}
