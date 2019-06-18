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
    public interface INgachForm : IView<INgachPresenter>
    {
        GridControl GCNgach { get; set; }
        GridView GVNgach { get; set; }
        SaveFileDialog SaveFileDialog { get; set; }
        SimpleButton BTNExportExcel { get; set; }
    }
    public partial class NgachForm : XtraForm, INgachForm
    {
        public NgachForm()
        {
            InitializeComponent();
        }

        #region Controls
        public GridControl GCNgach { get => gcNgach; set => gcNgach = value; }
        public GridView GVNgach { get => gvNgach; set => gvNgach = value; }
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public SimpleButton BTNExportExcel { get => btnExportExcel; set => btnExportExcel = value; }
        #endregion

        public void Attach(INgachPresenter presenter)
        {
            btnRefresh.Click += (s, e) => presenter.RefreshGrid();
            btnAdd.Click += (s, e) => presenter.AddNewRow();
            btnSave.Click += (s, e) => presenter.SaveData();
            btnDelete.Click += (s, e) => presenter.DeleteRow();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gcNgach.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvNgach.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvNgach.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvNgach.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}