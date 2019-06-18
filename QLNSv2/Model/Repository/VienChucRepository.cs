using Model.Entities;
using Model.ObjectModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class VienChucRepository : Repository<VienChuc>
    {
        public VienChucRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        //public List<VienChucForGrid> GetListVienChucForGrid()
        //{
        //    var listVienChuc = _db.VienChucs.ToList();
        //    var listVienChucForGrid = new List<VienChucForGrid>();
        //    for(int i = 0; i < listVienChuc.Count; i++)
        //    {
        //        listVienChucForGrid.Add(new VienChucForGrid
        //        {
        //            idVienChuc = listVienChuc[i].idVienChuc,
        //            maVienChuc = listVienChuc[i].maVienChuc,
        //            ho = listVienChuc[i].ho,
        //            ten = listVienChuc[i].ten,
        //            sDT = listVienChuc[i].sDT,
        //            gioiTinh = ReturnGenderToGrid(listVienChuc[i].gioiTinh),
        //            anh = listVienChuc[i].anh,
        //            chinhTri = listVienChuc[i].ChinhTri.tenChinhTri,
        //            danToc = listVienChuc[i].DanToc.tenDanToc,
        //            ghiChu = listVienChuc[i].ghiChu,
        //            hoKhauThuongTru = listVienChuc[i].hoKhauThuongTru,
        //            laDangVien = listVienChuc[i].laDangVien,
        //            ngaySinh = listVienChuc[i].ngaySinh,
        //            ngayThamGiaCongTac = listVienChuc[i].ngayThamGiaCongTac,
        //            ngayVaoDang = listVienChuc[i].ngayVaoDang,
        //            ngayVaoNganh = listVienChuc[i].ngayVaoNganh,
        //            ngayVeTruong = listVienChuc[i].ngayVeTruong,
        //            noiOHienNay = listVienChuc[i].noiOHienNay,
        //            noiSinh = listVienChuc[i].noiSinh,
        //            quanLyNhaNuoc = listVienChuc[i].QuanLyNhaNuoc.tenQuanLyNhaNuoc,
        //            queQuan = listVienChuc[i].queQuan,
        //            tonGiao = listVienChuc[i].TonGiao.tenTonGiao,
        //            vanHoa = listVienChuc[i].vanHoa
        //        });
        //    }
        //    return listVienChucForGrid;
        //}

        public bool ReturnGenderToDatabase(int index)
        {
            if(index == 0)
            {
                return true;
            }
            return false;
        }

        public int ReturnGenderToTabThongTinCaNhan(bool? gioitinh)
        {
            if(gioitinh == true)
            {
                return 0;
            }
            return 1;
        }

        public string ReturnGenderToGrid(bool? gioitinh)
        {
            if (gioitinh == true)
            {
                return "Nam";
            }
            return "Nữ";
        }

        public DateTime? CheckDateTime(DateTime? datetime)
        {
            if(datetime != null)
            {
                return datetime;
            }
            return null;
        }

        private string SplitString(string s)
        {
            string[] words = s.Split('.');
            return words[1] + "/" + words[0] + "/" + words[2];
        }

        public List<VienChuc> GetListVienChuc()
        {
            return _db.VienChucs.ToList();
        }       

        public void DeleteById(int idvienchuc)
        {
            var a = _db.VienChucs.Where(x => x.idVienChuc == idvienchuc).FirstOrDefault();
            _db.VienChucs.Remove(a);
        }

        public bool ReturnBoolDangVien(string s)
        {
            if (s != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetIdVienChuc(string mavienchuc)
        {
            return _db.VienChucs.Where(x => x.maVienChuc == mavienchuc).Select(y => y.idVienChuc).FirstOrDefault();
        }

        public List<string> GetListMaVienChuc()
        {
            var list = from a in _db.VienChucs
                       select a.maVienChuc;
            return list.ToList();
        }

        //public VienChuc GetVienChucByMaVienChuc(string mavienchuc)
        //{
        //    return _db.VienChucs.Where(x => x.maVienChuc == mavienchuc).FirstOrDefault();
        //}

        public bool? ReturnGenderToDatabaseFromImport(string gioitinh)
        {
            if(gioitinh == "Nam")
            {
                return true;
            }
            return false;
        }

        public VienChuc GetVienChucByIdVienChuc(int idVienChuc)
        {
            return _db.VienChucs.Where(x => x.idVienChuc == idVienChuc).FirstOrDefault();
        }

        public List<ExportObjects> GetListFieldsDefaultByDuration(DateTime dtFromDuration, DateTime dtToDuration)
        {
            List<ExportObjects> listExportObjects = new List<ExportObjects>();
            ChucVuDonViVienChucRepository chucVuDonViVienChucRepository = new ChucVuDonViVienChucRepository(_db);
            TrangThaiVienChucRepository trangThaiVienChucRepository = new TrangThaiVienChucRepository(_db);
            var listVienChuc = from v in _db.VienChucs
                               select new { v.idVienChuc, v.maVienChuc, v.ho, v.ten, v.gioiTinh };
            int count = 0;

            foreach (var item in listVienChuc.ToList())
            {
                List<ChucVuDonViVienChuc> listChucVuDonViVienChuc = chucVuDonViVienChucRepository.GetListCongTacByIdVienChuc(item.idVienChuc);
                if(listChucVuDonViVienChuc.Count > 0)
                {
                    ChucVuDonViVienChuc chucVuDonViVienChuc = chucVuDonViVienChucRepository.GetCongTacByIdVienChucAndDuration(item.idVienChuc, dtFromDuration, dtToDuration);
                    TrangThaiVienChuc trangThaiVienChuc = trangThaiVienChucRepository.GetTrangThaiByIdVienChucAndDuration(item.idVienChuc, dtFromDuration, dtToDuration);
                    string trangthai = string.Empty;
                    string donvi = string.Empty;
                    if (trangThaiVienChuc != null)
                        trangthai = trangThaiVienChuc.TrangThai.tenTrangThai;
                    if (chucVuDonViVienChuc != null)
                        donvi = chucVuDonViVienChuc.DonVi.tenDonVi;
                    listExportObjects.Add(new ExportObjects
                    {
                        IdVienChuc = item.idVienChuc,
                        MaVienChuc = item.maVienChuc,
                        Ho = item.ho,
                        Ten = item.ten,
                        GioiTinh = ReturnGenderToGrid(item.gioiTinh),
                        Index = count,
                        DonVi = donvi,
                        TrangThai = trangthai
                    });
                    count++;
                }
            }
            //foreach (var item in listExportObjects)
            //{
            //    ChucVuDonViVienChuc chucVuDonViVienChuc = chucVuDonViVienChucRepository.GetCongTacByIdVienChucAndDuration(item.IdVienChuc, dtFromDuration, dtToDuration);
            //    string donvi = string.Empty;
            //    if (chucVuDonViVienChuc != null)
            //        donvi = chucVuDonViVienChuc.DonVi.tenDonVi;
            //    TrangThaiVienChuc trangThaiVienChuc = trangThaiVienChucRepository.GetTrangThaiByIdVienChucAndDuration(item.IdVienChuc, dtFromDuration, dtToDuration);
            //    string trangthai = string.Empty;
            //    if (trangThaiVienChuc != null)
            //        trangthai = trangThaiVienChuc.TrangThai.tenTrangThai;
            //    var exportObjects = listExportObjects.Where(x => x.IdVienChuc == item.IdVienChuc).FirstOrDefault();
            //    exportObjects.DonVi = donvi;
            //    exportObjects.TrangThai = trangthai;
            //}
            return listExportObjects;
        }

        public List<ExportObjects> GetListFieldsDefaultByTimeline(DateTime dtTimeline)
        {
            List<ExportObjects> listExportObjects = new List<ExportObjects>();
            ChucVuDonViVienChucRepository chucVuDonViVienChucRepository = new ChucVuDonViVienChucRepository(_db);
            TrangThaiVienChucRepository trangThaiVienChucRepository = new TrangThaiVienChucRepository(_db);
            var listVienChuc = from v in _db.VienChucs
                               select new { v.idVienChuc, v.maVienChuc, v.ho, v.ten, v.gioiTinh };
            int count = 0;
            foreach (var item in listVienChuc.ToList())
            {
                List<ChucVuDonViVienChuc> listChucVuDonViVienChuc = chucVuDonViVienChucRepository.GetListCongTacByIdVienChuc(item.idVienChuc);
                if(listChucVuDonViVienChuc.Count > 0)
                {
                    ChucVuDonViVienChuc chucVuDonViVienChuc = chucVuDonViVienChucRepository.GetCongTacByIdVienChucAndTimeline(item.idVienChuc, dtTimeline);
                    TrangThaiVienChuc trangThaiVienChuc = trangThaiVienChucRepository.GetTrangThaiByIdVienChucAndTimeline(item.idVienChuc, dtTimeline);
                    string trangthai = string.Empty;
                    string donvi = string.Empty;
                    if (trangThaiVienChuc != null)
                        trangthai = trangThaiVienChuc.TrangThai.tenTrangThai;
                    if (chucVuDonViVienChuc != null)
                        donvi = chucVuDonViVienChuc.DonVi.tenDonVi;
                    listExportObjects.Add(new ExportObjects
                    {
                        IdVienChuc = item.idVienChuc,
                        MaVienChuc = item.maVienChuc,
                        Ho = item.ho,
                        Ten = item.ten,
                        GioiTinh = ReturnGenderToGrid(item.gioiTinh),
                        Index = count,
                        DonVi = donvi,
                        TrangThai = trangthai
                    });
                    count++;
                }                
            }
            //foreach (var item in listExportObjects)
            //{
            //    ChucVuDonViVienChuc chucVuDonViVienChuc = chucVuDonViVienChucRepository.GetCongTacByIdVienChucAndTimeline(item.IdVienChuc, dtTimeline);
            //    string donvi = string.Empty;
            //    if (chucVuDonViVienChuc != null)
            //        donvi = chucVuDonViVienChuc.DonVi.tenDonVi;
            //    TrangThaiVienChuc trangThaiVienChuc = trangThaiVienChucRepository.GetTrangThaiByIdVienChucAndTimeline(item.IdVienChuc, dtTimeline);
            //    string trangthai = string.Empty;
            //    if (trangThaiVienChuc != null)
            //        trangthai = trangThaiVienChuc.TrangThai.tenTrangThai;
            //    var exportObjects = listExportObjects.Where(x => x.IdVienChuc == item.IdVienChuc).FirstOrDefault();
            //    exportObjects.DonVi = donvi;
            //    exportObjects.TrangThai = trangthai;
            //}
            return listExportObjects;
        }

        //public bool CheckExist(string mavienchuc)
        //{
        //    Dictionary<string, string> listMaVienChuc = _db.VienChucs.Select(y => y.maVienChuc).ToDictionary(x => x, x => x);
        //    if (listMaVienChuc.ContainsKey(mavienchuc))
        //        return true;
        //    return false;
        //}

        public string ReturnLaDangVienToGrid(bool? laDangVien)
        {
            if (laDangVien == true)
                return "x";
            return "";
        }
    }
}
 