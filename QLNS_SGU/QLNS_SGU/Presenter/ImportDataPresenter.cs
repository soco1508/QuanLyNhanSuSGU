using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using LinqToExcel;
using Model;
using Model.Entities;
using QLNS_SGU.View;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS_SGU.Presenter
{
    public interface IImportDataPresenter : IPresenter
    {
        void ChooseFile();
        void OpenTabThongTinVienChuc();
        void OpenTabTrangThai();
        void OpenTabChucVuDonVi();
        void OpenTabHopDong();
        void OpenTabNgachBacLuong();
        void OpenTabNganh();
        void OpenTabHocHamHocVi();
        void OpenTabLinhVucDangHocNangCao();
        void OpenTabChungChi();
        void ImportVienChuc();
        void ImportTrangThaiVienChuc();
        void ImportChucVu();
        void ImportDonVi();
        void ImportToChuyenMon();
        void ImportChucVuDonViVienChuc();
        void ImportNgach();
        void ImportBac();
        void ImportQuaTrinhLuong();
        void ImportLoaiNganh();
        void ImportNganhDaoTao();
        void ImportChuyenNganh();
        void ImportNganhVienChuc();
        void ImportHocHamHocViVienChuc();
        void ImportDangHocNangCao();
        void ImportLoaiHopDong();
        void ImportHopDongVienChuc();
        void ImportLoaiChungChi();
        void ImportChungChiVienChuc();
    }
    public class ImportDataPresenter : IImportDataPresenter
    {
        private ImportDataForm _view;

        public object UI => _view;

        public ImportDataPresenter(ImportDataForm view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _view.Attach(this);
            _view.XtraTabControl.SelectedTabPageIndex = 0;
        }
#region --OpenTab--
        public void OpenTabThongTinVienChuc()
        {
            if (_view.XtraTabControl.IsAccessible)
            {
                _view.XtraTabControl.SelectedTabPage = _view.TabPageThongTinVienChuc;
            }
            _view.XtraTabControl.Show();
            _view.XtraTabControl.SelectedTabPage = _view.TabPageThongTinVienChuc;
        }

        public void OpenTabTrangThai()
        {
            if (_view.XtraTabControl.IsAccessible)
            {
                _view.XtraTabControl.SelectedTabPage = _view.TabPageTrangThai;
            }
            _view.XtraTabControl.Show();
            _view.XtraTabControl.SelectedTabPage = _view.TabPageTrangThai;
        }

        public void OpenTabChucVuDonVi()
        {
            if (_view.XtraTabControl.IsAccessible)
            {
                _view.XtraTabControl.SelectedTabPage = _view.TabPageChucVuDonVi;
            }
            _view.XtraTabControl.Show();
            _view.XtraTabControl.SelectedTabPage = _view.TabPageChucVuDonVi;
        }

        public void OpenTabHopDong()
        {
            if (_view.XtraTabControl.IsAccessible)
            {
                _view.XtraTabControl.SelectedTabPage = _view.TabPageHopDong;
            }
            _view.XtraTabControl.Show();
            _view.XtraTabControl.SelectedTabPage = _view.TabPageHopDong;
        }

        public void OpenTabNgachBacLuong()
        {
            if (_view.XtraTabControl.IsAccessible)
            {
                _view.XtraTabControl.SelectedTabPage = _view.TabPageNgachBacLuong;
            }
            _view.XtraTabControl.Show();
            _view.XtraTabControl.SelectedTabPage = _view.TabPageNgachBacLuong;
        }

        public void OpenTabNganh()
        {
            if (_view.XtraTabControl.IsAccessible)
            {
                _view.XtraTabControl.SelectedTabPage = _view.TabPageNganh;
            }
            _view.XtraTabControl.Show();
            _view.XtraTabControl.SelectedTabPage = _view.TabPageNganh;
        }

        public void OpenTabHocHamHocVi()
        {
            if (_view.XtraTabControl.IsAccessible)
            {
                _view.XtraTabControl.SelectedTabPage = _view.TabPageHocHamHocVi;
            }
            _view.XtraTabControl.Show();
            _view.XtraTabControl.SelectedTabPage = _view.TabPageHocHamHocVi;
        }

        public void OpenTabLinhVucDangHocNangCao()
        {
            if (_view.XtraTabControl.IsAccessible)
            {
                _view.XtraTabControl.SelectedTabPage = _view.TabPageLinhVucDangHocNangCao;
            }
            _view.XtraTabControl.Show();
            _view.XtraTabControl.SelectedTabPage = _view.TabPageLinhVucDangHocNangCao;
        }

        public void OpenTabChungChi()
        {
            if (_view.XtraTabControl.IsAccessible)
            {
                _view.XtraTabControl.SelectedTabPage = _view.TabPageChungChi;
            }
            _view.XtraTabControl.Show();
            _view.XtraTabControl.SelectedTabPage = _view.TabPageChungChi;
        }
#endregion
        public void ChooseFile()
        {            
            _view.OpenFileDialog.FileName = string.Empty;
            _view.CbxListSheets.EditValue = "---------Chọn sheet---------";
            _view.OpenFileDialog.Filter = "Excel Files|*.xls*;*.xlsx*;*.xlsm*";
            if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
            string path = _view.OpenFileDialog.FileName;
            _view.TxtPathFile.Caption = path;
            var excelfile = new ExcelQueryFactory(path);
            var sheetnames = excelfile.GetWorksheetNames();
            RepositoryItemComboBox repositoryItem = _view.CbxListSheets.Edit as RepositoryItemComboBox;
            if (repositoryItem.Items != null)
            {
                repositoryItem.Items.Clear();
            }
            foreach (var sheetname in sheetnames)
            {
                repositoryItem.Items.Add(sheetname);
            }
        }
#region --Import Data To Database--
        public void ImportVienChuc()
        {
            try
            {
                if (_view.TxtPathFile.Caption != string.Empty && _view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    string path = _view.TxtPathFile.Caption;
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var excelfile = new ExcelQueryFactory(path);
                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               select a;

                    foreach (var row in rows)
                    {
                        unitOfWorks.VienChucRepository.Insert(new VienChuc
                        {
                            maVienChuc = row["mavienchuc"].ToString().Trim(),
                            ho = row["ho"].ToString().Trim(),
                            ten = row["ten"].ToString().Trim(),
                            sDT = row["sdt"].ToString().Trim(),
                            gioiTinh = unitOfWorks.VienChucRepository.ReturnGenderToDatabaseFromImport(row["gioitinh"].ToString().Trim()),
                            ngaySinh = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["ngaysinh"].ToString().Trim()),
                            noiSinh = row["noisinh"].ToString().Trim(),
                            queQuan = row["quequan"].ToString().Trim(),
                            idDanToc = unitOfWorks.DanTocRepository.GetIdByName(row["dantoc"].ToString().Trim()),
                            idTonGiao = unitOfWorks.TonGiaoRepository.GetIdByName(row["tongiao"].ToString().Trim()),
                            hoKhauThuongTru = row["hokhauthuongtru"].ToString().Trim(),
                            noiOHienNay = row["noiohiennay"].ToString().Trim(),
                            ngayThamGiaCongTac = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["ngaythamgiacongtac"].ToString().Trim()),
                            ngayVaoDang = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["dangvien_db"].ToString().Trim()),
                            laDangVien = unitOfWorks.VienChucRepository.ReturnBoolDangVien(row["dangvien_db"].ToString().Trim()),
                            ngayVaoNganh = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["ngayvaonganh"].ToString().Trim()),
                            ngayVeTruong = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["ngayvetruong"].ToString().Trim()),
                            vanHoa = row["vanhoa"].ToString().Trim(),
                            idQuanLyNhaNuoc = unitOfWorks.QuanLyNhaNuocRepository.GetIdByName(row["qlnn"].ToString().Trim())
                        });
                    }

                    unitOfWorks.Save();
                    XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn tập tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { XtraMessageBox.Show("Lỗi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        public void ImportTrangThaiVienChuc()
        {
            try
            {
                if (_view.TxtPathFile.Caption != string.Empty && _view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    string path = _view.TxtPathFile.Caption;
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var excelfile = new ExcelQueryFactory(path);
                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               select a;
                    foreach (var row in rows)
                    {
                        //unitOfWorks.TrangThaiVienChucRepository.Insert(new TrangThaiVienChuc
                        //{
                        //    idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(row["mavienchuc"].ToString().Trim()),
                        //    idTrangThai = 1/*unitOfWorks.TrangThaiRepository.GetIdTrangThai(row["trangthai"].ToString().Trim())*/
                        //                   //ngayBatDau = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["ngaybatdau"].ToString().Trim()),
                        //                   //ngayKetThuc = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["ngayketthuc"].ToString().Trim()),
                        //                   //moTa = row["mota"].ToString().Trim(),
                        //                   //diaDiem = row["diadiem"].ToString().Trim(),
                        //                   //linkVanBanDinhKem = ""
                        //});
                        //list.Add(row["mavienchuc"].ToString().Trim());
                        List<string> list = unitOfWorks.VienChucRepository.GetListMaVienChuc();
                        string mavienchuc = row["mavienchuc"].ToString().Trim();
                        string ngaybatdau = row["ngaybatdau"].ToString().Trim();
                        string ngayketthuc = row["ngayketthuc"].ToString().Trim();
                        if (list.Any(x => x == mavienchuc) && ngaybatdau != string.Empty && ngayketthuc != string.Empty)
                        {
                            DateTime _ngaybatdau = Convert.ToDateTime(ngaybatdau);
                            DateTime _ngayketthuc = Convert.ToDateTime(ngayketthuc);
                            string mota = row["mota"].ToString().Trim();
                            string diadiem = row["diadiem"].ToString().Trim();
                            unitOfWorks.TrangThaiVienChucRepository.Update(mavienchuc, _ngaybatdau, _ngayketthuc, mota, diadiem);
                        }
                    }

                    unitOfWorks.Save();
                    XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn tập tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { XtraMessageBox.Show("Lỗi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }            
        }

        public void ImportChucVu()
        {
            try
            {
                if (_view.TxtPathFile.Caption != string.Empty && _view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    string path = _view.TxtPathFile.Caption;
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var excelfile = new ExcelQueryFactory(path);
                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["chucvu"] != string.Empty
                               select a;
                    List<ChucVu> listChucVu = new List<ChucVu>();

                    foreach (var row in rows)
                    {
                        string chucvu = row["chucvu"].ToString().Trim();
                        if (listChucVu.Any(x => x.tenChucVu == chucvu) == false)
                        {
                            listChucVu.Add(new ChucVu
                            {
                                tenChucVu = chucvu,
                                heSoChucVu = 0,
                                idLoaiChucVu = 4
                            });
                        }
                    }
                    foreach (var row in rows)
                    {
                        string truongphobm = row["truong-phobm"].ToString().Trim();
                        if (truongphobm != string.Empty && listChucVu.Any(x => x.tenChucVu == truongphobm) == false)
                        {
                            listChucVu.Add(new ChucVu
                            {
                                tenChucVu = truongphobm,
                                heSoChucVu = 0,
                                idLoaiChucVu = 4
                            });
                        }
                    }

                    foreach (var row in listChucVu)
                    {
                        unitOfWorks.ChucVuRepository.Insert(new ChucVu
                        {
                            tenChucVu = row.tenChucVu,
                            heSoChucVu = row.heSoChucVu,
                            idLoaiChucVu = row.idLoaiChucVu
                        });
                    }

                    unitOfWorks.Save();
                    XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn tập tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { XtraMessageBox.Show("Lỗi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }            
        }

        public void ImportDonVi()
        {
            try
            {
                if (_view.TxtPathFile.Caption != string.Empty && _view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    string path = _view.TxtPathFile.Caption;
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var excelfile = new ExcelQueryFactory(path);
                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["donvi"] != string.Empty
                               select a;
                    List<DonVi> listDonVi = new List<DonVi>();

                    foreach (var row in rows)
                    {
                        string donvi = row["donvi"].ToString().Trim();
                        if (listDonVi.Any(x => x.tenDonVi == donvi) == false)
                        {
                            listDonVi.Add(new DonVi
                            {
                                tenDonVi = donvi,
                                diaDiem = "",
                                diaChi = "",
                                sDT = "",
                                idLoaiDonVi = unitOfWorks.LoaiDonViRepository.GetIdLoaiDonVi(donvi)
                            });
                        }
                    }

                    foreach (var row in listDonVi)
                    {
                        unitOfWorks.DonViRepository.Insert(new DonVi
                        {
                            tenDonVi = row.tenDonVi,
                            diaDiem = row.diaDiem,
                            diaChi = row.diaChi,
                            sDT = row.sDT,
                            idLoaiDonVi = row.idLoaiDonVi
                        });
                    }

                    unitOfWorks.Save();
                    XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn tập tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { XtraMessageBox.Show("Lỗi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }            
        }

        public void ImportToChuyenMon()
        {
            try
            {
                if (_view.TxtPathFile.Caption != string.Empty && _view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    string path = _view.TxtPathFile.Caption;
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var excelfile = new ExcelQueryFactory(path);
                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["tochuyenmon"] != string.Empty
                               select a;
                    List<ToChuyenMon> listToChuyenMon = new List<ToChuyenMon>();

                    foreach (var row in rows)
                    {
                        string donvi = row["donvi"].ToString().Trim();
                        string tochuyenmon = row["tochuyenmon"].ToString().Trim();
                        if (listToChuyenMon.Any(x => x.tenToChuyenMon == tochuyenmon) == false)
                        {
                            listToChuyenMon.Add(new ToChuyenMon
                            {
                                tenToChuyenMon = tochuyenmon,
                                idDonVi = unitOfWorks.DonViRepository.GetIdDonVi(donvi)
                            });
                        }
                    }
                    foreach (var row in listToChuyenMon)
                    {
                        unitOfWorks.ToChuyenMonRepository.Insert(new ToChuyenMon
                        {
                            tenToChuyenMon = row.tenToChuyenMon,
                            idDonVi = row.idDonVi
                        });
                    }
                    unitOfWorks.Save();
                    XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn tập tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { XtraMessageBox.Show("Lỗi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }            
        }

        public void ImportChucVuDonViVienChuc()
        {
            try
            {
                if (_view.TxtPathFile.Caption != string.Empty && _view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    string path = _view.TxtPathFile.Caption;
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var excelfile = new ExcelQueryFactory(path);
                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["donvi"] != string.Empty
                               select a;

                    foreach (var row in rows)
                    {
                        string mavienchuc = row["mavienchuc"].ToString().Trim();
                        string chucvu = row["chucvu"].ToString().Trim();
                        string donvi = row["donvi"].ToString().Trim();
                        string tochuyenmon = row["tochuyenmon"].ToString().Trim();
                        string truongphobm = row["truong-phobm"].ToString().Trim();
                        string phanloai = row["phanloai"].ToString().Trim();
                        if (truongphobm != string.Empty)
                        {
                            unitOfWorks.ChucVuDonViVienChucRepository.Insert(new ChucVuDonViVienChuc
                            {
                                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                                idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(chucvu),
                                idDonVi = unitOfWorks.DonViRepository.GetIdDonVi(donvi),
                                idToChuyenMon = unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(donvi, tochuyenmon),
                                phanLoaiCongTac = phanloai,
                                checkPhanLoaiCongTac = null,
                                ngayBatDau = null,
                                ngayKetThuc = null,
                                linkVanBanDinhKem = null,
                                loaiThayDoi = null,
                                kiemNhiem = 1
                            });
                            unitOfWorks.ChucVuDonViVienChucRepository.Insert(new ChucVuDonViVienChuc
                            {
                                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                                idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(truongphobm),
                                idDonVi = unitOfWorks.DonViRepository.GetIdDonVi(donvi),
                                idToChuyenMon = unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(donvi, tochuyenmon),
                                phanLoaiCongTac = phanloai,
                                checkPhanLoaiCongTac = null,
                                ngayBatDau = null,
                                ngayKetThuc = null,
                                linkVanBanDinhKem = null,
                                loaiThayDoi = null,
                                kiemNhiem = 1
                            });
                        }
                        else
                        {
                            unitOfWorks.ChucVuDonViVienChucRepository.Insert(new ChucVuDonViVienChuc
                            {
                                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                                idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(chucvu),
                                idDonVi = unitOfWorks.DonViRepository.GetIdDonVi(donvi),
                                idToChuyenMon = unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(donvi, tochuyenmon),
                                phanLoaiCongTac = phanloai,
                                checkPhanLoaiCongTac = null,
                                ngayBatDau = null,
                                ngayKetThuc = null,
                                linkVanBanDinhKem = null,
                                loaiThayDoi = null,
                                kiemNhiem = 0
                            });
                        }
                    }

                    unitOfWorks.Save();
                    XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn tập tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { XtraMessageBox.Show("Lỗi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }            
        }

        public void ImportNgach()
        {
            try
            {
                if (_view.TxtPathFile.Caption != string.Empty && _view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    string path = _view.TxtPathFile.Caption;
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var excelfile = new ExcelQueryFactory(path);
                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["mschucdanh"] != string.Empty
                               select a;
                    List<Ngach> listNgach = new List<Ngach>();
                    foreach (var row in rows)
                    {
                        string mangach = row["mschucdanh"].ToString().Trim();
                        if (listNgach.Any(x => x.maNgach == mangach) == false)
                        {
                            listNgach.Add(new Ngach
                            {
                                maNgach = row["mschucdanh"].ToString().Trim(),
                                tenNgach = "",
                                heSoVuotKhungBaNamDau = null,
                                heSoVuotKhungTrenBaNam = null,
                                thoiHanNangBac = null
                            });
                        }
                    }
                    foreach (var row in listNgach)
                    {
                        unitOfWorks.NgachRepository.Insert(new Ngach
                        {
                            maNgach = row.maNgach,
                            tenNgach = row.tenNgach,
                            heSoVuotKhungBaNamDau = row.heSoVuotKhungBaNamDau,
                            heSoVuotKhungTrenBaNam = row.heSoVuotKhungTrenBaNam,
                            thoiHanNangBac = row.thoiHanNangBac
                        });
                    }

                    unitOfWorks.Save();
                    XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa chọn tập tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { XtraMessageBox.Show("Lỗi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }            
        }

        public void ImportBac()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string path = _view.TxtPathFile.Caption;
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var excelfile = new ExcelQueryFactory(path);
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["mschucdanh"] != string.Empty && a["heso"] != string.Empty
                       select a;
            List<Bac> listBac = new List<Bac>();
            foreach (var row in rows)
            {
                double hesobac = unitOfWorks.BacRepository.ParseHeSoBacToDouble(row["heso"].ToString().Trim());
                int idngach = unitOfWorks.NgachRepository.GetIdNgach(row["mschucdanh"].ToString().Trim());
                if (listBac.Any(x => x.idNgach == idngach) == false && listBac.Any(y => y.heSoBac == hesobac) == false)
                {
                    unitOfWorks.BacRepository.Insert(new Bac
                    {
                        bac1 = Convert.ToInt32(row["bac"].ToString().Trim()),
                        heSoBac = hesobac,
                        idNgach = unitOfWorks.NgachRepository.GetIdNgach(row["mschucdanh"].ToString().Trim())
                    });
                }                
            }

            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportQuaTrinhLuong()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string path = _view.TxtPathFile.Caption;
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var excelfile = new ExcelQueryFactory(path);
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["mschucdanh"] != string.Empty && a["heso"] != string.Empty
                       select a;
            List<string> list = unitOfWorks.VienChucRepository.GetListMaVienChuc();
            foreach (var row in rows)
            {
                string mavienchuc = row["mavienchuc"].ToString().Trim();
                //double hesobac = unitOfWorks.BacRepository.ParseHeSoBacToDouble(row["heso"].ToString().Trim());
                int idngach = unitOfWorks.NgachRepository.GetIdNgach(row["mschucdanh"].ToString().Trim());
                if(list.Any(x => x == mavienchuc))
                {
                    unitOfWorks.QuaTrinhLuongRepository.Insert(new QuaTrinhLuong
                    {
                        idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                        //idBac = unitOfWorks.BacRepository.GetIdBac(hesobac, idngach),
                        ngayBatDau = unitOfWorks.QuaTrinhLuongRepository.ParseDatetimeMatchDatetimeDatabase(row["mocthoigian"].ToString().Trim()),
                        ngayLenLuong = null,
                        dangHuongLuong = true,
                        linkVanBanDinhKem = ""
                    });
                }                
            }

            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportLoaiNganh()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string path = _view.TxtPathFile.Caption;
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var excelfile = new ExcelQueryFactory(path);
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["loainganh"] != string.Empty
                       select a;
            List<string> list = new List<string>();
            foreach (var row in rows)
            {
                string loainganh = row["loainganh"].ToString().Trim();
                if (list.Any(x => x == loainganh) == false) 
                {
                    list.Add(loainganh);
                }
            }
            foreach(var row in list)
            {
                unitOfWorks.LoaiNganhRepository.Insert(new LoaiNganh
                {
                    tenLoaiNganh = row
                });
            }
            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportNganhDaoTao()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string path = _view.TxtPathFile.Caption;
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var excelfile = new ExcelQueryFactory(path);
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["nganhdaotao"] != string.Empty
                       select a;
            List<NganhDaoTao> listNganhDaoTao = new List<NganhDaoTao>();
            List<string> list = unitOfWorks.NganhDaoTaoRepository.GetListNganhDaoTaoForImport();
            foreach(var row in rows)
            {
                string nganhdaotao = row["nganhdaotao"].ToString().Trim();
                if(list.Any(x => x == nganhdaotao) == false && listNganhDaoTao.Any(y => y.tenNganhDaoTao == nganhdaotao) == false)
                {
                    listNganhDaoTao.Add(new NganhDaoTao
                    {
                        tenNganhDaoTao = nganhdaotao,
                        idLoaiNganh = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganh("Khác")
                    });
                }
            }
            foreach(var row in listNganhDaoTao)
            {
                unitOfWorks.NganhDaoTaoRepository.Insert(new NganhDaoTao
                {
                    tenNganhDaoTao = row.tenNganhDaoTao,
                    idLoaiNganh = row.idLoaiNganh
                });
            }
            //List<NganhDaoTao> list = new List<NganhDaoTao>();
            //foreach (var row in rows)
            //{
            //    string nganhdaotao = row["nganhdaotao"].ToString().Trim();
            //    if (list.Any(x => x.tenNganhDaoTao == nganhdaotao) == false)
            //    {
            //        list.Add(new NganhDaoTao
            //        {
            //            tenNganhDaoTao = nganhdaotao,
            //            idLoaiNganh = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganh(row["loainganh"].ToString().Trim())
            //        });
            //    }
            //}
            //foreach (var row in list)
            //{
            //    unitOfWorks.NganhDaoTaoRepository.Insert(new NganhDaoTao
            //    {
            //        tenNganhDaoTao = row.tenNganhDaoTao,
            //        idLoaiNganh = row.idLoaiNganh
            //    });
            //}
            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportChuyenNganh()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string path = _view.TxtPathFile.Caption;
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var excelfile = new ExcelQueryFactory(path);
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["chuyennganh"] != string.Empty
                       select a;
            List<ChuyenNganh> listChuyenNganh = new List<ChuyenNganh>();
            List<string> list = unitOfWorks.ChuyenNganhRepository.GetListChuyenNganhForImport();
            foreach (var row in rows)
            {
                string chuyennganh = row["chuyennganh"].ToString().Trim();
                string nganhdaotao = row["nganhdaotao"].ToString().Trim();
                if (list.Any(x => x == chuyennganh) == false && listChuyenNganh.Any(y => y.tenChuyenNganh == chuyennganh) == false)
                {
                    if(nganhdaotao != string.Empty)
                    {
                        listChuyenNganh.Add(new ChuyenNganh
                        {
                            tenChuyenNganh = chuyennganh,
                            idNganhDaoTao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTao(nganhdaotao)
                        });
                    }
                    else
                    {
                        listChuyenNganh.Add(new ChuyenNganh
                        {
                            tenChuyenNganh = chuyennganh,
                            idNganhDaoTao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTao("Khác")
                        });
                    }
                }
            }
            foreach (var row in listChuyenNganh)
            {
                unitOfWorks.ChuyenNganhRepository.Insert(new ChuyenNganh
                {
                    tenChuyenNganh = row.tenChuyenNganh,
                    idNganhDaoTao = row.idNganhDaoTao
                });
            }
            //List<ChuyenNganh> list = new List<ChuyenNganh>();
            //foreach (var row in rows)
            //{
            //    string chuyennganh = row["chuyennganh"].ToString().Trim();
            //    if (list.Any(x => x.tenChuyenNganh == chuyennganh) == false)
            //    {
            //        list.Add(new ChuyenNganh
            //        {
            //            tenChuyenNganh = chuyennganh,
            //            idNganhDaoTao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTao(row["nganhdaotao"].ToString().Trim())
            //        });
            //    }
            //}
            //foreach (var row in list)
            //{
            //    unitOfWorks.ChuyenNganhRepository.Insert(new ChuyenNganh
            //    {
            //        tenChuyenNganh = row.tenChuyenNganh,
            //        idNganhDaoTao = row.idNganhDaoTao
            //    });
            //}
            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportNganhVienChuc()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string path = _view.TxtPathFile.Caption;
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var excelfile = new ExcelQueryFactory(path);
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["nganhdaotao"] != string.Empty && a["chuyennganh"] != string.Empty
                       select a;

            foreach(var row in rows)
            {
                string nganhthamgiaday = row["nganhthamgiaday"].ToString().Trim();
                string chuyennganh = row["chuyennganh"].ToString().Trim();
                int idvienchuc = unitOfWorks.VienChucRepository.GetIdVienChuc(row["mavienchuc"].ToString().Trim());
                int idloaihochamhocvi = unitOfWorks.LoaiHocHamHocViRepository.GetIdLoaiHocHamHocViByTenLoaiHocHamHocVi(row["trinhdo"].ToString().Trim());
                int idloainganh = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganhByNganhDaoTao(row["nganhdaotao"].ToString().Trim());
                int idnganhdaotao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTao(row["nganhdaotao"].ToString().Trim());
                int idchuyennganh = unitOfWorks.ChuyenNganhRepository.GetIdChuyenNganh(chuyennganh);
                int idnganhday = unitOfWorks.ChuyenNganhRepository.GetIdChuyenNganh(nganhthamgiaday);
                if(nganhthamgiaday != "")
                {
                    if (chuyennganh.Equals(nganhthamgiaday))
                    {
                        unitOfWorks.NganhVienChucRepository.Insert(new NganhVienChuc
                        {
                            idVienChuc = idvienchuc,
                            idLoaiNganh = idloainganh,
                            idNganhDaoTao = idnganhdaotao,
                            idChuyenNganh = idchuyennganh,
                            idHocHamHocViVienChuc = unitOfWorks.HocHamHocViVienChucRepository.GetIdHocHamHocViVienChuc(idvienchuc, idloaihochamhocvi, idloainganh, idnganhdaotao, idchuyennganh),
                            //phanLoai = 0,
                            ngayBatDau = null,
                            ngayKetThuc = null,
                            linkVanBanDinhKem = ""
                        });
                    }
                    else
                    {
                        unitOfWorks.NganhVienChucRepository.Insert(new NganhVienChuc
                        {
                            idVienChuc = idvienchuc,
                            idLoaiNganh = idloainganh,
                            idNganhDaoTao = idnganhdaotao,
                            idChuyenNganh = idchuyennganh,
                            idHocHamHocViVienChuc = unitOfWorks.HocHamHocViVienChucRepository.GetIdHocHamHocViVienChuc(idvienchuc, idloaihochamhocvi, idloainganh, idnganhdaotao, idchuyennganh),
                            phanLoai = true,
                            ngayBatDau = null,
                            ngayKetThuc = null,
                            linkVanBanDinhKem = ""
                        });
                        int _idnganhdaotao = unitOfWorks.ChuyenNganhRepository.GetIdNganhDaoTaoByIdChuyenNganh(idnganhday);
                        int _idloainganh = unitOfWorks.NganhDaoTaoRepository.GetIdLoaiNganhByIdNganhDaoTao(_idnganhdaotao);
                        if(_idnganhdaotao > 0)
                        {
                            unitOfWorks.NganhVienChucRepository.Insert(new NganhVienChuc
                            {
                                idVienChuc = idvienchuc,
                                idLoaiNganh = _idloainganh,
                                idNganhDaoTao = _idnganhdaotao,
                                idChuyenNganh = idnganhday,
                                idHocHamHocViVienChuc = unitOfWorks.HocHamHocViVienChucRepository.GetIdHocHamHocViVienChuc(idvienchuc, idloaihochamhocvi, idloainganh, idnganhdaotao, idchuyennganh),
                                phanLoai = false,
                                ngayBatDau = null,
                                ngayKetThuc = null,
                                linkVanBanDinhKem = ""
                            });
                        }                        
                    }
                }
                else
                {
                    unitOfWorks.NganhVienChucRepository.Insert(new NganhVienChuc
                    {
                        idVienChuc = idvienchuc,
                        idLoaiNganh = idloainganh,
                        idNganhDaoTao = idnganhdaotao,
                        idChuyenNganh = idchuyennganh,
                        idHocHamHocViVienChuc = unitOfWorks.HocHamHocViVienChucRepository.GetIdHocHamHocViVienChuc(idvienchuc, idloaihochamhocvi, idloainganh, idnganhdaotao, idchuyennganh),
                        phanLoai = true,
                        ngayBatDau = null,
                        ngayKetThuc = null,
                        linkVanBanDinhKem = ""
                    });
                }
            }

            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportHocHamHocViVienChuc()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string path = _view.TxtPathFile.Caption;
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var excelfile = new ExcelQueryFactory(path);
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["nganhdaotao"] != string.Empty && a["chuyennganh"] != string.Empty
                       select a;

            foreach(var row in rows)
            {
                unitOfWorks.HocHamHocViVienChucRepository.Insert(new HocHamHocViVienChuc
                {
                    idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(row["mavienchuc"].ToString().Trim()),
                    idLoaiHocHamHocVi = unitOfWorks.LoaiHocHamHocViRepository.GetIdLoaiHocHamHocViByTenLoaiHocHamHocVi(row["trinhdo"].ToString().Trim()),
                    idLoaiNganh = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganhByNganhDaoTao(row["nganhdaotao"].ToString().Trim()),
                    idNganhDaoTao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTao(row["nganhdaotao"].ToString().Trim()),
                    idChuyenNganh = unitOfWorks.ChuyenNganhRepository.GetIdChuyenNganh(row["chuyennganh"].ToString().Trim()),
                    bacHocHamHocVi = unitOfWorks.HocHamHocViVienChucRepository.AssignBacHocHamHocVi(row["trinhdo"].ToString().Trim()),
                    tenHocHamHocVi = unitOfWorks.HocHamHocViVienChucRepository.ConcatString(row["trinhdo"].ToString().Trim(), row["nganhdaotao"].ToString().Trim(), row["chuyennganh"].ToString().Trim()),
                    ngayCapBang = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["namtotnghiep"].ToString().Trim()),                    
                    coSoDaoTao = row["cosodaotao"].ToString().Trim(),
                    hinhThucDaoTao = "",                    
                    ngonNguDaoTao = "",
                    nuocCapBang = "",
                    linkVanBanDinhKem = ""
                });
            }

            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportDangHocNangCao()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string path = _view.TxtPathFile.Caption;
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var excelfile = new ExcelQueryFactory(path);
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["mavienchuc"] != string.Empty
                       select a;
            List<string> list = unitOfWorks.VienChucRepository.GetListMaVienChuc();
            foreach(var row in rows)
            {
                if(row["tenquyetdinh"].ToString().Trim() == "cao học" || row["tenquyetdinh"].ToString().Trim() == "nghiên cứu sinh")
                {
                    string mavienchuc = row["mavienchuc"].ToString().Trim();
                    if(list.Any(x => x == mavienchuc))
                    {
                        unitOfWorks.DangHocNangCaoRepository.Insert(new DangHocNangCao
                        {
                            idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                            idLoaiHocHamHocVi = unitOfWorks.LoaiHocHamHocViRepository.GetIdLoaiHocHamHocViForDangHocNangCao(row["tenquyetdinh"].ToString().Trim()),
                            soQuyetDinh = row["soquyetdinhcudihoc"].ToString().Trim(),
                            linkAnhQuyetDinh = "",
                            tenHocHamHocVi = unitOfWorks.DangHocNangCaoRepository.ConcatString(row["tenquyetdinh"].ToString().Trim(), row["chuyennganh"].ToString().Trim()),
                            loai = null,
                            coSoDaoTao = row["cosodaotao"].ToString().Trim(),
                            ngayBatDau = unitOfWorks.DangHocNangCaoRepository.ParseDatetimeMatchDatetimeDatabase(row["ngaybatdau"].ToString().Trim()),
                            ngayKetThuc = unitOfWorks.DangHocNangCaoRepository.ParseDatetimeMatchDatetimeDatabase(row["ngayketthuc"].ToString().Trim()),
                            ngonNguDaoTao = row["nuocngoai"].ToString().Trim(),
                            nuocCapBang = row["nuocngoai"].ToString().Trim(),
                            hinhThucDaoTao = row["hinhthucdaotao"].ToString().Trim()
                        });
                    }                    
                }                
            }

            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportLoaiHopDong()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string path = _view.TxtPathFile.Caption;
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var excelfile = new ExcelQueryFactory(path);
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["loaihopdong"].ToString().Trim() != string.Empty
                       select a;
            List<LoaiHopDong> listLoaiHopDong = new List<LoaiHopDong>();
            foreach (var row in rows)
            {
                string loaihopdong = row["loaihopdong"].ToString().Trim();
                if (listLoaiHopDong.Any(x => x.tenLoaiHopDong == loaihopdong) == false)
                {
                    listLoaiHopDong.Add(new LoaiHopDong
                    {
                        maLoaiHopDong = unitOfWorks.LoaiHopDongRepository.GenerateMaLoaiHopDong(loaihopdong),
                        tenLoaiHopDong = loaihopdong
                    });
                }                
            }
            foreach (var row in listLoaiHopDong)
            {
                unitOfWorks.LoaiHopDongRepository.Insert(new LoaiHopDong
                {
                    maLoaiHopDong = row.maLoaiHopDong,
                    tenLoaiHopDong = row.tenLoaiHopDong
                });
            }
            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportHopDongVienChuc()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string path = _view.TxtPathFile.Caption;
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var excelfile = new ExcelQueryFactory(path);
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["loaihopdong"].ToString().Trim() != string.Empty
                       select a;
            List<string> list = unitOfWorks.VienChucRepository.GetListMaVienChuc();
            foreach (var row in rows)
            {
                string mavienchuc = row["mavienchuc"].ToString().Trim();
                string loaihopdong = row["loaihopdong"].ToString().Trim();
                if(list.Any(x => x == mavienchuc))
                {
                    unitOfWorks.HopDongVienChucRepository.Insert(new HopDongVienChuc
                    {
                        idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                        idLoaiHopDong = unitOfWorks.LoaiHopDongRepository.GetIdLoaiHopDong(loaihopdong),
                        ngayBatDau = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["ngaybatdau"].ToString().Trim()),
                        ngayKetThuc = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["ngayketthuc"].ToString().Trim()),
                        moTa = "",
                        linkVanBanDinhKem = ""
                    });
                }                
            }

            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportLoaiChungChi()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string path = _view.TxtPathFile.Caption;
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var excelfile = new ExcelQueryFactory(path);
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       select a;
            List<LoaiChungChi> list = new List<LoaiChungChi>();
            foreach (var row in rows)
            {
                string ngoaingu = row["ngoaingu"].ToString().Trim();
                string capdongoaingu = row["capdo"].ToString().Trim();
                if(ngoaingu != string.Empty && capdongoaingu != string.Empty)
                {
                    string _ngoaingu = unitOfWorks.LoaiChungChiRepository.GetFirstChar(ngoaingu);
                    string _capdongoaingu = unitOfWorks.LoaiChungChiRepository.GetFirstChar(capdongoaingu);
                    if (list.Any(x => x.tenLoaiChungChi == _ngoaingu) == false)
                    {
                        list.Add(new LoaiChungChi
                        {
                            tenLoaiChungChi = _ngoaingu,
                        });
                    }
                }
            }
            foreach(var row in rows)
            {
                string capdotinhoc = row["tinhoc"].ToString().Trim();
                if(capdotinhoc != string.Empty)
                {
                    string _capdotinhoc = unitOfWorks.LoaiChungChiRepository.GetFirstChar(capdotinhoc);
                    if (list.Any(x => x.tenLoaiChungChi == "Tin học") == false)
                    {
                        list.Add(new LoaiChungChi
                        {
                            tenLoaiChungChi = "Tin học",
                        });
                    }
                }
            }
            foreach(var row in list)
            {
                unitOfWorks.LoaiChungChiRepository.Insert(new LoaiChungChi
                {
                    tenLoaiChungChi = row.tenLoaiChungChi,
                });
            }
            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportChungChiVienChuc()
        {
            //try
            //{

            //}
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        MessageBox.Show("Entity of type " + eve.Entry.Entity.GetType().Name + " in state " + eve.Entry.State + " has the following validation errors:", "1", MessageBoxButtons.OK);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            MessageBox.Show("- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage + "", "2", MessageBoxButtons.OK);
            //        }
            //    }
            //    throw;
            //}
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string path = _view.TxtPathFile.Caption;
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var excelfile = new ExcelQueryFactory(path);
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       select a;
            List<string> listmavienchuc = unitOfWorks.VienChucRepository.GetListMaVienChuc();
            foreach (var row in rows)
            {
                string mavienchuc = row["mavienchuc"].ToString().Trim();
                string ngoaingu = row["ngoaingu"].ToString().Trim();
                string capdongoaingu = row["capdo"].ToString().Trim();
                string capdotinhoc = row["tinhoc"].ToString().Trim();
                //string capdonvsp = row["nvsp"].ToString().Trim();
                if (listmavienchuc.Any(x => x == mavienchuc))
                {
                    if (ngoaingu != string.Empty && capdongoaingu != string.Empty)
                    {
                        string _ngoaingu = unitOfWorks.LoaiChungChiRepository.GetFirstChar(ngoaingu);
                        string _capdongoaingu = unitOfWorks.LoaiChungChiRepository.GetFirstChar(capdongoaingu);
                        unitOfWorks.ChungChiVienChucRepository.Insert(new ChungChiVienChuc
                        {
                            idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                            //idLoaiChungChi = unitOfWorks.LoaiChungChiRepository.GetIdLoaiChungChi(_ngoaingu, _capdongoaingu),
                            ghiChu = "",
                            linkVanBanDinhKem = ""
                        });
                    }
                    if (capdotinhoc != string.Empty)
                    {
                        string _capdotinhoc = unitOfWorks.LoaiChungChiRepository.GetFirstChar(capdotinhoc);
                        unitOfWorks.ChungChiVienChucRepository.Insert(new ChungChiVienChuc
                        {
                            idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                            //idLoaiChungChi = unitOfWorks.LoaiChungChiRepository.GetIdLoaiChungChi("Tin học", _capdotinhoc),
                            ghiChu = "",
                            linkVanBanDinhKem = ""
                        });
                    }
                }            
            }
            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
