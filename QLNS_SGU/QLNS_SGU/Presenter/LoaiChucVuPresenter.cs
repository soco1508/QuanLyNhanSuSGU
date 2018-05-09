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
    public interface ILoaiChucVuPresenter : IPresenter
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
    public class LoaiChucVuPresenter : ILoaiChucVuPresenter
    {
        private LoaiChucVuForm _view;
        private bool checkNewRowExist = true;

        public object UI => _view;
        public LoaiChucVuPresenter(LoaiChucVuForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVLoaiChucVu.IndicatorWidth = 50;
            LoadDataToGrid();
        }
        private void CloseEditor()
        {
            _view.GVLoaiChucVu.CloseEditor();
            _view.GVLoaiChucVu.UpdateCurrentRow();
        }
        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<LoaiChucVu> listLoaiChucVu = new BindingList<LoaiChucVu>(unitOfWorks.LoaiChucVuRepository.GetListLoaiChucVu());
            _view.GCLoaiChucVu.DataSource = listLoaiChucVu;
            SplashScreenManager.CloseForm(false);            
        }

        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVLoaiChucVu.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVLoaiChucVu.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVLoaiChucVu.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                LoadDataToGrid();
                checkNewRowExist = true;
            }
        }

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVLoaiChucVu.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVLoaiChucVu.GetFocusedRowCellDisplayText("idLoaiChucVu"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.LoaiChucVuRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVLoaiChucVu.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Loại Chức Vụ này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVLoaiChucVu.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVLoaiChucVu.Columns[1].OptionsColumn.AllowEdit = false;
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], string.Empty);
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVLoaiChucVu.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVLoaiChucVu.Columns[1])
            {
                _view.GVLoaiChucVu.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVLoaiChucVu.ShowEditor();
            }
        }

        public void RefreshGrid() => LoadDataToGrid();

        public void SaveData()
        {
            CloseEditor();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVLoaiChucVu.FocusedRowHandle;
            int idRowFocused = Convert.ToInt32(_view.GVLoaiChucVu.GetFocusedRowCellDisplayText("idLoaiChucVu"));
            if (idRowFocused == 0)
            {
                string loaichucvu = _view.GVLoaiChucVu.GetFocusedRowCellDisplayText("tenLoaiChucVu").ToString();
                if (loaichucvu != string.Empty)
                {
                    unitOfWorks.LoaiChucVuRepository.Create(loaichucvu);
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVLoaiChucVu.MoveLast();
                }
                else
                {
                    _view.GVLoaiChucVu.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Loại Chức Vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int id = Convert.ToInt32(_view.GVLoaiChucVu.GetRowCellValue(row_handle, "idLoaiChucVu"));
                string loaichucvu = _view.GVLoaiChucVu.GetFocusedRowCellDisplayText("tenLoaiChucVu").ToString();
                if (loaichucvu != string.Empty)
                {
                    unitOfWorks.LoaiChucVuRepository.Update(id, loaichucvu);
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Loại Chức Vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVLoaiChucVu.SelectRow(row_handle);
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
