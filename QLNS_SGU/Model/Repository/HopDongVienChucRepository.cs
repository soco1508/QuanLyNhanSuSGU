using Model.Entities;
using Model.ObjectModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class HopDongVienChucRepository : Repository<HopDongVienChuc>
    {
        public HopDongVienChucRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public List<HopDongVienChuc> GetListNull()
        {
            return _db.HopDongVienChucs.Where(x => x.ngayBatDau == null).ToList();
        }

        public List<HopDongForView> GetListHopDongVienChuc(string mavienchuc)
        {
            int idvienchuc = _db.VienChucs.Where(x => x.maVienChuc == mavienchuc).Select(y => y.idVienChuc).FirstOrDefault();
            List<HopDongVienChuc> listHopDongVienChuc = _db.HopDongVienChucs.Where(x => x.idVienChuc == idvienchuc).ToList();
            List<HopDongForView> listHopDong = new List<HopDongForView>();
            for(int i = 0; i < listHopDongVienChuc.Count; i++)
            {
                listHopDong.Add(new HopDongForView
                {
                    Id = listHopDongVienChuc[i].idHopDongVienChuc,
                    LoaiHopDong = listHopDongVienChuc[i].LoaiHopDong.tenLoaiHopDong,
                    NgayBatDau = listHopDongVienChuc[i].ngayBatDau,
                    NgayKetThuc = listHopDongVienChuc[i].ngayKetThuc,
                    LinkVanBanDinhKem = listHopDongVienChuc[i].linkVanBanDinhKem,
                    GhiChu = listHopDongVienChuc[i].moTa
                });
            }
            return listHopDong;
        }

        public string GetLoaiHopDongVienChucForLbHopDong(string mavienchuc)
        {
            int idvienchuc = _db.VienChucs.Where(x => x.maVienChuc == mavienchuc).Select(y => y.idVienChuc).FirstOrDefault();
            return _db.HopDongVienChucs.Where(x => x.idVienChuc == idvienchuc)
                                                     .OrderByDescending(t => t.ngayBatDau)
                                                     .Select(y => y.LoaiHopDong.tenLoaiHopDong)
                                                     .FirstOrDefault();
        }

        public object ReturnNullIfDateTimeNullToView(string datetime)
        {
            if (datetime != string.Empty)
                return DateTime.ParseExact(datetime, "dd/MM/yyyy", null);
            return null;
        }

        public DateTime? ReturnDateTimeToDatabase(string datetime)
        {
            if (datetime == string.Empty)
            {
                return null;
            }
            else
            {
                return DateTime.ParseExact(datetime, "dd/MM/yyyy", null);
            }
        }

        public HopDongVienChuc GetObjectById(int idhopdongvienchuc)
        {
            return _db.HopDongVienChucs.Where(x => x.idHopDongVienChuc == idhopdongvienchuc).FirstOrDefault();
        }

        public void DeleteById(int id)
        {
            var a = _db.HopDongVienChucs.Where(x => x.idHopDongVienChuc == id).FirstOrDefault();
            _db.HopDongVienChucs.Remove(a);
        }

        public HopDongVienChuc GetListHopDongByIdVienChucAndTimelineForExportOne(int idVienChuc, DateTime dtTimeline)
        {
            var rows = _db.HopDongVienChucs.Where(x => x.idVienChuc == idVienChuc);
            List<HopDongVienChuc> listHopDongVienChuc = new List<HopDongVienChuc>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau <= dtTimeline && row.ngayKetThuc >= dtTimeline)
                    {
                        listHopDongVienChuc.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau <= dtTimeline)
                    {
                        listHopDongVienChuc.Add(row);
                    }
                }
            }
            if (listHopDongVienChuc.Count > 0)
                return listHopDongVienChuc.OrderByDescending(x => x.ngayBatDau).FirstOrDefault();
            return null;
        }

        public HopDongVienChuc GetListHopDongByIdVienChucAndDurationForExportOne(int idVienChuc, DateTime dtFromDuration, DateTime dtToDuration)
        {
            var rows = _db.HopDongVienChucs.Where(x => x.idVienChuc == idVienChuc);
            List<HopDongVienChuc> listHopDongVienChuc = new List<HopDongVienChuc>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau >= dtFromDuration && row.ngayKetThuc <= dtToDuration)
                    {
                        listHopDongVienChuc.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau >= dtFromDuration)
                    {
                        listHopDongVienChuc.Add(row);
                    }
                }
            }
            if (listHopDongVienChuc.Count > 0)
                return listHopDongVienChuc.OrderByDescending(x => x.ngayBatDau).FirstOrDefault();
            return null;
        }

        public List<string> GetListLinkVanBanDinhKem(string maVienChucForGetListLinkVanBanDinhKemHD)
        {
            return _db.HopDongVienChucs.Where(x => x.VienChuc.maVienChuc == maVienChucForGetListLinkVanBanDinhKemHD).Select(y => y.linkVanBanDinhKem).ToList();
        }

        public List<HopDongVienChuc> GetListHopDongByIdVienChucAndDurationForExportFull(int idVienChuc, DateTime dtFromDuration, DateTime dtToDuration)
        {
            var rows = _db.HopDongVienChucs.Where(x => x.idVienChuc == idVienChuc);
            List<HopDongVienChuc> listHopDongVienChuc = new List<HopDongVienChuc>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau >= dtFromDuration && row.ngayKetThuc <= dtToDuration)
                    {
                        listHopDongVienChuc.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau >= dtFromDuration)
                    {
                        listHopDongVienChuc.Add(row);
                    }
                }
            }
            return listHopDongVienChuc;
        }

        public List<HopDongVienChuc> GetListHopDongByIdVienChucAndTimelineForExportFull(int idVienChuc, DateTime dtTimeline)
        {
            var rows = _db.HopDongVienChucs.Where(x => x.idVienChuc == idVienChuc);
            List<HopDongVienChuc> listHopDongVienChuc = new List<HopDongVienChuc>();
            foreach (var row in rows)
            {
                if (row.ngayKetThuc != null)
                {
                    if (row.ngayBatDau <= dtTimeline && row.ngayKetThuc >= dtTimeline)
                    {
                        listHopDongVienChuc.Add(row);
                    }
                }
                else
                {
                    if (row.ngayBatDau <= dtTimeline)
                    {
                        listHopDongVienChuc.Add(row);
                    }
                }
            }
            return listHopDongVienChuc;
        }
    }
}
