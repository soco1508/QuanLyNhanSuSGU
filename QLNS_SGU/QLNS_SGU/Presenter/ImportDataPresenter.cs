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
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS_SGU.Presenter
{
    public interface IImportDataPresenter : IPresenter
    {
        void ChooseExcelFile();
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
        ExcelQueryFactory excelfile = new ExcelQueryFactory();
        string successMessage = "Nhập dữ liệu thành công.";
        public object UI => _view;
        public ImportDataPresenter(ImportDataForm view) => _view = view;

        public void Initialize()
        {
            _view.Attach(this);
            _view.XtraTabControl.SelectedTabPageIndex = 0;
        }        

        private void WriteLogToTextFile(string error, string tableName, string exception)
        {
            if (_view.CHKLog.Checked)
            {
                string timeLine = "Mốc thời gian: " + DateTime.Now.ToString();
                string[] arrDatetime = DateTime.Now.ToString().Split(' ');
                string[] arrDate = arrDatetime[0].Split('/');
                string path = "" + AppDomain.CurrentDomain.BaseDirectory + "Log\\" + arrDate[2] + "-" + arrDate[0] + "-" + arrDate[1] + ".txt";
                if (exception.Equals(successMessage))
                {                   
                    if (File.Exists(path))
                    {
                        string all = Environment.NewLine + timeLine + Environment.NewLine + tableName + Environment.NewLine + exception;
                        File.AppendAllText(path, all);
                    }
                    else
                    {
                        string all = timeLine + Environment.NewLine + tableName + Environment.NewLine + exception;
                        File.WriteAllText(path, all);
                    }
                }
                else
                {
                    
                    if (File.Exists(path))
                    {
                        string all = Environment.NewLine + timeLine + Environment.NewLine + error + Environment.NewLine + tableName + Environment.NewLine + exception;
                        File.AppendAllText(path, all);
                    }
                    else
                    {
                        string all = timeLine + Environment.NewLine + error + Environment.NewLine + tableName + Environment.NewLine + exception;
                        File.WriteAllText(path, all);
                    }
                }
            }
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
        public void ChooseExcelFile()
        {            
            _view.OpenFileDialog.FileName = string.Empty;
            _view.CbxListSheets.EditValue = "---------Chọn sheet---------";
            _view.OpenFileDialog.Filter = "Excel Files|*.xls*;*.xlsx*;*.xlsm*";
            if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
            string path = _view.OpenFileDialog.FileName;
            excelfile.FileName = string.Empty;
            excelfile.FileName = path;
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
            int count = 2;          //dữ liệu file excel bắt đầu từ dòng 2
            if (_view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                           where a["Mã thẻ VC"] != string.Empty
                           select a;
                string error = string.Empty;
                if (sheetName.Equals("VienChuc") && rows != null)
                {                    
                    foreach (var row in rows)
                    {
                        VienChuc vienChuc = null;
                        string mavienchuc = row["Mã thẻ VC"].ToString().Trim();
                        bool checkExist = unitOfWorks.VienChucRepository.CheckExist(mavienchuc);
                        if (checkExist)
                            vienChuc = unitOfWorks.VienChucRepository.GetVienChucByMaVienChuc(mavienchuc);
                        else
                            vienChuc = new VienChuc();

                        vienChuc.maVienChuc = mavienchuc;
                        vienChuc.ho = row["Họ"].ToString().Trim();
                        vienChuc.ten = row["Tên"].ToString().Trim();
                        vienChuc.sDT = row["SĐT"].ToString().Trim();
                        vienChuc.gioiTinh = unitOfWorks.VienChucRepository.ReturnGenderToDatabaseFromImport(row["Giới tính"].ToString().Trim());
                        vienChuc.ngaySinh = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["Ngày sinh"].ToString().Trim());
                        vienChuc.noiSinh = row["Nơi sinh"].ToString().Trim();
                        vienChuc.queQuan = row["Quê quán"].ToString().Trim();
                        vienChuc.idDanToc = unitOfWorks.DanTocRepository.GetIdByName(row["Dân tộc"].ToString().Trim());
                        vienChuc.idTonGiao = unitOfWorks.TonGiaoRepository.GetIdByName(row["Tôn giáo"].ToString().Trim());
                        vienChuc.hoKhauThuongTru = row["Hộ khẩu thường trú"].ToString().Trim();
                        vienChuc.noiOHienNay = row["Nơi ở hiện nay (Tạm trú)"].ToString().Trim();
                        vienChuc.ngayThamGiaCongTac = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["Ngày tham gia công tác"].ToString().Trim());
                        vienChuc.ngayVaoDang = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["Đảng viên (DB)"].ToString().Trim());
                        vienChuc.laDangVien = unitOfWorks.VienChucRepository.ReturnBoolDangVien(row["Đảng viên (DB)"].ToString().Trim());
                        vienChuc.ngayVaoNganh = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["Ngày vào ngành"].ToString().Trim());
                        vienChuc.ngayVeTruong = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["Ngày về trường"].ToString().Trim());
                        vienChuc.vanHoa = row["Văn hóa"].ToString().Trim();
 
                        string errorCol = string.Empty;
                        string errorLine = "Dòng " + count + " bị lỗi: ";
                        if (vienChuc.idDanToc == 0)
                        {
                            errorCol = "Dân tộc";
                        }
                        if (vienChuc.idTonGiao == 0)
                        {
                            if (errorCol == string.Empty) errorCol = "Tôn giáo";
                            else errorCol += ", Tôn giáo";
                        }
                        if (errorCol != string.Empty)
                            error += errorLine + errorCol + Environment.NewLine;
                        if(checkExist == false)
                            unitOfWorks.VienChucRepository.Insert(vienChuc);
                        count++;
                    }
                    try
                    {
                        unitOfWorks.Save();
                        XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        WriteLogToTextFile(error, "***Import VienChuc***", successMessage);
                    }
                    catch (Exception ex)
                    {
                        WriteLogToTextFile(error, "***Import VienChuc***", ex.InnerException.ToString());
                        XtraMessageBox.Show("Dữ liệu nhập bị lỗi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else XtraMessageBox.Show("Bạn chọn sai sheet hoặc sheet không có dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn tập tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ImportTrangThaiVienChuc()
        {
            try
            {
                if (_view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               select a;
                    List<string> listMaVienChuc = unitOfWorks.VienChucRepository.GetListMaVienChuc();
                    foreach (var row in rows)
                    {
                        string mavienchuc = row["mavienchuc"].ToString().Trim();
                        if(listMaVienChuc.Any(x => x == mavienchuc))
                        {
                            unitOfWorks.TrangThaiVienChucRepository.Insert(new TrangThaiVienChuc
                            {
                                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                                //idTrangThai = 1
                                idTrangThai = unitOfWorks.TrangThaiRepository.GetIdTrangThai(row["trangthai"].ToString().Trim()),
                                ngayBatDau = unitOfWorks.DangHocNangCaoRepository.ParseDatetimeMatchDatetimeDatabase(row["ngaybatdau"].Value.ToString().Trim()),
                                ngayKetThuc = unitOfWorks.DangHocNangCaoRepository.ParseDatetimeMatchDatetimeDatabase(row["ngayketthuc"].ToString().Trim()),
                                moTa = row["mota"].ToString().Trim(),
                                diaDiem = row["diadiem"].ToString().Trim(),
                                linkVanBanDinhKem = string.Empty
                            });
                        }
                        
                        //list.Add(row["mavienchuc"].ToString().Trim());
                        //List<string> list = unitOfWorks.VienChucRepository.GetListMaVienChuc();
                        //string mavienchuc = row["mavienchuc"].ToString().Trim();
                        //string ngaybatdau = row["ngaybatdau"].ToString().Trim();
                        //string ngayketthuc = row["ngayketthuc"].ToString().Trim();
                        //if (list.Any(x => x == mavienchuc) && ngaybatdau != string.Empty && ngayketthuc != string.Empty)
                        //{
                        //    DateTime _ngaybatdau = Convert.ToDateTime(ngaybatdau);
                        //    DateTime _ngayketthuc = Convert.ToDateTime(ngayketthuc);
                        //    string mota = row["mota"].ToString().Trim();
                        //    string diadiem = row["diadiem"].ToString().Trim();
                        //    unitOfWorks.TrangThaiVienChucRepository.Update(mavienchuc, _ngaybatdau, _ngayketthuc, mota, diadiem);
                        //}
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
                if (_view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var excelfile = new ExcelQueryFactory(_view.OpenFileDialog.FileName);
                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["Chức vụ"] != string.Empty
                               select a;
                    List<ChucVu> listChucVu = new List<ChucVu>();

                    foreach (var row in rows)
                    {
                        string chucvu = row["Chức vụ"].ToString().Trim();
                        string truongphobm = row["Trưởng, Phó bộ môn"].ToString().Trim();
                        string chucvudang = row["Chức vụ Đảng"].ToString().Trim();
                        string chucvudoanthe = row["Chức vụ Đoàn thể"].ToString().Trim();
                        if (listChucVu.Any(x => x.tenChucVu == chucvu) == false)
                        {
                            listChucVu.Add(new ChucVu
                            {
                                tenChucVu = chucvu,
                                heSoChucVu = 0,
                                idLoaiChucVu = 3
                            });
                        }
                        if (truongphobm != string.Empty && listChucVu.Any(x => x.tenChucVu == truongphobm) == false)
                        {
                            listChucVu.Add(new ChucVu
                            {
                                tenChucVu = truongphobm,
                                heSoChucVu = 0,
                                idLoaiChucVu = 3
                            });
                        }
                        if(chucvudang != string.Empty && listChucVu.Any(x => x.tenChucVu == chucvudang) == false)
                        {
                            listChucVu.Add(new ChucVu
                            {
                                tenChucVu = chucvudang,
                                heSoChucVu = 0,
                                idLoaiChucVu = 1
                            });
                        }
                        if (chucvudoanthe != string.Empty && listChucVu.Any(x => x.tenChucVu == chucvudoanthe) == false)
                        {
                            listChucVu.Add(new ChucVu
                            {
                                tenChucVu = chucvudoanthe,
                                heSoChucVu = 0,
                                idLoaiChucVu = 2
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
                if (_view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());                    
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var excelfile = new ExcelQueryFactory(_view.OpenFileDialog.FileName);
                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["Đơn vị"] != string.Empty
                               select a;
                    List<DonVi> listDonVi = new List<DonVi>();

                    foreach (var row in rows)
                    {
                        string donvi = row["Đơn vị"].ToString().Trim();
                        if (listDonVi.Any(x => x.tenDonVi == donvi) == false)
                        {
                            listDonVi.Add(new DonVi
                            {
                                tenDonVi = donvi,
                                diaDiem = string.Empty,
                                diaChi = string.Empty,
                                sDT = string.Empty,
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
                if (_view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var excelfile = new ExcelQueryFactory(_view.OpenFileDialog.FileName);
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
                if (_view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var excelfile = new ExcelQueryFactory(_view.OpenFileDialog.FileName);
                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["Mã thẻ VC"] != string.Empty
                               select a;
                    List<ChucVuDonViVienChuc> listChucVuDonViVienChuc = new List<ChucVuDonViVienChuc>();
                    foreach (var row in rows)
                    {
                        string mavienchuc = row["Mã thẻ VC"].ToString().Trim();
                        int idvienchuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc);
                        if (idvienchuc > 0)
                        {
                            bool checkNhieuChucVu = false;
                            string chucvu = row["Chức vụ"].ToString().Trim();
                            string donvi = row["Đơn vị"].ToString().Trim();
                            //string tochuyenmon = row["tochuyenmon"].ToString().Trim();
                            string truongphobm = row["Trưởng, Phó bộ môn"].ToString().Trim();
                            string chucvudang = row["Chức vụ Đảng"].ToString().Trim();
                            string chucvudoanthe = row["Chức vụ Đoàn thể"].ToString().Trim();
                            string ngaybatdau = row["Ngày bắt đầu"].ToString().Trim();
                            string ngayketthuc = row["Ngày kết thúc"].ToString().Trim();
                            string ngaybatdau1 = row["Ngày bắt đầu (1)"].ToString().Trim(); //ngày bd chức vụ trưởng phó
                            string ngayketthuc1 = row["Ngày kết thúc (1)"].ToString().Trim(); //ngày kt chức vụ trưởng phó
                                                                                              //string phanloai = row["phanloai"].ToString().Trim();

                            int iddonvi = unitOfWorks.DonViRepository.GetIdDonVi(donvi);
                            if (truongphobm != string.Empty)
                            {
                                listChucVuDonViVienChuc.Add(new ChucVuDonViVienChuc
                                {
                                    idVienChuc = idvienchuc,
                                    idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(truongphobm),
                                    idDonVi = iddonvi,
                                    idToChuyenMon = 1/*unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(donvi, tochuyenmon)*/,
                                    phanLoaiCongTac = string.Empty/*phanloai*/,
                                    checkPhanLoaiCongTac = 2, //nhiều chức vụ hiện tại
                                    ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(ngaybatdau1),
                                    ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(ngayketthuc1),
                                    linkVanBanDinhKem = null,
                                    loaiThayDoi = null,
                                    kiemNhiem = 1
                                });
                                checkNhieuChucVu = true;
                            }
                            if (chucvudang != string.Empty)
                            {
                                listChucVuDonViVienChuc.Add(new ChucVuDonViVienChuc
                                {
                                    idVienChuc = idvienchuc,
                                    idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(chucvudang),
                                    idDonVi = 1,
                                    idToChuyenMon = 1/*unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(donvi, tochuyenmon)*/,
                                    phanLoaiCongTac = string.Empty/*phanloai*/,
                                    checkPhanLoaiCongTac = 2, //nhiều chức vụ hiện tại
                                    ngayBatDau = null,
                                    ngayKetThuc = null,
                                    linkVanBanDinhKem = null,
                                    loaiThayDoi = null,
                                    kiemNhiem = 1
                                });
                                checkNhieuChucVu = true;
                            }
                            if (chucvudoanthe != string.Empty)
                            {
                                listChucVuDonViVienChuc.Add(new ChucVuDonViVienChuc
                                {
                                    idVienChuc = idvienchuc,
                                    idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(chucvudoanthe),
                                    idDonVi = 1,
                                    idToChuyenMon = 1/*unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(donvi, tochuyenmon)*/,
                                    phanLoaiCongTac = string.Empty/*phanloai*/,
                                    checkPhanLoaiCongTac = 2, //nhiều chức vụ hiện tại
                                    ngayBatDau = null,
                                    ngayKetThuc = null,
                                    linkVanBanDinhKem = null,
                                    loaiThayDoi = null,
                                    kiemNhiem = 1
                                });
                                checkNhieuChucVu = true;
                            }
                            if (checkNhieuChucVu)
                            {
                                listChucVuDonViVienChuc.Add(new ChucVuDonViVienChuc
                                {
                                    idVienChuc = idvienchuc,
                                    idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(chucvu),
                                    idDonVi = iddonvi,
                                    idToChuyenMon = 1/*unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(donvi, tochuyenmon)*/,
                                    phanLoaiCongTac = string.Empty/*phanloai*/,
                                    checkPhanLoaiCongTac = null,
                                    ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(ngaybatdau),
                                    ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(ngayketthuc),
                                    linkVanBanDinhKem = null,
                                    loaiThayDoi = null,
                                    kiemNhiem = 1
                                });
                            }
                            if (checkNhieuChucVu == false)
                            {
                                listChucVuDonViVienChuc.Add(new ChucVuDonViVienChuc
                                {
                                    idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                                    idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(chucvu),
                                    idDonVi = unitOfWorks.DonViRepository.GetIdDonVi(donvi),
                                    idToChuyenMon = 1/*unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(donvi, tochuyenmon)*/,
                                    phanLoaiCongTac = string.Empty,
                                    checkPhanLoaiCongTac = null,
                                    ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(ngaybatdau),
                                    ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(ngayketthuc),
                                    linkVanBanDinhKem = null,
                                    loaiThayDoi = null,
                                    kiemNhiem = 0
                                });
                            }
                        }
                    }
                    foreach (var item in listChucVuDonViVienChuc)
                    {
                        unitOfWorks.ChucVuDonViVienChucRepository.Insert(new ChucVuDonViVienChuc
                        {
                            idVienChuc = item.idVienChuc,
                            idChucVu = item.idChucVu,
                            idDonVi = item.idDonVi,
                            idToChuyenMon = item.idToChuyenMon/*unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(donvi, tochuyenmon)*/,
                            phanLoaiCongTac = item.phanLoaiCongTac,
                            checkPhanLoaiCongTac = item.checkPhanLoaiCongTac,
                            ngayBatDau = item.ngayBatDau,
                            ngayKetThuc = item.ngayKetThuc,
                            linkVanBanDinhKem = item.linkVanBanDinhKem,
                            loaiThayDoi = item.loaiThayDoi,
                            kiemNhiem = item.kiemNhiem
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

        public void ImportNgach()
        {
            try
            {
                if (_view.CbxListSheets.EditValue.ToString().Contains("Chọn sheet") == false)
                {
                    UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                    string sheetName = _view.CbxListSheets.EditValue.ToString();
                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["mschucdanh"] != string.Empty
                               select a;
                    List<Ngach> listNgach = new List<Ngach>();
                    foreach (var row in rows)
                    {
                        string mangach = row["mschucdanh"].ToString().Trim();
                        if (!listNgach.Any(x => x.maNgach == mangach))
                        {
                            listNgach.Add(new Ngach
                            {
                                maNgach = row["mschucdanh"].ToString().Trim(),
                                tenNgach = string.Empty,
                                heSoVuotKhungBaNamDau = null,
                                heSoVuotKhungTrenBaNam = null,
                                thoiHanNangBac = null
                            });
                            unitOfWorks.NgachRepository.Insert(new Ngach
                            {
                                maNgach = row["mschucdanh"].ToString().Trim(),
                                tenNgach = string.Empty,
                                heSoVuotKhungBaNamDau = null,
                                heSoVuotKhungTrenBaNam = null,
                                thoiHanNangBac = null
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

        public void ImportBac()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string sheetName = _view.CbxListSheets.EditValue.ToString();
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
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["mschucdanh"] != string.Empty && a["heso"] != string.Empty
                       select a;
            List<string> list = unitOfWorks.VienChucRepository.GetListMaVienChuc();
            foreach (var row in rows)
            {
                string mavienchuc = row["mavienchuc"].ToString().Trim();
                int bac = Convert.ToInt32(row["bac"].ToString().Trim());
                int idngach = unitOfWorks.NgachRepository.GetIdNgach(row["mschucdanh"].ToString().Trim());
                if (list.Any(x => x == mavienchuc))
                {
                    unitOfWorks.QuaTrinhLuongRepository.Insert(new QuaTrinhLuong
                    {
                        idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                        idBac = unitOfWorks.BacRepository.GetIdBac(bac, idngach),
                        ngayBatDau = unitOfWorks.QuaTrinhLuongRepository.ParseDatetimeMatchDatetimeDatabase(row["mocthoigian"].ToString().Trim()),
                        ngayLenLuong = null,
                        dangHuongLuong = true,
                        linkVanBanDinhKem = string.Empty
                    });
                }
            }

            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportLoaiNganh()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["Loại ngành"] != string.Empty
                       select a;
            List<string> list = new List<string>();
            foreach (var row in rows)
            {
                string loainganh = row["Loại ngành"].ToString().Trim();
                if (list.Any(x => x == loainganh) == false)
                {
                    unitOfWorks.LoaiNganhRepository.Insert(new LoaiNganh
                    {
                        tenLoaiNganh = loainganh
                    });
                    list.Add(loainganh);
                }
            }
            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportNganhDaoTao()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["Ngành đào tạo"] != string.Empty
                       select a;
            List<string> listUnique = new List<string>();
            foreach (var row in rows)
            {
                int idloainganh = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganh(row["Loại ngành"].ToString().Trim());
                if(idloainganh > 0)
                {
                    string nganhdaotao = row["Ngành đào tạo"].ToString().Trim();
                    string unique = idloainganh.ToString() + "-" + nganhdaotao;
                    if (!listUnique.Any(x => x == unique)) //check exists
                    {
                        unitOfWorks.NganhDaoTaoRepository.Insert(new NganhDaoTao
                        {
                            tenNganhDaoTao = nganhdaotao,
                            idLoaiNganh = idloainganh
                        });
                        listUnique.Add(unique);
                    }
                }
            }
            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportChuyenNganh()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["Chuyên ngành đào tạo(ChinhSua)"] != string.Empty
                       select a;
            List<string> listUnique = new List<string>();
            foreach (var row in rows)
            {
                int idnganhdaotao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTao(row["Ngành đào tạo"].ToString().Trim());
                if(idnganhdaotao > 0)
                {
                    string chuyennganh = row["Chuyên ngành đào tạo(ChinhSua)"].ToString().Trim();
                    string unique = idnganhdaotao.ToString() + "-" + chuyennganh;
                    if (!listUnique.Any(x => x == unique))
                    {
                        unitOfWorks.ChuyenNganhRepository.Insert(new ChuyenNganh
                        {
                            tenChuyenNganh = chuyennganh,
                            idNganhDaoTao = idnganhdaotao
                        });
                        listUnique.Add(unique);
                    }
                }
            }
            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportNganhVienChuc()
        {
            //UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            //string path = _view.TxtPathFile.Caption;
            //string sheetName = _view.CbxListSheets.EditValue.ToString();
            //var excelfile = new ExcelQueryFactory(path);
            //var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
            //           where a["nganhdaotao"] != string.Empty && a["chuyennganh"] != string.Empty
            //           select a;

            //foreach(var row in rows)
            //{
            //    string nganhthamgiaday = row["nganhthamgiaday"].ToString().Trim();
            //    string chuyennganh = row["chuyennganh"].ToString().Trim();
            //    int idvienchuc = unitOfWorks.VienChucRepository.GetIdVienChuc(row["mavienchuc"].ToString().Trim());
            //    int idloaihochamhocvi = unitOfWorks.LoaiHocHamHocViRepository.GetIdLoaiHocHamHocViByTenLoaiHocHamHocVi(row["trinhdo"].ToString().Trim());
            //    int idloainganh = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganhByNganhDaoTao(row["nganhdaotao"].ToString().Trim());
            //    int idnganhdaotao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTao(row["nganhdaotao"].ToString().Trim());
            //    int idchuyennganh = unitOfWorks.ChuyenNganhRepository.GetIdChuyenNganh(chuyennganh);
            //    int idnganhday = unitOfWorks.ChuyenNganhRepository.GetIdChuyenNganh(nganhthamgiaday);
            //    if(nganhthamgiaday != "")
            //    {
            //        if (chuyennganh.Equals(nganhthamgiaday))
            //        {
            //            unitOfWorks.NganhVienChucRepository.Insert(new NganhVienChuc
            //            {
            //                idVienChuc = idvienchuc,
            //                idLoaiNganh = idloainganh,
            //                idNganhDaoTao = idnganhdaotao,
            //                idChuyenNganh = idchuyennganh,
            //                idHocHamHocViVienChuc = unitOfWorks.HocHamHocViVienChucRepository.GetIdHocHamHocViVienChuc(idvienchuc, idloaihochamhocvi, idloainganh, idnganhdaotao, idchuyennganh),
            //                //phanLoai = 0,
            //                ngayBatDau = null,
            //                ngayKetThuc = null,
            //                linkVanBanDinhKem = ""
            //            });
            //        }
            //        else
            //        {
            //            unitOfWorks.NganhVienChucRepository.Insert(new NganhVienChuc
            //            {
            //                idVienChuc = idvienchuc,
            //                idLoaiNganh = idloainganh,
            //                idNganhDaoTao = idnganhdaotao,
            //                idChuyenNganh = idchuyennganh,
            //                idHocHamHocViVienChuc = unitOfWorks.HocHamHocViVienChucRepository.GetIdHocHamHocViVienChuc(idvienchuc, idloaihochamhocvi, idloainganh, idnganhdaotao, idchuyennganh),
            //                phanLoai = true,
            //                ngayBatDau = null,
            //                ngayKetThuc = null,
            //                linkVanBanDinhKem = ""
            //            });
            //            int _idnganhdaotao = unitOfWorks.ChuyenNganhRepository.GetIdNganhDaoTaoByIdChuyenNganh(idnganhday);
            //            int _idloainganh = unitOfWorks.NganhDaoTaoRepository.GetIdLoaiNganhByIdNganhDaoTao(_idnganhdaotao);
            //            if(_idnganhdaotao > 0)
            //            {
            //                unitOfWorks.NganhVienChucRepository.Insert(new NganhVienChuc
            //                {
            //                    idVienChuc = idvienchuc,
            //                    idLoaiNganh = _idloainganh,
            //                    idNganhDaoTao = _idnganhdaotao,
            //                    idChuyenNganh = idnganhday,
            //                    idHocHamHocViVienChuc = unitOfWorks.HocHamHocViVienChucRepository.GetIdHocHamHocViVienChuc(idvienchuc, idloaihochamhocvi, idloainganh, idnganhdaotao, idchuyennganh),
            //                    phanLoai = false,
            //                    ngayBatDau = null,
            //                    ngayKetThuc = null,
            //                    linkVanBanDinhKem = ""
            //                });
            //            }                        
            //        }
            //    }
            //    else
            //    {
            //        unitOfWorks.NganhVienChucRepository.Insert(new NganhVienChuc
            //        {
            //            idVienChuc = idvienchuc,
            //            idLoaiNganh = idloainganh,
            //            idNganhDaoTao = idnganhdaotao,
            //            idChuyenNganh = idchuyennganh,
            //            idHocHamHocViVienChuc = unitOfWorks.HocHamHocViVienChucRepository.GetIdHocHamHocViVienChuc(idvienchuc, idloaihochamhocvi, idloainganh, idnganhdaotao, idchuyennganh),
            //            phanLoai = true,
            //            ngayBatDau = null,
            //            ngayKetThuc = null,
            //            linkVanBanDinhKem = ""
            //        });
            //    }
            //}

            //unitOfWorks.Save();
            //XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportHocHamHocViVienChuc()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            List<int[]> listFiveArgument = new List<int[]>(); //chứa 5 tham số để import ngành học
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["Loại ngành"] != string.Empty && a["Ngành đào tạo"] != string.Empty && a["Chuyên ngành đào tạo(ChinhSua)"] != string.Empty
                       select a;

            foreach (var row in rows)
            {
                string hocham = row["Học hàm"].ToString().Trim();
                string hocvi = row["Học vị"].ToString().Trim();                
                string loainganh = row["Loại ngành"].ToString().Trim();
                string nganhdaotao = row["Ngành đào tạo"].ToString().Trim();
                string chuyennganh = row["Chuyên ngành đào tạo(ChinhSua)"].ToString().Trim();               
                string nuoccapbang = row["Nước đào tạo"].ToString().Trim();
                int idvienchuc = unitOfWorks.VienChucRepository.GetIdVienChuc(row["Mã thẻ VC"].ToString().Trim());
                if(idvienchuc > 0)
                {
                    int idloainganh = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganh(loainganh);
                    int idnganhdaotao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTao(nganhdaotao);
                    int idchuyennganh = unitOfWorks.ChuyenNganhRepository.GetIdChuyenNganh(chuyennganh);
                    int idloaihocvi = unitOfWorks.LoaiHocHamHocViRepository.GetIdLoaiHocHamHocViByTenLoaiHocHamHocVi(hocvi);
                    string tenhocvi = unitOfWorks.HocHamHocViVienChucRepository.ConcatString(hocvi, nganhdaotao, chuyennganh);

                    if (hocham != string.Empty)
                    {
                        int idloaihocham = unitOfWorks.LoaiHocHamHocViRepository.GetIdLoaiHocHamHocViByTenLoaiHocHamHocVi(hocham);
                        string tenhocham = unitOfWorks.HocHamHocViVienChucRepository.ConcatString(hocham, nganhdaotao, chuyennganh);
                        unitOfWorks.HocHamHocViVienChucRepository.Insert(new HocHamHocViVienChuc
                        {
                            idVienChuc = idvienchuc,
                            idLoaiHocHamHocVi = idloaihocham,
                            idLoaiNganh = idloainganh,
                            idNganhDaoTao = idnganhdaotao,
                            idChuyenNganh = idchuyennganh,
                            bacHocHamHocVi = unitOfWorks.HocHamHocViVienChucRepository.AssignBacHocHamHocVi(hocham),
                            tenHocHamHocVi = tenhocham,
                            ngayCapBang = unitOfWorks.HocHamHocViVienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["Năm phong"].ToString().Trim()),
                            coSoDaoTao = string.Empty,
                            hinhThucDaoTao = string.Empty,
                            ngonNguDaoTao = string.Empty,
                            nuocCapBang = nuoccapbang,
                            linkVanBanDinhKem = string.Empty
                        });
                        listFiveArgument.Add(new int[] { idvienchuc, idloaihocham, idloainganh, idnganhdaotao, idchuyennganh });
                    }
                    unitOfWorks.HocHamHocViVienChucRepository.Insert(new HocHamHocViVienChuc
                    {
                        idVienChuc = idvienchuc,
                        idLoaiHocHamHocVi = idloaihocvi,
                        idLoaiNganh = idloainganh,
                        idNganhDaoTao = idnganhdaotao,
                        idChuyenNganh = idchuyennganh,
                        bacHocHamHocVi = unitOfWorks.HocHamHocViVienChucRepository.AssignBacHocHamHocVi(hocvi),
                        tenHocHamHocVi = tenhocvi,
                        ngayCapBang = unitOfWorks.HocHamHocViVienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["Năm tốt nghiệp"].ToString().Trim()),
                        coSoDaoTao = string.Empty,
                        hinhThucDaoTao = string.Empty,
                        ngonNguDaoTao = string.Empty,
                        nuocCapBang = nuoccapbang,
                        linkVanBanDinhKem = string.Empty
                    });
                    listFiveArgument.Add(new int[] { idvienchuc, idloaihocvi, idloainganh, idnganhdaotao, idchuyennganh });
                }                
            }

            unitOfWorks.Save();

            foreach(int[] item in listFiveArgument)
            {
                unitOfWorks.NganhVienChucRepository.Insert(new NganhVienChuc
                {
                    idVienChuc = item[0],
                    idHocHamHocViVienChuc = unitOfWorks.HocHamHocViVienChucRepository.GetIdHocHamHocViVienChucByFiveArguments(item[0], item[1], item[2], item[3], item[4]),
                    idLoaiNganh = item[2],
                    idNganhDaoTao = item[3],
                    idChuyenNganh = item[4],
                    trinhDoDay = string.Empty,
                    phanLoai = true
                });
            }

            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportDangHocNangCao()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["mavienchuc"] != string.Empty
                       select a;
            List<string> list = unitOfWorks.VienChucRepository.GetListMaVienChuc();
            foreach (var row in rows)
            {
                if (row["tenquyetdinh"].ToString().Trim() == "cao học" || row["tenquyetdinh"].ToString().Trim() == "nghiên cứu sinh")
                {
                    string mavienchuc = row["mavienchuc"].ToString().Trim();
                    if (list.Any(x => x == mavienchuc))
                    {
                        unitOfWorks.DangHocNangCaoRepository.Insert(new DangHocNangCao
                        {
                            idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                            idLoaiHocHamHocVi = unitOfWorks.LoaiHocHamHocViRepository.GetIdLoaiHocHamHocViForDangHocNangCao(row["tenquyetdinh"].ToString().Trim()),
                            soQuyetDinh = row["soquyetdinhcudihoc"].ToString().Trim(),
                            linkAnhQuyetDinh = string.Empty,
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
            string sheetName = _view.CbxListSheets.EditValue.ToString();
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
                    unitOfWorks.LoaiHopDongRepository.Insert(new LoaiHopDong
                    {
                        maLoaiHopDong = unitOfWorks.LoaiHopDongRepository.GenerateMaLoaiHopDong(loaihopdong),
                        tenLoaiHopDong = loaihopdong
                    });
                }
            }

            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportHopDongVienChuc()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["loaihopdong"].ToString().Trim() != string.Empty
                       select a;
            List<string> list = unitOfWorks.VienChucRepository.GetListMaVienChuc();
            foreach (var row in rows)
            {
                string mavienchuc = row["mavienchuc"].ToString().Trim();
                string loaihopdong = row["loaihopdong"].ToString().Trim();
                if (list.Any(x => x == mavienchuc))
                {
                    unitOfWorks.HopDongVienChucRepository.Insert(new HopDongVienChuc
                    {
                        idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                        idLoaiHopDong = unitOfWorks.LoaiHopDongRepository.GetIdLoaiHopDong(loaihopdong),
                        ngayBatDau = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["ngaybatdau"].ToString().Trim()),
                        ngayKetThuc = unitOfWorks.VienChucRepository.ParseDatetimeMatchDatetimeDatabase(row["ngayketthuc"].ToString().Trim()),
                        moTa = string.Empty,
                        linkVanBanDinhKem = string.Empty
                    });
                }
            }

            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportLoaiChungChi()
        {
            
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
            string sheetName = _view.CbxListSheets.EditValue.ToString();
            var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                       where a["Mã thẻ VC"] != string.Empty
                       select a;
            List<string> listmavienchuc = unitOfWorks.VienChucRepository.GetListMaVienChuc();
            foreach (var row in rows)
            {
                string mavienchuc = row["Mã thẻ VC"].ToString().Trim();
                string ngoaingu = row["Ngoại ngữ"].ToString().Trim();
                string capdongoaingu = row["Cấp độ ngoại ngữ"].ToString().Trim();
                string capdotinhoc = row["Tin học"].ToString().Trim();
                string capdonvsp = row["NVSP"].ToString().Trim();
                string capdoqlnn = row["QLNN"].ToString().Trim();
                string capdochinhtri = row["Chính trị"].ToString().Trim();
                if (listmavienchuc.Any(x => x == mavienchuc))
                {
                    int idvienchuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc);
                    if (ngoaingu != string.Empty)
                        unitOfWorks.ChungChiVienChucRepository.InsertChungChi(idvienchuc, "NN", ngoaingu, capdongoaingu);
                    if (capdotinhoc != string.Empty)
                        unitOfWorks.ChungChiVienChucRepository.InsertChungChi(idvienchuc, "TH", "Tin học", capdotinhoc);
                    if (capdonvsp != string.Empty)
                        unitOfWorks.ChungChiVienChucRepository.InsertChungChi(idvienchuc, "CCCM", "Nghiệp vụ sư phạm", capdonvsp);
                    if (capdoqlnn != string.Empty)
                        unitOfWorks.ChungChiVienChucRepository.InsertChungChi(idvienchuc, "QLNN", "Quản lý nhà nước", capdoqlnn);
                    if (capdochinhtri != string.Empty)
                        unitOfWorks.ChungChiVienChucRepository.InsertChungChi(idvienchuc, "CT", "Chính trị", capdochinhtri);
                }
            }
            unitOfWorks.Save();
            XtraMessageBox.Show("Đã nhập dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
