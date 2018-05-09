using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Model;
using Model.Entities;
using QLNS_SGU.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS_SGU.Presenter
{
    public interface ITabPageBaoHiemXaHoiPresenter : IPresenterArgument
    {
        void LoadForm();
        void SaveBaoHiemXaHoi();
        void SoSoChanged(object sender, EventArgs e);
        void NgayThamGiaChanged(object sender, EventArgs e);
        void NgayCapSoChanged(object sender, EventArgs e);
        void NgayRutSoChanged(object sender, EventArgs e);
        void GhiChuChanged(object sender, EventArgs e);
        void ButtonClick(object sender, NavigatorButtonClickEventArgs e);
        void MouseDoubleClick(object sender, MouseEventArgs e);
        void HiddenEditor(object sender, EventArgs e);
        void InitNewRow(object sender, InitNewRowEventArgs e);
        void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e);
    }
    public class TabPageBaoHiemXaHoiPresenter : ITabPageBaoHiemXaHoiPresenter
    {
        private string maVienChuc = string.Empty;
        private bool soSoChanged = false;
        private bool ngayThamGiaChanged = false;
        private bool ngayCapSoChanged = false;
        private bool ngayRutSoChanged = false;
        private bool ghiChuChanged = false;
        private TabPageBaoHiemXaHoi _view;

        public object UI => _view;
        public TabPageBaoHiemXaHoiPresenter(TabPageBaoHiemXaHoi view) => _view = view;
        public void Initialize(string mavienchuc)
        {
            _view.Attach(this);
            maVienChuc = mavienchuc;
            _view.GVQuaTrinhGianDoanBaoHiemXaHoi.IndicatorWidth = 50;
        }
        private void CloseEditor()
        {
            _view.GVQuaTrinhGianDoanBaoHiemXaHoi.CloseEditor();
            _view.GVQuaTrinhGianDoanBaoHiemXaHoi.UpdateCurrentRow();
        }
        private void InsertBaoHiemXaHoi()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int idvienchuc = unitOfWorks.VienChucRepository.GetIdVienChuc(maVienChuc);
            unitOfWorks.BaoHiemXaHoiRepository.Insert(new BaoHiemXaHoi
            {
                soSo = _view.TXTSoSo.Text,
                ngayThamGia = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayThamGia.Text),
                ngayCapSo = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayCapSo.Text),
                ngayRutSo = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayRutSo.Text),
                ghiChu = _view.TXTGhiChu.Text,
                idVienChuc = idvienchuc
            });
            unitOfWorks.Save();
            XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void UpdateBaoHiemXaHoi()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BaoHiemXaHoi baoHiemXaHoi = unitOfWorks.BaoHiemXaHoiRepository.GetObjectByMaVienChuc(maVienChuc);
            if (soSoChanged)
            {
                baoHiemXaHoi.soSo = _view.TXTSoSo.Text;
                soSoChanged = false;
            }
            if (ngayThamGiaChanged)
            {
                baoHiemXaHoi.ngayThamGia = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayThamGia.Text);
                ngayThamGiaChanged = false;
            }
            if (ngayCapSoChanged)
            {
                baoHiemXaHoi.ngayCapSo = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayCapSo.Text);
                ngayCapSoChanged = false;
            }
            if (ngayRutSoChanged)
            {
                baoHiemXaHoi.ngayRutSo = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.DTNgayRutSo.Text);
                ngayRutSoChanged = false;
            }
            if (ghiChuChanged)
            {
                baoHiemXaHoi.ghiChu = _view.TXTGhiChu.Text;
                ghiChuChanged = false;
            }
            unitOfWorks.Save();
            XtraMessageBox.Show("Cập nhật dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void LoadGridQuaTrinhGianDoanBaoHiemXaHoi()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<QuaTrinhGianDoanBaoHiemXaHoi> list = unitOfWorks.QuaTrinhGianDoanBaoHiemXaHoiRepository.GetListQuaTrinhGianDoan(maVienChuc);
            list.AllowNew = true;
            _view.GCQuaTrinhGianDoanBaoHiemXaHoi.DataSource = list;
        }
        private void InsertQuaTrinhGianDoanBaoHiemXaHoi()
        {
            CloseEditor();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int idvienchuc = unitOfWorks.VienChucRepository.GetIdVienChuc(maVienChuc);
            unitOfWorks.QuaTrinhGianDoanBaoHiemXaHoiRepository.Insert(new QuaTrinhGianDoanBaoHiemXaHoi
            {
                idVienChuc = idvienchuc,
                lyDo = _view.GVQuaTrinhGianDoanBaoHiemXaHoi.GetFocusedRowCellDisplayText("lyDo"),
                ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.GVQuaTrinhGianDoanBaoHiemXaHoi.GetFocusedRowCellDisplayText("ngayBatDau")),
                ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.GVQuaTrinhGianDoanBaoHiemXaHoi.GetFocusedRowCellDisplayText("ngayKetThuc"))
            });
            unitOfWorks.Save();
            _view.GVQuaTrinhGianDoanBaoHiemXaHoi.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            LoadGridQuaTrinhGianDoanBaoHiemXaHoi();
            XtraMessageBox.Show("Thêm dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);            
            
        }
        private void UpdateQuaTrinhGianDoanBaoHiemXaHoi()
        {
            CloseEditor();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int id = Convert.ToInt32(_view.GVQuaTrinhGianDoanBaoHiemXaHoi.GetFocusedRowCellValue("idQuaTrinhGianDoan"));
            QuaTrinhGianDoanBaoHiemXaHoi quaTrinhGianDoanBaoHiemXaHoi = unitOfWorks.QuaTrinhGianDoanBaoHiemXaHoiRepository.GetObjectById(id);
            quaTrinhGianDoanBaoHiemXaHoi.lyDo = _view.GVQuaTrinhGianDoanBaoHiemXaHoi.GetFocusedRowCellDisplayText("lyDo").ToString();
            quaTrinhGianDoanBaoHiemXaHoi.ngayBatDau = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.GVQuaTrinhGianDoanBaoHiemXaHoi.GetFocusedRowCellDisplayText("ngayBatDau"));
            quaTrinhGianDoanBaoHiemXaHoi.ngayKetThuc = unitOfWorks.HopDongVienChucRepository.ReturnDateTimeToDatabase(_view.GVQuaTrinhGianDoanBaoHiemXaHoi.GetFocusedRowCellDisplayText("ngayKetThuc"));
            unitOfWorks.Save();
            XtraMessageBox.Show("Cập nhật dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void DeleteQuaTrinhGianDoan(int id)
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    unitOfWorks.QuaTrinhGianDoanBaoHiemXaHoiRepository.DeleteById(id);
                    unitOfWorks.Save();
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Quá trình gián đoạn bảo hiểm xã hội này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadForm()
        {
            if (maVienChuc != string.Empty)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                BaoHiemXaHoi baoHiemXaHoi = unitOfWorks.BaoHiemXaHoiRepository.GetObjectByMaVienChuc(maVienChuc);
                if(baoHiemXaHoi != null)
                {
                    _view.TXTSoSo.Text = baoHiemXaHoi.soSo;
                    _view.DTNgayThamGia.EditValue = baoHiemXaHoi.ngayThamGia;
                    _view.DTNgayCapSo.EditValue = baoHiemXaHoi.ngayCapSo;
                    _view.DTNgayRutSo.EditValue = baoHiemXaHoi.ngayRutSo;
                    _view.TXTGhiChu.Text = baoHiemXaHoi.ghiChu;
                    LoadGridQuaTrinhGianDoanBaoHiemXaHoi();
                }
            }
        }

        public void SaveBaoHiemXaHoi()
        {
            if(maVienChuc != string.Empty)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int idbaohiemxahoi = unitOfWorks.BaoHiemXaHoiRepository.GetIdBaoHiemXaHoiByMaVienChuc(maVienChuc);
                if (idbaohiemxahoi > 0)
                    UpdateBaoHiemXaHoi();
                else InsertBaoHiemXaHoi();
            }
            else XtraMessageBox.Show("Vui lòng thêm thông tin viên chức trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void SoSoChanged(object sender, EventArgs e)
        {
            soSoChanged = true;
        }

        public void NgayThamGiaChanged(object sender, EventArgs e)
        {
            ngayThamGiaChanged = true;
        }

        public void NgayCapSoChanged(object sender, EventArgs e)
        {
            ngayCapSoChanged = true;
        }

        public void NgayRutSoChanged(object sender, EventArgs e)
        {
            ngayRutSoChanged = true;
        }

        public void GhiChuChanged(object sender, EventArgs e)
        {
            ghiChuChanged = true;
        }

        public void ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if(e.Button.ButtonType == NavigatorButtonType.Append)
            {
                _view.GVQuaTrinhGianDoanBaoHiemXaHoi.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            }
            if(e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                int id = Convert.ToInt32(_view.GVQuaTrinhGianDoanBaoHiemXaHoi.GetFocusedRowCellDisplayText("idQuaTrinhGianDoan"));
                if (id > 0)
                    DeleteQuaTrinhGianDoan(id);
            }
            if(e.Button.ButtonType == NavigatorButtonType.EndEdit)
            {
                int id = Convert.ToInt32(_view.GVQuaTrinhGianDoanBaoHiemXaHoi.GetFocusedRowCellValue("idQuaTrinhGianDoan"));
                if(id > 0)
                    UpdateQuaTrinhGianDoanBaoHiemXaHoi();
                else InsertQuaTrinhGianDoanBaoHiemXaHoi();
            }
            if(e.Button.ButtonType == NavigatorButtonType.CancelEdit)
            {
                _view.GVQuaTrinhGianDoanBaoHiemXaHoi.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                _view.GVQuaTrinhGianDoanBaoHiemXaHoi.FocusedRowHandle = 0;
            }
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVQuaTrinhGianDoanBaoHiemXaHoi.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVQuaTrinhGianDoanBaoHiemXaHoi.Columns[0])
            {
                _view.GVQuaTrinhGianDoanBaoHiemXaHoi.Columns[0].OptionsColumn.AllowEdit = true;
                _view.GVQuaTrinhGianDoanBaoHiemXaHoi.ShowEditor();
            }
            if (hinfo.Column == _view.GVQuaTrinhGianDoanBaoHiemXaHoi.Columns[1])
            {
                _view.GVQuaTrinhGianDoanBaoHiemXaHoi.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVQuaTrinhGianDoanBaoHiemXaHoi.ShowEditor();
            }
            if (hinfo.Column == _view.GVQuaTrinhGianDoanBaoHiemXaHoi.Columns[2])
            {
                _view.GVQuaTrinhGianDoanBaoHiemXaHoi.Columns[2].OptionsColumn.AllowEdit = true;
                _view.GVQuaTrinhGianDoanBaoHiemXaHoi.ShowEditor();
            }
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVQuaTrinhGianDoanBaoHiemXaHoi.Columns[0].OptionsColumn.AllowEdit = false;
            _view.GVQuaTrinhGianDoanBaoHiemXaHoi.Columns[1].OptionsColumn.AllowEdit = false;
            _view.GVQuaTrinhGianDoanBaoHiemXaHoi.Columns[2].OptionsColumn.AllowEdit = false;
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[0], string.Empty);
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], string.Empty);
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[2], string.Empty);
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
