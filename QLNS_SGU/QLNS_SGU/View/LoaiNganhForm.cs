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
    public interface ILoaiNganhForm : IView<ILoaiNganhPresenter>
    {
        GridControl GCLoaiNganh { get; set; }
        GridView GVLoaiNganh { get; set; }
        SaveFileDialog SaveFileDialog { get; set; }
        SimpleButton BTNExportExcel { get; set; }
    }
    public partial class LoaiNganhForm : XtraForm, ILoaiNganhForm
    {
        public LoaiNganhForm()
        {
            InitializeComponent();
        }
#region Controls
        public GridControl GCLoaiNganh { get => gcLoaiNganh; set => gcLoaiNganh = value; }
        public GridView GVLoaiNganh { get => gvLoaiNganh; set => gvLoaiNganh = value; }
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public SimpleButton BTNExportExcel { get => btnExportExcel; set => btnExportExcel = value; }
        #endregion
        public void Attach(ILoaiNganhPresenter presenter)
        {
            btnRefresh.Click += (s, e) => presenter.RefreshGrid();
            btnAdd.Click += (s, e) => presenter.AddNewRow();
            btnSave.Click += (s, e) => presenter.SaveData();
            btnDelete.Click += (s, e) => presenter.DeleteRow();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gcLoaiNganh.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvLoaiNganh.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvLoaiNganh.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvLoaiNganh.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}