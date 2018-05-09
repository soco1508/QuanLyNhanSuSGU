using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLNS_SGU.View;
using Model.Entities;
using Model;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;

namespace QLNS_SGU.Presenter
{
    public interface ILoaiNganhPresenter : IPresenter
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

    public class LoaiNganhPresenter : ILoaiNganhPresenter
    {
        private LoaiNganhForm _view;
        private bool checkNewRowExist = true;

        public object UI => _view;
        public LoaiNganhPresenter(LoaiNganhForm view) => _view = view;

        public void Initialize()
        {
            _view.Attach(this);
            _view.GVLoaiNganh.IndicatorWidth = 50;
            LoadDataToGrid();
        }

        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<LoaiNganh> listLoaiNganh = new BindingList<LoaiNganh>(unitOfWorks.LoaiNganhRepository.GetListLoaiNganh());
            _view.GCLoaiNganh.DataSource = listLoaiNganh;
            SplashScreenManager.CloseForm(false);
            
        }
        
        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVLoaiNganh.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVLoaiNganh.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVLoaiNganh.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                LoadDataToGrid();
                checkNewRowExist = true;
            }
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVLoaiNganh.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVLoaiNganh.Columns[1])
            {
                _view.GVLoaiNganh.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVLoaiNganh.ShowEditor();
            }
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVLoaiNganh.Columns[1].OptionsColumn.AllowEdit = false;
        }

        public void RefreshGrid() => LoadDataToGrid();

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVLoaiNganh.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void SaveData()
        {
            _view.GVLoaiNganh.CloseEditor();
            _view.GVLoaiNganh.UpdateCurrentRow();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVLoaiNganh.FocusedRowHandle;
            int idRowFocused = Convert.ToInt32(_view.GVLoaiNganh.GetFocusedRowCellDisplayText("idLoaiNganh"));
            if (idRowFocused == 0)
            {
                string loainganh = _view.GVLoaiNganh.GetFocusedRowCellDisplayText("tenLoaiNganh").ToString();
                if (loainganh != string.Empty)
                {                    
                    unitOfWorks.LoaiNganhRepository.Create(loainganh);
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVLoaiNganh.MoveLast();
                }
                else
                {
                    _view.GVLoaiNganh.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Loại Ngành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }
            }
            else
            {
                int id = Convert.ToInt32(_view.GVLoaiNganh.GetRowCellValue(row_handle, "idLoaiNganh"));
                string loainganh = _view.GVLoaiNganh.GetFocusedRowCellDisplayText("tenLoaiNganh").ToString();
                if (loainganh != string.Empty)
                {
                    unitOfWorks.LoaiNganhRepository.Update(id, loainganh);
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);                    
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Loại Ngành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVLoaiNganh.SelectRow(row_handle);
                }
            }
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {            
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], string.Empty); 
        }

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVLoaiNganh.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVLoaiNganh.GetFocusedRowCellDisplayText("idLoaiNganh"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.LoaiNganhRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVLoaiNganh.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);                               
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Loại Ngành này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
