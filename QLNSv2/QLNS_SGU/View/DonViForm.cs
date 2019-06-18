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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using QLNS_SGU.Presenter;

namespace QLNS_SGU.View
{
    public interface IDonViForm : IView<IDonViPresenter>
    {
        GridControl GCDonVi { get; set; }
        GridView GVDonVi { get; set; }
        SaveFileDialog SaveFileDialog { get; set; }
        SimpleButton BTNExportExcel { get; set; }
    }
    public partial class DonViForm : XtraForm, IDonViForm
    {
        public DonViForm()
        {
            InitializeComponent();
        }

        #region Controls
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public GridControl GCDonVi { get => gcDonVi; set => gcDonVi = value; }
        public GridView GVDonVi { get => gvDonVi; set => gvDonVi = value; }
        public SimpleButton BTNExportExcel { get => btnExportExcel; set => btnExportExcel = value; }
        #endregion

        public void Attach(IDonViPresenter presenter)
        {
            btnRefresh.Click += (s, e) => presenter.RefreshGrid();
            btnAdd.Click += (s, e) => presenter.AddNewRow();
            btnSave.Click += (s, e) => presenter.SaveData();
            btnDelete.Click += (s, e) => presenter.DeleteRow();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gcDonVi.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvDonVi.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvDonVi.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvDonVi.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}