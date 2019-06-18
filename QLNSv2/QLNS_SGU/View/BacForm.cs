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
    public interface IBacForm : IView<IBacPresenter>
    {
        SaveFileDialog SaveFileDialog { get; set; }
        GridControl GCBac { get; set; }
        GridView GVBac { get; set; }
        SimpleButton BTNExportExcel { get; set; }
    }
    public partial class BacForm : XtraForm, IBacForm
    {
        public BacForm()
        {
            InitializeComponent();
        }
        #region Controls
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public GridControl GCBac { get => gcBac; set => gcBac = value; }
        public GridView GVBac { get => gvBac; set => gvBac = value; }
        public SimpleButton BTNExportExcel { get => btnExportExcel; set => btnExportExcel = value; }
        #endregion

        public void Attach(IBacPresenter presenter)
        {
            btnRefresh.Click += (s, e) => presenter.RefreshGrid();
            btnAdd.Click += (s, e) => presenter.AddNewRow();
            btnSave.Click += (s, e) => presenter.SaveData();
            btnDelete.Click += (s, e) => presenter.DeleteRow();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gcBac.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvBac.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvBac.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvBac.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}