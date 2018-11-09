using Model.Entities;
using Model.Helper;
using Model.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class ChungChiVienChucRepository : Repository<ChungChiVienChuc>
    {
        public ChungChiVienChucRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public List<string> GetListTenChungChi()
        {
            return _db.ChungChiVienChucs.Where(x => x.tenChungChi != null).Select(x => x.tenChungChi).Distinct().ToList();
        }

        public List<string> GetListCapDoChungChi()
        {
            return _db.ChungChiVienChucs.Where(x => x.capDoChungChi != null).Select(x => x.capDoChungChi).Distinct().ToList();
        }

        public List<ChungChiForView> GetListChungChiVienChuc(string mavienchuc)
        {
            int idvienchuc = _db.VienChucs.Where(x => x.maVienChuc == mavienchuc).Select(y => y.idVienChuc).FirstOrDefault();
            List<ChungChiVienChuc> listChungChiVienChuc = _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc).ToList();
            List<ChungChiForView> listChungChiForView = new List<ChungChiForView>();
            for (int i = listChungChiVienChuc.Count - 1; i >= 0; i--)
            {
                listChungChiForView.Add(new ChungChiForView
                {
                    Id = listChungChiVienChuc[i].idChungChiVienChuc,
                    IdLoaiChungChi = listChungChiVienChuc[i].idLoaiChungChi,
                    LoaiChungChi = listChungChiVienChuc[i].LoaiChungChi.tenLoaiChungChi,
                    TenChungChi = listChungChiVienChuc[i].tenChungChi,
                    CapDo = listChungChiVienChuc[i].capDoChungChi,
                    NgayCapChungChi = listChungChiVienChuc[i].ngayCapChungChi,
                    CoSoDaoTao = listChungChiVienChuc[i].coSoDaoTao,
                    LinkVanBanDinhKem = listChungChiVienChuc[i].linkVanBanDinhKem,
                    GhiChu = listChungChiVienChuc[i].ghiChu
                });
            }
            return listChungChiForView;
        }

        public void DeleteById(int id)
        {
            _db.ChungChiVienChucs.Remove(_db.ChungChiVienChucs.Where(x => x.idChungChiVienChuc == id).FirstOrDefault());
        }

        public ChungChiVienChuc GetObjectById(int idchungchivienchuc)
        {
            return _db.ChungChiVienChucs.Where(x => x.idChungChiVienChuc == idchungchivienchuc).FirstOrDefault();
        }

        public ChungChiVienChuc GetObjectByIdVienChuc(int idVienChuc)
        {
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idVienChuc).OrderByDescending(y => y.idChungChiVienChuc).FirstOrDefault();
        }

        public List<ChungChiVienChuc> GetListChungChiByIdVienChucAndDurationForExportFull(int idvienchuc, DateTime? dtFromDuration, DateTime? dtToDuration)
        {
            List<ChungChiVienChuc> listChungChiVienChuc = _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.ngayCapChungChi >= dtFromDuration && x.ngayCapChungChi <= dtToDuration).ToList();
            if (listChungChiVienChuc.Count > 0)
                return listChungChiVienChuc;
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.ngayCapChungChi == null).ToList();
        }

        public List<ChungChiVienChuc> GetListChungChiByIdVienChucAndTimelineForExportFull(int idvienchuc, DateTime? dtTimeline)
        {
            List<ChungChiVienChuc> listChungChiVienChuc = _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.ngayCapChungChi <= dtTimeline).ToList();
            if(listChungChiVienChuc.Count > 0)
                return listChungChiVienChuc;
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.ngayCapChungChi == null).ToList();
        }

        public ChungChiVienChuc GetChungChiNgoaiNguByIdVienChucAndDuration(int idvienchuc, DateTime? dtFromDuration, DateTime? dtToDuration)
        {
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.LoaiChungChi.tenLoaiChungChi.Contains("Ngoại ngữ") && x.ngayCapChungChi >= dtFromDuration && x.ngayCapChungChi <= dtToDuration).OrderByDescending(d => d.ngayCapChungChi).FirstOrDefault();
        }

        public ChungChiVienChuc GetChungChiTinHocByIdVienChucAndDuration(int idvienchuc, DateTime? dtFromDuration, DateTime? dtToDuration)
        {
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.LoaiChungChi.tenLoaiChungChi.Contains("Tin học") && x.ngayCapChungChi >= dtFromDuration && x.ngayCapChungChi <= dtToDuration).OrderByDescending(d => d.ngayCapChungChi).FirstOrDefault();
        }

        public ChungChiVienChuc GetChungChiNghiepVuSuPhamByIdVienChucAndDuration(int idvienchuc, DateTime? dtFromDuration, DateTime? dtToDuration)
        {
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.capDoChungChi.Contains("Nghiệp vụ sư phạm") && x.ngayCapChungChi >= dtFromDuration && x.ngayCapChungChi <= dtToDuration).OrderByDescending(d => d.ngayCapChungChi).FirstOrDefault();
        }

        public ChungChiVienChuc GetChungChiChinhTriByIdVienChucAndDuration(int idvienchuc, DateTime? dtFromDuration, DateTime? dtToDuration)
        {
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.LoaiChungChi.tenLoaiChungChi.Contains("Chính trị") && x.ngayCapChungChi >= dtFromDuration && x.ngayCapChungChi <= dtToDuration).OrderByDescending(d => d.ngayCapChungChi).FirstOrDefault();
        }

        public ChungChiVienChuc GetChungChiQuanLyNhaNuocByIdVienChucAndDuration(int idvienchuc, DateTime? dtFromDuration, DateTime? dtToDuration)
        {
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.LoaiChungChi.tenLoaiChungChi.Contains("Quản lý nhà nước") && x.ngayCapChungChi >= dtFromDuration && x.ngayCapChungChi <= dtToDuration).OrderByDescending(d => d.ngayCapChungChi).FirstOrDefault();
        }

        public ChungChiVienChuc GetChungChiNgoaiNguByIdVienChucAndTimeline(int idvienchuc, DateTime? dtTimeline)
        {
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.LoaiChungChi.tenLoaiChungChi.Contains("Ngoại ngữ") && x.ngayCapChungChi <= dtTimeline).OrderByDescending(d => d.ngayCapChungChi).FirstOrDefault();
        }

        public ChungChiVienChuc GetChungChiTinHocByIdVienChucAndTimeline(int idvienchuc, DateTime? dtTimeline)
        {
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.LoaiChungChi.tenLoaiChungChi.Contains("Tin học") && x.ngayCapChungChi <= dtTimeline).OrderByDescending(d => d.ngayCapChungChi).FirstOrDefault();
        }

        public ChungChiVienChuc GetChungChiNghiepVuSuPhamByIdVienChucAndTimeline(int idvienchuc, DateTime? dtTimeline)
        {
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.capDoChungChi.Contains("Nghiệp vụ sư phạm") && x.ngayCapChungChi <= dtTimeline).OrderByDescending(d => d.ngayCapChungChi).FirstOrDefault();
        }

        public ChungChiVienChuc GetChungChiChinhTriByIdVienChucAndTimeline(int idvienchuc, DateTime? dtTimeline)
        {
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.LoaiChungChi.tenLoaiChungChi.Contains("Chính trị") && x.ngayCapChungChi <= dtTimeline).OrderByDescending(d => d.ngayCapChungChi).FirstOrDefault();
        }

        public ChungChiVienChuc GetChungChiQuanLyNhaNuocByIdVienChucAndTimeline(int idvienchuc, DateTime? dtTimeline)
        {
            return _db.ChungChiVienChucs.Where(x => x.idVienChuc == idvienchuc && x.LoaiChungChi.tenLoaiChungChi.Contains("Quản lý nhà nước") && x.ngayCapChungChi <= dtTimeline).OrderByDescending(d => d.ngayCapChungChi).FirstOrDefault();
        }

        public List<string> GetListLinkVanBanDinhKem(string maVienChucForGetListLinkVanBanDinhKemCC)
        {
            return _db.ChungChiVienChucs.Where(x => x.VienChuc.maVienChuc == maVienChucForGetListLinkVanBanDinhKemCC).Select(y => y.linkVanBanDinhKem).ToList();
        }

        public ChungChiVienChuc GetFirstRow()
        {
            return _db.ChungChiVienChucs.Where(x => x.idChungChiVienChuc == 2065).FirstOrDefault();
        }

        
    }
}
