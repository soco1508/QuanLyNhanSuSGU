using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using Model;
using Model.Entities;
using Model.ObjectModels;
using QLNS_SGU.View;

namespace QLNS_SGU.Presenter
{
    public interface ITabPageChuyenMonPresenter : IPresenterArgument
    {
        void LoadForm();
        void CopyAndPasteInfoNganhVienChuc();
        //HHHV
        void ClickRowAndShowInfoHHHV();
        void RefreshHHHV();
        void AddHHHV();
        void SaveHHHV();
        void DeleteHHHV();
        void ExportExcelHHHV();
        void UploadFileToGoogleDriveHHHV();
        void DownloadFileToDeviceHHHV();
        void LoaiHocHamHocViHHHVChanged(object sender, EventArgs e);
        void LoaiNganhHHHVChanged(object sender, EventArgs e);
        void NganhDaoTaoHHHVChanged(object sender, EventArgs e);
        void ChuyenNganhHHHVChanged(object sender, EventArgs e);
        void TenHocHamHocViHHHVChanged(object sender, EventArgs e);
        void NgayCapBangHHHVChanged(object sender, EventArgs e);
        void CoSoDaoTaoHHHVChanged(object sender, EventArgs e);
        void NgonNguDaoTaoHHHVChanged(object sender, EventArgs e);
        void HinhThucDaoTaoHHHVChanged(object sender, EventArgs e);
        void NuocCapBangHHHVChanged(object sender, EventArgs e);
        void LinkVanBanDinhKemHHHVChanged(object sender, EventArgs e);
        void RowIndicatorHHHV(object sender, RowIndicatorCustomDrawEventArgs e);
        //DHNC 
        void ClickRowAndShowInfoDHNC();
        void RefreshDHNC();
        void AddDHNC();
        void SaveDHNC();
        void DeleteDHNC();
        void ExportExcelDHNC();
        void UploadFileToGoogleDriveDHNC();
        void DownloadFileToDeviceDHNC();
        void SoQuyetDinhChanged(object sender, EventArgs e);
        void LoaiHocHamHocViDangHocNangCaoChanged(object sender, EventArgs e);
        void TenHocHamHocViDangHocNangCaoChanged(object sender, EventArgs e);
        void NgayBatDauDangHocNangCaoChanged(object sender, EventArgs e);
        void NgayKetThucDangHocNangCaoChanged(object sender, EventArgs e);
        void CoSoDaoTaoDangHocNangCaoChanged(object sender, EventArgs e);
        void NgonNguDaoTaoDangHocNangCaoChanged(object sender, EventArgs e);
        void HinhThucDaoTaoDangHocNangCaoChanged(object sender, EventArgs e);
        void NuocCapBangDangHocNangCaoChanged(object sender, EventArgs e);
        void LoaiDangHocNangCaoChanged(object sender, EventArgs e);
        void LinkAnhQuyetDinhChanged(object sender, EventArgs e);
        void RowIndicatorDHNC(object sender, RowIndicatorCustomDrawEventArgs e);
        //Nganh
        void ClickRowAndShowInfoN();
        void RefreshN();
        void AddN();
        void SaveN();
        void DeleteN();
        void ExportExcelN();
        void UploadFileToGoogleDriveN();
        void DownloadFileToDeviceN();
        void LoaiNganhNChanged(object sender, EventArgs e);
        void NganhDaoTaoNChanged(object sender, EventArgs e);
        void ChuyenNganhNChanged(object sender, EventArgs e);
        void TenHocHamHocViNChanged(object sender, EventArgs e);
        void NgayBatDauNChanged(object sender, EventArgs e);
        void NgayKetThucNChanged(object sender, EventArgs e);
        void TrinhDoDayNChanged(object sender, EventArgs e);
        void PhanLoaiNChanged(object sender, EventArgs e);
        void LinkVanBanDinhKemNChanged(object sender, EventArgs e);
        void RowIndicatorN(object sender, RowIndicatorCustomDrawEventArgs e);
        //ChungChi
        void ClickRowAndShowInfoCC();
        void RefreshCC();
        void AddCC();
        void SaveCC();
        void DeleteCC();
        void ExportExcelCC();
        void UploadFileToGoogleDriveCC();
        void DownloadFileToDeviceCC();
        void LoaiChungChiChanged(object sender, EventArgs e);
        void CapDoChungChiChanged(object sender, EventArgs e);
        void NgayCapChungChiChanged(object sender, EventArgs e);
        void GhiChuChungChiChanged(object sender, EventArgs e);
        void LinkVanBanDinhKemChungChiChanged(object sender, EventArgs e);
        void RowIndicatorCC(object sender, RowIndicatorCustomDrawEventArgs e);
    }
    public class TabPageChuyenMonPresenter : ITabPageChuyenMonPresenter
    {
        public static string maVienChucFromTabPageThongTinCaNhan = string.Empty;
        public int rowFocusFromCreateAndEditPersonalInfoForm = -1;
        public bool checkClickGridForLoadForm = false;
        private bool checkCopyOrPaste = false; // copy
        private static CreateAndEditPersonInfoForm _createAndEditPersonInfoForm = new CreateAndEditPersonInfoForm();
        private TabPageChuyenMon _view;
        public object UI => _view;
        public TabPageChuyenMonPresenter(TabPageChuyenMon view) => _view = view;
        public void Initialize(string mavienchuc)
        {
            _view.Attach(this);
            _view.TXTMaVienChuc.Text = mavienchuc;
            _view.GVHocHamHocVi.IndicatorWidth = 50;
            _view.GVDangHocNangCao.IndicatorWidth = 50;
            _view.GVNganh.IndicatorWidth = 50;
            _view.GVChungChi.IndicatorWidth = 50;
        }                      
        private string GenerateCode() => Guid.NewGuid().ToString("N");                             
        private void Download(string linkvanbandinhkem)
        {
            if (linkvanbandinhkem != string.Empty)
            {
                string[] arr_linkvanbandinhkem = linkvanbandinhkem.Split('=');
                string idvanbandinhkem = arr_linkvanbandinhkem[1];
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                if (unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
                {
                    try
                    {
                        SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), true, true, false);
                        SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                        SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin xuống thiết bị....");
                        unitOfWorks.GoogleDriveFileRepository.DownloadFile(idvanbandinhkem);
                        SplashScreenManager.CloseForm();
                    }
                    catch
                    {
                        XtraMessageBox.Show("Tải xuống thất bại. Vui lòng kiểm tra lại đường truyền hoặc làm mới dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Tải xuống thất bại. Vui lòng kiểm tra lại đường truyền hoặc làm mới dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else XtraMessageBox.Show("Không có văn bản đính kèm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ExportExcel(GridView gv)
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            gv.ExportToXlsx(_view.SaveFileDialog.FileName);
            XtraMessageBox.Show("Xuất Excel thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void LoadForm()
        {
            LoadCbxDataHHHV();
            LoadCbxDataDHNC();
            LoadCbxDataN();
            LoadCbxDataCC();
            _view.XtraTabControl.SelectedTabPageIndex = 0;
            string mavienchuc = _view.TXTMaVienChuc.Text;
            if (mavienchuc != string.Empty)
            {
                LoadGridTabPageHocHamHocVi(mavienchuc);
                LoadGridTabPageDangHocNangCao(mavienchuc);
                LoadGridTabPageNganh(mavienchuc);
                LoadGridTabPageChungChi(mavienchuc);
                if(rowFocusFromCreateAndEditPersonalInfoForm >= 0)
                {
                    if(checkClickGridForLoadForm == false)
                    {
                        _view.GVHocHamHocVi.FocusedRowHandle = rowFocusFromCreateAndEditPersonalInfoForm;
                        ClickRowAndShowInfoHHHV();
                    }
                    else
                    {
                        _view.GVChungChi.FocusedRowHandle = rowFocusFromCreateAndEditPersonalInfoForm;
                        ClickRowAndShowInfoCC();
                    }
                }
            }
        }
        public void CopyAndPasteInfoNganhVienChuc()
        {
            if (checkCopyOrPaste)
            {
                checkCopyOrPaste = false;
                _view.LBCopyAndPasteInfo.Text = "Sao chép thông tin";
                IDataObject data_object = Clipboard.GetDataObject();
                if (data_object.GetDataPresent("loainganh"))
                {
                    _view.CBXLoaiNganhN.EditValue = Convert.ToInt32(data_object.GetData("loainganh"));
                }
                if (data_object.GetDataPresent("nganhdaotao"))
                {
                    _view.CBXNganhDaoTaoN.EditValue = Convert.ToInt32(data_object.GetData("nganhdaotao"));
                }
                if (data_object.GetDataPresent("chuyennganh"))
                {
                    _view.CBXChuyenNganhN.EditValue = Convert.ToInt32(data_object.GetData("chuyennganh"));
                }
                if (data_object.GetDataPresent("hochamhocvi"))
                {
                    _view.CBXTenHocHamHocViN.EditValue = Convert.ToInt32(data_object.GetData("hochamhocvi"));
                }
                if (data_object.GetDataPresent("ngaybatdau"))
                {
                    _view.DTNgayBatDauN.DateTime = DateTime.ParseExact(data_object.GetData("ngaybatdau").ToString(), "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
                }
                if (data_object.GetDataPresent("ngayketthuc"))
                {
                    _view.DTNgayKetThucN.DateTime = DateTime.ParseExact(data_object.GetData("ngayketthuc").ToString(), "dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
                }
                if (data_object.GetDataPresent("trinhdoday"))
                {
                    _view.TXTTrinhDoDay.Text = data_object.GetData("trinhdoday").ToString();
                }
                if (data_object.GetDataPresent("phanloai"))
                {
                    _view.RADPhanLoaiN.SelectedIndex = Convert.ToInt32(data_object.GetData("phanloai"));
                }
                if (data_object.GetDataPresent("linkvanbandinhkem"))
                {
                    _view.TXTLinkVanBanDinhKemN.Text = data_object.GetData("linkvanbandinhkem").ToString();
                }
            }
            else
            {
                checkCopyOrPaste = true;
                _view.LBCopyAndPasteInfo.Text = "Dán thông tin";
                Clipboard.Clear();
                IDataObject clipboard = new DataObject();
                if (_view.CBXLoaiNganhN.Text != string.Empty)
                {
                    object loainganh = _view.CBXLoaiNganhN.EditValue;
                    clipboard.SetData("loainganh", loainganh);
                }
                if (_view.CBXNganhDaoTaoN.Text != string.Empty)
                {
                    object nganhdaotao = _view.CBXNganhDaoTaoN.EditValue;
                    clipboard.SetData("nganhdaotao", nganhdaotao);
                }
                if (_view.CBXChuyenNganhN.Text != string.Empty)
                {
                    object chuyennganh = _view.CBXChuyenNganhN.EditValue;
                    clipboard.SetData("chuyennganh", chuyennganh);
                }
                if(_view.CBXTenHocHamHocViN.Text != string.Empty)
                {
                    object hochamhocvi = _view.CBXTenHocHamHocViN.EditValue;
                    clipboard.SetData("hochamhocvi", hochamhocvi);
                }
                if(_view.DTNgayBatDauN.Text != string.Empty)
                {
                    string ngaybatdau = _view.DTNgayBatDauN.Text;
                    clipboard.SetData("ngaybatdau", ngaybatdau);
                }
                if (_view.DTNgayKetThucN.Text != string.Empty)
                {
                    string ngayketthuc = _view.DTNgayKetThucN.Text;
                    clipboard.SetData("ngayketthuc", ngayketthuc);
                }
                if (_view.TXTTrinhDoDay.Text != string.Empty)
                {
                    string trinhdoday = _view.TXTTrinhDoDay.Text;
                    clipboard.SetData("trinhdoday", trinhdoday);
                }
                if (_view.TXTLinkVanBanDinhKemN.Text != string.Empty)
                {
                    string linkvanbandinhkem = _view.TXTLinkVanBanDinhKemN.Text;
                    clipboard.SetData("linkvanbandinhkem", linkvanbandinhkem);
                }
                string phanloai = _view.RADPhanLoaiN.SelectedIndex.ToString();
                clipboard.SetData("phanloai", phanloai);
                Clipboard.SetDataObject(clipboard, true);
            }
        }
        #region HHHV        
        public static string idFileUploadHHHV = string.Empty;
        private static string maVienChucForGetListLinkVanBanDinhKemHHHV = string.Empty;
        private bool checkAddNewHHHV = true;
        private bool loaiHocHamHocViHHHVChanged = false;
        private bool loaiNganhHHHVChanged = false;
        private bool nganhDaoTaoHHHVChanged = false;
        private bool chuyenNganhHHHVChanged = false;
        private bool tenHocHamHocViHHHVChanged = false;
        private bool ngayCapBangHHHVChanged = false;        
        private bool coSoDaoTaoHHHVChanged = false;
        private bool ngonNguDaoTaoHHHVChanged = false;
        private bool hinhThucDaoTaoHHHVChanged = false;
        private bool nuocCapBangHHHVChanged = false;
        private bool linkVanBanDinhKemHHHVChanged = false;
        private void LoadCbxDataHHHV()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<LoaiNganh> listLoaiNganh = unitOfWorks.LoaiNganhRepository.GetListLoaiNganh().ToList();
            _view.CBXLoaiNganhHHHV.Properties.DataSource = listLoaiNganh;
            _view.CBXLoaiNganhHHHV.Properties.DisplayMember = "tenLoaiNganh";
            _view.CBXLoaiNganhHHHV.Properties.ValueMember = "idLoaiNganh";
            _view.CBXLoaiNganhHHHV.Properties.DropDownRows = listLoaiNganh.Count;
            _view.CBXLoaiNganhHHHV.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idLoaiNganh", string.Empty));
            _view.CBXLoaiNganhHHHV.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenLoaiNganh", string.Empty));
            _view.CBXLoaiNganhHHHV.Properties.Columns[0].Visible = false;
            List<NganhDaoTao> listNganhDaoTao = unitOfWorks.NganhDaoTaoRepository.GetListNganhDaoTao().ToList();
            _view.CBXNganhDaoTaoHHHV.Properties.DataSource = listNganhDaoTao;
            _view.CBXNganhDaoTaoHHHV.Properties.DisplayMember = "tenNganhDaoTao";
            _view.CBXNganhDaoTaoHHHV.Properties.ValueMember = "idNganhDaoTao";
            _view.CBXNganhDaoTaoHHHV.Properties.DropDownRows = listNganhDaoTao.Count;
            _view.CBXNganhDaoTaoHHHV.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idNganhDaoTao", string.Empty));
            _view.CBXNganhDaoTaoHHHV.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenNganhDaoTao", string.Empty));
            _view.CBXNganhDaoTaoHHHV.Properties.Columns[0].Visible = false;
            List<ChuyenNganh> listChuyenNganh = unitOfWorks.ChuyenNganhRepository.GetListChuyenNganh().ToList();
            _view.CBXChuyenNganhHHHV.Properties.DataSource = listChuyenNganh;
            _view.CBXChuyenNganhHHHV.Properties.DisplayMember = "tenChuyenNganh";
            _view.CBXChuyenNganhHHHV.Properties.ValueMember = "idChuyenNganh";
            _view.CBXChuyenNganhHHHV.Properties.DropDownRows = listChuyenNganh.Count;
            _view.CBXChuyenNganhHHHV.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idChuyenNganh", string.Empty));
            _view.CBXChuyenNganhHHHV.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenChuyenNganh", string.Empty));
            _view.CBXChuyenNganhHHHV.Properties.Columns[0].Visible = false;
            List<LoaiHocHamHocVi> listLoaiHocHamHocVi = unitOfWorks.LoaiHocHamHocViRepository.GetListLoaiHocHamHocVi().ToList();
            _view.CBXLoaiHocHamHocViHHHV.Properties.DataSource = listLoaiHocHamHocVi;
            _view.CBXLoaiHocHamHocViHHHV.Properties.DisplayMember = "tenLoaiHocHamHocVi";
            _view.CBXLoaiHocHamHocViHHHV.Properties.ValueMember = "idLoaiHocHamHocVi";
            _view.CBXLoaiHocHamHocViHHHV.Properties.DropDownRows = listLoaiHocHamHocVi.Count;
            _view.CBXLoaiHocHamHocViHHHV.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idLoaiHocHamHocVi", string.Empty));
            _view.CBXLoaiHocHamHocViHHHV.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenLoaiHocHamHocVi", string.Empty));
            _view.CBXLoaiHocHamHocViHHHV.Properties.Columns[0].Visible = false;
            List<string> listCoSoDaoTao = unitOfWorks.HocHamHocViVienChucRepository.GetListCoSoDaoTao();
            AutoCompleteStringCollection coSoDaoTaoSource = new AutoCompleteStringCollection();
            listCoSoDaoTao.ForEach(x => coSoDaoTaoSource.Add(x)); // autocompleteStringCollection if add null value app will be crashed
            _view.TXTCoSoDaoTaoHHHV.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _view.TXTCoSoDaoTaoHHHV.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _view.TXTCoSoDaoTaoHHHV.MaskBox.AutoCompleteCustomSource = coSoDaoTaoSource;

            List<string> listHinhThucDaoTao = unitOfWorks.HocHamHocViVienChucRepository.GetListHinhThucDaoTao();
            AutoCompleteStringCollection hinhThucDaoTaoSource = new AutoCompleteStringCollection();
            listHinhThucDaoTao.ForEach(x => hinhThucDaoTaoSource.Add(x)); // autocompleteStringCollection if add null value app will be crashed
            _view.TXTHinhThucDaoTaoHHHV.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _view.TXTHinhThucDaoTaoHHHV.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _view.TXTHinhThucDaoTaoHHHV.MaskBox.AutoCompleteCustomSource = hinhThucDaoTaoSource;

            List<string> listNgonNguDaoTao = unitOfWorks.HocHamHocViVienChucRepository.GetListNgonNguDaoTao();
            AutoCompleteStringCollection ngonNguDaoTaoSource = new AutoCompleteStringCollection();
            listNgonNguDaoTao.ForEach(x => ngonNguDaoTaoSource.Add(x)); // autocompleteStringCollection if add null value app will be crashed
            _view.TXTNgonNguDaoTaoHHHV.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _view.TXTNgonNguDaoTaoHHHV.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _view.TXTNgonNguDaoTaoHHHV.MaskBox.AutoCompleteCustomSource = ngonNguDaoTaoSource;

            List<string> listNuocCapBang = unitOfWorks.HocHamHocViVienChucRepository.GetListNuocCapBang();
            AutoCompleteStringCollection nuocCapBangSource = new AutoCompleteStringCollection();
            listNuocCapBang.ForEach(x => nuocCapBangSource.Add(x)); // autocompleteStringCollection if add null value app will be crashed
            _view.TXTNuocCapBangHHHV.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _view.TXTNuocCapBangHHHV.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _view.TXTNuocCapBangHHHV.MaskBox.AutoCompleteCustomSource = nuocCapBangSource;

        }
        private void SetDefaultValueControlHHHV()
        {
            checkAddNewHHHV = true;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            _view.CBXLoaiHocHamHocViHHHV.ErrorText = string.Empty;
            _view.CBXNganhDaoTaoHHHV.ErrorText = string.Empty;
            _view.CBXChuyenNganhHHHV.ErrorText = string.Empty;
            _view.CBXLoaiHocHamHocViHHHV.EditValue = -1;
            _view.CBXLoaiNganhHHHV.EditValue = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganhEmpty();
            _view.CBXNganhDaoTaoHHHV.EditValue = -1;
            _view.CBXChuyenNganhHHHV.EditValue = -1;
            _view.TXTTenHocHamHocViHHHV.Text = string.Empty;
            _view.TXTCoSoDaoTaoHHHV.Text = string.Empty;
            _view.TXTNgonNguDaoTaoHHHV.Text = string.Empty;
            _view.TXTHinhThucDaoTaoHHHV.Text = string.Empty;
            _view.TXTNuocCapBangHHHV.Text = string.Empty;
            _view.DTNgayCapBang.Text = string.Empty;
            _view.TXTLinkVanBanDinhKemHHHV.Text = string.Empty;
        }
        private void GenerateTenHocHamHocVi()
        {
            if (_view.CBXLoaiHocHamHocViHHHV.Text != string.Empty && _view.CBXNganhDaoTaoHHHV.Text != string.Empty && _view.CBXChuyenNganhHHHV.Text != string.Empty)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                string loaihochamhocvi = unitOfWorks.LoaiHocHamHocViRepository.ReturnFirstCharOfLoaiHocHamHocVi(_view.CBXLoaiHocHamHocViHHHV.Text);
                string nganhdaotao = _view.CBXNganhDaoTaoHHHV.Text;
                string chuyennganh = _view.CBXChuyenNganhHHHV.Text;
                string tenhochamhocvi = loaihochamhocvi + " " + nganhdaotao + " - " + chuyennganh;
                _view.TXTTenHocHamHocViHHHV.Text = tenhochamhocvi;
            }
        }
        private void LoadGridTabPageHocHamHocVi(string mavienchuc)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<HocHamHocViForTabPageChuyenMon> listHocHamHocViForTabPageChuyenMon = unitOfWorks.HocHamHocViVienChucRepository.GetListHocHamHocViForTabPageChuyenMon(mavienchuc);
            _view.GCHocHamHocVi.DataSource = listHocHamHocViForTabPageChuyenMon;
        }
        private void InsertDataHHHV()
        {
            string mavienchuc = _view.TXTMaVienChuc.Text;
            int idloaihochamhocvi = Convert.ToInt32(_view.CBXLoaiHocHamHocViHHHV.EditValue);
            int idloainganh = Convert.ToInt32(_view.CBXLoaiNganhHHHV.EditValue);
            int idnganhdaotao = Convert.ToInt32(_view.CBXNganhDaoTaoHHHV.EditValue);
            int idchuyennganh = Convert.ToInt32(_view.CBXChuyenNganhHHHV.EditValue);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            unitOfWorks.HocHamHocViVienChucRepository.Insert(new HocHamHocViVienChuc
            {
                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                idLoaiHocHamHocVi = idloaihochamhocvi,
                idLoaiNganh = idloainganh,                
                idNganhDaoTao = idnganhdaotao,
                idChuyenNganh = idchuyennganh,
                bacHocHamHocVi = unitOfWorks.HocHamHocViVienChucRepository.HardCodeBacToDatabase(_view.CBXLoaiHocHamHocViHHHV.Text),
                tenHocHamHocVi = _view.TXTTenHocHamHocViHHHV.Text,
                coSoDaoTao = _view.TXTCoSoDaoTaoHHHV.Text,
                ngonNguDaoTao = _view.TXTNgonNguDaoTaoHHHV.Text,
                hinhThucDaoTao = _view.TXTHinhThucDaoTaoHHHV.Text,
                nuocCapBang = _view.TXTNuocCapBangHHHV.Text,
                ngayCapBang = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayCapBang.Text),
                linkVanBanDinhKem = _view.TXTLinkVanBanDinhKemHHHV.Text
            });
            unitOfWorks.Save();
            unitOfWorks.NganhVienChucRepository.Insert(new NganhVienChuc
            {
                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                idHocHamHocViVienChuc = unitOfWorks.HocHamHocViVienChucRepository.GetNewestIdHocHamHocViVienChuc(),
                idLoaiNganh = idloainganh,
                idNganhDaoTao = idnganhdaotao,
                idChuyenNganh = idchuyennganh,
                trinhDoDay = string.Empty,
                phanLoai = true
            });
            unitOfWorks.Save();
            LoadGridTabPageHocHamHocVi(_view.TXTMaVienChuc.Text);
            LoadGridTabPageNganh(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetDefaultValueControlHHHV();
            MainPresenter.LoadGridHocHamHocViAtRightViewInMainForm();            
        }
        private void UpdateDataHHHV()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int idhochamhocvi = Convert.ToInt32(_view.GVHocHamHocVi.GetFocusedRowCellDisplayText("Id"));
            HocHamHocViVienChuc hocHamHocViVienChuc = unitOfWorks.HocHamHocViVienChucRepository.GetObjectById(idhochamhocvi);
            NganhVienChuc nganhVienChuc = unitOfWorks.NganhVienChucRepository.GetObjectByIdHocHamHocViVienChuc(idhochamhocvi);
            int idloainganh = Convert.ToInt32(_view.CBXLoaiNganhHHHV.EditValue);
            int idnganhdaotao = Convert.ToInt32(_view.CBXNganhDaoTaoHHHV.EditValue);
            int idchuyennganh = Convert.ToInt32(_view.CBXChuyenNganhHHHV.EditValue);
            if (loaiHocHamHocViHHHVChanged)
            {
                hocHamHocViVienChuc.idLoaiHocHamHocVi = Convert.ToInt32(_view.CBXLoaiHocHamHocViHHHV.EditValue);
                loaiHocHamHocViHHHVChanged = false;
            }
            if (loaiNganhHHHVChanged)
            {
                hocHamHocViVienChuc.idLoaiNganh = idloainganh;
                nganhVienChuc.idLoaiNganh = idloainganh;
                loaiNganhHHHVChanged = false;
            }
            if (nganhDaoTaoHHHVChanged)
            {
                hocHamHocViVienChuc.idNganhDaoTao = idnganhdaotao;
                nganhVienChuc.idNganhDaoTao = idnganhdaotao;
                nganhDaoTaoHHHVChanged = false;
            }
            if (chuyenNganhHHHVChanged)
            {
                hocHamHocViVienChuc.idChuyenNganh = idchuyennganh;
                nganhVienChuc.idChuyenNganh = idchuyennganh;
                chuyenNganhHHHVChanged = false;
            }
            if (tenHocHamHocViHHHVChanged)
            {
                hocHamHocViVienChuc.tenHocHamHocVi = _view.TXTTenHocHamHocViHHHV.Text;
                tenHocHamHocViHHHVChanged = false;
            }
            if (coSoDaoTaoHHHVChanged)
            {
                hocHamHocViVienChuc.coSoDaoTao = _view.TXTCoSoDaoTaoHHHV.Text;
                coSoDaoTaoHHHVChanged = false;
            }
            if (ngonNguDaoTaoHHHVChanged)
            {
                hocHamHocViVienChuc.ngonNguDaoTao = _view.TXTNgonNguDaoTaoHHHV.Text;
                ngonNguDaoTaoHHHVChanged = false;
            }
            if (hinhThucDaoTaoHHHVChanged)
            {
                hocHamHocViVienChuc.hinhThucDaoTao = _view.TXTHinhThucDaoTaoHHHV.Text;
                hinhThucDaoTaoHHHVChanged = false;
            }
            if (ngayCapBangHHHVChanged)
            {
                hocHamHocViVienChuc.ngayCapBang = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayCapBang.Text);
                ngayCapBangHHHVChanged = false;
            }
            if (nuocCapBangHHHVChanged)
            {
                hocHamHocViVienChuc.nuocCapBang = _view.TXTNuocCapBangHHHV.Text;
                nuocCapBangHHHVChanged = false;
            }
            if (linkVanBanDinhKemHHHVChanged)
            {
                hocHamHocViVienChuc.linkVanBanDinhKem = _view.TXTLinkVanBanDinhKemHHHV.Text;
                linkVanBanDinhKemHHHVChanged = false;
            }
            unitOfWorks.Save();
            LoadGridTabPageHocHamHocVi(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainPresenter.LoadGridHocHamHocViAtRightViewInMainForm();
        }

        public void SelectTabHocHamHocVi() => _view.XtraTabControl.SelectedTabPageIndex = 0;

        public void ClickRowAndShowInfoHHHV()
        {
            checkAddNewHHHV = false;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVHocHamHocVi.FocusedRowHandle;
            if(row_handle >= 0)
            {
                string loaihochamhocvi = _view.GVHocHamHocVi.GetFocusedRowCellDisplayText("LoaiHocHamHocVi").ToString();
                string loainganh = _view.GVHocHamHocVi.GetFocusedRowCellDisplayText("LoaiNganh").ToString();
                string nganhdaotao = _view.GVHocHamHocVi.GetFocusedRowCellDisplayText("NganhDaoTao").ToString();
                string chuyennganh = _view.GVHocHamHocVi.GetFocusedRowCellDisplayText("ChuyenNganh").ToString();
                _view.CBXLoaiHocHamHocViHHHV.EditValue = unitOfWorks.LoaiHocHamHocViRepository.GetIdLoaiHocHamHocViByTenLoaiHocHamHocVi(loaihochamhocvi);
                _view.CBXLoaiNganhHHHV.EditValue = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganh(loainganh);
                _view.CBXNganhDaoTaoHHHV.EditValue = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTao(nganhdaotao);
                _view.CBXChuyenNganhHHHV.EditValue = unitOfWorks.ChuyenNganhRepository.GetIdChuyenNganh(chuyennganh);
                _view.TXTTenHocHamHocViHHHV.Text = _view.GVHocHamHocVi.GetFocusedRowCellDisplayText("TenHocHamHocVi");
                _view.TXTCoSoDaoTaoHHHV.Text = _view.GVHocHamHocVi.GetFocusedRowCellDisplayText("CoSoDaoTao");
                _view.TXTNgonNguDaoTaoHHHV.Text = _view.GVHocHamHocVi.GetFocusedRowCellDisplayText("NgonNguDaoTao");
                _view.TXTHinhThucDaoTaoHHHV.Text = _view.GVHocHamHocVi.GetFocusedRowCellDisplayText("HinhThucDaoTao");
                _view.TXTNuocCapBangHHHV.Text = _view.GVHocHamHocVi.GetFocusedRowCellDisplayText("NuocCapBang");
                _view.DTNgayCapBang.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVHocHamHocVi.GetFocusedRowCellDisplayText("NgayCapBang"));
                _view.TXTLinkVanBanDinhKemHHHV.Text = _view.GVHocHamHocVi.GetFocusedRowCellDisplayText("LinkVanBanDinhKem");
            }
        }

        public void RefreshHHHV() => SetDefaultValueControlHHHV();

        public void AddHHHV() => SetDefaultValueControlHHHV();

        public void SaveHHHV()
        {
            if (checkAddNewHHHV)
            {
                if (_view.TXTMaVienChuc.Text != string.Empty && maVienChucFromTabPageThongTinCaNhan == string.Empty)
                {
                    InsertDataHHHV();
                }
                else if (_view.TXTMaVienChuc.Text == string.Empty && maVienChucFromTabPageThongTinCaNhan != string.Empty)
                {
                    _view.TXTMaVienChuc.Text = maVienChucFromTabPageThongTinCaNhan;
                    InsertDataHHHV();
                }
                else XtraMessageBox.Show("Vui lòng thêm thông tin viên chức trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int row_handle = _view.GVHocHamHocVi.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    UpdateDataHHHV();
                }
            }
        }

        public void DeleteHHHV()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVHocHamHocVi.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVHocHamHocVi.GetFocusedRowCellDisplayText("Id"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.HocHamHocViVienChucRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVHocHamHocVi.DeleteRow(row_handle);
                        RefreshHHHV();
                        MainPresenter.LoadGridHocHamHocViAtRightViewInMainForm();
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Công tác này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcelHHHV() => ExportExcel(_view.GVHocHamHocVi);

        public static void RemoveFileIfNotSaveHHHV(string id)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<string> listLinkVanBanDinhKem = unitOfWorks.HocHamHocViVienChucRepository.GetListLinkVanBanDinhKem(maVienChucForGetListLinkVanBanDinhKemHHHV);
            if (listLinkVanBanDinhKem.Any(x => x.Equals("https://drive.google.com/open?id=" + id + "")) == false)
            {
                unitOfWorks.GoogleDriveFileRepository.DeleteFile(id);
            }
        }

        public void UploadFileToGoogleDriveHHHV()
        {
            if (_view.GVHocHamHocVi.FocusedRowHandle >= 0)
            {
                string mavienchuc = _view.TXTMaVienChuc.Text;
                _view.OpenFileDialog.FileName = string.Empty;
                _view.OpenFileDialog.Filter = "Pdf Files|*.pdf";
                if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                if (unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
                {
                    try
                    {
                        SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), false, false, true);
                        SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                        SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin lên Google Drive....");
                        string code = GenerateCode(); // code xac dinh file duy nhat
                        string filename = _view.OpenFileDialog.FileName;
                        string[] temp = filename.Split('\\');
                        string[] split_filename = filename.Split('.');
                        string new_filename = split_filename[0] + "-" + mavienchuc + "-" + code + "." + split_filename[1];
                        FileInfo fileInfo = new FileInfo(filename);
                        fileInfo.MoveTo(new_filename);
                        unitOfWorks.GoogleDriveFileRepository.UploadFile(new_filename);
                        FileInfo fileInfo1 = new FileInfo(new_filename); //doi lai filename cu~
                        fileInfo1.MoveTo(filename);
                        string id = unitOfWorks.GoogleDriveFileRepository.GetIdDriveFile(mavienchuc, code);
                        idFileUploadHHHV = id;
                        maVienChucForGetListLinkVanBanDinhKemHHHV = mavienchuc;
                        _view.TXTLinkVanBanDinhKemHHHV.Text = string.Empty;
                        _view.TXTLinkVanBanDinhKemHHHV.Text = "https://drive.google.com/open?id=" + id + "";
                        SplashScreenManager.CloseForm();
                    }
                    catch
                    {
                        XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn dòng cần upload!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DownloadFileToDeviceHHHV()
        {
            string linkvanbandinhkem = _view.TXTLinkVanBanDinhKemHHHV.ToString().Trim();
            Download(linkvanbandinhkem);
        }

        public void LoaiHocHamHocViHHHVChanged(object sender, EventArgs e)
        {
            loaiHocHamHocViHHHVChanged = true;
            GenerateTenHocHamHocVi();
        }

        public void LoaiNganhHHHVChanged(object sender, EventArgs e)
        {
            loaiNganhHHHVChanged = true;
            if(_view.CBXLoaiNganhHHHV.Text != string.Empty)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int idloainganh = Convert.ToInt32(_view.CBXLoaiNganhHHHV.EditValue);
                List<NganhDaoTao> list = unitOfWorks.NganhDaoTaoRepository.GetListNganhDaoTaoByIdLoaiNganh(idloainganh);
                _view.CBXNganhDaoTaoHHHV.Properties.DisplayMember = "tenNganhDaoTao";
                _view.CBXNganhDaoTaoHHHV.Properties.ValueMember = "idNganhDaoTao";
                _view.CBXNganhDaoTaoHHHV.Properties.DataSource = list;
                _view.CBXNganhDaoTaoHHHV.Properties.DropDownRows = list.Count;
                _view.CBXNganhDaoTaoHHHV.Properties.Columns[0].Visible = false;
            }
        }

        public void NganhDaoTaoHHHVChanged(object sender, EventArgs e)
        {
            nganhDaoTaoHHHVChanged = true;           
            if(_view.CBXNganhDaoTaoHHHV.Text != string.Empty)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int idnganhdaotao = Convert.ToInt32(_view.CBXNganhDaoTaoHHHV.EditValue);
                int idloainganh = unitOfWorks.NganhDaoTaoRepository.GetIdLoaiNganhByIdNganhDaoTao(idnganhdaotao);
                _view.CBXLoaiNganhHHHV.EditValue = idloainganh;
                List<ChuyenNganh> list = unitOfWorks.ChuyenNganhRepository.GetListChuyenNganhByIdNganhDaoTao(idnganhdaotao);
                _view.CBXChuyenNganhHHHV.Properties.DisplayMember = "tenChuyenNganh";
                _view.CBXChuyenNganhHHHV.Properties.ValueMember = "idChuyenNganh";
                _view.CBXChuyenNganhHHHV.Properties.DataSource = list;
                _view.CBXChuyenNganhHHHV.Properties.DropDownRows = list.Count;
                _view.CBXChuyenNganhHHHV.Properties.Columns[0].Visible = false;
                GenerateTenHocHamHocVi();
            }           
        }

        public void ChuyenNganhHHHVChanged(object sender, EventArgs e)
        {
            chuyenNganhHHHVChanged = true;
            if(_view.CBXChuyenNganhHHHV.Text != string.Empty)
            {
                GenerateTenHocHamHocVi();
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                if(_view.CBXLoaiNganhHHHV.Text == string.Empty && _view.CBXNganhDaoTaoHHHV.Text == string.Empty)
                {
                    int idchuyennganh = Convert.ToInt32(_view.CBXChuyenNganhHHHV.EditValue);
                    int idnganhdaotao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTaoByIdChuyenNganh(idchuyennganh);
                    int idloainganh = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganhByIdNganhDaoTao(idnganhdaotao);
                    _view.CBXLoaiNganhHHHV.EditValue = idloainganh;
                    _view.CBXNganhDaoTaoHHHV.EditValue = idnganhdaotao;
                }
                if (_view.CBXLoaiNganhHHHV.Text != string.Empty && _view.CBXNganhDaoTaoHHHV.Text == string.Empty)
                {
                    int idchuyennganh = Convert.ToInt32(_view.CBXChuyenNganhHHHV.EditValue);
                    int idnganhdaotao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTaoByIdChuyenNganh(idchuyennganh);
                    _view.CBXNganhDaoTaoHHHV.EditValue = idnganhdaotao;
                }
            }
        }

        public void TenHocHamHocViHHHVChanged(object sender, EventArgs e)
        {
            tenHocHamHocViHHHVChanged = true;
        }

        public void NgayCapBangHHHVChanged(object sender, EventArgs e)
        {
            ngayCapBangHHHVChanged = true;
        }

        public void CoSoDaoTaoHHHVChanged(object sender, EventArgs e)
        {
            coSoDaoTaoHHHVChanged = true;
        }

        public void NgonNguDaoTaoHHHVChanged(object sender, EventArgs e)
        {
            ngonNguDaoTaoHHHVChanged = true;
        }

        public void HinhThucDaoTaoHHHVChanged(object sender, EventArgs e)
        {
            hinhThucDaoTaoHHHVChanged = true;
        }

        public void NuocCapBangHHHVChanged(object sender, EventArgs e)
        {
            nuocCapBangHHHVChanged = true;
        }

        public void LinkVanBanDinhKemHHHVChanged(object sender, EventArgs e)
        {
            linkVanBanDinhKemHHHVChanged = true;
        }

        public void RowIndicatorHHHV(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        #endregion
        #region DHNC
        public static string idFileUploadDHNC = string.Empty;
        private static string maVienChucForGetListLinkAnhQuyetDinh = string.Empty;
        private bool checkAddNewDHNC = true;
        private bool soQuyetDinhChanged = false;
        private bool loaiHocHamHocViDangHocNangCaoChanged = false;
        private bool tenHocHamHocViDangHocNangCaoChanged = false;
        private bool ngayBatDauDangHocNangCaoChanged = false;
        private bool ngayKetThucDangHocNangCaoChanged = false;
        private bool coSoDaoTaoDangHocNangCaoChanged = false;
        private bool ngonNguDaoTaoDangHocNangCaoChanged = false;
        private bool hinhThucDaoTaoDangHocNangCaoChanged = false;
        private bool nuocCapBangDangHocNangCaoChanged = false;
        private bool loaiDangHocNangCaoChanged = false;
        private bool linkAnhQuyetDinhChanged = false;
        private void LoadGridTabPageDangHocNangCao(string mavienchuc)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<DangHocNangCaoForView> listDangHocNangCaoForView = unitOfWorks.DangHocNangCaoRepository.GetListDangHocNangCao(mavienchuc);
            _view.GCDangHocNangCao.DataSource = listDangHocNangCaoForView;
        }
        private void LoadCbxDataDHNC()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<LoaiHocHamHocVi> listLoaiHocHamHocVi = unitOfWorks.LoaiHocHamHocViRepository.GetListLoaiHocHamHocVi().ToList();
            _view.CBXLoaiHocHamHocViDHNC.Properties.DataSource = listLoaiHocHamHocVi;
            _view.CBXLoaiHocHamHocViDHNC.Properties.DisplayMember = "tenLoaiHocHamHocVi";
            _view.CBXLoaiHocHamHocViDHNC.Properties.ValueMember = "idLoaiHocHamHocVi";
            _view.CBXLoaiHocHamHocViDHNC.Properties.DropDownRows = listLoaiHocHamHocVi.Count;
            _view.CBXLoaiHocHamHocViDHNC.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idLoaiHocHamHocVi", string.Empty));
            _view.CBXLoaiHocHamHocViDHNC.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenLoaiHocHamHocVi", string.Empty));
            _view.CBXLoaiHocHamHocViDHNC.Properties.Columns[0].Visible = false;
            List<string> listLoai = new List<string>() { "Đang học", "Đã xong", "Gia hạn", "Hết hạn" };
            _view.CBXLoai.Properties.DataSource = listLoai;
            _view.CBXLoai.Properties.DropDownRows = listLoai.Count;
            List<string> listCoSoDaoTao = unitOfWorks.DangHocNangCaoRepository.GetListCoSoDaoTao();
            AutoCompleteStringCollection coSoDaoTaoSource = new AutoCompleteStringCollection();
            listCoSoDaoTao.ForEach(x => coSoDaoTaoSource.Add(x)); // autocompleteStringCollection if add null value app will be crashed
            _view.TXTCoSoDaoTaoDHNC.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _view.TXTCoSoDaoTaoDHNC.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _view.TXTCoSoDaoTaoDHNC.MaskBox.AutoCompleteCustomSource = coSoDaoTaoSource;

            List<string> listHinhThucDaoTao = unitOfWorks.DangHocNangCaoRepository.GetListHinhThucDaoTao();
            AutoCompleteStringCollection hinhThucDaoTaoSource = new AutoCompleteStringCollection();
            listHinhThucDaoTao.ForEach(x => hinhThucDaoTaoSource.Add(x)); // autocompleteStringCollection if add null value app will be crashed
            _view.TXTHinhThucDaoTaoDHNC.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _view.TXTHinhThucDaoTaoDHNC.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _view.TXTHinhThucDaoTaoDHNC.MaskBox.AutoCompleteCustomSource = hinhThucDaoTaoSource;

            List<string> listNgonNguDaoTao = unitOfWorks.DangHocNangCaoRepository.GetListNgonNguDaoTao();
            AutoCompleteStringCollection ngonNguDaoTaoSource = new AutoCompleteStringCollection();
            listNgonNguDaoTao.ForEach(x => ngonNguDaoTaoSource.Add(x)); // autocompleteStringCollection if add null value app will be crashed
            _view.TXTNgonNguDaoTaoDHNC.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _view.TXTNgonNguDaoTaoDHNC.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _view.TXTNgonNguDaoTaoDHNC.MaskBox.AutoCompleteCustomSource = ngonNguDaoTaoSource;

            List<string> listNuocCapBang = unitOfWorks.DangHocNangCaoRepository.GetListNuocCapBang();
            AutoCompleteStringCollection nuocCapBangSource = new AutoCompleteStringCollection();
            listNuocCapBang.ForEach(x => nuocCapBangSource.Add(x)); // autocompleteStringCollection if add null value app will be crashed
            _view.TXTNuocCapBangDHNC.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _view.TXTNuocCapBangDHNC.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _view.TXTNuocCapBangDHNC.MaskBox.AutoCompleteCustomSource = nuocCapBangSource;
        }
        private void SetDefaultValueControlDHNC()
        {
            checkAddNewDHNC = true;
            _view.CBXLoaiHocHamHocViDHNC.ErrorText = string.Empty;
            _view.CBXLoai.ErrorText = string.Empty;
            _view.CBXLoaiHocHamHocViDHNC.Text = string.Empty;
            _view.CBXLoai.Text = string.Empty;
            _view.TXTTenHocHamHocViDHNC.Text = string.Empty;
            _view.TXTCoSoDaoTaoDHNC.Text = string.Empty;
            _view.TXTNgonNguDaoTaoDHNC.Text = string.Empty;
            _view.TXTHinhThucDaoTaoDHNC.Text = string.Empty;
            _view.TXTNuocCapBangDHNC.Text = string.Empty;
            _view.DTNgayBatDauDHNC.Text = string.Empty;
            _view.DTNgayKetThucDHNC.Text = string.Empty;
            _view.TXTSoQuyetDinh.Text = string.Empty;
            _view.TXTLinkAnhQuyetDinh.Text = string.Empty;
        }
        private void InsertDataDHNC()
        {
            string mavienchuc = _view.TXTMaVienChuc.Text;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            unitOfWorks.DangHocNangCaoRepository.Insert(new DangHocNangCao
            {
                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                idLoaiHocHamHocVi = Convert.ToInt32(_view.CBXLoaiHocHamHocViDHNC.EditValue),
                ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayBatDauDHNC.Text),
                ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayKetThucDHNC.Text),
                tenHocHamHocVi = _view.TXTTenHocHamHocViDHNC.Text,
                loai = unitOfWorks.DangHocNangCaoRepository.HardCodeLoaiToDatabase(_view.CBXLoai.EditValue.ToString()),
                coSoDaoTao = _view.TXTCoSoDaoTaoDHNC.Text,
                ngonNguDaoTao = _view.TXTNgonNguDaoTaoDHNC.Text,
                hinhThucDaoTao = _view.TXTHinhThucDaoTaoDHNC.Text,
                nuocCapBang = _view.TXTNuocCapBangDHNC.Text,
                soQuyetDinh = _view.TXTSoQuyetDinh.Text,
                linkAnhQuyetDinh = _view.TXTLinkAnhQuyetDinh.Text
            });
            unitOfWorks.Save();
            LoadGridTabPageDangHocNangCao(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetDefaultValueControlDHNC();
        }
        private void UpdateDataDHNC()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int iddanghocnangcao = Convert.ToInt32(_view.GVDangHocNangCao.GetFocusedRowCellDisplayText("Id"));
            DangHocNangCao dangHocNangCao = unitOfWorks.DangHocNangCaoRepository.GetObjectById(iddanghocnangcao);
            if (loaiHocHamHocViDangHocNangCaoChanged)
            {
                dangHocNangCao.idLoaiHocHamHocVi = Convert.ToInt32(_view.CBXLoaiHocHamHocViDHNC.EditValue);
                loaiHocHamHocViDangHocNangCaoChanged = false;
            }
            if (loaiDangHocNangCaoChanged)
            {
                dangHocNangCao.loai = unitOfWorks.HocHamHocViVienChucRepository.HardCodeBacToDatabase(_view.CBXLoai.EditValue.ToString());
                loaiDangHocNangCaoChanged = false;
            }
            if (tenHocHamHocViDangHocNangCaoChanged)
            {
                dangHocNangCao.tenHocHamHocVi = _view.TXTTenHocHamHocViDHNC.Text;
                tenHocHamHocViDangHocNangCaoChanged = false;
            }
            if (coSoDaoTaoDangHocNangCaoChanged)
            {
                dangHocNangCao.coSoDaoTao = _view.TXTCoSoDaoTaoDHNC.Text;
                coSoDaoTaoDangHocNangCaoChanged = false;
            }
            if (ngonNguDaoTaoDangHocNangCaoChanged)
            {
                dangHocNangCao.ngonNguDaoTao = _view.TXTNgonNguDaoTaoDHNC.Text;
                ngonNguDaoTaoDangHocNangCaoChanged = false;
            }
            if (hinhThucDaoTaoDangHocNangCaoChanged)
            {
                dangHocNangCao.hinhThucDaoTao = _view.TXTHinhThucDaoTaoDHNC.Text;
                hinhThucDaoTaoDangHocNangCaoChanged = false;
            }
            if (nuocCapBangDangHocNangCaoChanged)
            {
                dangHocNangCao.nuocCapBang = _view.TXTNuocCapBangDHNC.Text;
                nuocCapBangDangHocNangCaoChanged = false;
            }
            if (ngayBatDauDangHocNangCaoChanged)
            {
                dangHocNangCao.ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayBatDauDHNC.Text);
                ngayBatDauDangHocNangCaoChanged = false;
            }
            if (ngayKetThucDangHocNangCaoChanged)
            {
                dangHocNangCao.ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayKetThucDHNC.Text);
                ngayKetThucDangHocNangCaoChanged = false;
            }
            if (soQuyetDinhChanged)
            {
                dangHocNangCao.soQuyetDinh = _view.TXTSoQuyetDinh.Text;
                soQuyetDinhChanged = false;
            }
            if (linkAnhQuyetDinhChanged)
            {
                dangHocNangCao.linkAnhQuyetDinh = _view.TXTLinkAnhQuyetDinh.Text;
                linkAnhQuyetDinhChanged = false;
            }
            unitOfWorks.Save();
            LoadGridTabPageDangHocNangCao(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void SelectTabDangHocNangCao() => _view.XtraTabControl.SelectedTabPageIndex = 1;

        public void ClickRowAndShowInfoDHNC()
        {
            checkAddNewDHNC = false;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVDangHocNangCao.FocusedRowHandle;
            if (row_handle >= 0)
            {
                string loaihochamhocvi = _view.GVDangHocNangCao.GetFocusedRowCellDisplayText("LoaiHocHamHocVi");
                _view.CBXLoaiHocHamHocViDHNC.EditValue = unitOfWorks.LoaiHocHamHocViRepository.GetIdLoaiHocHamHocViByTenLoaiHocHamHocVi(loaihochamhocvi);
                _view.CBXLoai.EditValue = _view.GVDangHocNangCao.GetFocusedRowCellDisplayText("Loai");
                _view.TXTTenHocHamHocViDHNC.Text = _view.GVDangHocNangCao.GetFocusedRowCellDisplayText("TenHocHamHocVi");
                _view.TXTCoSoDaoTaoDHNC.Text = _view.GVDangHocNangCao.GetFocusedRowCellDisplayText("CoSoDaoTao");
                _view.TXTNgonNguDaoTaoDHNC.Text = _view.GVDangHocNangCao.GetFocusedRowCellDisplayText("NgonNguDaoTao");
                _view.TXTHinhThucDaoTaoDHNC.Text = _view.GVDangHocNangCao.GetFocusedRowCellDisplayText("HinhThucDaoTao");
                _view.TXTNuocCapBangDHNC.Text = _view.GVDangHocNangCao.GetFocusedRowCellDisplayText("NuocCapBang");
                _view.DTNgayBatDauDHNC.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVDangHocNangCao.GetFocusedRowCellDisplayText("NgayBatDau"));
                _view.DTNgayKetThucDHNC.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVDangHocNangCao.GetFocusedRowCellDisplayText("NgayKetThuc"));
                _view.TXTSoQuyetDinh.Text = _view.GVDangHocNangCao.GetFocusedRowCellDisplayText("SoQuyetDinh");
                _view.TXTLinkAnhQuyetDinh.Text = _view.GVDangHocNangCao.GetFocusedRowCellDisplayText("LinkAnhQuyetDinh");
            }
        }

        public void RefreshDHNC() => SetDefaultValueControlDHNC();

        public void AddDHNC() => SetDefaultValueControlDHNC();

        public void SaveDHNC()
        {
            if (checkAddNewDHNC)
            {
                if (_view.TXTMaVienChuc.Text != string.Empty && maVienChucFromTabPageThongTinCaNhan == string.Empty)
                {
                    if (_view.CBXLoaiHocHamHocViDHNC.Text != string.Empty)
                    {
                        InsertDataDHNC();
                    }
                    else
                    {
                        _view.CBXLoaiHocHamHocViDHNC.ErrorText = "Vui lòng chọn trình độ.";
                        _view.CBXLoaiHocHamHocViDHNC.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                }
                else if (_view.TXTMaVienChuc.Text == string.Empty && maVienChucFromTabPageThongTinCaNhan != string.Empty)
                {
                    _view.TXTMaVienChuc.Text = maVienChucFromTabPageThongTinCaNhan;
                    if (_view.CBXLoaiHocHamHocViDHNC.Text != string.Empty)
                    {
                        InsertDataDHNC();
                    }
                    else
                    {
                        _view.CBXLoaiHocHamHocViDHNC.ErrorText = "Vui lòng chọn trình độ.";
                        _view.CBXLoaiHocHamHocViDHNC.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                }
                else XtraMessageBox.Show("Vui lòng thêm thông tin viên chức trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int row_handle = _view.GVDangHocNangCao.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    UpdateDataDHNC();
                }
            }
        }

        public void DeleteDHNC()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVDangHocNangCao.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVDangHocNangCao.GetFocusedRowCellDisplayText("Id"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.DangHocNangCaoRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVDangHocNangCao.DeleteRow(row_handle);
                        RefreshDHNC();
                        MainPresenter.LoadGridHocHamHocViAtRightViewInMainForm();
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Công tác này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcelDHNC() => ExportExcel(_view.GVDangHocNangCao);

        public static void RemoveFileIfNotSaveDHNC(string id)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<string> listLinkAnhQuyetDinh = unitOfWorks.DangHocNangCaoRepository.GetListLinkAnhQuyetDinh(maVienChucForGetListLinkAnhQuyetDinh);
            if (listLinkAnhQuyetDinh.Any(x => x.Equals("https://drive.google.com/open?id=" + id + "")) == false)
            {
                unitOfWorks.GoogleDriveFileRepository.DeleteFile(id);
            }
        }

        public void UploadFileToGoogleDriveDHNC()
        {
            if (_view.GVDangHocNangCao.FocusedRowHandle >= 0)
            {
                string mavienchuc = _view.TXTMaVienChuc.Text;
                _view.OpenFileDialog.FileName = string.Empty;
                _view.OpenFileDialog.Filter = "Pdf Files|*.pdf";
                if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                if (unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
                {
                    try
                    {
                        SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), false, false, true);
                        SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                        SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin lên Google Drive....");
                        string code = GenerateCode(); // code xac dinh file duy nhat
                        string filename = _view.OpenFileDialog.FileName;
                        string[] temp = filename.Split('\\');
                        string[] split_filename = filename.Split('.');
                        string new_filename = split_filename[0] + "-" + mavienchuc + "-" + code + "." + split_filename[1];
                        FileInfo fileInfo = new FileInfo(filename);
                        fileInfo.MoveTo(new_filename);
                        unitOfWorks.GoogleDriveFileRepository.UploadFile(new_filename);
                        FileInfo fileInfo1 = new FileInfo(new_filename); //doi lai filename cu~
                        fileInfo1.MoveTo(filename);
                        string id = unitOfWorks.GoogleDriveFileRepository.GetIdDriveFile(mavienchuc, code);
                        idFileUploadDHNC = id;
                        maVienChucForGetListLinkAnhQuyetDinh = mavienchuc;
                        _view.TXTLinkAnhQuyetDinh.Text = string.Empty;
                        _view.TXTLinkAnhQuyetDinh.Text = "https://drive.google.com/open?id=" + id + "";
                        SplashScreenManager.CloseForm();
                    }
                    catch
                    {
                        XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn dòng cần upload!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DownloadFileToDeviceDHNC()
        {
            string linkvanbandinhkem = _view.TXTLinkAnhQuyetDinh.Text.Trim();
            Download(linkvanbandinhkem);
        }

        public void SoQuyetDinhChanged(object sender, EventArgs e)
        {
            soQuyetDinhChanged = true;
        }

        public void LoaiHocHamHocViDangHocNangCaoChanged(object sender, EventArgs e)
        {
            loaiHocHamHocViDangHocNangCaoChanged = true;
        }

        public void TenHocHamHocViDangHocNangCaoChanged(object sender, EventArgs e)
        {
            tenHocHamHocViDangHocNangCaoChanged = true;
        }

        public void NgayBatDauDangHocNangCaoChanged(object sender, EventArgs e)
        {
            ngayBatDauDangHocNangCaoChanged = true;
        }

        public void NgayKetThucDangHocNangCaoChanged(object sender, EventArgs e)
        {
            ngayKetThucDangHocNangCaoChanged = true;
        }

        public void CoSoDaoTaoDangHocNangCaoChanged(object sender, EventArgs e)
        {
            coSoDaoTaoDangHocNangCaoChanged = true;
        }
        
        public void NgonNguDaoTaoDangHocNangCaoChanged(object sender, EventArgs e)
        {
            ngonNguDaoTaoDangHocNangCaoChanged = true;
        }

        public void HinhThucDaoTaoDangHocNangCaoChanged(object sender, EventArgs e)
        {
            hinhThucDaoTaoDangHocNangCaoChanged = true;
        }

        public void NuocCapBangDangHocNangCaoChanged(object sender, EventArgs e)
        {
            nuocCapBangDangHocNangCaoChanged = true;
        }

        public void LoaiDangHocNangCaoChanged(object sender, EventArgs e)
        {
            loaiDangHocNangCaoChanged = true;
        }

        public void LinkAnhQuyetDinhChanged(object sender, EventArgs e)
        {
            linkAnhQuyetDinhChanged = true;
        }

        public void RowIndicatorDHNC(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        #endregion
        #region Nganh
        public static string idFileUploadN = string.Empty;
        private static string maVienChucForGetListLinkVanBanDinhKemN = string.Empty;
        private bool checkAddNewN = true;
        private bool loaiNganhNChanged = false;
        private bool nganhDaoTaoNChanged = false;
        private bool chuyenNganhNChanged = false;
        private bool tenHocHamHocViNChanged = false;
        private bool ngayBatDauNChanged = false;
        private bool ngayKetThucNChanged = false;
        private bool trinhDoDayNChanged = false;
        private bool phanLoaiNChanged = false;
        private bool linkVanBanDinhKemNChanged = false;
        private void LoadCbxDataN()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<LoaiNganh> listLoaiNganh = unitOfWorks.LoaiNganhRepository.GetListLoaiNganh().ToList();
            _view.CBXLoaiNganhN.Properties.DataSource = listLoaiNganh;
            _view.CBXLoaiNganhN.Properties.DisplayMember = "tenLoaiNganh";
            _view.CBXLoaiNganhN.Properties.ValueMember = "idLoaiNganh";
            _view.CBXLoaiNganhN.Properties.DropDownRows = listLoaiNganh.Count;
            _view.CBXLoaiNganhN.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idLoaiNganh", string.Empty));
            _view.CBXLoaiNganhN.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenLoaiNganh", string.Empty));
            _view.CBXLoaiNganhN.Properties.Columns[0].Visible = false;
            List<NganhDaoTao> listNganhDaoTao = unitOfWorks.NganhDaoTaoRepository.GetListNganhDaoTao().ToList();
            _view.CBXNganhDaoTaoN.Properties.DataSource = listNganhDaoTao;
            _view.CBXNganhDaoTaoN.Properties.DisplayMember = "tenNganhDaoTao";
            _view.CBXNganhDaoTaoN.Properties.ValueMember = "idNganhDaoTao";
            _view.CBXNganhDaoTaoN.Properties.DropDownRows = listNganhDaoTao.Count;
            _view.CBXNganhDaoTaoN.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idNganhDaoTao", string.Empty));
            _view.CBXNganhDaoTaoN.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenNganhDaoTao", string.Empty));
            _view.CBXNganhDaoTaoN.Properties.Columns[0].Visible = false;
            List<ChuyenNganh> listChuyenNganh = unitOfWorks.ChuyenNganhRepository.GetListChuyenNganh().ToList();
            _view.CBXChuyenNganhN.Properties.DataSource = listChuyenNganh;
            _view.CBXChuyenNganhN.Properties.DisplayMember = "tenChuyenNganh";
            _view.CBXChuyenNganhN.Properties.ValueMember = "idChuyenNganh";
            _view.CBXChuyenNganhN.Properties.DropDownRows = listChuyenNganh.Count;
            _view.CBXChuyenNganhN.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idChuyenNganh", string.Empty));
            _view.CBXChuyenNganhN.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenChuyenNganh", string.Empty));
            _view.CBXChuyenNganhN.Properties.Columns[0].Visible = false;
            String mavienchuc = _view.TXTMaVienChuc.Text;
            List<HocHamHocViVienChuc> listTenHocHamHocViVienChuc = unitOfWorks.HocHamHocViVienChucRepository.GetListTenHocHamHocViVienChuc(mavienchuc);
            _view.CBXTenHocHamHocViN.Properties.DataSource = listTenHocHamHocViVienChuc;
            _view.CBXTenHocHamHocViN.Properties.DisplayMember = "tenHocHamHocVi";
            _view.CBXTenHocHamHocViN.Properties.ValueMember = "idHocHamHocViVienChuc";
            _view.CBXTenHocHamHocViN.Properties.DropDownRows = listTenHocHamHocViVienChuc.Count;
            _view.CBXTenHocHamHocViN.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idHocHamHocViVienChuc", string.Empty));
            _view.CBXTenHocHamHocViN.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenHocHamHocVi", string.Empty));
            _view.CBXTenHocHamHocViN.Properties.Columns[0].Visible = false;
            List<string> listTrinhDoDay = unitOfWorks.NganhVienChucRepository.GetListTrinhDoDay();
            AutoCompleteStringCollection trinhdodaySource = new AutoCompleteStringCollection();
            listTrinhDoDay.ForEach(x => trinhdodaySource.Add(x)); // autocompleteStringCollection if add null value app will be crashed
            _view.TXTTrinhDoDay.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _view.TXTTrinhDoDay.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _view.TXTTrinhDoDay.MaskBox.AutoCompleteCustomSource = trinhdodaySource;
        }
        private void SetDefaultValueControlN()
        {
            checkAddNewN = true;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            _view.CBXTenHocHamHocViN.ErrorText = string.Empty;
            _view.CBXNganhDaoTaoN.ErrorText = string.Empty;
            _view.CBXChuyenNganhN.ErrorText = string.Empty;
            _view.CBXTenHocHamHocViN.EditValue = unitOfWorks.HocHamHocViVienChucRepository.GetIdHocHamHocViVienChucEmpty();
            _view.CBXLoaiNganhN.EditValue = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganhEmpty();
            _view.CBXNganhDaoTaoN.EditValue = -1;
            _view.CBXChuyenNganhN.EditValue = -1;
            _view.DTNgayBatDauN.Text = string.Empty;
            _view.DTNgayKetThucN.Text = string.Empty;
            _view.TXTLinkVanBanDinhKemN.Text = string.Empty;
        }
        private void InsertDataN()
        {
            string mavienchuc = _view.TXTMaVienChuc.Text;
            int nganhdaotao = Convert.ToInt32(_view.CBXNganhDaoTaoN.EditValue);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            unitOfWorks.NganhVienChucRepository.Insert(new NganhVienChuc
            {
                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                idLoaiNganh = unitOfWorks.NganhDaoTaoRepository.GetIdLoaiNganhByIdNganhDaoTao(nganhdaotao),
                idHocHamHocViVienChuc = Convert.ToInt32(_view.CBXTenHocHamHocViN.EditValue),
                idNganhDaoTao = nganhdaotao,
                idChuyenNganh = Convert.ToInt32(_view.CBXChuyenNganhN.EditValue),
                phanLoai = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToDatabase(_view.RADPhanLoaiN.SelectedIndex),
                ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayBatDauN.Text),
                ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayKetThucN.Text),
                linkVanBanDinhKem = _view.TXTLinkVanBanDinhKemN.Text,
                trinhDoDay = _view.TXTTrinhDoDay.Text.Trim()
            });
            unitOfWorks.Save();
            LoadGridTabPageNganh(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetDefaultValueControlN();
        }
        private void UpdateDataN()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int idnganhvienchuc = Convert.ToInt32(_view.GVNganh.GetFocusedRowCellDisplayText("Id"));
            int idLoaiNganh = Convert.ToInt32(_view.CBXLoaiNganhN.EditValue);
            int idNganhDaoTao = Convert.ToInt32(_view.CBXNganhDaoTaoN.EditValue);
            int idChuyenNganh = Convert.ToInt32(_view.CBXChuyenNganhN.EditValue);
            int idHocHamHocViVienChuc = Convert.ToInt32(_view.CBXTenHocHamHocViN.EditValue);
            NganhVienChuc nganhVienChuc = unitOfWorks.NganhVienChucRepository.GetObjectById(idnganhvienchuc);
            if (loaiNganhNChanged)
            {
                nganhVienChuc.idLoaiNganh = idLoaiNganh;
                loaiNganhNChanged = false;
            }
            if (nganhDaoTaoNChanged)
            {
                nganhVienChuc.idNganhDaoTao = idNganhDaoTao;
                nganhDaoTaoNChanged = false;
            }
            if (chuyenNganhNChanged)
            {
                nganhVienChuc.idChuyenNganh = idChuyenNganh;
                chuyenNganhNChanged = false;
            }
            if (tenHocHamHocViNChanged)
            {
                nganhVienChuc.idHocHamHocViVienChuc = idHocHamHocViVienChuc;
                tenHocHamHocViNChanged = false;
            }
            if (phanLoaiNChanged)
            {
                nganhVienChuc.phanLoai = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToDatabase(_view.RADPhanLoaiN.SelectedIndex);
                phanLoaiNChanged = false;
            }
            if (ngayBatDauNChanged)
            {
                nganhVienChuc.ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayBatDauN.Text);
                ngayBatDauNChanged = false;
            }
            if (ngayKetThucNChanged)
            {
                nganhVienChuc.ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayKetThucN.Text);
                ngayKetThucNChanged = false;
            }
            if (trinhDoDayNChanged)
            {
                nganhVienChuc.trinhDoDay = _view.TXTTrinhDoDay.Text.Trim();
                trinhDoDayNChanged = false;
            }
            if (linkVanBanDinhKemNChanged)
            {
                nganhVienChuc.linkVanBanDinhKem = _view.TXTLinkVanBanDinhKemN.Text;
                linkVanBanDinhKemNChanged = false;
            }
            unitOfWorks.Save();
            LoadGridTabPageNganh(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void LoadGridTabPageNganh(string mavienchuc)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<NganhForView> listNganhForView = unitOfWorks.NganhVienChucRepository.GetListNganh(mavienchuc);
            _view.GCNganh.DataSource = listNganhForView;
        }

        public void SelectTabNganh() => _view.XtraTabControl.SelectedTabPageIndex = 2;

        public void ClickRowAndShowInfoN()
        {
            checkAddNewN = false;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVNganh.FocusedRowHandle;
            if (row_handle >= 0)
            {
                string loainganh = _view.GVNganh.GetFocusedRowCellDisplayText("LoaiNganh");
                string nganhdaotao = _view.GVNganh.GetFocusedRowCellDisplayText("NganhDaoTao");
                string chuyennganh = _view.GVNganh.GetFocusedRowCellDisplayText("ChuyenNganh");
                _view.CBXTenHocHamHocViN.EditValue = Convert.ToInt32(_view.GVNganh.GetFocusedRowCellDisplayText("IdHocHamHocViVienChuc"));
                _view.CBXLoaiNganhN.EditValue = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganh(loainganh);
                _view.CBXNganhDaoTaoN.EditValue = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTao(nganhdaotao);
                _view.CBXChuyenNganhN.EditValue = unitOfWorks.ChuyenNganhRepository.GetIdChuyenNganh(chuyennganh);
                _view.TXTTrinhDoDay.Text = _view.GVNganh.GetRowCellValue(row_handle, "TrinhDoDay").ToString();
                _view.RADPhanLoaiN.SelectedIndex = unitOfWorks.NganhVienChucRepository.HardCodePhanLoaiToRadioGroup(_view.GVNganh.GetFocusedRowCellDisplayText("PhanLoai"));
                _view.DTNgayBatDauN.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVNganh.GetFocusedRowCellDisplayText("NgayBatDau"));
                _view.DTNgayKetThucN.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVNganh.GetFocusedRowCellDisplayText("NgayKetThuc"));
                _view.TXTLinkVanBanDinhKemN.Text = _view.GVNganh.GetFocusedRowCellDisplayText("LinkVanBanDinhKem");
            }
        }

        public void RefreshN() => SetDefaultValueControlN();

        public void AddN() => SetDefaultValueControlN();

        public void SaveN()
        {
            if (checkAddNewN)
            {
                if (_view.TXTMaVienChuc.Text != string.Empty && maVienChucFromTabPageThongTinCaNhan == string.Empty)
                {                    
                    if (_view.CBXNganhDaoTaoN.Text == string.Empty)
                    {
                        _view.CBXNganhDaoTaoN.ErrorText = "Vui lòng chọn ngành đào tạo.";
                        _view.CBXNganhDaoTaoN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.CBXChuyenNganhN.Text == string.Empty)
                    {
                        _view.CBXChuyenNganhN.ErrorText = "Vui lòng chọn chuyên ngành.";
                        _view.CBXChuyenNganhN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.CBXNganhDaoTaoN.Text != string.Empty && _view.CBXChuyenNganhN.Text != string.Empty && _view.CBXTenHocHamHocViN.Text != string.Empty)
                    {
                        InsertDataN();
                    }
                }
                else if (_view.TXTMaVienChuc.Text == string.Empty && maVienChucFromTabPageThongTinCaNhan != string.Empty)
                {
                    _view.TXTMaVienChuc.Text = maVienChucFromTabPageThongTinCaNhan;                    
                    if (_view.CBXNganhDaoTaoN.Text == string.Empty)
                    {
                        _view.CBXNganhDaoTaoN.ErrorText = "Vui lòng chọn ngành đào tạo.";
                        _view.CBXNganhDaoTaoN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.CBXChuyenNganhN.Text == string.Empty)
                    {
                        _view.CBXChuyenNganhN.ErrorText = "Vui lòng chọn chuyên ngành.";
                        _view.CBXChuyenNganhN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.CBXNganhDaoTaoN.Text != string.Empty && _view.CBXChuyenNganhN.Text != string.Empty && _view.CBXTenHocHamHocViN.Text != string.Empty)
                    {
                        InsertDataN();
                    }
                }
                else XtraMessageBox.Show("Vui lòng thêm thông tin viên chức trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int row_handle = _view.GVNganh.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    UpdateDataN();
                }
            }
        }

        public void DeleteN()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVNganh.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVNganh.GetFocusedRowCellDisplayText("Id"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.NganhVienChucRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVNganh.DeleteRow(row_handle);
                        RefreshN();
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Công tác này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcelN() => ExportExcel(_view.GVNganh);

        public static void RemoveFileIfNotSaveN(string id)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<string> listLinkVanBanDinhKem = unitOfWorks.NganhVienChucRepository.GetListLinkVanBanDinhKem(maVienChucForGetListLinkVanBanDinhKemN);
            if (listLinkVanBanDinhKem.Any(x => x.Equals("https://drive.google.com/open?id=" + id + "")) == false)
            {
                unitOfWorks.GoogleDriveFileRepository.DeleteFile(id);
            }
        }

        public void UploadFileToGoogleDriveN()
        {
            if (_view.GVNganh.FocusedRowHandle >= 0)
            {
                string mavienchuc = _view.TXTMaVienChuc.Text;
                _view.OpenFileDialog.FileName = string.Empty;
                _view.OpenFileDialog.Filter = "Pdf Files|*.pdf";
                if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                if (unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
                {
                    try
                    {
                        SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), false, false, true);
                        SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                        SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin lên Google Drive....");
                        string code = GenerateCode(); // code xac dinh file duy nhat
                        string filename = _view.OpenFileDialog.FileName;
                        string[] temp = filename.Split('\\');
                        string[] split_filename = filename.Split('.');
                        string new_filename = split_filename[0] + "-" + mavienchuc + "-" + code + "." + split_filename[1];
                        FileInfo fileInfo = new FileInfo(filename);
                        fileInfo.MoveTo(new_filename);
                        unitOfWorks.GoogleDriveFileRepository.UploadFile(new_filename);
                        FileInfo fileInfo1 = new FileInfo(new_filename); //doi lai filename cu~
                        fileInfo1.MoveTo(filename);
                        string id = unitOfWorks.GoogleDriveFileRepository.GetIdDriveFile(mavienchuc, code);
                        idFileUploadN = id;
                        maVienChucForGetListLinkVanBanDinhKemN = mavienchuc;
                        _view.TXTLinkVanBanDinhKemN.Text = string.Empty;
                        _view.TXTLinkVanBanDinhKemN.Text = "https://drive.google.com/open?id=" + id + "";
                        SplashScreenManager.CloseForm();
                    }
                    catch
                    {
                        XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn dòng cần upload!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DownloadFileToDeviceN()
        {
            string linkvanbandinhkem = _view.TXTLinkVanBanDinhKemN.Text;
            Download(linkvanbandinhkem);
        }

        public void NganhDaoTaoNChanged(object sender, EventArgs e)
        {
            nganhDaoTaoNChanged = true;
            if (_view.CBXNganhDaoTaoN.Text != string.Empty)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int idnganhdaotao = Convert.ToInt32(_view.CBXNganhDaoTaoN.EditValue);
                int idloainganh = unitOfWorks.NganhDaoTaoRepository.GetIdLoaiNganhByIdNganhDaoTao(idnganhdaotao);
                _view.CBXLoaiNganhHHHV.EditValue = idloainganh;
                List<ChuyenNganh> list = unitOfWorks.ChuyenNganhRepository.GetListChuyenNganhByIdNganhDaoTao(idnganhdaotao);
                _view.CBXChuyenNganhHHHV.Properties.DisplayMember = "tenChuyenNganh";
                _view.CBXChuyenNganhHHHV.Properties.ValueMember = "idChuyenNganh";
                _view.CBXChuyenNganhHHHV.Properties.DataSource = list;
                _view.CBXChuyenNganhHHHV.Properties.DropDownRows = list.Count;
                _view.CBXChuyenNganhHHHV.Properties.Columns[0].Visible = false;
            }
        }

        public void LoaiNganhNChanged(object sender, EventArgs e)
        {
            loaiNganhNChanged = true;
            if (_view.CBXLoaiNganhN.Text != string.Empty)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int idloainganh = Convert.ToInt32(_view.CBXLoaiNganhN.EditValue);
                List<NganhDaoTao> list = unitOfWorks.NganhDaoTaoRepository.GetListNganhDaoTaoByIdLoaiNganh(idloainganh);
                _view.CBXNganhDaoTaoN.Properties.DisplayMember = "tenNganhDaoTao";
                _view.CBXNganhDaoTaoN.Properties.ValueMember = "idNganhDaoTao";
                _view.CBXNganhDaoTaoN.Properties.DataSource = list;
                _view.CBXNganhDaoTaoN.Properties.DropDownRows = list.Count;
                _view.CBXNganhDaoTaoN.Properties.Columns[0].Visible = false;
            }
        }

        public void ChuyenNganhNChanged(object sender, EventArgs e)
        {
            chuyenNganhNChanged = true;
            if (_view.CBXChuyenNganhN.Text != string.Empty)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                if (_view.CBXLoaiNganhN.Text == string.Empty && _view.CBXNganhDaoTaoN.Text == string.Empty)
                {
                    int idchuyennganh = Convert.ToInt32(_view.CBXChuyenNganhN.EditValue);
                    int idnganhdaotao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTaoByIdChuyenNganh(idchuyennganh);
                    int idloainganh = unitOfWorks.LoaiNganhRepository.GetIdLoaiNganhByIdNganhDaoTao(idnganhdaotao);
                    _view.CBXLoaiNganhN.EditValue = idloainganh;
                    _view.CBXNganhDaoTaoN.EditValue = idnganhdaotao;
                }
                if (_view.CBXLoaiNganhN.Text != string.Empty && _view.CBXNganhDaoTaoN.Text == string.Empty)
                {
                    int idchuyennganh = Convert.ToInt32(_view.CBXChuyenNganhN.EditValue);
                    int idnganhdaotao = unitOfWorks.NganhDaoTaoRepository.GetIdNganhDaoTaoByIdChuyenNganh(idchuyennganh);
                    _view.CBXNganhDaoTaoN.EditValue = idnganhdaotao;
                }
            }
        }

        public void TenHocHamHocViNChanged(object sender, EventArgs e)
        {
            tenHocHamHocViNChanged = true;
        }

        public void NgayBatDauNChanged(object sender, EventArgs e)
        {
            ngayBatDauNChanged = true;
        }

        public void NgayKetThucNChanged(object sender, EventArgs e)
        {
            ngayKetThucNChanged = true;
        }

        public void TrinhDoDayNChanged(object sender, EventArgs e)
        {
            trinhDoDayNChanged = true;
        }

        public void PhanLoaiNChanged(object sender, EventArgs e)
        {
            phanLoaiNChanged = true;
        }

        public void LinkVanBanDinhKemNChanged(object sender, EventArgs e)
        {
            linkVanBanDinhKemNChanged = true;
        }

        public void RowIndicatorN(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        #endregion
        #region ChungChi
        private bool loaiChungChiChanged = false;
        private bool capDoChungChiChanged = false;
        private bool ngayCapChungChiChanged = false;
        private bool ghiChuChungChiChanged = false;
        private bool linkVanBanDinhKemChungChiChanged = false;
        private bool checkAddNewCC = true;
        private static string maVienChucForGetListLinkVanBanDinhKemCC = string.Empty;
        public static string idFileUploadCC = string.Empty;
        private void LoadGridTabPageChungChi(string mavienchuc)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<ChungChiForView> listChungChiForView = unitOfWorks.ChungChiVienChucRepository.GetListChungChiVienChuc(mavienchuc);
            _view.GCChungChi.DataSource = listChungChiForView;
        }
        private void LoadCbxDataCC()
        {
            _view.CBXLoaiChungChi.Properties.DataSource = null;
            _view.CBXLoaiChungChi.Properties.Columns.Clear();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<string> listTenLoaiChungChi = unitOfWorks.LoaiChungChiRepository.GetListTenLoaiChungChi();
            _view.CBXLoaiChungChi.Properties.DataSource = listTenLoaiChungChi;
            _view.CBXLoaiChungChi.Properties.DropDownRows = listTenLoaiChungChi.Count;

            List<string> listCapDoChungChi = unitOfWorks.NganhVienChucRepository.GetListCapDoChungChi();
            AutoCompleteStringCollection capdoSource = new AutoCompleteStringCollection();
            listCapDoChungChi.ForEach(x => capdoSource.Add(x)); // autocompleteStringCollection if add null value app will be crashed
            _view.TXTCapDoChungChi.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _view.TXTCapDoChungChi.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _view.TXTCapDoChungChi.MaskBox.AutoCompleteCustomSource = capdoSource;
        }
        private void SetDefaultValueControlCC()
        {
            checkAddNewCC = true;
            _view.CBXLoaiChungChi.ErrorText = string.Empty;
            _view.TXTCapDoChungChi.ErrorText = string.Empty;
            _view.CBXLoaiChungChi.Text = string.Empty;
            _view.TXTCapDoChungChi.Text = string.Empty;
            _view.TXTGhiChuCC.Text = string.Empty;
            _view.DTNgayCapChungChi.Text = string.Empty;
            _view.TXTLinkVanBanDinhKemCC.Text = string.Empty;
        }
        private void InsertDataCC()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string mavienchuc = _view.TXTMaVienChuc.Text;
            string loaichungchi = _view.CBXLoaiChungChi.Text;
            string capdo = _view.TXTCapDoChungChi.Text.Trim();
            int idloaichungchi = unitOfWorks.LoaiChungChiRepository.GetIdLoaiChungChi(loaichungchi);
            if (idloaichungchi > 0)
            {
                unitOfWorks.ChungChiVienChucRepository.Insert(new ChungChiVienChuc
                {
                    idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                    idLoaiChungChi = idloaichungchi,
                    capDoChungChi = capdo,
                    ngayCapChungChi = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayCapChungChi.Text),
                    ghiChu = _view.TXTGhiChuCC.Text,
                    linkVanBanDinhKem = _view.TXTGhiChuCC.Text
                });
                unitOfWorks.Save();
                LoadGridTabPageChungChi(_view.TXTMaVienChuc.Text);
                XtraMessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetDefaultValueControlCC();
                MainPresenter.LoadGridChungChi();                
            }
            else XtraMessageBox.Show("Bạn chọn sai chứng chỉ hoặc cấp độ. Vui lòng chọn lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void UpdateDataCC()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int idchungchivienchuc = Convert.ToInt32(_view.GVChungChi.GetFocusedRowCellDisplayText("Id"));
            string tenloaichungchi = _view.CBXLoaiChungChi.EditValue.ToString();
            string capdo = _view.TXTCapDoChungChi.Text;
            ChungChiVienChuc chungChiVienChuc = unitOfWorks.ChungChiVienChucRepository.GetObjectById(idchungchivienchuc);
            if (loaiChungChiChanged)
            {
                chungChiVienChuc.idLoaiChungChi = unitOfWorks.LoaiChungChiRepository.GetIdLoaiChungChiByTen(tenloaichungchi);
                loaiChungChiChanged = false;
            }
            if (capDoChungChiChanged)
            {
                chungChiVienChuc.capDoChungChi = capdo;
                capDoChungChiChanged = false;
            }
            if (ngayCapChungChiChanged)
            {
                chungChiVienChuc.ngayCapChungChi = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayCapChungChi.Text);
                ngayCapChungChiChanged = false;
            }
            if (ghiChuChungChiChanged)
            {
                chungChiVienChuc.ghiChu = _view.TXTGhiChuCC.Text;
                ghiChuChungChiChanged = false;
            }
            if (linkVanBanDinhKemChungChiChanged)
            {
                chungChiVienChuc.linkVanBanDinhKem = _view.TXTLinkVanBanDinhKemCC.Text;
                linkVanBanDinhKemChungChiChanged = false;
            }
            unitOfWorks.Save();
            LoadGridTabPageChungChi(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MainPresenter.LoadGridChungChi();       
        }

        public void SelectTabChungChi() => _view.XtraTabControl.SelectedTabPageIndex = 3;
        
        public void ClickRowAndShowInfoCC()
        {
            checkAddNewCC = false;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVChungChi.FocusedRowHandle;
            if (row_handle >= 0)
            {
                _view.CBXLoaiChungChi.EditValue = _view.GVChungChi.GetFocusedRowCellDisplayText("LoaiChungChi");
                _view.TXTCapDoChungChi.Text = _view.GVChungChi.GetFocusedRowCellDisplayText("CapDo");
                _view.DTNgayCapChungChi.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVChungChi.GetFocusedRowCellDisplayText("NgayCapChungChi"));
                _view.TXTGhiChuCC.Text = _view.GVChungChi.GetFocusedRowCellDisplayText("GhiChu");
                _view.TXTLinkVanBanDinhKemCC.Text = _view.GVChungChi.GetFocusedRowCellDisplayText("LinkVanBanDinhKem");
            }
        }

        public void RefreshCC() => SetDefaultValueControlCC();

        public void AddCC() => SetDefaultValueControlCC();

        public void SaveCC()
        {
            if (checkAddNewCC)
            {
                if (_view.TXTMaVienChuc.Text != string.Empty && maVienChucFromTabPageThongTinCaNhan == string.Empty)
                {                    
                    if (_view.CBXLoaiChungChi.Text == string.Empty)
                    {
                        _view.CBXLoaiChungChi.ErrorText = "Vui lòng chọn chứng chỉ.";
                        _view.CBXLoaiChungChi.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.CBXLoaiChungChi.Text != string.Empty && _view.TXTCapDoChungChi.Text != string.Empty)
                    {
                        InsertDataCC();
                    }
                }
                else if (_view.TXTMaVienChuc.Text == string.Empty && maVienChucFromTabPageThongTinCaNhan != string.Empty)
                {
                    _view.TXTMaVienChuc.Text = maVienChucFromTabPageThongTinCaNhan;
                    if (_view.CBXLoaiChungChi.Text == string.Empty)
                    {
                        _view.CBXLoaiChungChi.ErrorText = "Vui lòng chọn chứng chỉ.";
                        _view.CBXLoaiChungChi.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    }
                    if (_view.CBXLoaiChungChi.Text != string.Empty && _view.TXTCapDoChungChi.Text != string.Empty)
                    {
                        InsertDataCC();
                    }                    
                }
                else XtraMessageBox.Show("Vui lòng thêm thông tin viên chức trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int row_handle = _view.GVChungChi.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    UpdateDataCC();
                }
            }            
        }

        public void DeleteCC()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVChungChi.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVChungChi.GetFocusedRowCellDisplayText("Id"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.ChungChiVienChucRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVChungChi.DeleteRow(row_handle);
                        RefreshCC();
                        MainPresenter.LoadGridChungChi();
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Công tác này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcelCC() => ExportExcel(_view.GVChungChi);

        public static void RemoveFileIfNotSaveCC(string id)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<string> listLinkVanBanDinhKem = unitOfWorks.ChungChiVienChucRepository.GetListLinkVanBanDinhKem(maVienChucForGetListLinkVanBanDinhKemCC);
            if (listLinkVanBanDinhKem.Any(x => x.Equals("https://drive.google.com/open?id=" + id + "")) == false)
            {
                unitOfWorks.GoogleDriveFileRepository.DeleteFile(id);
            }
        }

        public void UploadFileToGoogleDriveCC()
        {
            if (_view.GVChungChi.FocusedRowHandle >= 0)
            {
                string mavienchuc = _view.TXTMaVienChuc.Text;
                _view.OpenFileDialog.FileName = string.Empty;
                _view.OpenFileDialog.Filter = "Pdf Files|*.pdf|All files (*.*)|*.*";
                if (_view.OpenFileDialog.ShowDialog() == DialogResult.Cancel) return;
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                if (unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
                {
                    try
                    {
                        SplashScreenManager.ShowForm(_createAndEditPersonInfoForm, typeof(WaitForm1), false, false, true);
                        SplashScreenManager.Default.SetWaitFormCaption("Vui lòng chờ");
                        SplashScreenManager.Default.SetWaitFormDescription("Đang tải tập tin lên Google Drive....");
                        string code = GenerateCode(); // code xac dinh file duy nhat
                        string filename = _view.OpenFileDialog.FileName;
                        string[] temp = filename.Split('\\');
                        string[] split_filename = filename.Split('.');
                        string new_filename = split_filename[0] + "-" + mavienchuc + "-" + code + "." + split_filename[1];
                        FileInfo fileInfo = new FileInfo(filename);
                        fileInfo.MoveTo(new_filename);
                        unitOfWorks.GoogleDriveFileRepository.UploadFile(new_filename);
                        FileInfo fileInfo1 = new FileInfo(new_filename); //doi lai filename cu~
                        fileInfo1.MoveTo(filename);
                        string id = unitOfWorks.GoogleDriveFileRepository.GetIdDriveFile(mavienchuc, code);
                        idFileUploadCC = id;//
                        maVienChucForGetListLinkVanBanDinhKemCC = mavienchuc;//
                        _view.TXTLinkVanBanDinhKemCC.Text = string.Empty;//
                        _view.TXTLinkVanBanDinhKemCC.Text = "https://drive.google.com/open?id=" + id + "";
                        SplashScreenManager.CloseForm();
                    }
                    catch
                    {
                        XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Tải lên thất bại. Vui lòng kiểm tra lại đường truyền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Bạn chưa chọn dòng cần upload!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DownloadFileToDeviceCC()
        {
            string linkvanbandinhkem = _view.TXTLinkVanBanDinhKemCC.Text.Trim();
            Download(linkvanbandinhkem);
        }

        public void LoaiChungChiChanged(object sender, EventArgs e)
        {
            loaiChungChiChanged = true;
        }

        public void CapDoChungChiChanged(object sender, EventArgs e)
        {
            capDoChungChiChanged = true;
        }

        public void NgayCapChungChiChanged(object sender, EventArgs e)
        {
            ngayCapChungChiChanged = true;
        }

        public void GhiChuChungChiChanged(object sender, EventArgs e)
        {
            ghiChuChungChiChanged = true;
        }

        public void LinkVanBanDinhKemChungChiChanged(object sender, EventArgs e)
        {
            linkVanBanDinhKemChungChiChanged = true;
        }

        public void RowIndicatorCC(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        #endregion
    }
}
