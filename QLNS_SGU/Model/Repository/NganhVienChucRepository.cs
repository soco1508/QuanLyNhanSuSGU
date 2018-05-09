using Model.Entities;
using Model.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class NganhVienChucRepository : Repository<NganhVienChuc>
    {
        public NganhVienChucRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public List<NganhForView> GetListNganh(string mavienchuc)
        {
            int idvienchuc = _db.VienChucs.Where(x => x.maVienChuc == mavienchuc).Select(y => y.idVienChuc).FirstOrDefault();
            List<NganhVienChuc> listNganhVienChuc = _db.NganhVienChucs.Where(x => x.idVienChuc == idvienchuc).ToList();
            List<NganhForView> listNganhForView = new List<NganhForView>();
            for(int i = 0; i < listNganhVienChuc.Count; i++)
            {
                listNganhForView.Add(new NganhForView
                {
                    Id = listNganhVienChuc[i].idNganhVienChuc,
                    IdHocHamHocViVienChuc = listNganhVienChuc[i].idHocHamHocViVienChuc,
                    LoaiNganh = listNganhVienChuc[i].LoaiNganh.tenLoaiNganh,
                    NganhDaoTao = listNganhVienChuc[i].NganhDaoTao.tenNganhDaoTao,
                    ChuyenNganh = listNganhVienChuc[i].ChuyenNganh.tenChuyenNganh,
                    TenHocHamHocVi = listNganhVienChuc[i].HocHamHocViVienChuc.tenHocHamHocVi,
                    TrinhDoDay = listNganhVienChuc[i].trinhDoDay,
                    PhanLoai = HardCodePhanLoaiToGrid(listNganhVienChuc[i].phanLoai),
                    NgayBatDau = listNganhVienChuc[i].ngayBatDau,
                    NgayKetThuc = listNganhVienChuc[i].ngayKetThuc,
                    LinkVanBanDinhKem = listNganhVienChuc[i].linkVanBanDinhKem
                });
            }
            return listNganhForView;
        }

        public List<NganhVienChuc> GetListNganhHocByIdVienChucAndDurationForExportFull(int idVienChuc, DateTime dtFromDuration, DateTime dtToDuration)
        {
            List<NganhVienChuc> listNganhVienChucByIdVienChucAndTimeline = _db.NganhVienChucs
                                .Where(x => x.idVienChuc == idVienChuc && x.phanLoai == true && x.HocHamHocViVienChuc.ngayCapBang <= dtToDuration && x.HocHamHocViVienChuc.ngayCapBang >= dtFromDuration)
                                .ToList();
            return listNganhVienChucByIdVienChucAndTimeline;
        }

        public List<NganhVienChuc> GetListNganhHocByIdVienChucAndTimelineForExportFull(int idVienChuc, DateTime dtTimeline)
        {
            List<NganhVienChuc> listNganhVienChucByIdVienChucAndTimeline = _db.NganhVienChucs
                                .Where(x => x.idVienChuc == idVienChuc && x.phanLoai == true && x.HocHamHocViVienChuc.ngayCapBang <= dtTimeline)
                                .ToList();
            return listNganhVienChucByIdVienChucAndTimeline;
        }

        public List<NganhVienChuc> GetListNganhDayByIdVienChucAndDurationForExportFull(int idVienChuc, DateTime dtFromDuration, DateTime dtToDuration)
        {
            var rows = _db.NganhVienChucs.Where(x => x.idVienChuc == idVienChuc && x.phanLoai == false);
            List<NganhVienChuc> listNganhVienChuc = new List<NganhVienChuc>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau >= dtFromDuration && row.ngayKetThuc <= dtToDuration)
                    {
                        listNganhVienChuc.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau >= dtFromDuration)
                    {
                        listNganhVienChuc.Add(row);
                    }
                }
            }
            return listNganhVienChuc;
        }

        public List<NganhVienChuc> GetListNganhDayByIdVienChucAndTimelineForExportFull(int idVienChuc, DateTime dtTimeline)
        {
            var rows = _db.NganhVienChucs.Where(x => x.idVienChuc == idVienChuc && x.phanLoai == false);
            List<NganhVienChuc> listNganhVienChuc = new List<NganhVienChuc>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau <= dtTimeline && row.ngayKetThuc >= dtTimeline)
                    {
                        listNganhVienChuc.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau <= dtTimeline)
                    {
                        listNganhVienChuc.Add(row);
                    }
                }
            }
            return listNganhVienChuc;
        }

        public NganhVienChuc GetNganhHocByIdVienChucAndDurationForExportOne(int idVienChuc, DateTime dtFromDuration, DateTime dtToDuration)
        {
            List<NganhVienChuc> listNganhVienChucByIdVienChucAndDuration = _db.NganhVienChucs.Where(x => x.idVienChuc == idVienChuc && x.phanLoai == true && x.HocHamHocViVienChuc.ngayCapBang >= dtFromDuration && x.HocHamHocViVienChuc.ngayCapBang <= dtToDuration).OrderByDescending(y => y.HocHamHocViVienChuc.bacHocHamHocVi).ToList();
            NganhVienChuc nganhVienChuc = null;
            List<NganhVienChuc> listNganhVienChucToAdd = new List<NganhVienChuc>();
            if (listNganhVienChucByIdVienChucAndDuration.Count == 1)
            {
                nganhVienChuc = new NganhVienChuc(listNganhVienChucByIdVienChucAndDuration[0]);
            }
            if (listNganhVienChucByIdVienChucAndDuration.Count > 1)
            {
                int? bac = listNganhVienChucByIdVienChucAndDuration[0].HocHamHocViVienChuc.bacHocHamHocVi;
                int? countbac = listNganhVienChucByIdVienChucAndDuration.Where(x => x.HocHamHocViVienChuc.bacHocHamHocVi == bac).Count();
                if (countbac > 1)
                {
                    for (int i = 0; i < listNganhVienChucByIdVienChucAndDuration.Count; i++)
                    {
                        if (listNganhVienChucByIdVienChucAndDuration[i].HocHamHocViVienChuc.bacHocHamHocVi == bac)
                        {
                            listNganhVienChucToAdd.Add(listNganhVienChucByIdVienChucAndDuration[i]);
                        }
                        else
                        {
                            break;
                        }
                    }
                    nganhVienChuc = listNganhVienChucToAdd.OrderByDescending(x => x.HocHamHocViVienChuc.ngayCapBang).FirstOrDefault();
                }
                else
                {
                    nganhVienChuc = new NganhVienChuc(listNganhVienChucByIdVienChucAndDuration[0]);
                }
            }
            return nganhVienChuc;
        }

        public NganhVienChuc GetNganhHocByIdVienChucAndTimelineForExportOne(int idVienChuc, DateTime dtTimeline)
        {
            List<NganhVienChuc> listNganhVienChucByIdVienChucAndTimeline = _db.NganhVienChucs.Where(x => x.idVienChuc == idVienChuc && x.phanLoai == true && x.HocHamHocViVienChuc.ngayCapBang <= dtTimeline).OrderByDescending(y => y.HocHamHocViVienChuc.bacHocHamHocVi).ToList();
            NganhVienChuc nganhVienChuc = null;
            List<NganhVienChuc> listNganhVienChucToAdd = new List<NganhVienChuc>();
            if(listNganhVienChucByIdVienChucAndTimeline.Count == 1)
            {
                nganhVienChuc = new NganhVienChuc(listNganhVienChucByIdVienChucAndTimeline[0]);
            }
            if(listNganhVienChucByIdVienChucAndTimeline.Count > 1)
            {
                int? bac = listNganhVienChucByIdVienChucAndTimeline[0].HocHamHocViVienChuc.bacHocHamHocVi;
                int? countbac = listNganhVienChucByIdVienChucAndTimeline.Where(x => x.HocHamHocViVienChuc.bacHocHamHocVi == bac).Count();
                if(countbac > 1)
                {
                    for (int i = 0; i < listNganhVienChucByIdVienChucAndTimeline.Count; i++)
                    {
                        if(listNganhVienChucByIdVienChucAndTimeline[i].HocHamHocViVienChuc.bacHocHamHocVi == bac)
                        {
                            listNganhVienChucToAdd.Add(listNganhVienChucByIdVienChucAndTimeline[i]);
                        }
                        else
                        {
                            break;
                        }
                    }
                    nganhVienChuc = listNganhVienChucToAdd.OrderByDescending(x => x.HocHamHocViVienChuc.ngayCapBang).FirstOrDefault();
                }
                else
                {
                    nganhVienChuc = new NganhVienChuc(listNganhVienChucByIdVienChucAndTimeline[0]);
                }                
            }
            return nganhVienChuc;
        }

        public NganhVienChuc GetMaxTrinhDoDay(List<NganhVienChuc> list)
        {
            NganhVienChuc nganhVienChuc = null;
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].trinhDoDay.Equals("Giáo sư"))
                {
                    nganhVienChuc = new NganhVienChuc(list[i]);
                    break;
                }
                if (list[i].trinhDoDay.Equals("Phó giáo sư"))
                {
                    nganhVienChuc = new NganhVienChuc(list[i]);
                    break;
                }
                if (list[i].trinhDoDay.Equals("Tiến sĩ"))
                {
                    nganhVienChuc = new NganhVienChuc(list[i]);
                    break;
                }
                if (list[i].trinhDoDay.Equals("Thạc sĩ"))
                {
                    nganhVienChuc = new NganhVienChuc(list[i]);
                    break;
                }
                if (list[i].trinhDoDay.Equals("Đại học"))
                {
                    nganhVienChuc = new NganhVienChuc(list[i]);
                    break;
                }
                if (list[i].trinhDoDay.Equals("Cao đẳng"))
                {
                    nganhVienChuc = new NganhVienChuc(list[i]);
                    break;
                }
                if (list[i].trinhDoDay.Equals("Trung cấp"))
                {
                    nganhVienChuc = new NganhVienChuc(list[i]);
                    break;
                }
                if (list[i].trinhDoDay.Equals("Phổ thông"))
                {
                    nganhVienChuc = new NganhVienChuc(list[i]);
                    break;
                }
                if (list[i].trinhDoDay.Equals("Khác"))
                {
                    nganhVienChuc = new NganhVienChuc(list[i]);
                    break;
                }
            }
            return nganhVienChuc;
        }

        public NganhVienChuc GetNganhDayByIdVienChucAndTimeline(int idVienChuc, DateTime dtTimeline)
        {
            var rows = _db.NganhVienChucs.Where(x => x.idVienChuc == idVienChuc && x.phanLoai == false);
            List<NganhVienChuc> listNganhVienChuc = new List<NganhVienChuc>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau <= dtTimeline && row.ngayKetThuc >= dtTimeline)
                    {
                        listNganhVienChuc.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau <= dtTimeline)
                    {
                        listNganhVienChuc.Add(row);
                    }
                }
            }
            if(listNganhVienChuc.Count > 0)
            {
                NganhVienChuc nganhVienChuc = GetMaxTrinhDoDay(listNganhVienChuc);
                return nganhVienChuc;
            }
            return null;
        }

        public NganhVienChuc GetNganhDayByIdVienChucAndDuration(int idVienChuc, DateTime dtFromPeriodOfTime, DateTime dtToPeriodOfTime)
        {
            var rows = _db.NganhVienChucs.Where(x => x.idVienChuc == idVienChuc && x.phanLoai == false);
            List<NganhVienChuc> listNganhVienChuc = new List<NganhVienChuc>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau >= dtFromPeriodOfTime && row.ngayKetThuc <= dtToPeriodOfTime)
                    {
                        listNganhVienChuc.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau >= dtFromPeriodOfTime)
                    {
                        listNganhVienChuc.Add(row);
                    }
                }
            }
            if (listNganhVienChuc.Count > 0)
            {
                NganhVienChuc nganhVienChuc = GetMaxTrinhDoDay(listNganhVienChuc);
                return nganhVienChuc;
            }
            return null;
        }

        public NganhVienChuc GetObjectByIdHocHamHocViVienChuc(int idhochamhocvi)
        {
            return _db.NganhVienChucs.Where(x => x.idHocHamHocViVienChuc == idhochamhocvi).FirstOrDefault();
        }

        public int HardCodePhanLoaiToRadioGroup(string phanloai)
        {
            if (phanloai == "Học")
                return 0;
            return 1;
        }

        public List<string> GetListCapDoChungChi()
        {
            return _db.ChungChiVienChucs.Where(x => x.capDoChungChi != null).Select(x => x.capDoChungChi).Distinct().ToList();
        }

        public List<string> GetListLinkVanBanDinhKem(string maVienChucForGetListLinkVanBanDinhKemN)
        {
            return _db.NganhVienChucs.Where(x => x.VienChuc.maVienChuc == maVienChucForGetListLinkVanBanDinhKemN).Select(y => y.linkVanBanDinhKem).ToList();
        }

        public List<string> GetListTrinhDoDay()
        {
            return _db.LoaiHocHamHocVis.Where(x => x.tenLoaiHocHamHocVi != null).Select(x => x.tenLoaiHocHamHocVi).ToList();
        }

        public string HardCodePhanLoaiToGrid(bool? phanLoai)
        {
            if (phanLoai == true)
                return "Học";
            return "Dạy";
        }

        public bool? HardCodePhanLoaiToDatabase(int phanloai)
        {
            if (phanloai == 0)
                return true; //Hoc
            return false; //Day
        }

        public NganhVienChuc GetObjectById(int idnganhvienchuc)
        {
            return _db.NganhVienChucs.Where(x => x.idNganhVienChuc == idnganhvienchuc).FirstOrDefault();
        }

        public void DeleteById(int id)
        {
            _db.NganhVienChucs.Remove(_db.NganhVienChucs.Where(x => x.idNganhVienChuc == id).FirstOrDefault());
        }
    }
}
