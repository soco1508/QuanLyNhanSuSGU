using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraSplashScreen;
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
    public interface ITrangThaiPresenter : IPresenter
    {
        void AddNewRow();
        void MouseDoubleClick(object sender, MouseEventArgs e);
        void HiddenEditor(object sender, EventArgs e);
        void RefreshGrid();
        void ExportExcel();
        void SaveData();
        void InitNewRow(object sender, InitNewRowEventArgs e);
        void DeleteRow();
        void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e);
    }
    public class TrangThaiPresenter : ITrangThaiPresenter
    {
        private TrangThaiForm _view;
        private bool checkNewRowExist = true;
        public object UI => _view;
        public TrangThaiPresenter(TrangThaiForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVTrangThai.IndicatorWidth = 50;
            LoadDataToGrid();
        }

        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<TrangThai> listTrangThai = new BindingList<TrangThai>(unitOfWorks.TrangThaiRepository.GetListTrangThai());
            _view.GCTrangThai.DataSource = listTrangThai;
            SplashScreenManager.CloseForm(false);
        }

        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVTrangThai.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVTrangThai.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVTrangThai.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                LoadDataToGrid();
                checkNewRowExist = true;
            }
        }

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVTrangThai.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVTrangThai.GetFocusedRowCellDisplayText("idTrangThai"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.TrangThaiRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVTrangThai.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Trạng Thái này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVTrangThai.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVTrangThai.Columns[1].OptionsColumn.AllowEdit = false;
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], string.Empty);
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVTrangThai.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVTrangThai.Columns[1])
            {
                _view.GVTrangThai.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVTrangThai.ShowEditor();
            }
        }

        public void RefreshGrid() => LoadDataToGrid();

        public void SaveData()
        {
            _view.GVTrangThai.CloseEditor();
            _view.GVTrangThai.UpdateCurrentRow();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVTrangThai.FocusedRowHandle;
            int idRowFocused = Convert.ToInt32(_view.GVTrangThai.GetFocusedRowCellDisplayText("idTrangThai"));
            if (idRowFocused == 0)
            {
                string trangthai = _view.GVTrangThai.GetFocusedRowCellDisplayText("tenTrangThai").ToString();
                if (trangthai != string.Empty)
                {
                    unitOfWorks.TrangThaiRepository.Create(trangthai);
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVTrangThai.MoveLast();
                }
                else
                {
                    _view.GVTrangThai.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Trạng Thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int id = Convert.ToInt32(_view.GVTrangThai.GetRowCellValue(row_handle, "idTrangThai"));
                string trangthai = _view.GVTrangThai.GetFocusedRowCellDisplayText("tenTrangThai").ToString();
                if (trangthai != string.Empty)
                {
                    unitOfWorks.TrangThaiRepository.Update(id, trangthai);
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Trạng Thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVTrangThai.SelectRow(row_handle);
                }
            }
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
