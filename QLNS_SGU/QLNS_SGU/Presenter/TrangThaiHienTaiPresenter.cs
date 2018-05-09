using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Model;
using Model.Entities;
using QLNS_SGU.View;

namespace QLNS_SGU.Presenter
{
    public interface ITrangThaiHienTaiPresenter : IPresenterArgument
    {
        void Close();
        void LoadForm();
        void DownloadFile();
    }
    public class TrangThaiHienTaiPresenter : ITrangThaiHienTaiPresenter
    {
        private TrangThaiHienTaiForm _view;
        private string maVienChucFromCreateAndEditPersonalInfoForm = string.Empty;
        public object UI => _view;

        public TrangThaiHienTaiPresenter(TrangThaiHienTaiForm view) => _view = view;

        public void Initialize(string mavienchuc)
        {
            _view.Attach(this);
            maVienChucFromCreateAndEditPersonalInfoForm = mavienchuc;
        }

        public void Close() => _view.Close();

        public void LoadForm()
        {
            LoadDataToLabel();
        }
        private void SetEmptyValue()
        {
            _view.LBTrangThai.Text = "Đang làm";
            _view.LBNgayBatDau.Text = string.Empty;
            _view.LBNgayKetThuc.Text = string.Empty;
            _view.LBMoTa.Text = string.Empty;
            _view.LBDiaDiem.Text = string.Empty;
            _view.LinkLBQuyetDinh.Text = string.Empty;
        }
        private void LoadDataToLabel()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            TrangThaiVienChuc trangThaiHienTai = unitOfWorks.TrangThaiVienChucRepository.GetTrangThaiHienTai(maVienChucFromCreateAndEditPersonalInfoForm);
            if(trangThaiHienTai != null)
            {
                DateTime currentDate = DateTime.Now.Date;
                DateTime ngayKetThuc = trangThaiHienTai.ngayKetThuc.Value.Date;
                if (ngayKetThuc >= currentDate)
                {
                    _view.LBTrangThai.Text = trangThaiHienTai.TrangThai.tenTrangThai;
                    _view.LBNgayBatDau.Text = string.Format("{0:dd/MM/yyyy}", trangThaiHienTai.ngayBatDau);
                    _view.LBNgayKetThuc.Text = string.Format("{0:dd/MM/yyyy}", trangThaiHienTai.ngayKetThuc);
                    _view.LBMoTa.Text = trangThaiHienTai.moTa;
                    _view.LBDiaDiem.Text = trangThaiHienTai.diaDiem;
                    _view.LinkLBQuyetDinh.Text = trangThaiHienTai.linkVanBanDinhKem;

                    _view.LBTrangThai.ToolTip = trangThaiHienTai.TrangThai.tenTrangThai;
                    _view.LBMoTa.ToolTip = trangThaiHienTai.moTa;
                    _view.LBDiaDiem.ToolTip = trangThaiHienTai.diaDiem;
                }
                else SetEmptyValue();
            }
            else SetEmptyValue();
        }

        public void DownloadFile()
        {
            string linkvanbandinhkem = _view.LinkLBQuyetDinh.Text;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string[] arr_linkvanbandinhkem = linkvanbandinhkem.Split('=');
            string idvanbandinhkem = arr_linkvanbandinhkem[1];
            if (unitOfWorks.GoogleDriveFileRepository.InternetAvailable() == true)
            {
                try
                {
                    SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false);
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
    }
}
