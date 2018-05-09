using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
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
    public interface IChucVuPresenter : IPresenter
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
    public class ChucVuPresenter : IChucVuPresenter
    {
        private ChucVuForm _view;
        private bool checkNewRowExist = true;
        public object UI => _view;
        public ChucVuPresenter(ChucVuForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVChucVu.IndicatorWidth = 50;
            LoadDataToGrid();
        }
        private void CloseEditor()
        {
            _view.GVChucVu.CloseEditor();
            _view.GVChucVu.UpdateCurrentRow();
        }
        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<ChucVu> listChucVu = new BindingList<ChucVu>(unitOfWorks.ChucVuRepository.GetListChucVu());
            _view.GCChucVu.DataSource = listChucVu;
            RepositoryItemLookUpEdit mylookup = new RepositoryItemLookUpEdit();
            mylookup.DataSource = unitOfWorks.LoaiChucVuRepository.GetListLoaiChucVu();
            mylookup.ValueMember = "idLoaiChucVu";
            mylookup.DisplayMember = "tenLoaiChucVu";
            mylookup.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            mylookup.DropDownRows = unitOfWorks.LoaiChucVuRepository.GetListLoaiChucVu().Count;
            mylookup.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoFilter;
            mylookup.ShowHeader = false;
            mylookup.ShowFooter = false;
            mylookup.AutoSearchColumnIndex = 1;
            mylookup.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick;
            _view.GVChucVu.Columns[3].ColumnEdit = mylookup;
            mylookup.PopulateColumns();
            mylookup.Columns[0].Visible = false;
            mylookup.Columns[2].Visible = false;
            SplashScreenManager.CloseForm(false);            
        }

        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVChucVu.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVChucVu.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVChucVu.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                LoadDataToGrid();
                checkNewRowExist = true;
            }
        }

        public void RefreshGrid() => LoadDataToGrid();

        public void SaveData()
        {
            CloseEditor();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVChucVu.FocusedRowHandle;
            int idRowFocused = Convert.ToInt32(_view.GVChucVu.GetFocusedRowCellDisplayText("idChucVu"));
            if (idRowFocused == 0)
            {
                string chucvu = _view.GVChucVu.GetFocusedRowCellDisplayText("tenChucVu").ToString();
                //double hesochucvu = Convert.ToDouble(_view.GVChucVu.GetFocusedRowCellDisplayText("heSoChucVu"));
                int idloaichucvu = Convert.ToInt32(_view.GVChucVu.GetRowCellValue(row_handle, "idLoaiChucVu"));
                if (chucvu != string.Empty && idloaichucvu > 0)
                {
                    unitOfWorks.ChucVuRepository.Create(chucvu, /*hesochucvu,*/ idloaichucvu);
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVChucVu.MoveLast();
                }
                else if (chucvu == string.Empty && idloaichucvu > 0)
                {
                    _view.GVChucVu.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Chức Vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _view.GVChucVu.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa chọn Loại Chức Vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int id = Convert.ToInt32(_view.GVChucVu.GetRowCellValue(row_handle, "idChucVu"));
                string chucvu = _view.GVChucVu.GetFocusedRowCellDisplayText("tenChucVu").ToString();
                //double hesochucvu = Convert.ToDouble(_view.GVChucVu.GetFocusedRowCellDisplayText("heSoChucVu"));
                int idloaichucvu = Convert.ToInt32(_view.GVChucVu.GetRowCellValue(row_handle, "idLoaiChucVu"));
                if (chucvu != string.Empty)
                {
                    unitOfWorks.ChucVuRepository.Update(id, chucvu, /*hesoChucVu,*/ idloaichucvu);
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Chức Vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVChucVu.SelectRow(row_handle);
                }
            }
        }

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVChucVu.FocusedRowHandle;
                if (row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVChucVu.GetFocusedRowCellDisplayText("idChucVu"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.ChucVuRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVChucVu.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Chức Vụ này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVChucVu.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVChucVu.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVChucVu.Columns[1])
            {
                _view.GVChucVu.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVChucVu.ShowEditor();
            }
            else if (hinfo.Column == _view.GVChucVu.Columns[2])
            {
                _view.GVChucVu.Columns[2].OptionsColumn.AllowEdit = true;
                _view.GVChucVu.ShowEditor();
            }
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVChucVu.Columns[1].OptionsColumn.AllowEdit = false;
            _view.GVChucVu.Columns[2].OptionsColumn.AllowEdit = false;
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], string.Empty);
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
