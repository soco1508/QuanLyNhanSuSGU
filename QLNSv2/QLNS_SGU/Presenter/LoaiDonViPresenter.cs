using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraSplashScreen;
using Model;
using Model.Entities;
using QLNS_SGU.View;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace QLNS_SGU.Presenter
{
    public interface ILoaiDonViPresenter : IPresenter
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
    public class LoaiDonViPresenter : ILoaiDonViPresenter
    {
        private LoaiDonViForm _view;
        private bool checkNewRowExist = true;
       
        public object UI => _view;
        public LoaiDonViPresenter(LoaiDonViForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVLoaiDonVi.IndicatorWidth = 50;
            LoadDataToGrid();
        }

        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<LoaiDonVi> listLoaiDonVi = new BindingList<LoaiDonVi>(unitOfWorks.LoaiDonViRepository.GetListLoaiDonVi());
            _view.GCLoaiDonVi.DataSource = listLoaiDonVi;
            SplashScreenManager.CloseForm(false);
            
        }

        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVLoaiDonVi.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVLoaiDonVi.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVLoaiDonVi.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                LoadDataToGrid();
                checkNewRowExist = true;
            }
        }

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVLoaiDonVi.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVLoaiDonVi.GetFocusedRowCellDisplayText("idLoaiDonVi"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.LoaiDonViRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVLoaiDonVi.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Loại Đơn Vị này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVLoaiDonVi.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVLoaiDonVi.Columns[1].OptionsColumn.AllowEdit = false;
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], "");
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVLoaiDonVi.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVLoaiDonVi.Columns[1])
            {
                _view.GVLoaiDonVi.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVLoaiDonVi.ShowEditor();
            }
        }

        public void RefreshGrid()
        {
            LoadDataToGrid();
        }

        public void SaveData()
        {
            _view.GVLoaiDonVi.CloseEditor();
            _view.GVLoaiDonVi.UpdateCurrentRow();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVLoaiDonVi.FocusedRowHandle;
            int idRowFocused = Convert.ToInt32(_view.GVLoaiDonVi.GetFocusedRowCellDisplayText("idLoaiDonVi"));            
            if (idRowFocused == 0)
            {
                string loaidonvi = _view.GVLoaiDonVi.GetFocusedRowCellDisplayText("tenLoaiDonVi").ToString();
                if (loaidonvi != string.Empty)
                {
                    unitOfWorks.LoaiDonViRepository.Create(loaidonvi);
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVLoaiDonVi.MoveLast();
                }
                else
                {
                    _view.GVLoaiDonVi.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Loại Đơn Vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int id = Convert.ToInt32(_view.GVLoaiDonVi.GetRowCellValue(row_handle, "idLoaiDonVi"));
                string loaidonvi = _view.GVLoaiDonVi.GetFocusedRowCellDisplayText("tenLoaiDonVi").ToString();
                if (loaidonvi != string.Empty)
                {
                    unitOfWorks.LoaiDonViRepository.Update(id, loaidonvi);
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Loại Đơn Vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVLoaiDonVi.SelectRow(row_handle);
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
