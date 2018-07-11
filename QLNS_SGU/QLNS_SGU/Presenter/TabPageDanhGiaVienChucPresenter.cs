using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
    public interface ITabPageDanhGiaVienChucPresenter : IPresenterArgument
    {
        void LoadForm();
        void KhoangThoiGianChanged(object sender, EventArgs e);
        void MucDoDanhGiaChanged(object sender, EventArgs e);
        void NgayBatDauChanged(object sender, EventArgs e);
        void NgayKetThucChanged(object sender, EventArgs e);
        void ClickRowAndShowInfo();
        void Save();
        void Refresh();
        void Add();
        void Delete();
        void ExportExcel();
        void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e);
    }
    public class TabPageDanhGiaVienChucPresenter : ITabPageDanhGiaVienChucPresenter
    {
        private TabPageDanhGiaVienChuc _view;
        private bool checkAddNew = true;
        private bool khoangThoiGianChanged = false;
        private bool ngayBatDauChanged = false;
        private bool ngayKetThucChanged = false;
        private bool mucDoDanhGiaChanged = false;
        public static string maVienChucFromTabPageThongTinCaNhan = string.Empty;
        public object UI => _view;
        public TabPageDanhGiaVienChucPresenter(TabPageDanhGiaVienChuc view)
        {
            _view = view;
        }
        public void Initialize(string mavienchuc)
        {
            _view.Attach(this);
            _view.TXTMaVienChuc.Text = mavienchuc;
        }

        private void LoadCbxData()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());

            List<DanhMucThoiGian> listDanhMucThoiGian = unitOfWorks.DanhMucThoiGianRepository.GetListDanhMucThoiGian();
            _view.CBXKhoangThoiGian.Properties.DataSource = listDanhMucThoiGian;
            _view.CBXKhoangThoiGian.Properties.DisplayMember = "tenDanhMucThoiGian";
            _view.CBXKhoangThoiGian.Properties.ValueMember = "idDanhMucThoiGian";
            _view.CBXKhoangThoiGian.Properties.DropDownRows = listDanhMucThoiGian.Count;
            _view.CBXKhoangThoiGian.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idDanhMucThoiGian", string.Empty));
            _view.CBXKhoangThoiGian.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenDanhMucThoiGian", string.Empty));
            _view.CBXKhoangThoiGian.Properties.Columns[0].Visible = false;

            List<MucDoDanhGia> listMucDoDanhGia = unitOfWorks.MucDoDanhGiaRepository.GetListMucDoDanhGia();
            _view.CBXMucDoDanhGia.Properties.DataSource = listMucDoDanhGia;
            _view.CBXMucDoDanhGia.Properties.DisplayMember = "tenMucDoDanhGia";
            _view.CBXMucDoDanhGia.Properties.ValueMember = "idMucDoDanhGia";
            _view.CBXMucDoDanhGia.Properties.DropDownRows = listMucDoDanhGia.Count;
            _view.CBXMucDoDanhGia.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idMucDoDanhGia", string.Empty));
            _view.CBXMucDoDanhGia.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenMucDoDanhGia", string.Empty));
            _view.CBXMucDoDanhGia.Properties.Columns[0].Visible = false;
        }
        private void LoadGridTabPageDanhGiaVienChuc(string mavienchuc)
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<QuaTrinhDanhGiaVienChucForView> list = unitOfWorks.QuaTrinhDanhGiaVienChucRepository.GetListQuaTrinhDanhGia(mavienchuc);
            _view.GCDanhGiaVienChuc.DataSource = list;
        }
        private void SetDefaultValueControl()
        {
            checkAddNew = true;
            _view.CBXKhoangThoiGian.Text = string.Empty;
            _view.DTNgayBatDau.Text = string.Empty;
            _view.DTNgayKetThuc.Text = string.Empty;
            _view.CBXMucDoDanhGia.Text = string.Empty;
        }
        private void InsertData()
        {
            string mavienchuc = _view.TXTMaVienChuc.Text;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            unitOfWorks.QuaTrinhDanhGiaVienChucRepository.Insert(new QuaTrinhDanhGiaVienChuc
            {
                idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc),
                idDanhMucThoiGian = Convert.ToInt32(_view.CBXKhoangThoiGian.EditValue),
                ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayBatDau.Text),
                ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayKetThuc.Text),
                idMucDoDanhGia = Convert.ToInt32(_view.CBXMucDoDanhGia.EditValue)
            });
            unitOfWorks.Save();
            LoadGridTabPageDanhGiaVienChuc(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetDefaultValueControl();
        }
        private void UpdateData()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int id = Convert.ToInt32(_view.GVDanhGiaVienChuc.GetFocusedRowCellDisplayText("Id"));
            DateTime? ngaybatdau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayBatDau.Text);
            DateTime? ngayketthuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayKetThuc.Text);
            QuaTrinhDanhGiaVienChuc quaTrinhDanhGia = unitOfWorks.QuaTrinhDanhGiaVienChucRepository.GetObjectById(id);
            if (khoangThoiGianChanged)
            {
                quaTrinhDanhGia.idDanhMucThoiGian = Convert.ToInt32(_view.CBXKhoangThoiGian.EditValue);
                khoangThoiGianChanged = false;
            }
            if (ngayBatDauChanged)
            {
                quaTrinhDanhGia.ngayBatDau = ngaybatdau;
                ngayBatDauChanged = false;
            }
            if (ngayKetThucChanged)
            {
                quaTrinhDanhGia.ngayKetThuc = ngayketthuc;
                ngayKetThucChanged = false;
            }
            if (mucDoDanhGiaChanged)
            {
                quaTrinhDanhGia.idMucDoDanhGia = Convert.ToInt32(_view.CBXMucDoDanhGia.EditValue);
                mucDoDanhGiaChanged = false;
            }
            unitOfWorks.Save();
            LoadGridTabPageDanhGiaVienChuc(_view.TXTMaVienChuc.Text);
            XtraMessageBox.Show("Sửa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void LoadForm()
        {
            LoadCbxData();
            string mavienchuc = _view.TXTMaVienChuc.Text;
            if (mavienchuc != string.Empty)
                LoadGridTabPageDanhGiaVienChuc(mavienchuc);
        }

        public void KhoangThoiGianChanged(object sender, EventArgs e)
        {
            khoangThoiGianChanged = true;
        }

        public void MucDoDanhGiaChanged(object sender, EventArgs e)
        {
            mucDoDanhGiaChanged = true;
        }

        public void NgayBatDauChanged(object sender, EventArgs e)
        {
            ngayBatDauChanged = true;
        }

        public void NgayKetThucChanged(object sender, EventArgs e)
        {
            ngayKetThucChanged = true;
        }

        public void ClickRowAndShowInfo()
        {
            checkAddNew = false;
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVDanhGiaVienChuc.FocusedRowHandle;
            if (row_handle >= 0)
            {
                string khoangthoigian = _view.GVDanhGiaVienChuc.GetFocusedRowCellDisplayText("KhoangThoiGian").ToString();
                string mucdodanhgia = _view.GVDanhGiaVienChuc.GetFocusedRowCellDisplayText("MucDoDanhGia").ToString();
                _view.CBXKhoangThoiGian.EditValue = unitOfWorks.DanhMucThoiGianRepository.GetIdByName(khoangthoigian);
                _view.CBXMucDoDanhGia.EditValue = unitOfWorks.MucDoDanhGiaRepository.GetIdByName(mucdodanhgia);
                _view.DTNgayBatDau.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVDanhGiaVienChuc.GetFocusedRowCellDisplayText("NgayBatDau"));
                _view.DTNgayKetThuc.EditValue = unitOfWorks.HopDongVienChucRepository.ReturnNullIfDateTimeNullToView(_view.GVDanhGiaVienChuc.GetFocusedRowCellDisplayText("NgayKetThuc"));
            }
        }

        public void Save()
        {
            if (checkAddNew)
            {
                if (_view.TXTMaVienChuc.Text != string.Empty && maVienChucFromTabPageThongTinCaNhan == string.Empty)
                {
                    InsertData();
                }
                else if (_view.TXTMaVienChuc.Text == string.Empty && maVienChucFromTabPageThongTinCaNhan != string.Empty)
                {
                    _view.TXTMaVienChuc.Text = maVienChucFromTabPageThongTinCaNhan;
                    InsertData();
                }
                else XtraMessageBox.Show("Vui lòng thêm thông tin viên chức trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int row_handle = _view.GVDanhGiaVienChuc.FocusedRowHandle;
                if (row_handle >= 0)
                    UpdateData();
            }
        }

        public void Refresh() => SetDefaultValueControl();

        public void Add() => SetDefaultValueControl();

        public void Delete()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVDanhGiaVienChuc.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVDanhGiaVienChuc.GetFocusedRowCellDisplayText("Id"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.QuaTrinhDanhGiaVienChucRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVDanhGiaVienChuc.DeleteRow(row_handle);
                        Refresh();
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Quá trình đánh giá này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVDanhGiaVienChuc.ExportToXlsx(_view.SaveFileDialog.FileName);
            XtraMessageBox.Show("Xuất Excel thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
