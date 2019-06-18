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
    public interface IChucVuForm : IView<IChucVuPresenter>
    {
        SaveFileDialog SaveFileDialog { get; set; }
        GridControl GCChucVu { get; set; }
        GridView GVChucVu { get; set; }
        SimpleButton BTNExportExcel { get; set; }
    }
    public partial class ChucVuForm : XtraForm, IChucVuForm
    {
        public ChucVuForm()
        {
            InitializeComponent();
        }

        #region Controls
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public GridControl GCChucVu { get => gcChucVu; set => gcChucVu = value; }
        public GridView GVChucVu { get => gvChucVu; set => gvChucVu = value; }
        public SimpleButton BTNExportExcel { get => btnExportExcel; set => btnExportExcel = value; }
        #endregion

        public void Attach(IChucVuPresenter presenter)
        {
            btnRefresh.Click += (s, e) => presenter.RefreshGrid();
            btnAdd.Click += (s, e) => presenter.AddNewRow();
            btnSave.Click += (s, e) => presenter.SaveData();
            btnDelete.Click += (s, e) => presenter.DeleteRow();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gcChucVu.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvChucVu.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvChucVu.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvChucVu.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}