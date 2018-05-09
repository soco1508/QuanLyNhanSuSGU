using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using Model;
using Model.Entities;
using Model.ObjectModels;
using QLNS_SGU.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS_SGU.Presenter
{
    public interface IExportDataOneDomainPresenter : IPresenter
    {
        void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e);
        void ExportExcel();
        void Cancel();
        void EnableFilterDatetime(object sender, EventArgs e);
        void ExportData();
    }
    public class ExportDataOneDomainPresenter : IExportDataOneDomainPresenter
    {
        private ExportDataOneDomainForm _view;
        public object UI => _view;
        public ExportDataOneDomainPresenter(ExportDataOneDomainForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVCustom.IndicatorWidth = 50;
            _view.RADDomain.SelectedIndex = 0;
            _view.GVCustom.OptionsView.AllowCellMerge = true;
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVCustom.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void Cancel()
        {
            _view.RADDomain.SelectedIndex = 0;
            _view.RADSelectTimeToFilter.SelectedIndex = 0;
            _view.DTTimeline.Enabled = true;
            _view.DTFromDuration.Enabled = false;
            _view.DTToDuration.Enabled = false;
            _view.DTTimeline.EditValue = null;
            _view.DTFromDuration.EditValue = null;
            _view.DTToDuration.EditValue = null;
            _view.DTTimeline.ErrorText = null;
            _view.DTFromDuration.ErrorText = null;
            _view.DTToDuration.ErrorText = null;
            _view.GCCustom.DataSource = null;
        }

        public void EnableFilterDatetime(object sender, EventArgs e)
        {
            if (_view.RADSelectTimeToFilter.SelectedIndex == 0)
            {
                _view.DTTimeline.Enabled = true;
                _view.DTFromDuration.Enabled = false;
                _view.DTToDuration.Enabled = false;
                _view.DTFromDuration.EditValue = null;
                _view.DTToDuration.EditValue = null;
            }
            else
            {
                _view.DTTimeline.Enabled = false;
                _view.DTFromDuration.Enabled = true;
                _view.DTToDuration.Enabled = true;
                _view.DTTimeline.EditValue = null;
            }
        }

        public void ExportData()
        {
            int selectedDomain = _view.RADDomain.SelectedIndex;
            if (_view.RADSelectTimeToFilter.SelectedIndex == 0) //moc thoi gian
            {
                if (_view.DTTimeline.Text != "")
                {                   
                    GetDataTimeline(selectedDomain);
                }
                else
                {
                    _view.DTTimeline.ErrorText = "Vui lòng chọn thời gian.";
                    _view.DTTimeline.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
            }
            else                                            //khoang thoi gian
            {
                if (_view.DTFromDuration.Text != "")
                {
                    GetDataDuration(selectedDomain);
                }
                else
                {
                    _view.DTFromDuration.ErrorText = "Vui lòng chọn thời gian bắt đầu.";
                    _view.DTFromDuration.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
            }
        }

        private void GetDataDuration(int selectedDomain)
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            DateTime dtFromDuration = _view.DTFromDuration.DateTime;
            DateTime dtToDuration = _view.DTToDuration.DateTime;
            List<ExportObjects> listFieldsDefault = unitOfWorks.VienChucRepository.GetListFieldsDefaultByDuration(dtFromDuration, dtToDuration).ToList();
            int tempIdVienChuc = -1;
            if (selectedDomain == 0) // thong tin ca nhan
            {
                foreach (var row in listFieldsDefault)
                {
                    VienChuc vienChuc = unitOfWorks.VienChucRepository.GetVienChucByIdVienChuc(row.IdVienChuc);
                    if (vienChuc != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.SoDienThoai = vienChuc.sDT;
                        exportObjects.NgaySinh = vienChuc.ngaySinh;
                        exportObjects.NoiSinh = vienChuc.noiSinh;
                        exportObjects.QueQuan = vienChuc.queQuan;
                        exportObjects.DanToc = vienChuc.DanToc.tenDanToc;
                        exportObjects.TonGiao = vienChuc.TonGiao.tenTonGiao;
                        exportObjects.HoKhauThuongTru = vienChuc.hoKhauThuongTru;
                        exportObjects.NoiOHienNay = vienChuc.noiOHienNay;
                        exportObjects.LaDangVien = unitOfWorks.VienChucRepository.ReturnLaDangVienToGrid(vienChuc.laDangVien);
                        exportObjects.NgayVaoDang = vienChuc.ngayVaoDang;
                        exportObjects.NgayThamGiaCongTac = vienChuc.ngayThamGiaCongTac;
                        exportObjects.NgayVaoNganh = vienChuc.ngayVaoNganh;
                        exportObjects.NgayVeTruong = vienChuc.ngayVeTruong;
                        exportObjects.VanHoa = vienChuc.vanHoa;
                        exportObjects.QuanLyNhaNuoc = vienChuc.QuanLyNhaNuoc.tenQuanLyNhaNuoc;
                        exportObjects.GhiChu = vienChuc.ghiChu;
                    }
                }
            }
            if (selectedDomain == 1) // trang thai
            {
                foreach (var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<TrangThaiVienChuc> listTrangThai = unitOfWorks.TrangThaiVienChucRepository.GetListTrangThaiByIdVienChucAndDuration(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if (listTrangThai.Count > 1)
                        {
                            exportObjects.MoTaTT = listTrangThai[0].moTa;
                            exportObjects.DiaDiemTT = listTrangThai[0].diaDiem;
                            exportObjects.NgayBatDauTT = listTrangThai[0].ngayBatDau;
                            exportObjects.NgayKetThucTT = listTrangThai[0].ngayKetThuc;
                            exportObjects.LinkVanBanDinhKemTT = listTrangThai[0].linkVanBanDinhKem;

                            IncreaseIndex(listFieldsDefault, row.Index, listTrangThai.Count - 1);
                            for (int i = 1; i < listTrangThai.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = row.DonVi,
                                    TrangThai = listTrangThai[i].TrangThai.tenTrangThai,
                                    MoTaTT = listTrangThai[i].moTa,
                                    DiaDiemTT = listTrangThai[i].diaDiem,
                                    NgayBatDauTT = listTrangThai[i].ngayBatDau,
                                    NgayKetThucTT = listTrangThai[i].ngayKetThuc,
                                    LinkVanBanDinhKemTT = listTrangThai[i].linkVanBanDinhKem
                                });
                            }
                        }
                        if (listTrangThai.Count == 1)
                        {
                            exportObjects.MoTaTT = listTrangThai[0].moTa;
                            exportObjects.DiaDiemTT = listTrangThai[0].diaDiem;
                            exportObjects.NgayBatDauTT = listTrangThai[0].ngayBatDau;
                            exportObjects.NgayKetThucTT = listTrangThai[0].ngayKetThuc;
                            exportObjects.LinkVanBanDinhKemTT = listTrangThai[0].linkVanBanDinhKem;
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 2) // cong tac
            {
                foreach (var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<ChucVuDonViVienChuc> listCongTac = unitOfWorks.ChucVuDonViVienChucRepository.GetListCongTacByIdVienChucAndDuration(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if (listCongTac.Count > 1)
                        {
                            exportObjects.LoaiChucVu = listCongTac[0].ChucVu.LoaiChucVu.tenLoaiChucVu;
                            exportObjects.ChucVu = listCongTac[0].ChucVu.tenChucVu;
                            exportObjects.HeSoChucVu = listCongTac[0].ChucVu.heSoChucVu;
                            exportObjects.LoaiDonVi = listCongTac[0].DonVi.LoaiDonVi.tenLoaiDonVi;
                            //exportObjects.DonVi = listCongTac[0].DonVi.tenDonVi;
                            exportObjects.DiaDiemCT = listCongTac[0].DonVi.diaDiem;
                            exportObjects.DiaChi = listCongTac[0].DonVi.diaChi;
                            exportObjects.SoDienThoaiDonVi = listCongTac[0].DonVi.sDT;
                            exportObjects.ToChuyenMon = listCongTac[0].ToChuyenMon.tenToChuyenMon;
                            exportObjects.PhanLoaiCongTac = listCongTac[0].phanLoaiCongTac;
                            exportObjects.CheckPhanLoaiCongTac = unitOfWorks.ChucVuDonViVienChucRepository.HardCheckPhanLoaiCongTacToGrid(listCongTac[0].checkPhanLoaiCongTac);
                            exportObjects.NgayBatDauCT = listCongTac[0].ngayBatDau;
                            exportObjects.NgayKetThucCT = listCongTac[0].ngayKetThuc;
                            exportObjects.LoaiThayDoi = unitOfWorks.ChucVuDonViVienChucRepository.HardLoaiThayDoiToGrid(listCongTac[0].loaiThayDoi);
                            exportObjects.KiemNhiem = unitOfWorks.ChucVuDonViVienChucRepository.HardKiemNhiemToGrid(listCongTac[0].kiemNhiem);
                            exportObjects.LinkVanBanDinhKemCT = listCongTac[0].linkVanBanDinhKem;

                            IncreaseIndex(listFieldsDefault, row.Index, listCongTac.Count - 1);
                            for (int i = 1; i < listCongTac.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = listCongTac[i].DonVi.tenDonVi,
                                    TrangThai = row.TrangThai,
                                    LoaiChucVu = listCongTac[i].ChucVu.LoaiChucVu.tenLoaiChucVu,
                                    ChucVu = listCongTac[i].ChucVu.tenChucVu,
                                    HeSoChucVu = listCongTac[i].ChucVu.heSoChucVu,
                                    LoaiDonVi = listCongTac[i].DonVi.LoaiDonVi.tenLoaiDonVi,
                                    DiaDiemCT = listCongTac[i].DonVi.diaDiem,
                                    DiaChi = listCongTac[i].DonVi.diaChi,
                                    SoDienThoaiDonVi = listCongTac[i].DonVi.sDT,
                                    ToChuyenMon = listCongTac[i].ToChuyenMon.tenToChuyenMon,
                                    PhanLoaiCongTac = listCongTac[i].phanLoaiCongTac,
                                    CheckPhanLoaiCongTac = unitOfWorks.ChucVuDonViVienChucRepository.HardCheckPhanLoaiCongTacToGrid(listCongTac[i].checkPhanLoaiCongTac),
                                    NgayBatDauCT = listCongTac[i].ngayBatDau,
                                    NgayKetThucCT = listCongTac[i].ngayKetThuc,
                                    LoaiThayDoi = unitOfWorks.ChucVuDonViVienChucRepository.HardLoaiThayDoiToGrid(listCongTac[i].loaiThayDoi),
                                    KiemNhiem = unitOfWorks.ChucVuDonViVienChucRepository.HardKiemNhiemToGrid(listCongTac[i].kiemNhiem),
                                    LinkVanBanDinhKemCT = listCongTac[i].linkVanBanDinhKem
                                });
                            }
                        }
                        if (listCongTac.Count == 1)
                        {
                            exportObjects.LoaiChucVu = listCongTac[0].ChucVu.LoaiChucVu.tenLoaiChucVu;
                            exportObjects.ChucVu = listCongTac[0].ChucVu.tenChucVu;
                            exportObjects.HeSoChucVu = listCongTac[0].ChucVu.heSoChucVu;
                            exportObjects.LoaiDonVi = listCongTac[0].DonVi.LoaiDonVi.tenLoaiDonVi;
                            //exportObjects.DonVi = listCongTac[0].DonVi.tenDonVi;
                            exportObjects.DiaDiemCT = listCongTac[0].DonVi.diaDiem;
                            exportObjects.DiaChi = listCongTac[0].DonVi.diaChi;
                            exportObjects.SoDienThoaiDonVi = listCongTac[0].DonVi.sDT;
                            exportObjects.ToChuyenMon = listCongTac[0].ToChuyenMon.tenToChuyenMon;
                            exportObjects.PhanLoaiCongTac = listCongTac[0].phanLoaiCongTac;
                            exportObjects.CheckPhanLoaiCongTac = unitOfWorks.ChucVuDonViVienChucRepository.HardCheckPhanLoaiCongTacToGrid(listCongTac[0].checkPhanLoaiCongTac);
                            exportObjects.NgayBatDauCT = listCongTac[0].ngayBatDau;
                            exportObjects.NgayKetThucCT = listCongTac[0].ngayKetThuc;
                            exportObjects.LoaiThayDoi = unitOfWorks.ChucVuDonViVienChucRepository.HardLoaiThayDoiToGrid(listCongTac[0].loaiThayDoi);
                            exportObjects.KiemNhiem = unitOfWorks.ChucVuDonViVienChucRepository.HardKiemNhiemToGrid(listCongTac[0].kiemNhiem);
                            exportObjects.LinkVanBanDinhKemCT = listCongTac[0].linkVanBanDinhKem;
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 3) // nganh hoc
            {
                foreach (var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<NganhVienChuc> listNganhHoc = unitOfWorks.NganhVienChucRepository.GetListNganhHocByIdVienChucAndDurationForExportFull(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if (listNganhHoc.Count > 1)
                        {
                            exportObjects.LoaiNganhNH = listNganhHoc[0].LoaiNganh.tenLoaiNganh;
                            exportObjects.NganhDaoTaoNH = listNganhHoc[0].NganhDaoTao.tenNganhDaoTao;
                            exportObjects.ChuyenNganhNH = listNganhHoc[0].ChuyenNganh.tenChuyenNganh;
                            exportObjects.LoaiHocHamHocViNH = listNganhHoc[0].HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi;
                            exportObjects.TenHocHamHocViNH = listNganhHoc[0].HocHamHocViVienChuc.tenHocHamHocVi;
                            exportObjects.CoSoDaoTaoNH = listNganhHoc[0].HocHamHocViVienChuc.coSoDaoTao;
                            exportObjects.NgonNguDaoTaoNH = listNganhHoc[0].HocHamHocViVienChuc.ngonNguDaoTao;
                            exportObjects.HinhThucDaoTaoNH = listNganhHoc[0].HocHamHocViVienChuc.hinhThucDaoTao;
                            exportObjects.NuocCapBangNH = listNganhHoc[0].HocHamHocViVienChuc.nuocCapBang;
                            exportObjects.NgayCapBangNH = listNganhHoc[0].HocHamHocViVienChuc.ngayCapBang;
                            exportObjects.LinkVanBanDinhKemHHHV_NH = listNganhHoc[0].HocHamHocViVienChuc.linkVanBanDinhKem;
                            exportObjects.PhanLoaiNH = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(listNganhHoc[0].phanLoai);
                            exportObjects.LinkVanBanDinhKemNH = listNganhHoc[0].linkVanBanDinhKem;

                            IncreaseIndex(listFieldsDefault, row.Index, listNganhHoc.Count - 1); // tru phan tu 0, con lai bn phan tu thi cong len bay nhieu
                            for (int i = 1; i < listNganhHoc.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = row.DonVi,
                                    TrangThai = row.TrangThai,
                                    LoaiNganhNH = listNganhHoc[i].LoaiNganh.tenLoaiNganh,
                                    NganhDaoTaoNH = listNganhHoc[i].NganhDaoTao.tenNganhDaoTao,
                                    ChuyenNganhNH = listNganhHoc[i].ChuyenNganh.tenChuyenNganh,
                                    LoaiHocHamHocViNH = listNganhHoc[i].HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi,
                                    TenHocHamHocViNH = listNganhHoc[i].HocHamHocViVienChuc.tenHocHamHocVi,
                                    CoSoDaoTaoNH = listNganhHoc[i].HocHamHocViVienChuc.coSoDaoTao,
                                    NgonNguDaoTaoNH = listNganhHoc[i].HocHamHocViVienChuc.ngonNguDaoTao,
                                    HinhThucDaoTaoNH = listNganhHoc[i].HocHamHocViVienChuc.hinhThucDaoTao,
                                    NuocCapBangNH = listNganhHoc[i].HocHamHocViVienChuc.nuocCapBang,
                                    NgayCapBangNH = listNganhHoc[i].HocHamHocViVienChuc.ngayCapBang,
                                    LinkVanBanDinhKemHHHV_NH = listNganhHoc[i].HocHamHocViVienChuc.linkVanBanDinhKem,
                                    PhanLoaiNH = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(listNganhHoc[i].phanLoai),
                                    LinkVanBanDinhKemNH = listNganhHoc[i].linkVanBanDinhKem
                                });
                            }
                        }
                        if (listNganhHoc.Count == 1)
                        {
                            exportObjects.LoaiNganhNH = listNganhHoc[0].LoaiNganh.tenLoaiNganh;
                            exportObjects.NganhDaoTaoNH = listNganhHoc[0].NganhDaoTao.tenNganhDaoTao;
                            exportObjects.ChuyenNganhNH = listNganhHoc[0].ChuyenNganh.tenChuyenNganh;
                            exportObjects.LoaiHocHamHocViNH = listNganhHoc[0].HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi;
                            exportObjects.TenHocHamHocViNH = listNganhHoc[0].HocHamHocViVienChuc.tenHocHamHocVi;
                            exportObjects.CoSoDaoTaoNH = listNganhHoc[0].HocHamHocViVienChuc.coSoDaoTao;
                            exportObjects.NgonNguDaoTaoNH = listNganhHoc[0].HocHamHocViVienChuc.ngonNguDaoTao;
                            exportObjects.HinhThucDaoTaoNH = listNganhHoc[0].HocHamHocViVienChuc.hinhThucDaoTao;
                            exportObjects.NuocCapBangNH = listNganhHoc[0].HocHamHocViVienChuc.nuocCapBang;
                            exportObjects.NgayCapBangNH = listNganhHoc[0].HocHamHocViVienChuc.ngayCapBang;
                            exportObjects.LinkVanBanDinhKemHHHV_NH = listNganhHoc[0].HocHamHocViVienChuc.linkVanBanDinhKem;
                            exportObjects.PhanLoaiNH = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(listNganhHoc[0].phanLoai);
                            exportObjects.LinkVanBanDinhKemNH = listNganhHoc[0].linkVanBanDinhKem;
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 4) // qua trinh luong
            {
                foreach (var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<QuaTrinhLuong> listQuaTrinhLuong = unitOfWorks.QuaTrinhLuongRepository.GetListQuaTrinhLuongByIdVienChucAndDurationForExportFull(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if (listQuaTrinhLuong.Count > 1)
                        {
                            exportObjects.MaNgach = listQuaTrinhLuong[0].Bac.Ngach.maNgach;
                            exportObjects.TenNgach = listQuaTrinhLuong[0].Bac.Ngach.tenNgach;
                            exportObjects.HeSoVuotKhungBaNamDau = listQuaTrinhLuong[0].Bac.Ngach.heSoVuotKhungBaNamDau;
                            exportObjects.HeSoVuotKhungTrenBaNam = listQuaTrinhLuong[0].Bac.Ngach.heSoVuotKhungTrenBaNam;
                            exportObjects.ThoiHanNangBac = listQuaTrinhLuong[0].Bac.Ngach.thoiHanNangBac;
                            exportObjects.Bac = listQuaTrinhLuong[0].Bac.bac1;
                            exportObjects.HeSoBac = listQuaTrinhLuong[0].Bac.heSoBac;
                            exportObjects.NgayBatDauQTL = listQuaTrinhLuong[0].ngayBatDau;
                            exportObjects.NgayLenLuong = listQuaTrinhLuong[0].ngayLenLuong;
                            exportObjects.LinkVanBanDinhKemQTL = listQuaTrinhLuong[0].linkVanBanDinhKem;

                            IncreaseIndex(listFieldsDefault, row.Index, listQuaTrinhLuong.Count - 1);
                            for (int i = 1; i < listQuaTrinhLuong.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = row.DonVi,
                                    TrangThai = row.TrangThai,
                                    MaNgach = listQuaTrinhLuong[i].Bac.Ngach.maNgach,
                                    TenNgach = listQuaTrinhLuong[i].Bac.Ngach.tenNgach,
                                    HeSoVuotKhungBaNamDau = listQuaTrinhLuong[i].Bac.Ngach.heSoVuotKhungBaNamDau,
                                    HeSoVuotKhungTrenBaNam = listQuaTrinhLuong[i].Bac.Ngach.heSoVuotKhungTrenBaNam,
                                    ThoiHanNangBac = listQuaTrinhLuong[i].Bac.Ngach.thoiHanNangBac,
                                    Bac = listQuaTrinhLuong[i].Bac.bac1,
                                    HeSoBac = listQuaTrinhLuong[i].Bac.heSoBac,
                                    NgayBatDauQTL = listQuaTrinhLuong[i].ngayBatDau,
                                    NgayLenLuong = listQuaTrinhLuong[i].ngayLenLuong,
                                    LinkVanBanDinhKemQTL = listQuaTrinhLuong[i].linkVanBanDinhKem
                                });
                            }
                        }
                        if (listQuaTrinhLuong.Count == 1)
                        {
                            exportObjects.MaNgach = listQuaTrinhLuong[0].Bac.Ngach.maNgach;
                            exportObjects.TenNgach = listQuaTrinhLuong[0].Bac.Ngach.tenNgach;
                            exportObjects.HeSoVuotKhungBaNamDau = listQuaTrinhLuong[0].Bac.Ngach.heSoVuotKhungBaNamDau;
                            exportObjects.HeSoVuotKhungTrenBaNam = listQuaTrinhLuong[0].Bac.Ngach.heSoVuotKhungTrenBaNam;
                            exportObjects.ThoiHanNangBac = listQuaTrinhLuong[0].Bac.Ngach.thoiHanNangBac;
                            exportObjects.Bac = listQuaTrinhLuong[0].Bac.bac1;
                            exportObjects.HeSoBac = listQuaTrinhLuong[0].Bac.heSoBac;
                            exportObjects.NgayBatDauQTL = listQuaTrinhLuong[0].ngayBatDau;
                            exportObjects.NgayLenLuong = listQuaTrinhLuong[0].ngayLenLuong;
                            exportObjects.LinkVanBanDinhKemQTL = listQuaTrinhLuong[0].linkVanBanDinhKem;
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 5) // nganh day
            {
                foreach (var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<NganhVienChuc> listNganhDay = unitOfWorks.NganhVienChucRepository.GetListNganhDayByIdVienChucAndDurationForExportFull(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if (listNganhDay.Count > 1)
                        {
                            exportObjects.LoaiNganhND = listNganhDay[0].LoaiNganh.tenLoaiNganh;
                            exportObjects.NganhDaoTaoND = listNganhDay[0].NganhDaoTao.tenNganhDaoTao;
                            exportObjects.ChuyenNganhND = listNganhDay[0].ChuyenNganh.tenChuyenNganh;
                            exportObjects.LoaiHocHamHocViND = listNganhDay[0].HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi;
                            exportObjects.TenHocHamHocViND = listNganhDay[0].HocHamHocViVienChuc.tenHocHamHocVi;
                            exportObjects.CoSoDaoTaoND = listNganhDay[0].HocHamHocViVienChuc.coSoDaoTao;
                            exportObjects.NgonNguDaoTaoND = listNganhDay[0].HocHamHocViVienChuc.ngonNguDaoTao;
                            exportObjects.HinhThucDaoTaoND = listNganhDay[0].HocHamHocViVienChuc.hinhThucDaoTao;
                            exportObjects.NuocCapBangND = listNganhDay[0].HocHamHocViVienChuc.nuocCapBang;
                            exportObjects.NgayCapBangND = listNganhDay[0].HocHamHocViVienChuc.ngayCapBang;
                            exportObjects.LinkVanBanDinhKemHHHV_ND = listNganhDay[0].HocHamHocViVienChuc.linkVanBanDinhKem;
                            exportObjects.PhanLoaiND = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(listNganhDay[0].phanLoai);
                            exportObjects.NgayBatDauND = listNganhDay[0].ngayBatDau;
                            exportObjects.NgayKetThucND = listNganhDay[0].ngayKetThuc;
                            exportObjects.LinkVanBanDinhKemND = listNganhDay[0].linkVanBanDinhKem;

                            IncreaseIndex(listFieldsDefault, row.Index, listNganhDay.Count - 1);
                            for (int i = 1; i < listNganhDay.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = row.DonVi,
                                    TrangThai = row.TrangThai,
                                    LoaiNganhND = listNganhDay[i].LoaiNganh.tenLoaiNganh,
                                    NganhDaoTaoND = listNganhDay[i].NganhDaoTao.tenNganhDaoTao,
                                    ChuyenNganhND = listNganhDay[i].ChuyenNganh.tenChuyenNganh,
                                    LoaiHocHamHocViND = listNganhDay[i].HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi,
                                    TenHocHamHocViND = listNganhDay[i].HocHamHocViVienChuc.tenHocHamHocVi,
                                    CoSoDaoTaoND = listNganhDay[i].HocHamHocViVienChuc.coSoDaoTao,
                                    NgonNguDaoTaoND = listNganhDay[i].HocHamHocViVienChuc.ngonNguDaoTao,
                                    HinhThucDaoTaoND = listNganhDay[i].HocHamHocViVienChuc.hinhThucDaoTao,
                                    NuocCapBangND = listNganhDay[i].HocHamHocViVienChuc.nuocCapBang,
                                    NgayCapBangND = listNganhDay[i].HocHamHocViVienChuc.ngayCapBang,
                                    LinkVanBanDinhKemHHHV_ND = listNganhDay[i].HocHamHocViVienChuc.linkVanBanDinhKem,
                                    PhanLoaiND = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(listNganhDay[i].phanLoai),
                                    NgayBatDauND = listNganhDay[i].ngayBatDau,
                                    NgayKetThucND = listNganhDay[i].ngayKetThuc,
                                    LinkVanBanDinhKemND = listNganhDay[i].linkVanBanDinhKem
                                });
                            }
                        }
                        if (listNganhDay.Count == 1)
                        {
                            exportObjects.LoaiNganhND = listNganhDay[0].LoaiNganh.tenLoaiNganh;
                            exportObjects.NganhDaoTaoND = listNganhDay[0].NganhDaoTao.tenNganhDaoTao;
                            exportObjects.ChuyenNganhND = listNganhDay[0].ChuyenNganh.tenChuyenNganh;
                            exportObjects.LoaiHocHamHocViND = listNganhDay[0].HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi;
                            exportObjects.TenHocHamHocViND = listNganhDay[0].HocHamHocViVienChuc.tenHocHamHocVi;
                            exportObjects.CoSoDaoTaoND = listNganhDay[0].HocHamHocViVienChuc.coSoDaoTao;
                            exportObjects.NgonNguDaoTaoND = listNganhDay[0].HocHamHocViVienChuc.ngonNguDaoTao;
                            exportObjects.HinhThucDaoTaoND = listNganhDay[0].HocHamHocViVienChuc.hinhThucDaoTao;
                            exportObjects.NuocCapBangND = listNganhDay[0].HocHamHocViVienChuc.nuocCapBang;
                            exportObjects.NgayCapBangND = listNganhDay[0].HocHamHocViVienChuc.ngayCapBang;
                            exportObjects.LinkVanBanDinhKemHHHV_ND = listNganhDay[0].HocHamHocViVienChuc.linkVanBanDinhKem;
                            exportObjects.PhanLoaiND = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(listNganhDay[0].phanLoai);
                            exportObjects.NgayBatDauND = listNganhDay[0].ngayBatDau;
                            exportObjects.NgayKetThucND = listNganhDay[0].ngayKetThuc;
                            exportObjects.LinkVanBanDinhKemND = listNganhDay[0].linkVanBanDinhKem;
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 6) //hop dong
            {
                foreach (var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<HopDongVienChuc> listHopDongVienChuc = unitOfWorks.HopDongVienChucRepository.GetListHopDongByIdVienChucAndDurationForExportFull(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if (listHopDongVienChuc.Count > 1)
                        {
                            exportObjects.MaHopDong = listHopDongVienChuc[0].LoaiHopDong.maLoaiHopDong;
                            exportObjects.TenHopDong = listHopDongVienChuc[0].LoaiHopDong.tenLoaiHopDong;
                            exportObjects.NgayBatDauHD = listHopDongVienChuc[0].ngayBatDau;
                            exportObjects.NgayKetThucHD = listHopDongVienChuc[0].ngayKetThuc;
                            exportObjects.MoTaHD = listHopDongVienChuc[0].moTa;
                            exportObjects.LinkVanBanDinhKemHD = listHopDongVienChuc[0].linkVanBanDinhKem;

                            IncreaseIndex(listFieldsDefault, row.Index, listHopDongVienChuc.Count - 1);
                            for (int i = 1; i < listHopDongVienChuc.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = row.DonVi,
                                    TrangThai = row.TrangThai,
                                    MaHopDong = listHopDongVienChuc[i].LoaiHopDong.maLoaiHopDong,
                                    TenHopDong = listHopDongVienChuc[i].LoaiHopDong.tenLoaiHopDong,
                                    NgayBatDauHD = listHopDongVienChuc[i].ngayBatDau,
                                    NgayKetThucHD = listHopDongVienChuc[i].ngayKetThuc,
                                    MoTaHD = listHopDongVienChuc[i].moTa,
                                    LinkVanBanDinhKemHD = listHopDongVienChuc[i].linkVanBanDinhKem
                                });
                            }
                        }
                        if (listHopDongVienChuc.Count == 1)
                        {
                            exportObjects.MaHopDong = listHopDongVienChuc[0].LoaiHopDong.maLoaiHopDong;
                            exportObjects.TenHopDong = listHopDongVienChuc[0].LoaiHopDong.tenLoaiHopDong;
                            exportObjects.NgayBatDauHD = listHopDongVienChuc[0].ngayBatDau;
                            exportObjects.NgayKetThucHD = listHopDongVienChuc[0].ngayKetThuc;
                            exportObjects.MoTaHD = listHopDongVienChuc[0].moTa;
                            exportObjects.LinkVanBanDinhKemHD = listHopDongVienChuc[0].linkVanBanDinhKem;
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 7) //chung chi
            {
                foreach (var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<ChungChiVienChuc> listChungChi = unitOfWorks.ChungChiVienChucRepository.GetListChungChiByIdVienChucAndDurationForExportFull(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if (listChungChi.Count > 1)
                        {
                            exportObjects.ChungChi = listChungChi[0].LoaiChungChi.tenLoaiChungChi;
                            if (listChungChi[0].capDoChungChi != null)
                            {
                                exportObjects.ChungChi += ", " + listChungChi[0].capDoChungChi;
                            }

                            IncreaseIndex(listFieldsDefault, row.Index, listChungChi.Count - 1);
                            for (int i = 1; i < listChungChi.Count; i++)
                            {
                                if (listChungChi[i].capDoChungChi != null)
                                {
                                    listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                    {
                                        Index = row.Index + 1,
                                        IdVienChuc = row.IdVienChuc,
                                        MaVienChuc = row.MaVienChuc,
                                        Ho = row.Ho,
                                        Ten = row.Ten,
                                        GioiTinh = row.GioiTinh,
                                        DonVi = row.DonVi,
                                        TrangThai = row.TrangThai,
                                        ChungChi = listChungChi[i].LoaiChungChi.tenLoaiChungChi + ", " + listChungChi[i].capDoChungChi
                                    });
                                }
                                else
                                {
                                    listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                    {
                                        Index = row.Index + 1,
                                        IdVienChuc = row.IdVienChuc,
                                        MaVienChuc = row.MaVienChuc,
                                        Ho = row.Ho,
                                        Ten = row.Ten,
                                        GioiTinh = row.GioiTinh,
                                        DonVi = row.DonVi,
                                        TrangThai = row.TrangThai,
                                        ChungChi = listChungChi[i].LoaiChungChi.tenLoaiChungChi
                                    });
                                }
                            }
                        }
                        if (listChungChi.Count == 1)
                        {
                            exportObjects.ChungChi = listChungChi[0].LoaiChungChi.tenLoaiChungChi;
                            if (listChungChi[0].capDoChungChi != null)
                            {
                                exportObjects.ChungChi += ", " + listChungChi[0].capDoChungChi;
                            }
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 8) // dang hoc nang cao
            {
                foreach (var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<DangHocNangCao> listDangHocNangCao = unitOfWorks.DangHocNangCaoRepository.GetListDangHocNangCaoByIdVienChucAndDurationForExportFull(row.IdVienChuc, dtFromDuration, dtToDuration);
                    if (row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if (listDangHocNangCao.Count > 1)
                        {
                            exportObjects.LoaiHocHamHocViDHNC = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiHocHamHocViToGrid(listDangHocNangCao[0].LoaiHocHamHocVi.tenLoaiHocHamHocVi);
                            exportObjects.SoQuyetDinh = listDangHocNangCao[0].soQuyetDinh;
                            exportObjects.LinkAnhQuyetDinh = listDangHocNangCao[0].linkAnhQuyetDinh;
                            exportObjects.TenHocHamHocViDHNC = listDangHocNangCao[0].tenHocHamHocVi;
                            exportObjects.CoSoDaoTaoDHNC = listDangHocNangCao[0].coSoDaoTao;
                            exportObjects.NgonNguDaoTaoDHNC = listDangHocNangCao[0].ngonNguDaoTao;
                            exportObjects.HinhThucDaoTaoDHNC = listDangHocNangCao[0].hinhThucDaoTao;
                            exportObjects.NuocCapBangDHNC = listDangHocNangCao[0].nuocCapBang;
                            exportObjects.NgayBatDauTT = listDangHocNangCao[0].ngayBatDau;
                            exportObjects.NgayKetThucTT = listDangHocNangCao[0].ngayKetThuc;
                            exportObjects.Loai = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiToGrid(listDangHocNangCao[0].loai);

                            IncreaseIndex(listFieldsDefault, row.Index, listDangHocNangCao.Count - 1);
                            for (int i = 1; i < listDangHocNangCao.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = row.DonVi,
                                    TrangThai = row.TrangThai,
                                    LoaiHocHamHocViDHNC = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiHocHamHocViToGrid(listDangHocNangCao[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi),
                                    SoQuyetDinh = listDangHocNangCao[i].soQuyetDinh,
                                    LinkAnhQuyetDinh = listDangHocNangCao[i].linkAnhQuyetDinh,
                                    TenHocHamHocViDHNC = listDangHocNangCao[i].tenHocHamHocVi,
                                    CoSoDaoTaoDHNC = listDangHocNangCao[i].coSoDaoTao,
                                    NgonNguDaoTaoDHNC = listDangHocNangCao[i].ngonNguDaoTao,
                                    HinhThucDaoTaoDHNC = listDangHocNangCao[i].hinhThucDaoTao,
                                    NuocCapBangDHNC = listDangHocNangCao[i].nuocCapBang,
                                    NgayBatDauTT = listDangHocNangCao[i].ngayBatDau,
                                    NgayKetThucTT = listDangHocNangCao[i].ngayKetThuc,
                                    Loai = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiToGrid(listDangHocNangCao[i].loai)
                                });
                            }
                        }
                        if (listDangHocNangCao.Count == 1)
                        {
                            exportObjects.LoaiHocHamHocViDHNC = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiHocHamHocViToGrid(listDangHocNangCao[0].LoaiHocHamHocVi.tenLoaiHocHamHocVi);
                            exportObjects.SoQuyetDinh = listDangHocNangCao[0].soQuyetDinh;
                            exportObjects.LinkAnhQuyetDinh = listDangHocNangCao[0].linkAnhQuyetDinh;
                            exportObjects.TenHocHamHocViDHNC = listDangHocNangCao[0].tenHocHamHocVi;
                            exportObjects.CoSoDaoTaoDHNC = listDangHocNangCao[0].coSoDaoTao;
                            exportObjects.NgonNguDaoTaoDHNC = listDangHocNangCao[0].ngonNguDaoTao;
                            exportObjects.HinhThucDaoTaoDHNC = listDangHocNangCao[0].hinhThucDaoTao;
                            exportObjects.NuocCapBangDHNC = listDangHocNangCao[0].nuocCapBang;
                            exportObjects.NgayBatDauTT = listDangHocNangCao[0].ngayBatDau;
                            exportObjects.NgayKetThucTT = listDangHocNangCao[0].ngayKetThuc;
                            exportObjects.Loai = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiToGrid(listDangHocNangCao[0].loai);
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            _view.GCCustom.DataSource = null;
            _view.GCCustom.DataSource = listFieldsDefault;
            _view.GVCustom.PopulateColumns();
            _view.GVCustom.Columns[0].Visible = false;
            SplashScreenManager.CloseForm(false);
            for (int i = 2; i < _view.GVCustom.Columns.Count; i++)
            {
                for (int j = 0; j < _view.GVCustom.RowCount; j++)
                {
                    if (_view.GVCustom.GetRowCellDisplayText(j, _view.GVCustom.Columns[i]) != string.Empty)
                    {
                        _view.GVCustom.Columns[i].Visible = true;
                        break;
                    }
                    else _view.GVCustom.Columns[i].Visible = false;
                }
            }
            SetCaptionColumn();
            SetCellMergeColumn(selectedDomain);
        }

        private void GetDataTimeline(int selectedDomain)
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            DateTime dtTimeline = _view.DTTimeline.DateTime;
            List<ExportObjects> listFieldsDefault = unitOfWorks.VienChucRepository.GetListFieldsDefaultByTimeline(dtTimeline).ToList();
            int tempIdVienChuc = -1;
            if (selectedDomain == 0) // thong tin ca nhan
            {
                foreach (var row in listFieldsDefault)
                {
                    VienChuc vienChuc = unitOfWorks.VienChucRepository.GetVienChucByIdVienChuc(row.IdVienChuc);
                    if (vienChuc != null)
                    {
                        ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc).FirstOrDefault();
                        exportObjects.SoDienThoai = vienChuc.sDT;
                        exportObjects.NgaySinh = vienChuc.ngaySinh;
                        exportObjects.NoiSinh = vienChuc.noiSinh;
                        exportObjects.QueQuan = vienChuc.queQuan;
                        exportObjects.DanToc = vienChuc.DanToc.tenDanToc;
                        exportObjects.TonGiao = vienChuc.TonGiao.tenTonGiao;
                        exportObjects.HoKhauThuongTru = vienChuc.hoKhauThuongTru;
                        exportObjects.NoiOHienNay = vienChuc.noiOHienNay;
                        exportObjects.LaDangVien = unitOfWorks.VienChucRepository.ReturnLaDangVienToGrid(vienChuc.laDangVien);
                        exportObjects.NgayVaoDang = vienChuc.ngayVaoDang;
                        exportObjects.NgayThamGiaCongTac = vienChuc.ngayThamGiaCongTac;
                        exportObjects.NgayVaoNganh = vienChuc.ngayVaoNganh;
                        exportObjects.NgayVeTruong = vienChuc.ngayVeTruong;
                        exportObjects.VanHoa = vienChuc.vanHoa;
                        exportObjects.QuanLyNhaNuoc = vienChuc.QuanLyNhaNuoc.tenQuanLyNhaNuoc;
                        exportObjects.GhiChu = vienChuc.ghiChu;
                    }
                }
            }
            if (selectedDomain == 1) // trang thai
            {
                foreach (var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<TrangThaiVienChuc> listTrangThai = unitOfWorks.TrangThaiVienChucRepository.GetListTrangThaiByIdVienChucAndTimeline(row.IdVienChuc, dtTimeline);
                    if (row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if (listTrangThai.Count > 1)
                        {
                            exportObjects.MoTaTT = listTrangThai[0].moTa;
                            exportObjects.DiaDiemTT = listTrangThai[0].diaDiem;
                            exportObjects.NgayBatDauTT = listTrangThai[0].ngayBatDau;
                            exportObjects.NgayKetThucTT = listTrangThai[0].ngayKetThuc;
                            exportObjects.LinkVanBanDinhKemTT = listTrangThai[0].linkVanBanDinhKem;

                            IncreaseIndex(listFieldsDefault, row.Index, listTrangThai.Count - 1);
                            for (int i = 1; i < listTrangThai.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = row.DonVi,
                                    TrangThai = listTrangThai[i].TrangThai.tenTrangThai,
                                    MoTaTT = listTrangThai[i].moTa,
                                    DiaDiemTT = listTrangThai[i].diaDiem,
                                    NgayBatDauTT = listTrangThai[i].ngayBatDau,
                                    NgayKetThucTT = listTrangThai[i].ngayKetThuc,
                                    LinkVanBanDinhKemTT = listTrangThai[i].linkVanBanDinhKem
                                });
                            }
                        }
                        if (listTrangThai.Count == 1)
                        {
                            exportObjects.MoTaTT = listTrangThai[0].moTa;
                            exportObjects.DiaDiemTT = listTrangThai[0].diaDiem;
                            exportObjects.NgayBatDauTT = listTrangThai[0].ngayBatDau;
                            exportObjects.NgayKetThucTT = listTrangThai[0].ngayKetThuc;
                            exportObjects.LinkVanBanDinhKemTT = listTrangThai[0].linkVanBanDinhKem;
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 2) // cong tac
            {
                foreach (var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<ChucVuDonViVienChuc> listCongTac = unitOfWorks.ChucVuDonViVienChucRepository.GetListCongTacByIdVienChucAndTimeline(row.IdVienChuc, dtTimeline);
                    if (row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;                        
                        if (listCongTac.Count > 1)
                        {
                            exportObjects.LoaiChucVu = listCongTac[0].ChucVu.LoaiChucVu.tenLoaiChucVu;
                            exportObjects.ChucVu = listCongTac[0].ChucVu.tenChucVu;
                            exportObjects.HeSoChucVu = listCongTac[0].ChucVu.heSoChucVu;
                            exportObjects.LoaiDonVi = listCongTac[0].DonVi.LoaiDonVi.tenLoaiDonVi;
                            //exportObjects.DonVi = listCongTac[0].DonVi.tenDonVi;
                            exportObjects.DiaDiemCT = listCongTac[0].DonVi.diaDiem;
                            exportObjects.DiaChi = listCongTac[0].DonVi.diaChi;
                            exportObjects.SoDienThoaiDonVi = listCongTac[0].DonVi.sDT;
                            exportObjects.ToChuyenMon = listCongTac[0].ToChuyenMon.tenToChuyenMon;
                            exportObjects.PhanLoaiCongTac = listCongTac[0].phanLoaiCongTac;
                            exportObjects.CheckPhanLoaiCongTac = unitOfWorks.ChucVuDonViVienChucRepository.HardCheckPhanLoaiCongTacToGrid(listCongTac[0].checkPhanLoaiCongTac);
                            exportObjects.NgayBatDauCT = listCongTac[0].ngayBatDau;
                            exportObjects.NgayKetThucCT = listCongTac[0].ngayKetThuc;
                            exportObjects.LoaiThayDoi = unitOfWorks.ChucVuDonViVienChucRepository.HardLoaiThayDoiToGrid(listCongTac[0].loaiThayDoi);
                            exportObjects.KiemNhiem = unitOfWorks.ChucVuDonViVienChucRepository.HardKiemNhiemToGrid(listCongTac[0].kiemNhiem);
                            exportObjects.LinkVanBanDinhKemCT = listCongTac[0].linkVanBanDinhKem;

                            IncreaseIndex(listFieldsDefault, row.Index, listCongTac.Count - 1);
                            for (int i = 1; i < listCongTac.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = listCongTac[i].DonVi.tenDonVi,
                                    TrangThai = row.TrangThai,
                                    LoaiChucVu = listCongTac[i].ChucVu.LoaiChucVu.tenLoaiChucVu,
                                    ChucVu = listCongTac[i].ChucVu.tenChucVu,
                                    HeSoChucVu = listCongTac[i].ChucVu.heSoChucVu,
                                    LoaiDonVi = listCongTac[i].DonVi.LoaiDonVi.tenLoaiDonVi,
                                    DiaDiemCT = listCongTac[i].DonVi.diaDiem,
                                    DiaChi = listCongTac[i].DonVi.diaChi,
                                    SoDienThoaiDonVi = listCongTac[i].DonVi.sDT,
                                    ToChuyenMon = listCongTac[i].ToChuyenMon.tenToChuyenMon,
                                    PhanLoaiCongTac = listCongTac[i].phanLoaiCongTac,
                                    CheckPhanLoaiCongTac = unitOfWorks.ChucVuDonViVienChucRepository.HardCheckPhanLoaiCongTacToGrid(listCongTac[i].checkPhanLoaiCongTac),
                                    NgayBatDauCT = listCongTac[i].ngayBatDau,
                                    NgayKetThucCT = listCongTac[i].ngayKetThuc,
                                    LoaiThayDoi = unitOfWorks.ChucVuDonViVienChucRepository.HardLoaiThayDoiToGrid(listCongTac[i].loaiThayDoi),
                                    KiemNhiem = unitOfWorks.ChucVuDonViVienChucRepository.HardKiemNhiemToGrid(listCongTac[i].kiemNhiem),
                                    LinkVanBanDinhKemCT = listCongTac[i].linkVanBanDinhKem
                                });
                            }
                        }
                        if (listCongTac.Count == 1)
                        {
                            exportObjects.LoaiChucVu = listCongTac[0].ChucVu.LoaiChucVu.tenLoaiChucVu;
                            exportObjects.ChucVu = listCongTac[0].ChucVu.tenChucVu;
                            exportObjects.HeSoChucVu = listCongTac[0].ChucVu.heSoChucVu;
                            exportObjects.LoaiDonVi = listCongTac[0].DonVi.LoaiDonVi.tenLoaiDonVi;
                            //exportObjects.DonVi = listCongTac[0].DonVi.tenDonVi;
                            exportObjects.DiaDiemCT = listCongTac[0].DonVi.diaDiem;
                            exportObjects.DiaChi = listCongTac[0].DonVi.diaChi;
                            exportObjects.SoDienThoaiDonVi = listCongTac[0].DonVi.sDT;
                            exportObjects.ToChuyenMon = listCongTac[0].ToChuyenMon.tenToChuyenMon;
                            exportObjects.PhanLoaiCongTac = listCongTac[0].phanLoaiCongTac;
                            exportObjects.CheckPhanLoaiCongTac = unitOfWorks.ChucVuDonViVienChucRepository.HardCheckPhanLoaiCongTacToGrid(listCongTac[0].checkPhanLoaiCongTac);
                            exportObjects.NgayBatDauCT = listCongTac[0].ngayBatDau;
                            exportObjects.NgayKetThucCT = listCongTac[0].ngayKetThuc;
                            exportObjects.LoaiThayDoi = unitOfWorks.ChucVuDonViVienChucRepository.HardLoaiThayDoiToGrid(listCongTac[0].loaiThayDoi);
                            exportObjects.KiemNhiem = unitOfWorks.ChucVuDonViVienChucRepository.HardKiemNhiemToGrid(listCongTac[0].kiemNhiem);
                            exportObjects.LinkVanBanDinhKemCT = listCongTac[0].linkVanBanDinhKem;
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 3) // nganh hoc
            {
                foreach (var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<NganhVienChuc> listNganhHoc = unitOfWorks.NganhVienChucRepository.GetListNganhHocByIdVienChucAndTimelineForExportFull(row.IdVienChuc, dtTimeline);
                    if (row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if (listNganhHoc.Count > 1)
                        {
                            exportObjects.LoaiNganhNH = listNganhHoc[0].LoaiNganh.tenLoaiNganh;
                            exportObjects.NganhDaoTaoNH = listNganhHoc[0].NganhDaoTao.tenNganhDaoTao;
                            exportObjects.ChuyenNganhNH = listNganhHoc[0].ChuyenNganh.tenChuyenNganh;
                            exportObjects.LoaiHocHamHocViNH = listNganhHoc[0].HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi;
                            exportObjects.TenHocHamHocViNH = listNganhHoc[0].HocHamHocViVienChuc.tenHocHamHocVi;
                            exportObjects.CoSoDaoTaoNH = listNganhHoc[0].HocHamHocViVienChuc.coSoDaoTao;
                            exportObjects.NgonNguDaoTaoNH = listNganhHoc[0].HocHamHocViVienChuc.ngonNguDaoTao;
                            exportObjects.HinhThucDaoTaoNH = listNganhHoc[0].HocHamHocViVienChuc.hinhThucDaoTao;
                            exportObjects.NuocCapBangNH = listNganhHoc[0].HocHamHocViVienChuc.nuocCapBang;
                            exportObjects.NgayCapBangNH = listNganhHoc[0].HocHamHocViVienChuc.ngayCapBang;
                            exportObjects.LinkVanBanDinhKemHHHV_NH = listNganhHoc[0].HocHamHocViVienChuc.linkVanBanDinhKem;
                            exportObjects.PhanLoaiNH = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(listNganhHoc[0].phanLoai);
                            exportObjects.LinkVanBanDinhKemNH = listNganhHoc[0].linkVanBanDinhKem;

                            IncreaseIndex(listFieldsDefault, row.Index, listNganhHoc.Count - 1); // tru phan tu 0, con lai bn phan tu thi cong len bay nhieu
                            for (int i = 1; i < listNganhHoc.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = row.DonVi,
                                    TrangThai = row.TrangThai,
                                    LoaiNganhNH = listNganhHoc[i].LoaiNganh.tenLoaiNganh,
                                    NganhDaoTaoNH = listNganhHoc[i].NganhDaoTao.tenNganhDaoTao,
                                    ChuyenNganhNH = listNganhHoc[i].ChuyenNganh.tenChuyenNganh,
                                    LoaiHocHamHocViNH = listNganhHoc[i].HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi,
                                    TenHocHamHocViNH = listNganhHoc[i].HocHamHocViVienChuc.tenHocHamHocVi,
                                    CoSoDaoTaoNH = listNganhHoc[i].HocHamHocViVienChuc.coSoDaoTao,
                                    NgonNguDaoTaoNH = listNganhHoc[i].HocHamHocViVienChuc.ngonNguDaoTao,
                                    HinhThucDaoTaoNH = listNganhHoc[i].HocHamHocViVienChuc.hinhThucDaoTao,
                                    NuocCapBangNH = listNganhHoc[i].HocHamHocViVienChuc.nuocCapBang,
                                    NgayCapBangNH = listNganhHoc[i].HocHamHocViVienChuc.ngayCapBang,
                                    LinkVanBanDinhKemHHHV_NH = listNganhHoc[i].HocHamHocViVienChuc.linkVanBanDinhKem,
                                    PhanLoaiNH = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(listNganhHoc[i].phanLoai),
                                    LinkVanBanDinhKemNH = listNganhHoc[i].linkVanBanDinhKem
                                });
                            }
                        }
                        if (listNganhHoc.Count == 1)
                        {
                            exportObjects.LoaiNganhNH = listNganhHoc[0].LoaiNganh.tenLoaiNganh;
                            exportObjects.NganhDaoTaoNH = listNganhHoc[0].NganhDaoTao.tenNganhDaoTao;
                            exportObjects.ChuyenNganhNH = listNganhHoc[0].ChuyenNganh.tenChuyenNganh;
                            exportObjects.LoaiHocHamHocViNH = listNganhHoc[0].HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi;
                            exportObjects.TenHocHamHocViNH = listNganhHoc[0].HocHamHocViVienChuc.tenHocHamHocVi;
                            exportObjects.CoSoDaoTaoNH = listNganhHoc[0].HocHamHocViVienChuc.coSoDaoTao;
                            exportObjects.NgonNguDaoTaoNH = listNganhHoc[0].HocHamHocViVienChuc.ngonNguDaoTao;
                            exportObjects.HinhThucDaoTaoNH = listNganhHoc[0].HocHamHocViVienChuc.hinhThucDaoTao;
                            exportObjects.NuocCapBangNH = listNganhHoc[0].HocHamHocViVienChuc.nuocCapBang;
                            exportObjects.NgayCapBangNH = listNganhHoc[0].HocHamHocViVienChuc.ngayCapBang;
                            exportObjects.LinkVanBanDinhKemHHHV_NH = listNganhHoc[0].HocHamHocViVienChuc.linkVanBanDinhKem;
                            exportObjects.PhanLoaiNH = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(listNganhHoc[0].phanLoai);
                            exportObjects.LinkVanBanDinhKemNH = listNganhHoc[0].linkVanBanDinhKem;
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 4) // qua trinh luong
            {
                foreach(var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<QuaTrinhLuong> listQuaTrinhLuong = unitOfWorks.QuaTrinhLuongRepository.GetListQuaTrinhLuongByIdVienChucAndTimelineForExportFull(row.IdVienChuc, dtTimeline);
                    if(row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if(listQuaTrinhLuong.Count > 1)
                        {
                            exportObjects.MaNgach = listQuaTrinhLuong[0].Bac.Ngach.maNgach;
                            exportObjects.TenNgach = listQuaTrinhLuong[0].Bac.Ngach.tenNgach;
                            exportObjects.HeSoVuotKhungBaNamDau = listQuaTrinhLuong[0].Bac.Ngach.heSoVuotKhungBaNamDau;
                            exportObjects.HeSoVuotKhungTrenBaNam = listQuaTrinhLuong[0].Bac.Ngach.heSoVuotKhungTrenBaNam;
                            exportObjects.ThoiHanNangBac = listQuaTrinhLuong[0].Bac.Ngach.thoiHanNangBac;
                            exportObjects.Bac = listQuaTrinhLuong[0].Bac.bac1;
                            exportObjects.HeSoBac = listQuaTrinhLuong[0].Bac.heSoBac;
                            exportObjects.NgayBatDauQTL = listQuaTrinhLuong[0].ngayBatDau;
                            exportObjects.NgayLenLuong = listQuaTrinhLuong[0].ngayLenLuong;
                            exportObjects.LinkVanBanDinhKemQTL = listQuaTrinhLuong[0].linkVanBanDinhKem;

                            IncreaseIndex(listFieldsDefault, row.Index, listQuaTrinhLuong.Count - 1);
                            for (int i = 1; i < listQuaTrinhLuong.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = row.DonVi,
                                    TrangThai = row.TrangThai,
                                    MaNgach = listQuaTrinhLuong[i].Bac.Ngach.maNgach,
                                    TenNgach = listQuaTrinhLuong[i].Bac.Ngach.tenNgach,
                                    HeSoVuotKhungBaNamDau = listQuaTrinhLuong[i].Bac.Ngach.heSoVuotKhungBaNamDau,
                                    HeSoVuotKhungTrenBaNam = listQuaTrinhLuong[i].Bac.Ngach.heSoVuotKhungTrenBaNam,
                                    ThoiHanNangBac = listQuaTrinhLuong[i].Bac.Ngach.thoiHanNangBac,
                                    Bac = listQuaTrinhLuong[i].Bac.bac1,
                                    HeSoBac = listQuaTrinhLuong[i].Bac.heSoBac,
                                    NgayBatDauQTL = listQuaTrinhLuong[i].ngayBatDau,
                                    NgayLenLuong = listQuaTrinhLuong[i].ngayLenLuong,
                                    LinkVanBanDinhKemQTL = listQuaTrinhLuong[i].linkVanBanDinhKem
                                });
                            }
                        }
                        if(listQuaTrinhLuong.Count == 1)
                        {
                            exportObjects.MaNgach = listQuaTrinhLuong[0].Bac.Ngach.maNgach;
                            exportObjects.TenNgach = listQuaTrinhLuong[0].Bac.Ngach.tenNgach;
                            exportObjects.HeSoVuotKhungBaNamDau = listQuaTrinhLuong[0].Bac.Ngach.heSoVuotKhungBaNamDau;
                            exportObjects.HeSoVuotKhungTrenBaNam = listQuaTrinhLuong[0].Bac.Ngach.heSoVuotKhungTrenBaNam;
                            exportObjects.ThoiHanNangBac = listQuaTrinhLuong[0].Bac.Ngach.thoiHanNangBac;
                            exportObjects.Bac = listQuaTrinhLuong[0].Bac.bac1;
                            exportObjects.HeSoBac = listQuaTrinhLuong[0].Bac.heSoBac;
                            exportObjects.NgayBatDauQTL = listQuaTrinhLuong[0].ngayBatDau;
                            exportObjects.NgayLenLuong = listQuaTrinhLuong[0].ngayLenLuong;
                            exportObjects.LinkVanBanDinhKemQTL = listQuaTrinhLuong[0].linkVanBanDinhKem;
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 5) // nganh day
            {
                foreach(var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<NganhVienChuc> listNganhDay = unitOfWorks.NganhVienChucRepository.GetListNganhDayByIdVienChucAndTimelineForExportFull(row.IdVienChuc, dtTimeline);
                    if(row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if(listNganhDay.Count > 1)
                        {
                            exportObjects.LoaiNganhND = listNganhDay[0].LoaiNganh.tenLoaiNganh;
                            exportObjects.NganhDaoTaoND = listNganhDay[0].NganhDaoTao.tenNganhDaoTao;
                            exportObjects.ChuyenNganhND = listNganhDay[0].ChuyenNganh.tenChuyenNganh;
                            exportObjects.LoaiHocHamHocViND = listNganhDay[0].HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi;
                            exportObjects.TenHocHamHocViND = listNganhDay[0].HocHamHocViVienChuc.tenHocHamHocVi;
                            exportObjects.CoSoDaoTaoND = listNganhDay[0].HocHamHocViVienChuc.coSoDaoTao;
                            exportObjects.NgonNguDaoTaoND = listNganhDay[0].HocHamHocViVienChuc.ngonNguDaoTao;
                            exportObjects.HinhThucDaoTaoND = listNganhDay[0].HocHamHocViVienChuc.hinhThucDaoTao;
                            exportObjects.NuocCapBangND = listNganhDay[0].HocHamHocViVienChuc.nuocCapBang;
                            exportObjects.NgayCapBangND = listNganhDay[0].HocHamHocViVienChuc.ngayCapBang;
                            exportObjects.LinkVanBanDinhKemHHHV_ND = listNganhDay[0].HocHamHocViVienChuc.linkVanBanDinhKem;
                            exportObjects.PhanLoaiND = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(listNganhDay[0].phanLoai);
                            exportObjects.NgayBatDauND = listNganhDay[0].ngayBatDau;
                            exportObjects.NgayKetThucND = listNganhDay[0].ngayKetThuc;
                            exportObjects.LinkVanBanDinhKemND = listNganhDay[0].linkVanBanDinhKem;

                            IncreaseIndex(listFieldsDefault, row.Index, listNganhDay.Count - 1);
                            for (int i = 1; i < listNganhDay.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = row.DonVi,
                                    TrangThai = row.TrangThai,
                                    LoaiNganhND = listNganhDay[i].LoaiNganh.tenLoaiNganh,
                                    NganhDaoTaoND = listNganhDay[i].NganhDaoTao.tenNganhDaoTao,
                                    ChuyenNganhND = listNganhDay[i].ChuyenNganh.tenChuyenNganh,
                                    LoaiHocHamHocViND = listNganhDay[i].HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi,
                                    TenHocHamHocViND = listNganhDay[i].HocHamHocViVienChuc.tenHocHamHocVi,
                                    CoSoDaoTaoND = listNganhDay[i].HocHamHocViVienChuc.coSoDaoTao,
                                    NgonNguDaoTaoND = listNganhDay[i].HocHamHocViVienChuc.ngonNguDaoTao,
                                    HinhThucDaoTaoND = listNganhDay[i].HocHamHocViVienChuc.hinhThucDaoTao,
                                    NuocCapBangND = listNganhDay[i].HocHamHocViVienChuc.nuocCapBang,
                                    NgayCapBangND = listNganhDay[i].HocHamHocViVienChuc.ngayCapBang,
                                    LinkVanBanDinhKemHHHV_ND = listNganhDay[i].HocHamHocViVienChuc.linkVanBanDinhKem,
                                    PhanLoaiND = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(listNganhDay[i].phanLoai),
                                    NgayBatDauND = listNganhDay[i].ngayBatDau,
                                    NgayKetThucND = listNganhDay[i].ngayKetThuc,
                                    LinkVanBanDinhKemND = listNganhDay[i].linkVanBanDinhKem
                                });
                            }
                        }
                        if (listNganhDay.Count == 1)
                        {
                            exportObjects.LoaiNganhND = listNganhDay[0].LoaiNganh.tenLoaiNganh;
                            exportObjects.NganhDaoTaoND = listNganhDay[0].NganhDaoTao.tenNganhDaoTao;
                            exportObjects.ChuyenNganhND = listNganhDay[0].ChuyenNganh.tenChuyenNganh;
                            exportObjects.LoaiHocHamHocViND = listNganhDay[0].HocHamHocViVienChuc.LoaiHocHamHocVi.tenLoaiHocHamHocVi;
                            exportObjects.TenHocHamHocViND = listNganhDay[0].HocHamHocViVienChuc.tenHocHamHocVi;
                            exportObjects.CoSoDaoTaoND = listNganhDay[0].HocHamHocViVienChuc.coSoDaoTao;
                            exportObjects.NgonNguDaoTaoND = listNganhDay[0].HocHamHocViVienChuc.ngonNguDaoTao;
                            exportObjects.HinhThucDaoTaoND = listNganhDay[0].HocHamHocViVienChuc.hinhThucDaoTao;
                            exportObjects.NuocCapBangND = listNganhDay[0].HocHamHocViVienChuc.nuocCapBang;
                            exportObjects.NgayCapBangND = listNganhDay[0].HocHamHocViVienChuc.ngayCapBang;
                            exportObjects.LinkVanBanDinhKemHHHV_ND = listNganhDay[0].HocHamHocViVienChuc.linkVanBanDinhKem;
                            exportObjects.PhanLoaiND = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToGrid(listNganhDay[0].phanLoai);
                            exportObjects.NgayBatDauND = listNganhDay[0].ngayBatDau;
                            exportObjects.NgayKetThucND = listNganhDay[0].ngayKetThuc;
                            exportObjects.LinkVanBanDinhKemND = listNganhDay[0].linkVanBanDinhKem;
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 6) //hop dong
            {
                foreach (var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<HopDongVienChuc> listHopDongVienChuc = unitOfWorks.HopDongVienChucRepository.GetListHopDongByIdVienChucAndTimelineForExportFull(row.IdVienChuc, dtTimeline);
                    if(row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if(listHopDongVienChuc.Count > 1)
                        {
                            exportObjects.MaHopDong = listHopDongVienChuc[0].LoaiHopDong.maLoaiHopDong;
                            exportObjects.TenHopDong = listHopDongVienChuc[0].LoaiHopDong.tenLoaiHopDong;
                            exportObjects.NgayBatDauHD = listHopDongVienChuc[0].ngayBatDau;
                            exportObjects.NgayKetThucHD = listHopDongVienChuc[0].ngayKetThuc;
                            exportObjects.MoTaHD = listHopDongVienChuc[0].moTa;
                            exportObjects.LinkVanBanDinhKemHD = listHopDongVienChuc[0].linkVanBanDinhKem;

                            IncreaseIndex(listFieldsDefault, row.Index, listHopDongVienChuc.Count - 1);
                            for (int i = 1; i < listHopDongVienChuc.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = row.DonVi,
                                    TrangThai = row.TrangThai,
                                    MaHopDong = listHopDongVienChuc[i].LoaiHopDong.maLoaiHopDong,
                                    TenHopDong = listHopDongVienChuc[i].LoaiHopDong.tenLoaiHopDong,
                                    NgayBatDauHD = listHopDongVienChuc[i].ngayBatDau,
                                    NgayKetThucHD = listHopDongVienChuc[i].ngayKetThuc,
                                    MoTaHD = listHopDongVienChuc[i].moTa,
                                    LinkVanBanDinhKemHD = listHopDongVienChuc[i].linkVanBanDinhKem
                                });
                            }
                        }
                        if(listHopDongVienChuc.Count == 1)
                        {
                            exportObjects.MaHopDong = listHopDongVienChuc[0].LoaiHopDong.maLoaiHopDong;
                            exportObjects.TenHopDong = listHopDongVienChuc[0].LoaiHopDong.tenLoaiHopDong;
                            exportObjects.NgayBatDauHD = listHopDongVienChuc[0].ngayBatDau;
                            exportObjects.NgayKetThucHD = listHopDongVienChuc[0].ngayKetThuc;
                            exportObjects.MoTaHD = listHopDongVienChuc[0].moTa;
                            exportObjects.LinkVanBanDinhKemHD = listHopDongVienChuc[0].linkVanBanDinhKem;
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 7) //chung chi
            {
                foreach(var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<ChungChiVienChuc> listChungChi = unitOfWorks.ChungChiVienChucRepository.GetListChungChiByIdVienChucAndTimelineForExportFull(row.IdVienChuc, dtTimeline);
                    if(row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if (listChungChi.Count > 1)
                        {
                            exportObjects.ChungChi = listChungChi[0].LoaiChungChi.tenLoaiChungChi;
                            if (listChungChi[0].capDoChungChi != null)
                            {
                                exportObjects.ChungChi += ", " + listChungChi[0].capDoChungChi;
                            }

                            IncreaseIndex(listFieldsDefault, row.Index, listChungChi.Count - 1);
                            for (int i = 1; i < listChungChi.Count; i++)
                            {
                                if(listChungChi[i].capDoChungChi != null)
                                {
                                    listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                    {
                                        Index = row.Index + 1,
                                        IdVienChuc = row.IdVienChuc,
                                        MaVienChuc = row.MaVienChuc,
                                        Ho = row.Ho,
                                        Ten = row.Ten,
                                        GioiTinh = row.GioiTinh,
                                        DonVi = row.DonVi,
                                        TrangThai = row.TrangThai,
                                        ChungChi = listChungChi[i].LoaiChungChi.tenLoaiChungChi + ", " + listChungChi[i].capDoChungChi
                                    });
                                }
                                else
                                {
                                    listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                    {
                                        Index = row.Index + 1,
                                        IdVienChuc = row.IdVienChuc,
                                        MaVienChuc = row.MaVienChuc,
                                        Ho = row.Ho,
                                        Ten = row.Ten,
                                        GioiTinh = row.GioiTinh,
                                        DonVi = row.DonVi,
                                        TrangThai = row.TrangThai,
                                        ChungChi = listChungChi[i].LoaiChungChi.tenLoaiChungChi
                                    });
                                }
                            }
                        }
                        if (listChungChi.Count == 1)
                        {
                            exportObjects.ChungChi = listChungChi[0].LoaiChungChi.tenLoaiChungChi;
                            if (listChungChi[0].capDoChungChi != null)
                            {
                                exportObjects.ChungChi += ", " + listChungChi[0].capDoChungChi;
                            }
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            if (selectedDomain == 8) // dang hoc nang cao
            {
                foreach(var row in listFieldsDefault)
                {
                    ExportObjects exportObjects = listFieldsDefault.Where(x => x.IdVienChuc == row.IdVienChuc && x.Index == row.Index).FirstOrDefault();
                    List<DangHocNangCao> listDangHocNangCao = unitOfWorks.DangHocNangCaoRepository.GetListDangHocNangCaoByIdVienChucAndTimelineForExportFull(row.IdVienChuc, dtTimeline);
                    if(row.IdVienChuc != tempIdVienChuc)
                    {
                        tempIdVienChuc = row.IdVienChuc;
                        if (listDangHocNangCao.Count > 1)
                        {
                            exportObjects.LoaiHocHamHocViDHNC = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiHocHamHocViToGrid(listDangHocNangCao[0].LoaiHocHamHocVi.tenLoaiHocHamHocVi);
                            exportObjects.SoQuyetDinh = listDangHocNangCao[0].soQuyetDinh;
                            exportObjects.LinkAnhQuyetDinh = listDangHocNangCao[0].linkAnhQuyetDinh;
                            exportObjects.TenHocHamHocViDHNC = listDangHocNangCao[0].tenHocHamHocVi;
                            exportObjects.CoSoDaoTaoDHNC = listDangHocNangCao[0].coSoDaoTao;
                            exportObjects.NgonNguDaoTaoDHNC = listDangHocNangCao[0].ngonNguDaoTao;
                            exportObjects.HinhThucDaoTaoDHNC = listDangHocNangCao[0].hinhThucDaoTao;
                            exportObjects.NuocCapBangDHNC = listDangHocNangCao[0].nuocCapBang;
                            exportObjects.NgayBatDauTT = listDangHocNangCao[0].ngayBatDau;
                            exportObjects.NgayKetThucTT = listDangHocNangCao[0].ngayKetThuc;
                            exportObjects.Loai = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiToGrid(listDangHocNangCao[0].loai);

                            IncreaseIndex(listFieldsDefault, row.Index, listDangHocNangCao.Count - 1);
                            for (int i = 1; i < listDangHocNangCao.Count; i++)
                            {
                                listFieldsDefault.Insert(row.Index + 1, new ExportObjects
                                {
                                    Index = row.Index + 1,
                                    IdVienChuc = row.IdVienChuc,
                                    MaVienChuc = row.MaVienChuc,
                                    Ho = row.Ho,
                                    Ten = row.Ten,
                                    GioiTinh = row.GioiTinh,
                                    DonVi = row.DonVi,
                                    TrangThai = row.TrangThai,
                                    LoaiHocHamHocViDHNC = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiHocHamHocViToGrid(listDangHocNangCao[i].LoaiHocHamHocVi.tenLoaiHocHamHocVi),
                                    SoQuyetDinh = listDangHocNangCao[i].soQuyetDinh,
                                    LinkAnhQuyetDinh = listDangHocNangCao[i].linkAnhQuyetDinh,
                                    TenHocHamHocViDHNC = listDangHocNangCao[i].tenHocHamHocVi,
                                    CoSoDaoTaoDHNC = listDangHocNangCao[i].coSoDaoTao,
                                    NgonNguDaoTaoDHNC = listDangHocNangCao[i].ngonNguDaoTao,
                                    HinhThucDaoTaoDHNC = listDangHocNangCao[i].hinhThucDaoTao,
                                    NuocCapBangDHNC = listDangHocNangCao[i].nuocCapBang,
                                    NgayBatDauTT = listDangHocNangCao[i].ngayBatDau,
                                    NgayKetThucTT = listDangHocNangCao[i].ngayKetThuc,
                                    Loai = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiToGrid(listDangHocNangCao[i].loai)
                                });
                            }
                        }
                        if (listDangHocNangCao.Count == 1)
                        {
                            exportObjects.LoaiHocHamHocViDHNC = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiHocHamHocViToGrid(listDangHocNangCao[0].LoaiHocHamHocVi.tenLoaiHocHamHocVi);
                            exportObjects.SoQuyetDinh = listDangHocNangCao[0].soQuyetDinh;
                            exportObjects.LinkAnhQuyetDinh = listDangHocNangCao[0].linkAnhQuyetDinh;
                            exportObjects.TenHocHamHocViDHNC = listDangHocNangCao[0].tenHocHamHocVi;
                            exportObjects.CoSoDaoTaoDHNC = listDangHocNangCao[0].coSoDaoTao;
                            exportObjects.NgonNguDaoTaoDHNC = listDangHocNangCao[0].ngonNguDaoTao;
                            exportObjects.HinhThucDaoTaoDHNC = listDangHocNangCao[0].hinhThucDaoTao;
                            exportObjects.NuocCapBangDHNC = listDangHocNangCao[0].nuocCapBang;
                            exportObjects.NgayBatDauTT = listDangHocNangCao[0].ngayBatDau;
                            exportObjects.NgayKetThucTT = listDangHocNangCao[0].ngayKetThuc;
                            exportObjects.Loai = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiToGrid(listDangHocNangCao[0].loai);
                        }
                    }
                }
                tempIdVienChuc = -1;
            }
            _view.GCCustom.DataSource = null;
            _view.GCCustom.DataSource = listFieldsDefault;
            _view.GVCustom.PopulateColumns();
            _view.GVCustom.Columns[0].Visible = false;
            SplashScreenManager.CloseForm(false);
            for (int i = 2; i < _view.GVCustom.Columns.Count; i++)
            {
                for (int j = 0; j < _view.GVCustom.RowCount; j++)
                {
                    if (_view.GVCustom.GetRowCellDisplayText(j, _view.GVCustom.Columns[i]) != string.Empty)
                    {
                        _view.GVCustom.Columns[i].Visible = true;
                        break;
                    }
                    else _view.GVCustom.Columns[i].Visible = false;
                }
            }
            SetCaptionColumn();
            SetCellMergeColumn(selectedDomain);
        }

        private void IncreaseIndex(List<ExportObjects> listFieldDefault, int index/*vi tri de filter cac item con lai*/, int count/*so phan tu con lai trong list cua moi~ vien chuc*/)
        {
            List<ExportObjects> listExportObjects = listFieldDefault.Where(x => x.Index > index).ToList();
            listExportObjects.ForEach(x => x.Index += count);
        }
        private void SetCellMergeColumn(int selectedDomain)
        {
            _view.GVCustom.Columns["MaVienChuc"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            _view.GVCustom.Columns["MaVienChuc"].Width = 100;
            for (int i = 2; i < _view.GVCustom.Columns.Count; i++)
            {
                _view.GVCustom.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            }
        }
        private void FormatDate(string fieldname)
        {
            _view.GVCustom.Columns[fieldname].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            _view.GVCustom.Columns[fieldname].DisplayFormat.FormatString = "dd/MM/yyyy";
        }
        private void SetCaptionColumn()
        {
            //Default
            _view.GVCustom.Columns["MaVienChuc"].Caption = "Mã viên chức";
            _view.GVCustom.Columns["Ho"].Caption = "Họ";
            _view.GVCustom.Columns["Ten"].Caption = "Tên";
            _view.GVCustom.Columns["GioiTinh"].Caption = "Giới tính";
            _view.GVCustom.Columns["DonVi"].Caption = "Đơn vị";
            _view.GVCustom.Columns["TrangThai"].Caption = "Trạng thái";
            //Vien chuc
            _view.GVCustom.Columns["SoDienThoai"].Caption = "Số điện thoại";
            _view.GVCustom.Columns["NgaySinh"].Caption = "Ngày sinh";
            FormatDate("NgaySinh");
            _view.GVCustom.Columns["NoiSinh"].Caption = "Nơi sinh";
            _view.GVCustom.Columns["QueQuan"].Caption = "Quê quán";
            _view.GVCustom.Columns["DanToc"].Caption = "Dân tộc";
            _view.GVCustom.Columns["TonGiao"].Caption = "Tôn giáo";
            _view.GVCustom.Columns["HoKhauThuongTru"].Caption = "Hộ khẩu thường trú";
            _view.GVCustom.Columns["NoiOHienNay"].Caption = "Nơi ở hiện nay";
            _view.GVCustom.Columns["LaDangVien"].Caption = "Là đảng viên";
            _view.GVCustom.Columns["NgayVaoDang"].Caption = "Ngày vào đảng";
            FormatDate("NgayVaoDang");
            _view.GVCustom.Columns["NgayThamGiaCongTac"].Caption = "Ngày tham gia công tác";
            FormatDate("NgayThamGiaCongTac");
            _view.GVCustom.Columns["NgayVaoNganh"].Caption = "Ngày vào ngành";
            FormatDate("NgayVaoNganh");
            _view.GVCustom.Columns["NgayVeTruong"].Caption = "Ngày về trường";
            FormatDate("NgayVeTruong");
            _view.GVCustom.Columns["VanHoa"].Caption = "Văn hóa";
            _view.GVCustom.Columns["QuanLyNhaNuoc"].Caption = "Quản lý nhà nước";
            //Cong tac
            _view.GVCustom.Columns["LoaiChucVu"].Caption = "Loại chức vụ";
            _view.GVCustom.Columns["ChucVu"].Caption = "Chức vụ";
            _view.GVCustom.Columns["HeSoChucVu"].Caption = "Hệ số chức vụ";
            _view.GVCustom.Columns["LoaiDonVi"].Caption = "Loại đơn vị";
            _view.GVCustom.Columns["DiaDiemCT"].Caption = "Địa điểm (Công tác)";
            _view.GVCustom.Columns["DiaChi"].Caption = "Địa chỉ";
            _view.GVCustom.Columns["SoDienThoaiDonVi"].Caption = "Số điện thoại đơn vị";
            _view.GVCustom.Columns["ToChuyenMon"].Caption = "Tổ chuyên môn";
            _view.GVCustom.Columns["PhanLoaiCongTac"].Caption = "Phân loại công tác";
            _view.GVCustom.Columns["CheckPhanLoaiCongTac"].Caption = "Phân loại (Chức vụ)";
            _view.GVCustom.Columns["NgayBatDauCT"].Caption = "Ngày bắt đầu (Công tác)";
            FormatDate("NgayBatDauCT");
            _view.GVCustom.Columns["NgayKetThucCT"].Caption = "Ngày kết thúc (Công tác)";
            FormatDate("NgayKetThucCT");
            _view.GVCustom.Columns["LoaiThayDoi"].Caption = "Loại thay đổi";
            _view.GVCustom.Columns["KiemNhiem"].Caption = "Kiêm nhiệm";
            _view.GVCustom.Columns["LinkVanBanDinhKemCT"].Caption = "Link văn bản đính kèm (Công tác)";
            //Qua trinh luong
            _view.GVCustom.Columns["MaNgach"].Caption = "Mã ngạch";
            _view.GVCustom.Columns["TenNgach"].Caption = "Tên ngạch";
            _view.GVCustom.Columns["HeSoVuotKhungBaNamDau"].Caption = "Hệ số vượt khung ba năm đầu";
            _view.GVCustom.Columns["HeSoVuotKhungTrenBaNam"].Caption = "Hệ số vượt khung trên ba năm";
            _view.GVCustom.Columns["ThoiHanNangBac"].Caption = "Thời hạn nâng bậc";
            _view.GVCustom.Columns["Bac"].Caption = "Bậc";
            _view.GVCustom.Columns["HeSoBac"].Caption = "Hệ số";
            _view.GVCustom.Columns["NgayBatDauQTL"].Caption = "Ngày bắt đầu (Quá trình lương)";
            FormatDate("NgayBatDauQTL");
            _view.GVCustom.Columns["NgayLenLuong"].Caption = "Ngày lên lương";
            FormatDate("NgayLenLuong");
            _view.GVCustom.Columns["LinkVanBanDinhKemQTL"].Caption = "Link văn bản đính kèm (Quá trình lương)";
            //Hop dong
            _view.GVCustom.Columns["MaHopDong"].Caption = "Mã hợp đồng";
            _view.GVCustom.Columns["TenHopDong"].Caption = "Hợp đồng";
            _view.GVCustom.Columns["NgayBatDauHD"].Caption = "Ngày bắt đầu (Hợp đồng)";
            FormatDate("NgayBatDauHD");
            _view.GVCustom.Columns["NgayKetThucHD"].Caption = "Ngày kết thúc (Hợp đồng)";
            FormatDate("NgayKetThucHD");
            _view.GVCustom.Columns["MoTaHD"].Caption = "Mô tả (Hợp đồng)";
            _view.GVCustom.Columns["LinkVanBanDinhKemHD"].Caption = "Link văn bản đính kèm (Hợp đồng)";
            //Trang thai            
            _view.GVCustom.Columns["MoTaTT"].Caption = "Mô tả (Trạng thái)";
            _view.GVCustom.Columns["DiaDiemTT"].Caption = "Địa điểm (Trạng thái)";
            _view.GVCustom.Columns["NgayBatDauTT"].Caption = "Ngày bắt đầu (Trạng thái)";
            FormatDate("NgayBatDauTT");
            _view.GVCustom.Columns["NgayKetThucTT"].Caption = "Ngày kết thúc (Trạng thái)";
            FormatDate("NgayKetThucTT");
            _view.GVCustom.Columns["LinkVanBanDinhKemTT"].Caption = "Link văn bản đính kèm (Trạng thái)";
            //Nganh hoc
            _view.GVCustom.Columns["NganhDaoTaoNH"].Caption = "Ngành đào tạo (Ngành học)";
            _view.GVCustom.Columns["ChuyenNganhNH"].Caption = "Chuyên ngành (Ngành học)";
            _view.GVCustom.Columns["LoaiHocHamHocViNH"].Caption = "Trình độ (Ngành học)";
            _view.GVCustom.Columns["TenHocHamHocViNH"].Caption = "Học hàm/Học vị (Ngành học)";
            _view.GVCustom.Columns["CoSoDaoTaoNH"].Caption = "Cơ sở đào tạo (Ngành học)";
            _view.GVCustom.Columns["NgonNguDaoTaoNH"].Caption = "Ngôn ngữ đào tạo (Ngành học)";
            _view.GVCustom.Columns["HinhThucDaoTaoNH"].Caption = "Hình thức đào tạo";
            _view.GVCustom.Columns["NuocCapBangNH"].Caption = "Nước cấp bằng (Ngành học)";
            _view.GVCustom.Columns["NgayCapBangNH"].Caption = "Ngày cấp bằng (Ngành học)";
            FormatDate("NgayCapBangNH");
            _view.GVCustom.Columns["LinkVanBanDinhKemHHHV_NH"].Caption = "Link văn bản đính kèm (Ngành học - Học hàm/Học vị)";
            _view.GVCustom.Columns["PhanLoaiNH"].Caption = "Phân loại (Ngành học)";
            _view.GVCustom.Columns["LinkVanBanDinhKemNH"].Caption = "Link văn bản đính kèm (Ngành học)";
            //Nganh day
            _view.GVCustom.Columns["NganhDaoTaoND"].Caption = "Ngành đào tạo (Ngành dạy)";
            _view.GVCustom.Columns["ChuyenNganhND"].Caption = "Chuyên ngành  (Ngành dạy)";
            _view.GVCustom.Columns["LoaiHocHamHocViND"].Caption = "Trình độ  (Ngành dạy)";
            _view.GVCustom.Columns["TenHocHamHocViND"].Caption = "Học hàm/Học vị (Ngành dạy)";
            _view.GVCustom.Columns["CoSoDaoTaoND"].Caption = "Cơ sở đào tạo (Ngành dạy)";
            _view.GVCustom.Columns["NgonNguDaoTaoND"].Caption = "Ngôn ngữ đào tạo (Ngành dạy)";
            _view.GVCustom.Columns["HinhThucDaoTaoND"].Caption = "Hình thức đào tạo (Ngành dạy)";
            _view.GVCustom.Columns["NuocCapBangND"].Caption = "Nước cấp bằng (Ngành dạy)";
            _view.GVCustom.Columns["NgayCapBangND"].Caption = "Ngày cấp bằng (Ngành dạy)";
            FormatDate("NgayCapBangND");
            _view.GVCustom.Columns["LinkVanBanDinhKemHHHV_ND"].Caption = "Link văn bản đính kèm (Ngành dạy - Học hàm/Học vị)";
            _view.GVCustom.Columns["PhanLoaiND"].Caption = "Phân loại (Ngành dạy)";
            _view.GVCustom.Columns["NgayBatDauND"].Caption = "Ngày bắt đầu (Ngành dạy)";
            FormatDate("NgayBatDauND");
            _view.GVCustom.Columns["NgayKetThucND"].Caption = "Ngày kết thúc (Ngành dạy)";
            FormatDate("NgayKetThucND");
            _view.GVCustom.Columns["LinkVanBanDinhKemND"].Caption = "Link văn bản đính kèm (Ngành dạy)";
            //Chung chi
            _view.GVCustom.Columns["NgoaiNgu"].Caption = "Ngoại ngữ";
            _view.GVCustom.Columns["TinHoc"].Caption = "Tin học";
            _view.GVCustom.Columns["LiLuanChinhTri"].Caption = "Lí luận chính trị";
            _view.GVCustom.Columns["ChungChiChuyenMon"].Caption = "Chứng chỉ chuyên môn";
            _view.GVCustom.Columns["ChungChi"].Caption = "Chứng chỉ";
            //Dang hoc nang cao
            _view.GVCustom.Columns["SoQuyetDinh"].Caption = "Số quyết định";
            _view.GVCustom.Columns["LinkAnhQuyetDinh"].Caption = "Link ảnh quyết định";
            _view.GVCustom.Columns["LoaiHocHamHocViDHNC"].Caption = "Trình độ (Đang học nâng cao)";
            _view.GVCustom.Columns["Loai"].Caption = "Loại (Đang học nâng cao)";
            _view.GVCustom.Columns["TenHocHamHocViDHNC"].Caption = "Học hàm/Học vị (Đang học nâng cao)";
            _view.GVCustom.Columns["CoSoDaoTaoDHNC"].Caption = "Cơ sở đào tạo (Đang học nâng cao)";
            _view.GVCustom.Columns["NgonNguDaoTaoDHNC"].Caption = "Ngôn ngữ đào tạo (Đang học nâng cao)";
            _view.GVCustom.Columns["HinhThucDaoTaoDHNC"].Caption = "Hình thức đào tạo (Đang học nâng cao)";
            _view.GVCustom.Columns["NuocCapBangDHNC"].Caption = "Nước cấp bằng (Đang học nâng cao)";
            _view.GVCustom.Columns["NgayBatDauDHNC"].Caption = "Ngày bắt đầu (Đang học nâng cao)";
            FormatDate("NgayBatDauDHNC");
            _view.GVCustom.Columns["NgayKetThucDHNC"].Caption = "Ngày kết thúc (Đang học nâng cao)";
            FormatDate("NgayKetThucDHNC");
            // index to add row for export one domain
            _view.GVCustom.Columns["Index"].Visible = false;
        }
    }
}
