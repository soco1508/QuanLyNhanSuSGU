using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using QLNS_SGU.View;
using DevExpress.XtraEditors.Repository;
using System.ComponentModel;
using Model;
using Model.Entities;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace QLNS_SGU.Presenter
{
    public interface IChuyenNganhPresenter : IPresenter
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

    public class ChuyenNganhPresenter : IChuyenNganhPresenter
    {
        private ChuyenNganhForm _view;
        private bool checkNewRowExist = true;
        public object UI => _view;
        public ChuyenNganhPresenter(ChuyenNganhForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVChuyenNganh.IndicatorWidth = 50;
            LoadDataToGrid();
        }
        private void CloseEditor()
        {
            _view.GVChuyenNganh.CloseEditor();
            _view.GVChuyenNganh.UpdateCurrentRow();
        }
        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<ChuyenNganh> listChuyenNganh = new BindingList<ChuyenNganh>(unitOfWorks.ChuyenNganhRepository.GetListChuyenNganh());
            _view.GCChuyenNganh.DataSource = listChuyenNganh;
            RepositoryItemLookUpEdit mylookup = new RepositoryItemLookUpEdit();
            mylookup.DataSource = unitOfWorks.NganhDaoTaoRepository.GetListNganhDaoTao();
            mylookup.ValueMember = "idNganhDaoTao";
            mylookup.DisplayMember = "tenNganhDaoTao";
            mylookup.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            mylookup.DropDownRows = unitOfWorks.NganhDaoTaoRepository.GetListNganhDaoTao().Count;
            mylookup.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoFilter;
            mylookup.ShowHeader = false;
            mylookup.ShowFooter = false;
            mylookup.AutoSearchColumnIndex = 1;
            mylookup.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick;
            _view.GVChuyenNganh.Columns[2].ColumnEdit = mylookup;
            mylookup.PopulateColumns();
            mylookup.Columns[0].Visible = false;
            mylookup.Columns[2].Visible = false;
            mylookup.Columns[3].Visible = false;
            mylookup.Columns[4].Visible = false;
            mylookup.Columns[5].Visible = false;
            mylookup.Columns[6].Visible = false;
            SplashScreenManager.CloseForm(false);
            
        }

        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVChuyenNganh.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVChuyenNganh.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVChuyenNganh.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                LoadDataToGrid();
                checkNewRowExist = true;
            }
        }

        public void RefreshGrid() => LoadDataToGrid();

        public void SaveData()
        {
            CloseEditor();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVChuyenNganh.FocusedRowHandle;
            int idRowFocused = Convert.ToInt32(_view.GVChuyenNganh.GetFocusedRowCellDisplayText("idChuyenNganh"));
            if (idRowFocused == 0)
            {
                string chuyennganh = _view.GVChuyenNganh.GetFocusedRowCellDisplayText("tenChuyenNganh").ToString();
                int idnganhdaotao = Convert.ToInt32(_view.GVChuyenNganh.GetRowCellValue(row_handle, "idNganhDaoTao"));
                if (chuyennganh != string.Empty && idnganhdaotao > 0)
                {
                    unitOfWorks.ChuyenNganhRepository.Create(chuyennganh, idnganhdaotao);
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVChuyenNganh.MoveLast();
                }
                else if (chuyennganh == string.Empty && idnganhdaotao > 0)
                {
                    _view.GVChuyenNganh.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Chuyên Ngành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _view.GVChuyenNganh.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa chọn Ngành Đào Tạo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int id = Convert.ToInt32(_view.GVChuyenNganh.GetRowCellValue(row_handle, "idChuyenNganh"));
                string chuyennganh = _view.GVChuyenNganh.GetFocusedRowCellDisplayText("tenChuyenNganh").ToString();
                int idnganhdaotao = Convert.ToInt32(_view.GVChuyenNganh.GetRowCellValue(row_handle, "idNganhDaoTao"));
                if (chuyennganh != string.Empty)
                {
                    unitOfWorks.ChuyenNganhRepository.Update(id, chuyennganh, idnganhdaotao);
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Chuyên Ngành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVChuyenNganh.SelectRow(row_handle);
                }
            }
        }

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVChuyenNganh.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVChuyenNganh.GetFocusedRowCellDisplayText("idChuyenNganh"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.ChuyenNganhRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVChuyenNganh.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Chuyên Ngành này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVChuyenNganh.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVChuyenNganh.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVChuyenNganh.Columns[1])
            {
                _view.GVChuyenNganh.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVChuyenNganh.ShowEditor();
            }
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVChuyenNganh.Columns[1].OptionsColumn.AllowEdit = false;
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
