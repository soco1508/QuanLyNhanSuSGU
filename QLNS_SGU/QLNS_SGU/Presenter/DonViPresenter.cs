using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
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
    public interface IDonViPresenter : IPresenter
    {
        void AddNewRow();
        void RefreshGrid();
        void SaveData();
        void DeleteRow();
        void ExportExcel();
        void MouseDoubleClick(object sender, MouseEventArgs e);
        void HiddenEditor(object sender, EventArgs e);
        void InitNewRow(object sender, InitNewRowEventArgs e);
        void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e);
    }
    public class DonViPresenter : IDonViPresenter
    {
        private DonViForm _view;
        private bool checkNewRowExist = true;
        public object UI => _view;
        public DonViPresenter(DonViForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVDonVi.IndicatorWidth = 50;
            LoadDataToGrid();
        }
        private void CloseEditor()
        {
            _view.GVDonVi.CloseEditor();
            _view.GVDonVi.UpdateCurrentRow();
        }
        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<DonVi> listDonVi = new BindingList<DonVi>(unitOfWorks.DonViRepository.GetListDonVi());
            _view.GCDonVi.DataSource = listDonVi;
            RepositoryItemLookUpEdit mylookup = new RepositoryItemLookUpEdit();
            mylookup.DataSource = unitOfWorks.LoaiDonViRepository.GetListLoaiDonVi();
            mylookup.ValueMember = "idLoaiDonVi";
            mylookup.DisplayMember = "tenLoaiDonVi";
            mylookup.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            mylookup.DropDownRows = unitOfWorks.LoaiDonViRepository.GetListLoaiDonVi().Count;
            mylookup.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoFilter;
            mylookup.ShowHeader = false;
            mylookup.ShowFooter = false;
            mylookup.AutoSearchColumnIndex = 1;
            mylookup.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick;
            _view.GVDonVi.Columns[5].ColumnEdit = mylookup;
            mylookup.PopulateColumns();
            mylookup.Columns[0].Visible = false;
            mylookup.Columns[2].Visible = false;
            SplashScreenManager.CloseForm(false);
            
        }

        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVDonVi.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVDonVi.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVDonVi.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                checkNewRowExist = true;
                LoadDataToGrid();
            }
        }

        public void RefreshGrid() => LoadDataToGrid();

        public void SaveData()
        {
            CloseEditor();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVDonVi.FocusedRowHandle;
            int idRowFocused = Convert.ToInt32(_view.GVDonVi.GetFocusedRowCellDisplayText("idDonVi"));
            if (idRowFocused == 0)
            {
                string donvi = _view.GVDonVi.GetFocusedRowCellDisplayText("tenDonVi").ToString();
                string diadiem = _view.GVDonVi.GetFocusedRowCellDisplayText("diaDiem").ToString();
                string diachi = _view.GVDonVi.GetFocusedRowCellDisplayText("diaChi").ToString();
                string sdt = _view.GVDonVi.GetFocusedRowCellDisplayText("sDT").ToString();
                int idloaidonvi = Convert.ToInt32(_view.GVDonVi.GetRowCellValue(row_handle, "idLoaiDonVi"));
                if (donvi != string.Empty && idloaidonvi > 0)
                {
                    unitOfWorks.DonViRepository.Create(donvi, diadiem, diachi, sdt, idloaidonvi);
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVDonVi.MoveLast();
                }
                else if (donvi == string.Empty && idloaidonvi > 0)
                {
                    _view.GVDonVi.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Đơn Vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _view.GVDonVi.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa chọn Loại Đơn Vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int id = Convert.ToInt32(_view.GVDonVi.GetRowCellValue(row_handle, "idDonVi"));
                string donvi = _view.GVDonVi.GetFocusedRowCellDisplayText("tenDonVi").ToString();
                string diadiem = _view.GVDonVi.GetFocusedRowCellDisplayText("diaDiem").ToString();
                string diachi = _view.GVDonVi.GetFocusedRowCellDisplayText("diaChi").ToString();
                string sdt = _view.GVDonVi.GetFocusedRowCellDisplayText("sDT").ToString();
                int idloaidonvi = Convert.ToInt32(_view.GVDonVi.GetRowCellValue(row_handle, "idLoaiDonVi"));
                if (donvi != string.Empty)
                {
                    unitOfWorks.DonViRepository.Update(id, donvi, diadiem, diachi, sdt, idloaidonvi);
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Đơn Vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVDonVi.SelectRow(row_handle);
                }
            }
        }

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVDonVi.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVDonVi.GetFocusedRowCellDisplayText("idDonVi"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.DonViRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVDonVi.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Đơn Vị này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVDonVi.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVDonVi.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVDonVi.Columns[1])
            {
                _view.GVDonVi.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVDonVi.ShowEditor();
            }
            else if(hinfo.Column == _view.GVDonVi.Columns[2])
            {
                _view.GVDonVi.Columns[2].OptionsColumn.AllowEdit = true;
                _view.GVDonVi.ShowEditor();
            }
            else if (hinfo.Column == _view.GVDonVi.Columns[3])
            {
                _view.GVDonVi.Columns[3].OptionsColumn.AllowEdit = true;
                _view.GVDonVi.ShowEditor();
            }
            else if (hinfo.Column == _view.GVDonVi.Columns[4])
            {
                _view.GVDonVi.Columns[4].OptionsColumn.AllowEdit = true;
                _view.GVDonVi.ShowEditor();
            }
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVDonVi.Columns[1].OptionsColumn.AllowEdit = false;
            _view.GVDonVi.Columns[2].OptionsColumn.AllowEdit = false;
            _view.GVDonVi.Columns[3].OptionsColumn.AllowEdit = false;
            _view.GVDonVi.Columns[4].OptionsColumn.AllowEdit = false;
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], "");
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[2], "");
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[3], "");
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[4], "");
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
