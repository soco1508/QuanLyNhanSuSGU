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
    public interface INgachPresenter : IPresenter
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
    public class NgachPresenter : INgachPresenter
    {
        private NgachForm _view;
        private bool checkNewRowExist = true;
        public object UI => _view;
        public NgachPresenter(NgachForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVNgach.IndicatorWidth = 50;
            LoadDataToGrid();
        }

        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<Ngach> listNgach = new BindingList<Ngach>(unitOfWorks.NgachRepository.GetListNgach());
            _view.GCNgach.DataSource = listNgach;
            SplashScreenManager.CloseForm(false);
            
        }

        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVNgach.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVNgach.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVNgach.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                LoadDataToGrid();
                checkNewRowExist = true;
            }
        }

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVNgach.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVNgach.GetFocusedRowCellDisplayText("idNgach"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.NgachRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVNgach.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Ngạch này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVNgach.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVNgach.Columns[1].OptionsColumn.AllowEdit = false;
            _view.GVNgach.Columns[2].OptionsColumn.AllowEdit = false;
            _view.GVNgach.Columns[3].OptionsColumn.AllowEdit = false;
            _view.GVNgach.Columns[4].OptionsColumn.AllowEdit = false;
            _view.GVNgach.Columns[5].OptionsColumn.AllowEdit = false;
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], string.Empty);
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[2], string.Empty);
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[3], 0);
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[4], 0);
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[5], 0);
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVNgach.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVNgach.Columns[1])
            {
                _view.GVNgach.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVNgach.ShowEditor();
            }
            if (hinfo.Column == _view.GVNgach.Columns[2])
            {
                _view.GVNgach.Columns[2].OptionsColumn.AllowEdit = true;
                _view.GVNgach.ShowEditor();
            }
            if (hinfo.Column == _view.GVNgach.Columns[3])
            {
                _view.GVNgach.Columns[3].OptionsColumn.AllowEdit = true;
                _view.GVNgach.ShowEditor();
            }
            if (hinfo.Column == _view.GVNgach.Columns[4])
            {
                _view.GVNgach.Columns[4].OptionsColumn.AllowEdit = true;
                _view.GVNgach.ShowEditor();
            }
            if (hinfo.Column == _view.GVNgach.Columns[5])
            {
                _view.GVNgach.Columns[5].OptionsColumn.AllowEdit = true;
                _view.GVNgach.ShowEditor();
            }
        }

        public void RefreshGrid() => LoadDataToGrid();

        public void SaveData()
        {
            _view.GVNgach.CloseEditor();
            _view.GVNgach.UpdateCurrentRow();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVNgach.FocusedRowHandle;
            int idRowFocused = Convert.ToInt32(_view.GVNgach.GetFocusedRowCellDisplayText("idNgach"));
            if (idRowFocused == 0)
            {
                string mangach = _view.GVNgach.GetFocusedRowCellDisplayText("maNgach").ToString();
                string ngach = _view.GVNgach.GetFocusedRowCellDisplayText("tenNgach").ToString();
                int hesovuotkhungbanamdau = Convert.ToInt32(_view.GVNgach.GetFocusedRowCellDisplayText("heSoVuotKhungBaNamDau"));
                int hesovuotkhungtrenbanam = Convert.ToInt32(_view.GVNgach.GetFocusedRowCellDisplayText("heSoVuotKhungTrenBaNam"));
                int thoihannangbac = Convert.ToInt32(_view.GVNgach.GetFocusedRowCellDisplayText("thoiHanNangBac"));
                if (mangach != string.Empty)
                {
                    unitOfWorks.NgachRepository.Create(mangach, ngach, hesovuotkhungbanamdau, hesovuotkhungtrenbanam, thoihannangbac);
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVNgach.MoveLast();
                }
                else
                {
                    _view.GVNgach.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Mã Ngạch.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int id = Convert.ToInt32(_view.GVNgach.GetRowCellValue(row_handle, "idNgach"));
                string mangach = _view.GVNgach.GetFocusedRowCellDisplayText("maNgach").ToString();
                string ngach = _view.GVNgach.GetFocusedRowCellDisplayText("tenNgach").ToString();
                int hesovuotkhungbanamdau = Convert.ToInt32(_view.GVNgach.GetFocusedRowCellDisplayText("heSoVuotKhungBaNamDau"));
                int hesovuotkhungtrenbanam = Convert.ToInt32(_view.GVNgach.GetFocusedRowCellDisplayText("heSoVuotKhungTrenBaNam"));
                int thoihannangbac = Convert.ToInt32(_view.GVNgach.GetFocusedRowCellDisplayText("thoiHanNangBac"));
                if (mangach != string.Empty)
                {
                    unitOfWorks.NgachRepository.Update(id, mangach, ngach, hesovuotkhungbanamdau, hesovuotkhungtrenbanam, thoihannangbac);
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Mã Ngạch.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVNgach.SelectRow(row_handle);
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
