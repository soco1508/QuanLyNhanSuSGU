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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS_SGU.Presenter
{
    public interface ILoaiChungChiPresenter : IPresenter
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
    public class LoaiChungChiPresenter : ILoaiChungChiPresenter
    {
        private LoaiChungChiForm _view;
        private bool checkNewRowExist = true;

        public object UI => _view;
        public LoaiChungChiPresenter(LoaiChungChiForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVLoaiChungChi.IndicatorWidth = 50;
            LoadDataToGrid();
        }
        private void CloseEditor()
        {
            _view.GVLoaiChungChi.CloseEditor();
            _view.GVLoaiChungChi.UpdateCurrentRow();
        }
        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<LoaiChungChi> listLoaiChungChi = new BindingList<LoaiChungChi>(unitOfWorks.LoaiChungChiRepository.GetListLoaiChungChiForCRUD());
            _view.GCLoaiChungChi.DataSource = listLoaiChungChi;
            SplashScreenManager.CloseForm(false);            
        }

        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVLoaiChungChi.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVLoaiChungChi.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVLoaiChungChi.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                LoadDataToGrid();
                checkNewRowExist = true;
            }
        }

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVLoaiChungChi.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    string idchungchi = _view.GVLoaiChungChi.GetFocusedRowCellDisplayText("idLoaiChungChi");
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.LoaiChungChiRepository.DeleteById(idchungchi);
                        unitOfWorks.Save();
                        _view.GVLoaiChungChi.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Loại Chứng Chỉ này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVLoaiChungChi.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVLoaiChungChi.Columns[0].OptionsColumn.AllowEdit = false;
            _view.GVLoaiChungChi.Columns[1].OptionsColumn.AllowEdit = false;
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[0], string.Empty);
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], string.Empty);
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVLoaiChungChi.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVLoaiChungChi.Columns[0])
            {
                int row_handle = _view.GVLoaiChungChi.FocusedRowHandle;
                if(row_handle < 0)
                {
                    _view.GVLoaiChungChi.Columns[0].OptionsColumn.AllowEdit = true;
                    _view.GVLoaiChungChi.ShowEditor();
                }
            }
            if (hinfo.Column == _view.GVLoaiChungChi.Columns[1])
            {
                _view.GVLoaiChungChi.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVLoaiChungChi.ShowEditor();
            }
        }

        public void RefreshGrid() => LoadDataToGrid();

        public void SaveData()
        {
            CloseEditor();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVLoaiChungChi.FocusedRowHandle;
            List<string> listIdLoaiChungChi = unitOfWorks.LoaiChungChiRepository.GetListIdLoaiChungChi();
            string idloaichungchi = _view.GVLoaiChungChi.GetFocusedRowCellDisplayText("idLoaiChungChi");
            string tenloaichungchi = _view.GVLoaiChungChi.GetFocusedRowCellDisplayText("tenLoaiChungChi").ToString();
            if (!listIdLoaiChungChi.Any(x => x == idloaichungchi))
            {               
                if (tenloaichungchi != string.Empty)
                {
                    unitOfWorks.LoaiChungChiRepository.Create(idloaichungchi, tenloaichungchi);
                    unitOfWorks.Save();
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVLoaiChungChi.MoveLast();
                }
                else
                {
                    _view.GVLoaiChungChi.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Loại Chứng Chỉ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (tenloaichungchi != string.Empty)
                {
                    unitOfWorks.LoaiChungChiRepository.Update(idloaichungchi, tenloaichungchi);
                    unitOfWorks.Save();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Loại Chứng Chỉ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVLoaiChungChi.SelectRow(row_handle);
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
