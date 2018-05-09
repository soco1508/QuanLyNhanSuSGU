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
    public interface ILoaiHocHamHocViPresenter : IPresenter
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
    public class LoaiHocHamHocViPresenter : ILoaiHocHamHocViPresenter
    {
        private LoaiHocHamHocViForm _view;
        private bool checkNewRowExist = true;

        public object UI => _view;
        public LoaiHocHamHocViPresenter(LoaiHocHamHocViForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVLoaiHocHamHocVi.IndicatorWidth = 50;
            LoadDataToGrid();
        }

        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<LoaiHocHamHocVi> listLoaiHocHamHocVi = new BindingList<LoaiHocHamHocVi>(unitOfWorks.LoaiHocHamHocViRepository.GetListLoaiHocHamHocVi());
            _view.GCLoaiHocHamHocVi.DataSource = listLoaiHocHamHocVi;
            SplashScreenManager.CloseForm(false);
            
        }

        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVLoaiHocHamHocVi.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVLoaiHocHamHocVi.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVLoaiHocHamHocVi.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                LoadDataToGrid();
                checkNewRowExist = true;
            }
        }

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVLoaiHocHamHocVi.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVLoaiHocHamHocVi.GetFocusedRowCellDisplayText("idLoaiHocHamHocVi"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.LoaiHocHamHocViRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVLoaiHocHamHocVi.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Loại Học Hàm Học Vị này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVLoaiHocHamHocVi.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVLoaiHocHamHocVi.Columns[1].OptionsColumn.AllowEdit = false;
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], string.Empty);
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVLoaiHocHamHocVi.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVLoaiHocHamHocVi.Columns[1])
            {
                _view.GVLoaiHocHamHocVi.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVLoaiHocHamHocVi.ShowEditor();
            }
        }

        public void RefreshGrid() => LoadDataToGrid();

        public void SaveData()
        {
            _view.GVLoaiHocHamHocVi.CloseEditor();
            _view.GVLoaiHocHamHocVi.UpdateCurrentRow();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVLoaiHocHamHocVi.FocusedRowHandle;
            int idRowFocused = Convert.ToInt32(_view.GVLoaiHocHamHocVi.GetFocusedRowCellDisplayText("idLoaiHocHamHocVi"));
            if (idRowFocused == 0)
            {
                string loaihochamhocvi = _view.GVLoaiHocHamHocVi.GetFocusedRowCellDisplayText("tenLoaiHocHamHocVi").ToString();
                if (loaihochamhocvi != string.Empty)
                {
                    unitOfWorks.LoaiHocHamHocViRepository.Create(loaihochamhocvi);
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVLoaiHocHamHocVi.MoveLast();
                }
                else
                {
                    _view.GVLoaiHocHamHocVi.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Loại Học Hàm Học Vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int id = Convert.ToInt32(_view.GVLoaiHocHamHocVi.GetRowCellValue(row_handle, "idLoaiHocHamHocVi"));
                string loaihochamhocvi = _view.GVLoaiHocHamHocVi.GetFocusedRowCellDisplayText("tenLoaiHocHamHocVi").ToString();
                if (loaihochamhocvi != string.Empty)
                {
                    unitOfWorks.LoaiHocHamHocViRepository.Update(id, loaihochamhocvi);
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Loại Học Hàm Học Vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVLoaiHocHamHocVi.SelectRow(row_handle);
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
