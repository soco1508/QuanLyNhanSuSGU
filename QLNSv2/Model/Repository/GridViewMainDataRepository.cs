using Model.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;
using System.Data.Entity;

namespace Model.Repository
{
    public class GridViewMainDataRepository : Repository<GridViewMainData>
    {
        public GridViewMainDataRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public List<GridViewMainData> LoadDataToMainGrid()
        {
            //IEnumerable<VienChuc> listVienChuc = _db.VienChucs;
            //foreach (var item in listVienChuc)
            //{
            //    List<ChucVuDonViVienChuc> listChucVuDonViVienChucToAdd = new List<ChucVuDonViVienChuc>();
            //    ChucVuDonViVienChuc[] arrChucVuDonViVienChuc = _db.ChucVuDonViVienChucs.Where(x => x.idVienChuc == item.idVienChuc).ToArray();
            //    if (arrChucVuDonViVienChuc.Length == 0) // chua co qua trinh cong tac
            //    {
            //        yield return new GridViewMainData
            //        {
            //            MaVienChuc = item.maVienChuc,
            //            Ho = item.ho,
            //            Ten = item.ten,
            //            GioiTinh = CheckGioiTinh(item.gioiTinh),
            //            NgaySinh = item.ngaySinh,
            //            ChucVu = string.Empty,
            //            DonVi = string.Empty,
            //            TrinhDo = GetMaxLoaiHocHamHocVi(item.idVienChuc),
            //            HeSo = 0
            //        };
            //    }
            //    if (arrChucVuDonViVienChuc.Length == 1)
            //    {
            //        yield return new GridViewMainData
            //        {
            //            MaVienChuc = item.maVienChuc,
            //            Ho = item.ho,
            //            Ten = item.ten,
            //            GioiTinh = CheckGioiTinh(item.gioiTinh),
            //            NgaySinh = item.ngaySinh,
            //            ChucVu = arrChucVuDonViVienChuc[0].ChucVu.tenChucVu,
            //            DonVi = arrChucVuDonViVienChuc[0].DonVi.tenDonVi,
            //            TrinhDo = GetMaxLoaiHocHamHocVi(item.idVienChuc),
            //            HeSo = arrChucVuDonViVienChuc[0].ChucVu.heSoChucVu
            //        };
            //    }
            //    if (arrChucVuDonViVienChuc.Length > 1)
            //    {
            //        for (int i = 0; i < arrChucVuDonViVienChuc.Length; i++)
            //        {
            //            if (arrChucVuDonViVienChuc[i].ngayKetThuc != null)
            //            {
            //                if (arrChucVuDonViVienChuc[i].ngayBatDau <= DateTime.Now && arrChucVuDonViVienChuc[i].ngayKetThuc >= DateTime.Now)
            //                {
            //                    listChucVuDonViVienChucToAdd.Add(arrChucVuDonViVienChuc[i]);
            //                }
            //            }
            //            else
            //            {
            //                if (arrChucVuDonViVienChuc[i].ngayBatDau <= DateTime.Now)
            //                {
            //                    listChucVuDonViVienChucToAdd.Add(arrChucVuDonViVienChuc[i]);
            //                }
            //            }
            //        }
            //        if (listChucVuDonViVienChucToAdd.Count > 0)
            //        {
            //            var chucVuDonViVienChuc = listChucVuDonViVienChucToAdd.OrderByDescending(x => x.ChucVu.heSoChucVu).FirstOrDefault();
            //            yield return new GridViewMainData
            //            {
            //                MaVienChuc = item.maVienChuc,
            //                Ho = item.ho,
            //                Ten = item.ten,
            //                GioiTinh = CheckGioiTinh(item.gioiTinh),
            //                NgaySinh = item.ngaySinh,
            //                ChucVu = chucVuDonViVienChuc.ChucVu.tenChucVu,
            //                DonVi = chucVuDonViVienChuc.DonVi.tenDonVi,
            //                TrinhDo = GetMaxLoaiHocHamHocVi(item.idVienChuc),
            //                HeSo = chucVuDonViVienChuc.ChucVu.heSoChucVu
            //            };
            //        }
            //        else
            //        {
            //            var chucVuDonViVienChuc = arrChucVuDonViVienChuc.OrderByDescending(x => x.ChucVu.heSoChucVu).Where(x => x.idDonVi != 1).FirstOrDefault();
            //            yield return new GridViewMainData
            //            {
            //                MaVienChuc = item.maVienChuc,
            //                Ho = item.ho,
            //                Ten = item.ten,
            //                GioiTinh = CheckGioiTinh(item.gioiTinh),
            //                NgaySinh = item.ngaySinh,
            //                ChucVu = chucVuDonViVienChuc.ChucVu.tenChucVu,
            //                DonVi = chucVuDonViVienChuc.DonVi.tenDonVi,
            //                TrinhDo = GetMaxLoaiHocHamHocVi(item.idVienChuc),
            //                HeSo = chucVuDonViVienChuc.ChucVu.heSoChucVu
            //            };
            //        }
            //    }
            //}
            List<GridViewMainData> listGridViewMainData = new List<GridViewMainData>();
            List<VienChuc> listVienChuc = _db.VienChucs.ToList();
            try
            {
                foreach (var item in listVienChuc)
                {
                    List<ChucVuDonViVienChuc> listChucVuDonViVienChucToAdd = new List<ChucVuDonViVienChuc>();
                    List<ChucVuDonViVienChuc> listChucVuDonViVienChuc = _db.ChucVuDonViVienChucs.Where(x => x.idVienChuc == item.idVienChuc).ToList();
                    if (listChucVuDonViVienChuc.Count == 0) // chua co qua trinh cong tac
                    {
                        try
                        {
                            listGridViewMainData.Add(new GridViewMainData
                            {
                                MaVienChuc = item.maVienChuc,
                                Ho = item.ho,
                                Ten = item.ten,
                                GioiTinh = CheckGioiTinh(item.gioiTinh),
                                NgaySinh = item.ngaySinh,
                                ChucVu = string.Empty,
                                DonVi = string.Empty,
                                TrinhDo = GetMaxLoaiHocHamHocVi(item.idVienChuc),
                                HeSo = 0
                            });
                        }
                        catch (Exception ex)
                        {
                            string s = ex.Message;
                        }
                    }
                    if (listChucVuDonViVienChuc.Count == 1)
                    {
                        try
                        {
                            listGridViewMainData.Add(new GridViewMainData
                            {
                                MaVienChuc = item.maVienChuc,
                                Ho = item.ho,
                                Ten = item.ten,
                                GioiTinh = CheckGioiTinh(item.gioiTinh),
                                NgaySinh = item.ngaySinh,
                                ChucVu = listChucVuDonViVienChuc[0].ChucVu != null ? listChucVuDonViVienChuc[0].ChucVu.tenChucVu : string.Empty,
                                DonVi = listChucVuDonViVienChuc[0].DonVi != null ? listChucVuDonViVienChuc[0].DonVi.tenDonVi : string.Empty,
                                TrinhDo = GetMaxLoaiHocHamHocVi(item.idVienChuc),
                                HeSo = listChucVuDonViVienChuc[0].ChucVu.heSoChucVu
                            });
                        }
                        catch (Exception ex)
                        {
                            string s = ex.Message;
                        }
                    }
                    if (listChucVuDonViVienChuc.Count > 1)
                    {
                        try
                        {
                            for (int i = 0; i < listChucVuDonViVienChuc.Count; i++)
                            {
                                if (listChucVuDonViVienChuc[i].ngayKetThuc != null)
                                {
                                    if (listChucVuDonViVienChuc[i].ngayBatDau <= DateTime.Now && listChucVuDonViVienChuc[i].ngayKetThuc >= DateTime.Now)
                                    {
                                        listChucVuDonViVienChucToAdd.Add(listChucVuDonViVienChuc[i]);
                                    }
                                }
                                else
                                {
                                    if (listChucVuDonViVienChuc[i].ngayBatDau <= DateTime.Now)
                                    {
                                        listChucVuDonViVienChucToAdd.Add(listChucVuDonViVienChuc[i]);
                                    }
                                }
                            }
                            if (listChucVuDonViVienChucToAdd.Count > 0)
                            {
                                var chucVuDonViVienChuc = listChucVuDonViVienChucToAdd.OrderByDescending(x => x.ChucVu.heSoChucVu).FirstOrDefault();
                                listGridViewMainData.Add(new GridViewMainData
                                {
                                    MaVienChuc = item.maVienChuc,
                                    Ho = item.ho,
                                    Ten = item.ten,
                                    GioiTinh = CheckGioiTinh(item.gioiTinh),
                                    NgaySinh = item.ngaySinh,
                                    ChucVu = chucVuDonViVienChuc.ChucVu != null ? chucVuDonViVienChuc.ChucVu.tenChucVu : string.Empty,
                                    DonVi = chucVuDonViVienChuc.DonVi != null ? chucVuDonViVienChuc.DonVi.tenDonVi : string.Empty,
                                    TrinhDo = GetMaxLoaiHocHamHocVi(item.idVienChuc),
                                    HeSo = chucVuDonViVienChuc.ChucVu.heSoChucVu
                                });
                            }
                            else
                            {
                                var chucVuDonViVienChuc = listChucVuDonViVienChuc.OrderByDescending(x => x.ChucVu.heSoChucVu).Where(x => x.idDonVi != 1).FirstOrDefault();
                                listGridViewMainData.Add(new GridViewMainData
                                {
                                    MaVienChuc = item.maVienChuc,
                                    Ho = item.ho,
                                    Ten = item.ten,
                                    GioiTinh = CheckGioiTinh(item.gioiTinh),
                                    NgaySinh = item.ngaySinh,
                                    ChucVu = chucVuDonViVienChuc.ChucVu != null ? chucVuDonViVienChuc.ChucVu.tenChucVu : string.Empty,
                                    DonVi = chucVuDonViVienChuc.DonVi != null ? chucVuDonViVienChuc.DonVi.tenDonVi : string.Empty,
                                    TrinhDo = GetMaxLoaiHocHamHocVi(item.idVienChuc),
                                    HeSo = chucVuDonViVienChuc.ChucVu.heSoChucVu
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            string s = ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return listGridViewMainData;
        }

        public string CheckGioiTinh(bool? t)
        {
            if (t == true)
                return "Nữ";
            return "Nam";
        }

        private string GetMaxLoaiHocHamHocVi(int idvienchuc)
        {
            var listHocHamHocVi = _db.HocHamHocViVienChucs.Where(x => x.idVienChuc == idvienchuc).ToList();
            if (listHocHamHocVi.Count > 0)
                return listHocHamHocVi.OrderByDescending(x => x.LoaiHocHamHocVi.phanCap).Select(y => y.LoaiHocHamHocVi.tenLoaiHocHamHocVi).FirstOrDefault();
            return string.Empty;
        }
    }
}
