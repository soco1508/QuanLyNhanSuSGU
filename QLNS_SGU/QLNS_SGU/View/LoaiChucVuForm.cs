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
    public interface ILoaiChucVuForm : IView<ILoaiChucVuPresenter>
    {
        GridControl GCLoaiChucVu { get; set; }
        GridView GVLoaiChucVu { get; set; }
        SaveFileDialog SaveFileDialog { get; set; }
        SimpleButton BTNExportExcel { get; set; }
    }
    public partial class LoaiChucVuForm : XtraForm, ILoaiChucVuForm
    {
        public LoaiChucVuForm()
        {
            InitializeComponent();
        }

        #region Controls
        public GridControl GCLoaiChucVu { get => gcLoaiChucVu; set => gcLoaiChucVu = value; }
        public GridView GVLoaiChucVu { get => gvLoaiChucVu; set => gvLoaiChucVu = value; }
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public SimpleButton BTNExportExcel { get => btnExportExcel; set => btnExportExcel = value; }
        #endregion

        public void Attach(ILoaiChucVuPresenter presenter)
        {
            btnRefresh.Click += (s, e) => presenter.RefreshGrid();
            btnAdd.Click += (s, e) => presenter.AddNewRow();
            btnSave.Click += (s, e) => presenter.SaveData();
            btnDelete.Click += (s, e) => presenter.DeleteRow();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gcLoaiChucVu.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvLoaiChucVu.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvLoaiChucVu.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvLoaiChucVu.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}