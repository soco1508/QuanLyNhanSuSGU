using DevExpress.XtraEditors.Repository;
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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace QLNS_SGU.Presenter
{
    public interface INganhDaoTaoPresenter : IPresenter
    {
        void MouseDoubleClick(object sender, MouseEventArgs e);
        void HiddenEditor(object sender, EventArgs e);
        void InitNewRow(object sender, InitNewRowEventArgs e);
        void AddNewRow();
        void SaveData();
        void RefreshGrid();
        void DeleteRow();
        void ExportExcel();
        void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e);
    }
    public class NganhDaoTaoPresenter : INganhDaoTaoPresenter
    {
        private NganhDaoTaoForm _view;
        private bool checkNewRowExist = true;
        public object UI => _view;
        public NganhDaoTaoPresenter(NganhDaoTaoForm view) => _view = view;
        public void Initialize()
        {
            _view.Attach(this);
            _view.GVNganhDaoTao.IndicatorWidth = 50;
            LoadDataToGrid();
        }

        public void SaveData()
        {
            _view.GVNganhDaoTao.CloseEditor();
            _view.GVNganhDaoTao.UpdateCurrentRow();
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            int row_handle = _view.GVNganhDaoTao.FocusedRowHandle;
            int idRowFocused = Convert.ToInt32(_view.GVNganhDaoTao.GetFocusedRowCellDisplayText("idNganhDaoTao"));
            if (idRowFocused == 0)
            {
                string nganhdaotao = _view.GVNganhDaoTao.GetFocusedRowCellDisplayText("tenNganhDaoTao").ToString();
                int idloainganh = Convert.ToInt32(_view.GVNganhDaoTao.GetRowCellValue(row_handle, "idLoaiNganh"));
                if (nganhdaotao != string.Empty && idloainganh > 0)
                {
                    unitOfWorks.NganhDaoTaoRepository.Create(nganhdaotao, idloainganh);
                    LoadDataToGrid();
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.GVNganhDaoTao.MoveLast();
                }
                else if(nganhdaotao == string.Empty && idloainganh > 0)
                {
                    _view.GVNganhDaoTao.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa nhập Ngành Đào Tạo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _view.GVNganhDaoTao.DeleteRow(row_handle);
                    XtraMessageBox.Show("Bạn chưa chọn Loại Ngành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int id = Convert.ToInt32(_view.GVNganhDaoTao.GetRowCellValue(row_handle, "idNganhDaoTao"));
                string nganhdaotao = _view.GVNganhDaoTao.GetFocusedRowCellDisplayText("tenNganhDaoTao").ToString();
                int idloainganh = Convert.ToInt32(_view.GVNganhDaoTao.GetRowCellValue(row_handle, "idLoaiNganh"));
                if (nganhdaotao != string.Empty)
                {
                    unitOfWorks.NganhDaoTaoRepository.Update(id, nganhdaotao, idloainganh);
                    XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Bạn chưa nhập Ngành Đào Tạo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadDataToGrid();
                    _view.GVNganhDaoTao.SelectRow(row_handle);
                }
            }
        }
     
        private void LoadDataToGrid()
        {
            SplashScreenManager.ShowForm(_view, typeof(WaitForm1), true, true, false, 0);
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            BindingList<NganhDaoTao> listNganhDaoTao = new BindingList<NganhDaoTao>(unitOfWorks.NganhDaoTaoRepository.GetListNganhDaoTao());            
            _view.GCNganhDaoTao.DataSource = listNganhDaoTao;
            RepositoryItemLookUpEdit mylookup = new RepositoryItemLookUpEdit();
            mylookup.DataSource = unitOfWorks.LoaiNganhRepository.GetListLoaiNganh();
            mylookup.ValueMember = "idLoaiNganh";
            mylookup.DisplayMember = "tenLoaiNganh";            
            mylookup.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            mylookup.DropDownRows = unitOfWorks.LoaiNganhRepository.GetListLoaiNganh().Count;
            mylookup.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoFilter;
            mylookup.ShowHeader = false;
            mylookup.ShowFooter = false;            
            mylookup.AutoSearchColumnIndex = 1;
            mylookup.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick;
            _view.GVNganhDaoTao.Columns[2].ColumnEdit = mylookup;
            mylookup.PopulateColumns();
            mylookup.Columns[0].Visible = false;
            mylookup.Columns[2].Visible = false;
            mylookup.Columns[3].Visible = false;
            mylookup.Columns[4].Visible = false;
            SplashScreenManager.CloseForm(false);
            
        }

        public void MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GridHitInfo hinfo = _view.GVNganhDaoTao.CalcHitInfo(e.Location);
            if (hinfo.Column == _view.GVNganhDaoTao.Columns[1])
            {
                _view.GVNganhDaoTao.Columns[1].OptionsColumn.AllowEdit = true;
                _view.GVNganhDaoTao.ShowEditor();
            }
        }

        public void HiddenEditor(object sender, EventArgs e)
        {
            _view.GVNganhDaoTao.Columns[1].OptionsColumn.AllowEdit = false;
        }

        public void InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView gridView = sender as GridView;
            gridView.SetRowCellValue(e.RowHandle, gridView.Columns[1], string.Empty);
        }

        public void AddNewRow()
        {
            if (checkNewRowExist == true)
            {
                _view.GVNganhDaoTao.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                _view.GVNganhDaoTao.AddNewRow();
                checkNewRowExist = false;
            }
            else
            {
                _view.GVNganhDaoTao.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                LoadDataToGrid();
                checkNewRowExist = true;
            }
        }

        public void RefreshGrid() => LoadDataToGrid();

        public void DeleteRow()
        {
            try
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int row_handle = _view.GVNganhDaoTao.FocusedRowHandle;
                if(row_handle >= 0)
                {
                    int id = Convert.ToInt32(_view.GVNganhDaoTao.GetFocusedRowCellDisplayText("idNganhDaoTao"));
                    DialogResult dialogResult = XtraMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        unitOfWorks.NganhDaoTaoRepository.DeleteById(id);
                        unitOfWorks.Save();
                        _view.GVNganhDaoTao.DeleteRow(row_handle);
                    }
                }
                else XtraMessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            catch
            {
                XtraMessageBox.Show("Không thể xóa. Ngành Đào Tạo này đang được sử dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportExcel()
        {
            _view.SaveFileDialog.FileName = string.Empty;
            _view.SaveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (_view.SaveFileDialog.ShowDialog() == DialogResult.Cancel) return;
            _view.GVNganhDaoTao.ExportToXlsx(_view.SaveFileDialog.FileName);
        }

        public void RowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
