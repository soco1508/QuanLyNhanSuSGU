using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLNS_SGU.Presenter;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace QLNS_SGU.View
{
    public interface IChuyenNganhForm : IView<IChuyenNganhPresenter>
    {
        SaveFileDialog SaveFileDialog { get; set; }
        GridControl GCChuyenNganh { get; set; }
        GridView GVChuyenNganh { get; set; }
        SimpleButton BTNExportExcel { get; set; }
    }
    public partial class ChuyenNganhForm : XtraForm, IChuyenNganhForm
    {
        public ChuyenNganhForm()
        {
            InitializeComponent();
        }
        
        #region Controls
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public GridControl GCChuyenNganh { get => gcChuyenNganh; set => gcChuyenNganh = value; }
        public GridView GVChuyenNganh { get => gvChuyenNganh; set => gvChuyenNganh = value; }
        public SimpleButton BTNExportExcel { get => btnExportExcel; set => btnExportExcel = value; }
        #endregion

        public void Attach(IChuyenNganhPresenter presenter)
        {
            btnRefresh.Click += (s, e) => presenter.RefreshGrid();
            btnAdd.Click += (s, e) => presenter.AddNewRow();
            btnSave.Click += (s, e) => presenter.SaveData();
            btnDelete.Click += (s, e) => presenter.DeleteRow();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gcChuyenNganh.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvChuyenNganh.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvChuyenNganh.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvChuyenNganh.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}