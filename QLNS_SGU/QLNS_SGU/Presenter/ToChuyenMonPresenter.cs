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
    public interface IToChuyenMonPresenter : IPresenter
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
    public class ToChuyenMonPresenter : IToChuyenMonPresenter
    {
        private ToChuyenMonForm _view;
        private bool checkNewRowExist = true;
        public object UI => _view;
        public ToChuyenMonPresenter(ToChuyenMonForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVToChuyenMon.IndicatorWidth = 50;
            LoadDataToGrid();
        }

        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<ToChuyenMon> listToChuyenMon = new BindingList<ToChuyenMon>(unitOfWorks.ToChuyenMonRepository.GetListToChuyenMon());
            _view.GCToChuyenMon.DataSource = listToChuyenMon;
            RepositoryItemLookUpEdit mylookup = new RepositoryItemLookUpEdit();
            mylookup.DataSource = unitOfWorks.DonViRepository.GetListDonVi();
            mylookup.ValueMember = "idDonVi";
            mylookup.DisplayMember = "tenDonVi";
            mylookup.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            mylookup.DropDownRows = unitOfWorks.DonViRepository.GetListDonVi().Count;
            mylookup.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoFilter;
            mylookup.ShowHeader = false;
            mylookup.ShowFooter = false;
            mylookup.AutoSearchColumnIndex = 1;
            mylookup.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick;
            _view.GVToChuyenMon.Columns[2].ColumnEdit = mylookup;
            mylookup.PopulateColumns();
            mylookup.Columns[0].Visible = false;
            mylookup.Columns[3].Visible = false;
            mylookup.Columns[4].Visible = false;
            mylookup.Columns[5].Visible = false;
            mylookup.Columns[6].Visible = false;
            mylookup.Columns[7].Visible = false;
            mylookup.Columns[8].Visible = false;
            SplashScreenManager.CloseForm(false);
            
        }

        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVToChuyenMon.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVToChuyenMon.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVToChuyenMon.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                checkNewRowExist = true;
                LoadDataToGrid();
            }
        }

        public void RefreshGrid() => LoadDataToGrid();

        public void SaveData()
        {
            _view.GVToChuyenMon.CloseEditor();
            _view.GVToChuyenMon.UpdateCurrentRow();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVToChuyenMon.FocusedRowHandle;
            int idRowFocused = Convert.ToInt32(_view.GVToChuyenMon.GetFocusedRowCellDisplayText("idToChuyenMon"));
            if (idRowFocused == 0)
            {
                string tochuyenmon = _view.GVToChuyenMon.GetFocusedRowCellDisplayText("tenToChuyenMon").ToString();
                int iddonvi = Convert.ToInt32(_view.GVToChuyenMon.GetRowCellValue(row_handle, "idDonVi"));
                if (tochuyenmon != string.Empty && iddonvi > 0)
                {
                    unitOfWorks.ToChuyenMonRepository.Create(tochuyenmon, iddonvi);
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVToChuyenMon.MoveLast();
                }
                else if (tochuyenmon == string.Empty && iddonvi > 0)
                {
                    _view.GVToChuyenMon.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Tổ Chuyên Môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _view.GVToChuyenMon.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa chọn Đơn Vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int id = Convert.ToInt32(_view.GVToChuyenMon.GetRowCellValue(row_handle, "idToChuyenMon"));
                string tochuyenmon = _view.GVToChuyenMon.GetFocusedRowCellDisplayText("tenToChuyenMon").ToString();
                int iddonvi = Convert.ToInt32(_view.GVToChuyenMon.GetRowCellValue(row_handle, "idDonVi"));
                if (tochuyenmon != string.Empty)
                {
                    unitOfWorks.ToChuyenMonRepository.Update(id, tochuyenmon, iddonvi);
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Tổ Chuyên Môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVToChuyenMon.SelectRow(row_handle);
                }
            }
        }

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVToChuyenMon.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVToChuyenMon.GetFocusedRowCellDisplayText("idToChuyenMon"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.ToChuyenMonRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVToChuyenMon.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Tổ Chuyên Môn này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVToChuyenMon.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVToChuyenMon.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVToChuyenMon.Columns[1])
            {
                _view.GVToChuyenMon.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVToChuyenMon.ShowEditor();
            }
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVToChuyenMon.Columns[1].OptionsColumn.AllowEdit = false;
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], "");
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
