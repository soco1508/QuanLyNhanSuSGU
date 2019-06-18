using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraSplashScreen;
using LinqToExcel;
using Model;
using Model.Entities;
using Model.Helper;
using QLNS_SGU.View;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

        void ImportDanToc();
        void ImportTonGiao();
        void ImportVienChuc();
        void ImportTrangThai();
        void ImportTrangThaiVienChuc();
        void ImportLoaiChucVu();
        void ImportChucVu();
        void ImportLoaiDonVi();
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
        void ImportLoaiHocHamHocVi();
        void ImportHocHamHocViVienChuc();
        void ImportDangHocNangCao();
        void ImportLoaiHopDong();
        void ImportHopDongVienChuc();
        void ImportLoaiChungChi();
        void ImportChungChiVienChuc();
    }
    public class ImportDataPresenter : IImportDataPresenter
    {
        bool checkFileNameIsEmpty = true;
        UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
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
            _view.CbxListSheets.EditValue = repositoryItem.Items[0];
            checkFileNameIsEmpty = false;
        }

        #region --Import Data To Database--
        public void ImportDanToc()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();                
                if (sheetName.Equals("VienChuc"))
                {
                    SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                    SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                    SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["mavienchuc"] != string.Empty
                               select a;
                    List<string> listTenDanToc = null;
                    if (unitOfWorks.DanTocRepository.CheckHasRows() > 0)
                        listTenDanToc = unitOfWorks.DanTocRepository.GetListTenDanToc();
                    else
                        listTenDanToc = new List<string>();
                    if (!listTenDanToc.Any(x => x == string.Empty))
                    {
                        unitOfWorks.DanTocRepository.Insert(new DanToc
                        {
                            tenDanToc = string.Empty                            
                        });
                        listTenDanToc.Add(string.Empty);
                    }
                    int line = 0;
                    int lines = rows.Count();
                    
                    foreach (var row in rows)
                    {
                        line++;
                        SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                            .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                        string tenDanToc = row["dantoc"].ToString().Trim();
                        if (!listTenDanToc.Any(x => x.ToLower() == tenDanToc.ToLower()))
                        {
                            DanToc danToc = new DanToc
                            {
                                tenDanToc = tenDanToc
                            };                            
                            unitOfWorks.DanTocRepository.Insert(danToc);
                            listTenDanToc.Add(tenDanToc);
                        }
                    }
                    try
                    {                        
                        unitOfWorks.Save();
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Import Dân tộc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Import Dân tộc không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet VienChuc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ImportTonGiao()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();                
                if (sheetName.Equals("VienChuc"))
                {
                    SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                    SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                    SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["mavienchuc"] != string.Empty
                               select a;
                    List<string> listTenTonGiao = null;
                    if (unitOfWorks.TonGiaoRepository.CheckHasRows() > 0)
                        listTenTonGiao = unitOfWorks.TonGiaoRepository.GetListTenTonGiao();
                    else
                        listTenTonGiao = new List<string>();
                    if (!listTenTonGiao.Any(x => x == string.Empty))
                    {
                        unitOfWorks.TonGiaoRepository.Insert(new TonGiao
                        {
                            tenTonGiao = string.Empty
                        });
                        listTenTonGiao.Add(string.Empty);
                    }
                    int line = 0;
                    int lines = rows.Count();
                    
                    foreach (var row in rows)
                    {
                        line++;
                        SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                            .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                        string tenTonGiao = row["tongiao"].ToString().Trim();
                        if (!listTenTonGiao.Any(x => x.ToLower() == tenTonGiao.ToLower()))
                        {
                            TonGiao tonGiao = new TonGiao
                            {
                                tenTonGiao = tenTonGiao
                            };
                            unitOfWorks.TonGiaoRepository.Insert(tonGiao);
                            listTenTonGiao.Add(tenTonGiao);
                        }
                    }

                    try
                    {
                        unitOfWorks.Save();
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Import Tôn giáo thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Import Tôn giáo không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet VienChuc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ImportVienChuc()
        {
            if (!checkFileNameIsEmpty)
            {
                int count = 2;          //dữ liệu file excel bắt đầu từ dòng 2
                string sheetName = _view.CbxListSheets.EditValue.ToString();                
                string error = string.Empty;
                if (sheetName.Equals("VienChuc"))
                {
                    if (unitOfWorks.TonGiaoRepository.CheckHasRows() > 0 && unitOfWorks.DanTocRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   where a["mavienchuc"] != string.Empty
                                   select a;
                        int line = 0;
                        int lines = rows.Count();

                        foreach (var row in rows)
                        {
                            line++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                            VienChuc vienChuc = null;
                            string mavienchuc = row["mavienchuc"].ToString().Trim();
                            int idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc);
                            if (idVienChuc > 0)
                                vienChuc = unitOfWorks.VienChucRepository.GetById(idVienChuc);
                            else
                                vienChuc = new VienChuc();

                            vienChuc.maVienChuc = mavienchuc;
                            vienChuc.ho = row["ho"].ToString().Trim();
                            vienChuc.ten = row["ten"].ToString().Trim();
                            vienChuc.sDT = row["sdt"].ToString().Trim();
                            vienChuc.gioiTinh = unitOfWorks.VienChucRepository.ReturnGenderToDatabaseFromImport(row["gioitinh"].ToString().Trim());
                            vienChuc.ngaySinh = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngaysinh"].ToString().Trim());
                            vienChuc.noiSinh = row["noisinh"].ToString().Trim();
                            vienChuc.queQuan = row["quequan"].ToString().Trim();
                            vienChuc.idDanToc = unitOfWorks.DanTocRepository.GetIdByName(row["dantoc"].ToString().Trim());
                            vienChuc.idTonGiao = unitOfWorks.TonGiaoRepository.GetIdByName(row["tongiao"].ToString().Trim());
                            vienChuc.hoKhauThuongTru = row["hokhauthuongtru"].ToString().Trim();
                            vienChuc.noiOHienNay = row["noiohiennay(tamtru)"].ToString().Trim();
                            vienChuc.ngayThamGiaCongTac = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngaythamgiacongtac"].ToString().Trim());
                            vienChuc.ngayVaoDang = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["dangvien(db)"].ToString().Trim());
                            vienChuc.laDangVien = unitOfWorks.VienChucRepository.ReturnBoolDangVien(row["dangvien(db)"].ToString().Trim());
                            vienChuc.ngayVaoNganh = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngayvaonganh"].ToString().Trim());
                            vienChuc.ngayVeTruong = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngayvetruong"].ToString().Trim());
                            vienChuc.vanHoa = row["vanhoa"].ToString().Trim();
                            vienChuc.ngayTuyenDungChinhThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngaytuyendungchinhthuc"].ToString().Trim());
                            vienChuc.email = row["email"].ToString().Trim();
                            vienChuc.gioChuan = row["giochuan"].ToString().Trim() != string.Empty ? int.Parse(row["giochuan"].ToString().Trim()) : 0;

                            string errorCol = string.Empty;
                            string errorLine = "Dòng " + count + " bị lỗi: ";
                            if (vienChuc.idDanToc == 0)
                            {
                                errorCol = "dantoc";
                            }
                            if (vienChuc.idTonGiao == 0)
                            {
                                if (errorCol == string.Empty) errorCol = "tongiao";
                                else errorCol += ", tongiao";
                            }
                            if (errorCol != string.Empty)
                                error += errorLine + errorCol + Environment.NewLine;
                            if (idVienChuc <= 0)
                                unitOfWorks.VienChucRepository.Insert(vienChuc);
                            count++;
                        }
                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Viên chức thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            WriteLogToTextFile(error, "***Import VienChuc***", successMessage);
                        }
                        catch (Exception ex)
                        {
                            SplashScreenManager.CloseForm(false);
                            WriteLogToTextFile(error, "***Import VienChuc***", ex.InnerException.ToString());
                            XtraMessageBox.Show("Import Viên chức không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Dân tộc, Tôn giáo trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet VienChuc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ImportTrangThai()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
            SplashScreenManager.Default.SetWaitFormDescription("0.0%");

            List<string> listTenTrangThai = null;
            if (unitOfWorks.TrangThaiRepository.CheckHasRows() > 0)
                listTenTrangThai = unitOfWorks.TrangThaiRepository.GetListTenTrangThai();
            else
                listTenTrangThai = new List<string>();
            if(!listTenTrangThai.Any(x => x == string.Empty))
            {
                unitOfWorks.TrangThaiRepository.Insert(new TrangThai { tenTrangThai = string.Empty });
                listTenTrangThai.Add(string.Empty);
            }
            string[] arrTenTrangThai = { "Đang làm", "Đi nước ngoài" };
            for (int i = 0; i < arrTenTrangThai.Length; i++)
            {
                if (!listTenTrangThai.Any(x => x.ToLower() == arrTenTrangThai[i].ToLower()))
                {
                    unitOfWorks.TrangThaiRepository.Insert(new TrangThai
                    {
                        tenTrangThai = arrTenTrangThai[i]
                    });
                    listTenTrangThai.Add(arrTenTrangThai[i]);
                }
            }

            unitOfWorks.Save();
            SplashScreenManager.Default.SetWaitFormDescription("100%");
            SplashScreenManager.CloseForm(false);
            XtraMessageBox.Show("Import Trạng thái thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportTrangThaiVienChuc()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("TrangThai"))
                {
                    if(unitOfWorks.TrangThaiRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");
                        
                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   where a["mavienchuc"] != string.Empty
                                   select a;
                        List<string> listMaVienChuc = unitOfWorks.VienChucRepository.GetListMaVienChuc();
                        int line = 0;
                        int lines = rows.Count();

                        foreach (var row in rows)
                        {
                            line++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                            .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                            string mavienchuc = row["mavienchuc"].ToString().Trim();
                            if (listMaVienChuc.Any(x => x == mavienchuc))
                            {
                                int idTrangThai = unitOfWorks.TrangThaiRepository.GetIdTrangThai(row["trangthai"].ToString().Trim());
                                if (idTrangThai > 0)
                                {
                                    unitOfWorks.TrangThaiVienChucRepository.Insert(new TrangThaiVienChuc
                                    {
                                        idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                                        idTrangThai = idTrangThai,
                                        ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngaybatdau"].ToString().Trim()),
                                        ngayKetThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngayketthuc"].ToString().Trim()),
                                        moTa = row["mota"].ToString().Trim(),
                                        diaDiem = row["diadiem"].ToString().Trim(),
                                        linkVanBanDinhKem = string.Empty
                                    });
                                }
                            }
                        }

                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Trạng thái-Viên chức thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Trạng thái-Viên chức không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Trạng thái trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet TrangThai.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);                        
        }

        public void ImportLoaiChucVu()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
            SplashScreenManager.Default.SetWaitFormDescription("0.0%");

            List<string> listTenLoaiChucVu = null;
            if (unitOfWorks.LoaiChucVuRepository.CheckHasRows() > 0)
                listTenLoaiChucVu = unitOfWorks.LoaiChucVuRepository.GetListTenLoaiChucVu();
            else
                listTenLoaiChucVu = new List<string>();

            string[] arrTenLoaiChucVu = { "Đảng", "Chính quyền", "Đoàn" };            
            for(int i = 0; i < arrTenLoaiChucVu.Length; i++)
            {
                if(!listTenLoaiChucVu.Any(x => x.ToLower() == arrTenLoaiChucVu[i].ToLower()))
                {
                    unitOfWorks.LoaiChucVuRepository.Insert(new LoaiChucVu
                    {
                        tenLoaiChucVu = arrTenLoaiChucVu[i]
                    });
                    listTenLoaiChucVu.Add(arrTenLoaiChucVu[i]);
                }
            }
            
            unitOfWorks.Save();
            SplashScreenManager.Default.SetWaitFormDescription("100%");
            SplashScreenManager.CloseForm(false);
            XtraMessageBox.Show("Import Loại chức vụ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportChucVu()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("ChucVuDonVi"))
                {
                    if (unitOfWorks.LoaiChucVuRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   where a["chucvu"] != string.Empty
                                   select a;
                        List<string> listTenChucVu = null;
                        if (unitOfWorks.ChucVuRepository.CheckHasRows() > 0)
                            listTenChucVu = unitOfWorks.ChucVuRepository.GetListTenChucVu();
                        else
                            listTenChucVu = new List<string>();
                        int line = 0;
                        int lines = rows.Count();

                        foreach (var row in rows)
                        {
                            line++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                            string chucvu = row["chucvu"].ToString().Trim();
                            double? hesochucvu = row["hesochucvu"].ToString().Trim() != string.Empty ? double.Parse(row["hesochucvu"].ToString().Trim()) : 0;
                            //string truongphobm = row["Trưởng, Phó bộ môn"].ToString().Trim();
                            string chucvudang = row["chucvudang"].ToString().Trim();
                            string chucvudoanthe = row["chucvudoanthe"].ToString().Trim();
                            if (chucvu != string.Empty && !listTenChucVu.Any(x => x.ToLower() == chucvu.ToLower()))
                            {
                                unitOfWorks.ChucVuRepository.Insert(new ChucVu
                                {
                                    tenChucVu = chucvu,
                                    heSoChucVu = hesochucvu,
                                    idLoaiChucVu = unitOfWorks.LoaiChucVuRepository.GetIdLoaiChucVuByName("Chính quyền")
                                });
                                listTenChucVu.Add(chucvu);
                            }
                            //if (truongphobm != string.Empty && listChucVu.Any(x => x.tenChucVu == truongphobm) == false)
                            //{
                            //    listChucVu.Add(new ChucVu
                            //    {
                            //        tenChucVu = truongphobm,
                            //        heSoChucVu = 0,
                            //        idLoaiChucVu = 3
                            //    });
                            //}
                            if (chucvudang != string.Empty && !listTenChucVu.Any(x => x.ToLower() == chucvudang.ToLower()))
                            {
                                unitOfWorks.ChucVuRepository.Insert(new ChucVu
                                {
                                    tenChucVu = chucvudang,
                                    heSoChucVu = 0,
                                    idLoaiChucVu = unitOfWorks.LoaiChucVuRepository.GetIdLoaiChucVuByName("Đảng")
                                });
                                listTenChucVu.Add(chucvudang);
                            }
                            if (chucvudoanthe != string.Empty && !listTenChucVu.Any(x => x.ToLower() == chucvudoanthe.ToLower()))
                            {
                                unitOfWorks.ChucVuRepository.Insert(new ChucVu
                                {
                                    tenChucVu = chucvudoanthe,
                                    heSoChucVu = 0,
                                    idLoaiChucVu = unitOfWorks.LoaiChucVuRepository.GetIdLoaiChucVuByName("Đoàn")
                                });
                                listTenChucVu.Add(chucvudoanthe);
                            }
                        }

                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Chức vụ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Chức vụ không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Loại chức vụ trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet ChucVuDonVi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void ImportLoaiDonVi()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            SplashScreenManager.Default.SetWaitFormCaption("Đang import......");

            List<string> listTenLoaiDonVi = null;
            if (unitOfWorks.LoaiDonViRepository.CheckHasRows() > 0)
                listTenLoaiDonVi = unitOfWorks.LoaiDonViRepository.GetListTenLoaiDonVi();
            else
                listTenLoaiDonVi = new List<string>();

            string[] arrTenLoaiDonVi = { "Phòng", "Khoa", "Trung tâm", "Viện", "Ban", "Khác" };
            for(int i = 0; i < arrTenLoaiDonVi.Length; i++)
            {
                if(!listTenLoaiDonVi.Any(x => x.ToLower() == arrTenLoaiDonVi[i].ToLower()))
                {
                    unitOfWorks.LoaiDonViRepository.Insert(new LoaiDonVi
                    {
                        tenLoaiDonVi = arrTenLoaiDonVi[i]
                    });
                    listTenLoaiDonVi.Add(arrTenLoaiDonVi[i]);
                }
            }
            unitOfWorks.Save();
            SplashScreenManager.CloseForm(false);
            XtraMessageBox.Show("Import Loại đơn vị thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportDonVi()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("ChucVuDonVi"))
                {
                    if (unitOfWorks.LoaiDonViRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   select a;
                        List<string> listTenDonVi = null;
                        if (unitOfWorks.DonViRepository.CheckHasRows() > 0)
                            listTenDonVi = unitOfWorks.DonViRepository.GetListTenDonVi();
                        else
                            listTenDonVi = new List<string>();
                        if (!listTenDonVi.Any(x => x == string.Empty))
                        {
                            unitOfWorks.DonViRepository.Insert(new DonVi
                            {
                                tenDonVi = string.Empty,
                                diaDiem = string.Empty,
                                diaChi = string.Empty,
                                sDT = string.Empty,
                                idLoaiDonVi = unitOfWorks.LoaiDonViRepository.GetIdLoaiDonVi(string.Empty)
                            });
                            listTenDonVi.Add(string.Empty);
                        }
                        int line = 0;
                        int lines = rows.Count();

                        foreach (var row in rows)
                        {
                            line++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                                .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                            string donvi = row["donvi"].ToString().Trim();
                            string diadiem = row["diadiem"].ToString().Trim();
                            
                            if (!listTenDonVi.Any(x => x.ToLower() == donvi.ToLower()))
                            {
                                unitOfWorks.DonViRepository.Insert(new DonVi
                                {
                                    tenDonVi = donvi,
                                    diaDiem = diadiem,
                                    diaChi = string.Empty,
                                    sDT = string.Empty,
                                    idLoaiDonVi = unitOfWorks.LoaiDonViRepository.GetIdLoaiDonVi(donvi)
                                });
                                listTenDonVi.Add(donvi);
                            }
                        }

                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Đơn vị thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Đơn vị không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Loại đơn vị trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet ChucVuDonVi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ImportToChuyenMon()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("ChucVuDonVi"))
                {
                    SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                    SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                    SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               select a;

                    List<string> listTenToChuyenMon = null;
                    if (unitOfWorks.ToChuyenMonRepository.CheckHasRows() > 0)
                        listTenToChuyenMon = unitOfWorks.ToChuyenMonRepository.GetListTenToChuyenMon();
                    else
                        listTenToChuyenMon = new List<string>();
                    int line = 0;
                    int lines = rows.Count();

                    foreach (var row in rows)
                    {
                        line++;
                        SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                            .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                        //string donvi = row["donvi"].ToString().Trim();
                        string tochuyenmon = row["tochuyenmon"].ToString().Trim();
                        string temp = tochuyenmon;

                        if (!listTenToChuyenMon.Any(x => x.ToLower() == temp.ToLower()))
                        {
                            unitOfWorks.ToChuyenMonRepository.Insert(new ToChuyenMon
                            {
                                tenToChuyenMon = tochuyenmon
                            });
                            listTenToChuyenMon.Add(temp);
                        }
                    }

                    try
                    {
                        unitOfWorks.Save();
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Import Tổ chuyên môn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Import Tổ chuyên môn không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet ChucVuDonVi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ImportChucVuDonViVienChuc()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("ChucVuDonVi"))
                {
                    if (unitOfWorks.VienChucRepository.CheckHasRows() > 0 && unitOfWorks.ChucVuRepository.CheckHasRows() > 0
                        && unitOfWorks.DonViRepository.CheckHasRows() > 0 && unitOfWorks.ToChuyenMonRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   where a["mavienchuc"] != string.Empty
                                   select a;
                        int line = 0;
                        int lines = rows.Count();
                        foreach (var row in rows)
                        {
                            line++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                            string mavienchuc = row["mavienchuc"].ToString().Trim();
                            int idvienchuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc);
                            if (idvienchuc > 0)
                            {
                                bool checkNhieuChucVu = false;
                                string chucvu = row["chucvu"].ToString().Trim();
                                string donvi = row["donvi"].ToString().Trim();
                                string diadiem = row["diadiem"].ToString().Trim();
                                string tochuyenmon = row["tochuyenmon"].ToString().Trim();
                                //string truongphobm = row["truongphobomon"].ToString().Trim();
                                string chucvudang = row["chucvudang"].ToString().Trim();
                                string chucvudoanthe = row["chucvudoanthe"].ToString().Trim();
                                string ngaybatdau = row["ngaybatdau"].ToString().Trim();
                                string ngayketthuc = row["ngayketthuc"].ToString().Trim();
                                //string ngaybatdau1 = row["ngaybatdau(1)"].ToString().Trim(); //ngày bd chức vụ trưởng phó
                                //string ngayketthuc1 = row["ngayketthuc(1)"].ToString().Trim(); //ngày kt chức vụ trưởng phó
                                string phanloai = row["phanloai"].ToString().Trim();

                                int iddonvi = unitOfWorks.DonViRepository.GetIdDonVi(donvi);
                                int idtochuyenmon = unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(tochuyenmon);

                                //if (truongphobm != string.Empty)
                                //{
                                //    unitOfWorks.ChucVuDonViVienChucRepository.Insert(new ChucVuDonViVienChuc
                                //    {
                                //        idVienChuc = idvienchuc,
                                //        idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(truongphobm),
                                //        idDonVi = iddonvi,
                                //        idToChuyenMon = 1/*unitOfWorks.ToChuyenMonRepository.GetIdToChuyenMon(donvi, tochuyenmon)*/,
                                //        phanLoaiCongTac = string.Empty/*phanloai*/,
                                //        checkPhanLoaiCongTac = 2, //nhiều chức vụ hiện tại
                                //        ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(ngaybatdau1),
                                //        ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(ngayketthuc1),
                                //        linkVanBanDinhKem = null,
                                //        loaiThayDoi = null,
                                //        kiemNhiem = 1
                                //    });
                                //    checkNhieuChucVu = true;
                                //}
                                if (chucvudang != string.Empty)
                                {
                                    unitOfWorks.ChucVuDonViVienChucRepository.Insert(new ChucVuDonViVienChuc
                                    {
                                        idVienChuc = idvienchuc,
                                        idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(chucvudang),
                                        idDonVi = iddonvi,
                                        idToChuyenMon = idtochuyenmon,
                                        phanLoaiCongTac = string.Empty,//chucvudang ko co phan loai
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
                                    unitOfWorks.ChucVuDonViVienChucRepository.Insert(new ChucVuDonViVienChuc
                                    {
                                        idVienChuc = idvienchuc,
                                        idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(chucvudoanthe),
                                        idDonVi = iddonvi,
                                        idToChuyenMon = idtochuyenmon,
                                        phanLoaiCongTac = string.Empty,//chucvudoan ko co phan loai
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
                                    unitOfWorks.ChucVuDonViVienChucRepository.Insert(new ChucVuDonViVienChuc
                                    {
                                        idVienChuc = idvienchuc,
                                        idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(chucvu),
                                        idDonVi = iddonvi,
                                        idToChuyenMon = idtochuyenmon,
                                        phanLoaiCongTac = phanloai,
                                        checkPhanLoaiCongTac = 2,
                                        ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(ngaybatdau),
                                        ngayKetThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(ngayketthuc),
                                        linkVanBanDinhKem = null,
                                        loaiThayDoi = null,
                                        kiemNhiem = 1
                                    });
                                }
                                if (!checkNhieuChucVu)
                                {
                                    unitOfWorks.ChucVuDonViVienChucRepository.Insert(new ChucVuDonViVienChuc
                                    {
                                        idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                                        idChucVu = unitOfWorks.ChucVuRepository.GetIdChucVuByTenChucVu(chucvu),
                                        idDonVi = iddonvi,
                                        idToChuyenMon = idtochuyenmon,
                                        phanLoaiCongTac = phanloai,
                                        checkPhanLoaiCongTac = null,
                                        ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(ngaybatdau),
                                        ngayKetThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(ngayketthuc),
                                        linkVanBanDinhKem = null,
                                        loaiThayDoi = null,
                                        kiemNhiem = 0
                                    });
                                }
                            }
                        }

                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Quá trình công tác thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Quá trình công tác không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Viên chức, Chức vụ, Đơn vị, Tổ chuyên môn trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet ChucVuDonVi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ImportNgach()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("QuaTrinhLuong"))
                {
                    SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                    SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                    SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["masochucdanh"] != string.Empty
                               select a;
                    List<string> listMaNgach = null;
                    if (unitOfWorks.NgachRepository.CheckHasRows() > 0)
                        listMaNgach = unitOfWorks.NgachRepository.GetListMaNgach();
                    else
                        listMaNgach = new List<string>();
                    int line = 0;
                    int lines = rows.Count();

                    foreach (var row in rows)
                    {
                        line++;
                        SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                            .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                        string mangach = row["masochucdanh"].ToString().Trim();
                        string tenngach = row["tenngach"].ToString().Trim();
                        if (!listMaNgach.Any(x => x == mangach))
                        {
                            unitOfWorks.NgachRepository.Insert(new Ngach
                            {
                                maNgach = mangach,
                                tenNgach = tenngach,
                                heSoVuotKhungBaNamDau = null,
                                heSoVuotKhungTrenBaNam = null,
                                thoiHanNangBac = null
                            });
                            listMaNgach.Add(mangach);
                        }
                    }

                    try
                    {
                        unitOfWorks.Save();
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Import Ngạch thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Import Ngạch không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet QuaTrinhLuong.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ImportBac()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("QuaTrinhLuong"))
                {
                    if(unitOfWorks.NgachRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   where a["bac"] != string.Empty && a["masochucdanh"] != string.Empty
                                   select a;
                        List<string> listBacVaNgach = null;
                        if (unitOfWorks.BacRepository.CheckHasRows() > 0)
                            listBacVaNgach = unitOfWorks.BacRepository.GetListBacVaNgach();
                        else
                            listBacVaNgach = new List<string>();
                        int line = 0;
                        int lines = rows.Count();

                        foreach (var row in rows)
                        {
                            line++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                            .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                            bool bacIsNumeric = int.TryParse(row["bac"].ToString().Trim(), out int bac);
                            if (!bacIsNumeric)
                            {
                                XtraMessageBox.Show("Cột Bậc có chứa chữ. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            double hesoluong = unitOfWorks.BacRepository.ParseHeSoBacToDouble(row["hesoluong"].ToString().Trim());
                            string mangach = row["masochucdanh"].ToString().Trim();
                            string temp = bac + mangach;
                            if (!listBacVaNgach.Any(x => x == temp))
                            {
                                unitOfWorks.BacRepository.Insert(new Bac
                                {
                                    bac1 = Convert.ToInt32(bac),
                                    heSoBac = hesoluong,
                                    idNgach = unitOfWorks.NgachRepository.GetIdNgach(row["masochucdanh"].ToString().Trim())
                                });
                                listBacVaNgach.Add(temp);
                            }
                        }

                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Bậc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Bậc không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Ngạch trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet QuaTrinhLuong.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ImportQuaTrinhLuong()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("QuaTrinhLuong"))
                {
                    if (unitOfWorks.VienChucRepository.CheckHasRows() > 0 && unitOfWorks.BacRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   where a["masochucdanh"] != string.Empty && a["bac"] != string.Empty
                                   select a;
                        List<string> list = unitOfWorks.VienChucRepository.GetListMaVienChuc();
                        int line = 0;
                        int lines = rows.Count();

                        foreach (var row in rows)
                        {
                            line++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                            .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                            string mavienchuc = row["mavienchuc"].ToString().Trim();
                            bool bacIsNumeric = int.TryParse(row["bac"].ToString().Trim(), out int bac);
                            if (!bacIsNumeric)
                            {
                                XtraMessageBox.Show("Cột Bậc có chứa chữ. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                            int idngach = unitOfWorks.NgachRepository.GetIdNgach(row["masochucdanh"].ToString().Trim());
                            if (list.Any(x => x == mavienchuc))
                            {
                                unitOfWorks.QuaTrinhLuongRepository.Insert(new QuaTrinhLuong
                                {
                                    idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                                    idBac = unitOfWorks.BacRepository.GetIdBac(bac, idngach),
                                    ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngaybatdau"].ToString().Trim()),
                                    ngayLenLuong = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngaylenluong"].ToString().Trim()),
                                    dangHuongLuong = true,
                                    linkVanBanDinhKem = string.Empty
                                });
                            }
                        }

                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Bậc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Bậc không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Viên chức, Bậc trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet QuaTrinhLuong.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }

        public void ImportLoaiNganh()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("Nganh_HHHV"))
                {
                    SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                    SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                    SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()                               
                               select a;
                    List<string> listTenLoaiNganh = null;
                    if (unitOfWorks.LoaiNganhRepository.CheckHasRows() > 0)
                        listTenLoaiNganh = unitOfWorks.LoaiNganhRepository.GetListTenLoaiNganh();
                    else
                        listTenLoaiNganh = new List<string>();
                    if (!listTenLoaiNganh.Any(x => x == string.Empty)) // insert 1 dong empty cho nhung truong hop dac biet
                    {
                        unitOfWorks.LoaiNganhRepository.Insert(new LoaiNganh { tenLoaiNganh = string.Empty });
                        listTenLoaiNganh.Add(string.Empty);
                    }
                    int line = 0;
                    int lines = rows.Count();

                    foreach (var row in rows)
                    {
                        line++;
                        SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                            .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                        string loainganh = row["loainganh"].ToString().Trim();
                        if (!listTenLoaiNganh.Any(x => x.ToLower() == loainganh.ToLower()))
                        {
                            unitOfWorks.LoaiNganhRepository.Insert(new LoaiNganh
                            {
                                tenLoaiNganh = loainganh
                            });
                            listTenLoaiNganh.Add(loainganh);
                        }
                    }
                    try
                    {
                        unitOfWorks.Save();
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Import Loại ngành thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Import Loại ngành thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet Nganh_HHHV.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);          
        }

        public void ImportNganhDaoTao()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("Nganh_HHHV"))
                {
                    if(unitOfWorks.LoaiNganhRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   select a;
                        List<string> listTenNganhDaoTaoVaLoaiNganh = null;
                        if (unitOfWorks.NganhDaoTaoRepository.CheckHasRows() > 0)
                            listTenNganhDaoTaoVaLoaiNganh = unitOfWorks.NganhDaoTaoRepository.GetListTenNganhDaoTaoVaLoaiNganh();
                        else
                            listTenNganhDaoTaoVaLoaiNganh = new List<string>();
                        if (!listTenNganhDaoTaoVaLoaiNganh.Any(x => x == string.Empty))
                        {
                            unitOfWorks.NganhDaoTaoRepository.Insert(new NganhDaoTao
                            {
                                tenNganhDaoTao = string.Empty,
                                idLoaiNganh = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganh(string.Empty)
                            });
                            listTenNganhDaoTaoVaLoaiNganh.Add(string.Empty);
                        }
                        int line = 0;
                        int lines = rows.Count();

                        foreach (var row in rows)
                        {
                            line++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                                .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                            string loainganh = row["loainganh"].ToString().Trim();
                            int idloainganh = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganh(loainganh);
                            string nganhdaotao = row["nganhdaotao"].ToString().Trim();
                            string temp = nganhdaotao + loainganh;
                            if (!listTenNganhDaoTaoVaLoaiNganh.Any(x => x.ToLower() == temp.ToLower())) // x <=> nganhdaotao + loainganh
                            {
                                unitOfWorks.NganhDaoTaoRepository.Insert(new NganhDaoTao
                                {
                                    tenNganhDaoTao = nganhdaotao,
                                    idLoaiNganh = idloainganh
                                });
                                listTenNganhDaoTaoVaLoaiNganh.Add(temp);
                            }
                        }

                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Ngành đào tạo thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Ngành đào tạo không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Loại ngành trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet Nganh_HHHV.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);         
        }

        public void ImportChuyenNganh()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("Nganh_HHHV"))
                {
                    if (unitOfWorks.NganhDaoTaoRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   select a;
                        List<string> listTenChuyenNganhVaNganhDaoTao = null;
                        if (unitOfWorks.ChuyenNganhRepository.CheckHasRows() > 0)
                            listTenChuyenNganhVaNganhDaoTao = unitOfWorks.ChuyenNganhRepository.GetListTenChuyenNganhVaNganhDaoTao();
                        else
                            listTenChuyenNganhVaNganhDaoTao = new List<string>();
                        if (!listTenChuyenNganhVaNganhDaoTao.Any(x => x == string.Empty))
                        {
                            unitOfWorks.ChuyenNganhRepository.Insert(new ChuyenNganh
                            {
                                tenChuyenNganh = string.Empty,
                                idNganhDaoTao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTao(string.Empty)
                            });
                            listTenChuyenNganhVaNganhDaoTao.Add(string.Empty);
                        }
                        int line = 0;
                        int lines = rows.Count();

                        foreach (var row in rows)
                        {
                            line++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                                .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                            string nganhdaotao = row["nganhdaotao"].ToString().Trim();
                            int idnganhdaotao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTao(nganhdaotao);
                            string chuyennganh = row["chuyennganh"].ToString().Trim();
                            string temp = chuyennganh + nganhdaotao;
                            if (!listTenChuyenNganhVaNganhDaoTao.Any(x => x.ToLower() == temp.ToLower())) //x <=> chuyennganh + nganhdaotao
                            {
                                unitOfWorks.ChuyenNganhRepository.Insert(new ChuyenNganh
                                {
                                    tenChuyenNganh = chuyennganh,
                                    idNganhDaoTao = idnganhdaotao
                                });
                                listTenChuyenNganhVaNganhDaoTao.Add(temp);
                            }
                        }

                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Chuyên ngành thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Chuyên ngành không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Ngành đào tạo trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet Nganh_HHHV.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);          
        }

        public void ImportNganhVienChuc()
        {
            
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

        public void ImportLoaiHocHamHocVi()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
            SplashScreenManager.Default.SetWaitFormDescription("0.0%");

            List<string> listTenLoaiHocHamHocVi = null;
            if (unitOfWorks.LoaiHocHamHocViRepository.CheckHasRows() > 0)
                listTenLoaiHocHamHocVi = unitOfWorks.LoaiHocHamHocViRepository.GetListTenLoaiHocHamHocVi();
            else
                listTenLoaiHocHamHocVi = new List<string>();
            int[] arrPhanCap = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            string[] arrTenLoaiHocHamHocVi = { "Khác", "Phổ thông" , "Trung cấp", "Cao đẳng", "Đại học", "Thạc sĩ", "Tiến sĩ", "Phó giáo sư", "Giáo sư" };
            for (int i = 0; i < arrTenLoaiHocHamHocVi.Length; i++)
            {
                if (!listTenLoaiHocHamHocVi.Any(x => x.ToLower() == arrTenLoaiHocHamHocVi[i].ToLower()))
                {
                    unitOfWorks.LoaiHocHamHocViRepository.Insert(new LoaiHocHamHocVi
                    {
                        tenLoaiHocHamHocVi = arrTenLoaiHocHamHocVi[i],
                        phanCap = arrPhanCap[i]
                    });
                    listTenLoaiHocHamHocVi.Add(arrTenLoaiHocHamHocVi[i]);
                }
            }

            unitOfWorks.Save();
            SplashScreenManager.Default.SetWaitFormDescription("100%");
            SplashScreenManager.CloseForm(false);
            XtraMessageBox.Show("Import Loại học hàm học vị thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportHocHamHocViVienChuc()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("Nganh_HHHV"))
                {
                    if(unitOfWorks.LoaiHocHamHocViRepository.CheckHasRows() > 0 && unitOfWorks.VienChucRepository.CheckHasRows() > 0
                        && unitOfWorks.LoaiNganhRepository.CheckHasRows() > 0 && unitOfWorks.NganhDaoTaoRepository.CheckHasRows() > 0
                        && unitOfWorks.ChuyenNganhRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                        List<int[]> listFiveArgument = new List<int[]>(); //chứa 5 tham số để import ngành học
                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   select a;
                        int lineHHHV = 0;
                        int linesHHHV = rows.Count();

                        foreach (var row in rows)
                        {
                            lineHHHV++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                                .InvariantCulture, "{0:0.0}", (lineHHHV * 1.0 / linesHHHV) * 50) + "%");
                            string hocham = row["hocham"].ToString().Trim();
                            string hocvi = row["hocvi"].ToString().Trim();
                            string loainganh = row["loainganh"].ToString().Trim();
                            string nganhdaotao = row["nganhdaotao"].ToString().Trim();
                            string chuyennganh = row["chuyennganh"].ToString().Trim();
                            string nuoccapbang = row["nuocdaotao"].ToString().Trim();
                            int idvienchuc = unitOfWorks.VienChucRepository.GetIdVienChuc(row["mavienchuc"].ToString().Trim());
                            if (idvienchuc > 0)
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
                                        tenHocHamHocVi = tenhocham,
                                        ngayCapBang = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["namphong"].ToString().Trim()),
                                        coSoDaoTao = string.Empty,
                                        hinhThucDaoTao = string.Empty,
                                        ngonNguDaoTao = string.Empty,
                                        nuocCapBang = nuoccapbang,
                                        linkVanBanDinhKem = string.Empty
                                    });
                                    listFiveArgument.Add(new int[] { idvienchuc, idloaihocham, idloainganh, idnganhdaotao, idchuyennganh });
                                }
                                if (hocvi != string.Empty)
                                {
                                    unitOfWorks.HocHamHocViVienChucRepository.Insert(new HocHamHocViVienChuc
                                    {
                                        idVienChuc = idvienchuc,
                                        idLoaiHocHamHocVi = idloaihocvi,
                                        idLoaiNganh = idloainganh,
                                        idNganhDaoTao = idnganhdaotao,
                                        idChuyenNganh = idchuyennganh,
                                        tenHocHamHocVi = tenhocvi,
                                        ngayCapBang = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["namtotnghiep"].ToString().Trim()),
                                        coSoDaoTao = string.Empty,
                                        hinhThucDaoTao = string.Empty,
                                        ngonNguDaoTao = string.Empty,
                                        nuocCapBang = nuoccapbang,
                                        linkVanBanDinhKem = string.Empty
                                    });
                                    listFiveArgument.Add(new int[] { idvienchuc, idloaihocvi, idloainganh, idnganhdaotao, idchuyennganh });
                                }
                            }
                        }

                        try
                        {
                            unitOfWorks.Save();
                        }
                        catch (Exception)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Học hàm học vị không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        int lineNH = 0;
                        int linesNH = listFiveArgument.Count;

                        foreach (int[] item in listFiveArgument)
                        {
                            lineNH++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                                .InvariantCulture, "{0:0.0}", 50 + (lineNH * 1.0 / linesNH) * 50) + "%");
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

                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Học hàm học vị và Ngành học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Ngành học không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Viên chức, Loại học hàm học vị, Loại ngành, Ngành đào tạo, Chuyên ngành trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet Nganh_HHHV.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }

        public void ImportDangHocNangCao()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("DangHocNangCao"))
                {
                    if (unitOfWorks.LoaiHocHamHocViRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   where a["mavienchuc"] != string.Empty
                                   select a;
                        List<string> list = unitOfWorks.VienChucRepository.GetListMaVienChuc();
                        int line = 0;
                        int lines = rows.Count();

                        foreach (var row in rows)
                        {
                            line++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                                .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                            string mavienchuc = row["mavienchuc"].ToString().Trim();
                            string trinhdodaotao = unitOfWorks.DangHocNangCaoRepository.ParseTrinhDoDaoTao(row["trinhdodaotao"].ToString().Trim());
                            string chuyennganh = row["chuyennganh"].ToString().Trim();

                            if (trinhdodaotao != string.Empty && chuyennganh != string.Empty)
                            {
                                if (list.Any(x => x == mavienchuc))
                                {
                                    unitOfWorks.DangHocNangCaoRepository.Insert(new DangHocNangCao
                                    {
                                        idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                                        idLoaiHocHamHocVi = unitOfWorks.LoaiHocHamHocViRepository.GetIdLoaiHocHamHocViByTenLoaiHocHamHocVi(trinhdodaotao),
                                        soQuyetDinh = row["soquyetdinh"].ToString().Trim(),
                                        linkAnhQuyetDinh = string.Empty,
                                        tenHocHamHocVi = trinhdodaotao + " " + chuyennganh,
                                        loai = null,
                                        coSoDaoTao = row["cosodaotao"].ToString().Trim(),
                                        ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngaybatdau"].ToString().Trim()),
                                        ngayKetThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngayketthuc"].ToString().Trim()),
                                        ngonNguDaoTao = row["ngonngudaotao"].ToString().Trim(),
                                        nuocCapBang = row["nuoccapbang"].ToString().Trim(),
                                        hinhThucDaoTao = row["hinhthucdaotao"].ToString().Trim()
                                    });
                                }
                            }
                            if (trinhdodaotao == string.Empty && chuyennganh != string.Empty)
                            {
                                if (list.Any(x => x == mavienchuc))
                                {
                                    unitOfWorks.DangHocNangCaoRepository.Insert(new DangHocNangCao
                                    {
                                        idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                                        idLoaiHocHamHocVi = unitOfWorks.LoaiHocHamHocViRepository.GetIdLoaiHocHamHocViByTenLoaiHocHamHocVi(trinhdodaotao),
                                        soQuyetDinh = row["soquyetdinh"].ToString().Trim(),
                                        linkAnhQuyetDinh = string.Empty,
                                        tenHocHamHocVi = trinhdodaotao + " " + chuyennganh,
                                        loai = null,
                                        coSoDaoTao = row["cosodaotao"].ToString().Trim(),
                                        ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngaybatdau"].ToString().Trim()),
                                        ngayKetThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngayketthuc"].ToString().Trim()),
                                        ngonNguDaoTao = row["ngonngudaotao"].ToString().Trim(),
                                        nuocCapBang = row["nuoccapbang"].ToString().Trim(),
                                        hinhThucDaoTao = row["hinhthucdaotao"].ToString().Trim()
                                    });
                                }
                            }
                        }

                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Đang học nâng cao thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Đang học nâng cao không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Loại học hàm học vị trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet DangHocNangCao.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }

        public void ImportLoaiHopDong()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("HopDong"))
                {
                    SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                    SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                    SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                    var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                               where a["loaihopdong"] != string.Empty
                               select a;
                    List<string> listLoaiHopDong = null;
                    if (unitOfWorks.LoaiHopDongRepository.CheckHasRows() > 0)
                        listLoaiHopDong = unitOfWorks.LoaiHopDongRepository.GetListMaLoaiHopDong();
                    listLoaiHopDong = new List<string>();
                    int line = 0;
                    int lines = rows.Count();

                    foreach (var row in rows)
                    {
                        line++;
                        SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                            .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                        string maloaihopdong = row["loaihopdong"].ToString().Trim();
                        if (!listLoaiHopDong.Any(x => x == maloaihopdong))
                        {                            
                            unitOfWorks.LoaiHopDongRepository.Insert(new LoaiHopDong
                            {
                                maLoaiHopDong = maloaihopdong,
                                tenLoaiHopDong = string.Empty
                            });
                            listLoaiHopDong.Add(maloaihopdong);
                        }
                    }

                    try
                    {
                        unitOfWorks.Save();
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Import Hợp đồng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Import Hợp đồng không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet HopDong.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ImportHopDongVienChuc()
        {
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("HopDong"))
                {
                    if (unitOfWorks.LoaiHopDongRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   where a["loaihopdong"] != string.Empty
                                   select a;
                        List<string> list = unitOfWorks.VienChucRepository.GetListMaVienChuc();
                        int line = 0;
                        int lines = rows.Count();

                        foreach (var row in rows)
                        {
                            line++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                                .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                            string mavienchuc = row["mavienchuc"].ToString().Trim();
                            string maloaihopdong = row["loaihopdong"].ToString().Trim();
                            string ghichu = row["ghichu"].ToString().Trim();
                            if (list.Any(x => x == mavienchuc)) //ktra vienchuc co ton tai
                            {
                                unitOfWorks.HopDongVienChucRepository.Insert(new HopDongVienChuc
                                {
                                    idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                                    idLoaiHopDong = unitOfWorks.LoaiHopDongRepository.GetIdLoaiHopDong(maloaihopdong),
                                    ngayBatDau = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngaybatdau"].ToString().Trim()),
                                    ngayKetThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(row["ngayketthuc"].ToString().Trim()),
                                    moTa = ghichu,
                                    linkVanBanDinhKem = string.Empty
                                });
                            }
                        }

                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Hợp đồng viên chức thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Hợp đồng viên chức không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Loại hợp đồng trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet HopDong.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }   
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ImportLoaiChungChi()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
            SplashScreenManager.Default.SetWaitFormDescription("0.0%");

            List<string> listTenLoaiChungChi = null;
            if (unitOfWorks.LoaiChungChiRepository.CheckHasRows() > 0)
                listTenLoaiChungChi = unitOfWorks.LoaiChungChiRepository.GetListTenLoaiChungChi();
            else
                listTenLoaiChungChi = new List<string>();
            string[] arrMaLoaiChungChi = { "QLNN", "CT", "NN", "TH", "CCCM" };
            string[] arrTenLoaiChungChi = { "Quản lý nhà nước", "Chính trị", "Ngoại ngữ", "Tin học", "Chứng chỉ chuyên môn" };
            for (int i = 0; i < arrTenLoaiChungChi.Length; i++)
            {
                if (!listTenLoaiChungChi.Any(x => x == arrTenLoaiChungChi[i]))
                {
                    unitOfWorks.LoaiChungChiRepository.Insert(new LoaiChungChi
                    {
                        idLoaiChungChi = arrMaLoaiChungChi[i],
                        tenLoaiChungChi = arrTenLoaiChungChi[i]
                    });
                    listTenLoaiChungChi.Add(arrTenLoaiChungChi[i]);
                }
            }

            unitOfWorks.Save();
            SplashScreenManager.Default.SetWaitFormDescription("100%");
            SplashScreenManager.CloseForm(false);
            XtraMessageBox.Show("Import Loại chứng chỉ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ImportChungChiVienChuc()
        {            
            if (!checkFileNameIsEmpty)
            {
                string sheetName = _view.CbxListSheets.EditValue.ToString();
                if (sheetName.Equals("ChungChi"))
                {
                    if (unitOfWorks.LoaiChungChiRepository.CheckHasRows() > 0)
                    {
                        SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
                        SplashScreenManager.Default.SetWaitFormCaption("Đang import......");
                        SplashScreenManager.Default.SetWaitFormDescription("0.0%");

                        var rows = from a in excelfile.Worksheet(sheetName).AsEnumerable()
                                   where a["mavienchuc"] != string.Empty
                                   select a;
                        List<string> listmavienchuc = unitOfWorks.VienChucRepository.GetListMaVienChuc();
                        int line = 0;
                        int lines = rows.Count();

                        foreach (var row in rows)
                        {
                            line++;
                            SplashScreenManager.Default.SetWaitFormDescription(String.Format(System.Globalization.CultureInfo
                                .InvariantCulture, "{0:0.0}", (line * 1.0 / lines) * 100) + "%");
                            string mavienchuc = row["mavienchuc"].ToString().Trim();
                            string ngoaingu = row["ngoaingu"].ToString().Trim();
                            string capdongoaingu = row["capdongoaingu"].ToString().Trim();
                            string capdotinhoc = row["tinhoc"].ToString().Trim();
                            string chuyenmon = row["chuyenmon"].ToString().Trim();
                            string capdochuyenmon = row["capdochuyenmon"].ToString().Trim();
                            string capdoqlnn = row["quanlynhanuoc"].ToString().Trim();
                            string capdochinhtri = row["chinhtri"].ToString().Trim();
                            if (listmavienchuc.Any(x => x == mavienchuc))
                            {                                
                                int idvienchuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc);
                                if (ngoaingu != string.Empty)
                                    InsertChungChi(idvienchuc, "NN", ngoaingu, capdongoaingu);
                                if (capdotinhoc != string.Empty)
                                    InsertChungChi(idvienchuc, "TH", "Tin học", capdotinhoc);
                                if (chuyenmon != string.Empty)
                                    InsertChungChi(idvienchuc, "CCCM", chuyenmon, capdochuyenmon);
                                if (capdoqlnn != string.Empty)
                                    InsertChungChi(idvienchuc, "QLNN", "Quản lý nhà nước", capdoqlnn);
                                if (capdochinhtri != string.Empty)
                                    InsertChungChi(idvienchuc, "CT", "Chính trị", capdochinhtri);
                            }
                        }

                        try
                        {
                            unitOfWorks.Save();
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Chứng chỉ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Import Chứng chỉ không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        XtraMessageBox.Show("Vui lòng import Loại chứng chỉ trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    XtraMessageBox.Show("Vui lòng chọn sheet ChungChi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                XtraMessageBox.Show("Vui lòng chọn tập tin excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        }

        private void InsertChungChi(int idvienchuc, string loaichungchi, string tenchungchi, string capdochungchi)
        {
            if (capdochungchi.Contains(","))
            {
                string[] arrCapDoChungChi = capdochungchi.Split(',');
                if (arrCapDoChungChi.Length == 3)
                {
                    unitOfWorks.ChungChiVienChucRepository.Insert(new ChungChiVienChuc
                    {
                        idVienChuc = idvienchuc,
                        idLoaiChungChi = loaichungchi,
                        tenChungChi = tenchungchi,
                        capDoChungChi = arrCapDoChungChi[0],
                        ngayCapChungChi = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(arrCapDoChungChi[1].Trim()),
                        coSoDaoTao = arrCapDoChungChi[2],
                        ghiChu = string.Empty,
                        linkVanBanDinhKem = string.Empty
                    });
                }
                if (arrCapDoChungChi.Length == 2)
                {
                    bool isNumeric = int.TryParse(arrCapDoChungChi[1], out int n);
                    if (isNumeric && n > 1900 && n < 3000)
                    {
                        unitOfWorks.ChungChiVienChucRepository.Insert(new ChungChiVienChuc
                        {
                            idVienChuc = idvienchuc,
                            idLoaiChungChi = loaichungchi,
                            tenChungChi = tenchungchi,
                            capDoChungChi = arrCapDoChungChi[0],
                            ngayCapChungChi = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(arrCapDoChungChi[1].Trim()),
                            coSoDaoTao = string.Empty,
                            ghiChu = string.Empty,
                            linkVanBanDinhKem = string.Empty
                        });
                    }
                    else
                    {
                        unitOfWorks.ChungChiVienChucRepository.Insert(new ChungChiVienChuc
                        {
                            idVienChuc = idvienchuc,
                            idLoaiChungChi = loaichungchi,
                            tenChungChi = tenchungchi,
                            capDoChungChi = arrCapDoChungChi[0],
                            ngayCapChungChi = null,
                            coSoDaoTao = arrCapDoChungChi[1],
                            ghiChu = string.Empty,
                            linkVanBanDinhKem = string.Empty
                        });
                    }
                }
            }
            else
            {
                bool isNumeric = int.TryParse(capdochungchi, out int n);
                if (isNumeric && n > 1900 && n < 3000)
                {
                    unitOfWorks.ChungChiVienChucRepository.Insert(new ChungChiVienChuc
                    {
                        idVienChuc = idvienchuc,
                        idLoaiChungChi = loaichungchi,
                        tenChungChi = tenchungchi,
                        capDoChungChi = string.Empty,
                        ngayCapChungChi = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(n.ToString().Trim()),
                        coSoDaoTao = string.Empty,
                        ghiChu = string.Empty,
                        linkVanBanDinhKem = string.Empty
                    });
                }
                else
                {
                    unitOfWorks.ChungChiVienChucRepository.Insert(new ChungChiVienChuc
                    {
                        idVienChuc = idvienchuc,
                        idLoaiChungChi = loaichungchi,
                        tenChungChi = tenchungchi,
                        capDoChungChi = capdochungchi,
                        ngayCapChungChi = null,
                        coSoDaoTao = string.Empty,
                        ghiChu = string.Empty,
                        linkVanBanDinhKem = string.Empty
                    });
                }
            }
        }
        #endregion
    }
}
