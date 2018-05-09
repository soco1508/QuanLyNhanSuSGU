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
    public interface ILoaiHopDongPresenter : IPresenter
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
    public class LoaiHopDongPresenter : ILoaiHopDongPresenter
    {
        private LoaiHopDongForm _view;
        private bool checkNewRowExist = true;

        public object UI => _view;
        public LoaiHopDongPresenter(LoaiHopDongForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVLoaiHopDong.IndicatorWidth = 50;
            LoadDataToGrid();
        }

        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<LoaiHopDong> listLoaiHopDong = new BindingList<LoaiHopDong>(unitOfWorks.LoaiHopDongRepository.GetListLoaiHopDong());
            _view.GCLoaiHopDong.DataSource = listLoaiHopDong;
            SplashScreenManager.CloseForm(false);           
        }

        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVLoaiHopDong.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVLoaiHopDong.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVLoaiHopDong.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                LoadDataToGrid();
                checkNewRowExist = true;
            }
        }

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVLoaiHopDong.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVLoaiHopDong.GetFocusedRowCellDisplayText("idLoaiHopDong"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.LoaiHopDongRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVLoaiHopDong.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Loại Hợp Đồng này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVLoaiHopDong.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVLoaiHopDong.Columns[1].OptionsColumn.AllowEdit = false;
            _view.GVLoaiHopDong.Columns[2].OptionsColumn.AllowEdit = false;
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], string.Empty);
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[2], string.Empty);
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVLoaiHopDong.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVLoaiHopDong.Columns[1])
            {
                _view.GVLoaiHopDong.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVLoaiHopDong.ShowEditor();
            }
            else if (hinfo.Column == _view.GVLoaiHopDong.Columns[2])
            {
                _view.GVLoaiHopDong.Columns[2].OptionsColumn.AllowEdit = true;
                _view.GVLoaiHopDong.ShowEditor();
            }
        }

        public void RefreshGrid() => LoadDataToGrid();

        public void SaveData()
        {
            _view.GVLoaiHopDong.CloseEditor();
            _view.GVLoaiHopDong.UpdateCurrentRow();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVLoaiHopDong.FocusedRowHandle;
            int idRowFocused = Convert.ToInt32(_view.GVLoaiHopDong.GetFocusedRowCellDisplayText("idLoaiHopDong"));
            if (idRowFocused == 0)
            {
                string maloaihopdong = _view.GVLoaiHopDong.GetFocusedRowCellDisplayText("maLoaiHopDong").ToString();
                string loaihopdong = _view.GVLoaiHopDong.GetFocusedRowCellDisplayText("tenLoaiHopDong").ToString();
                if (loaihopdong != string.Empty)
                {
                    unitOfWorks.LoaiHopDongRepository.Create(maloaihopdong, loaihopdong);
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVLoaiHopDong.MoveLast();
                }
                else
                {
                    _view.GVLoaiHopDong.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Loại Hợp Đồng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int id = Convert.ToInt32(_view.GVLoaiHopDong.GetRowCellValue(row_handle, "idLoaiHopDong"));
                string maloaihopdong = _view.GVLoaiHopDong.GetFocusedRowCellDisplayText("maLoaiHopDong").ToString();
                string loaihopdong = _view.GVLoaiHopDong.GetFocusedRowCellDisplayText("tenLoaiHopDong").ToString();
                if (loaihopdong != string.Empty)
                {
                    unitOfWorks.LoaiHopDongRepository.Update(id, maloaihopdong, loaihopdong);
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Loại Hợp Đồng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVLoaiHopDong.SelectRow(row_handle);
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
