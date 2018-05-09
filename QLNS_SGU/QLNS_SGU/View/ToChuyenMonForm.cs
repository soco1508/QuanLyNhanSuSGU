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
    public interface IToChuyenMonForm : IView<IToChuyenMonPresenter>
    {
        SaveFileDialog SaveFileDialog { get; set; }
        GridControl GCToChuyenMon { get; set; }
        GridView GVToChuyenMon { get; set; }
        SimpleButton BTNExportExcel { get; set; }
    }
    public partial class ToChuyenMonForm : XtraForm, IToChuyenMonForm
    {
        public ToChuyenMonForm()
        {
            InitializeComponent();
        }

        #region Controls
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public GridControl GCToChuyenMon { get => gcToChuyenMon; set => gcToChuyenMon = value; }
        public GridView GVToChuyenMon { get => gvToChuyenMon; set => gvToChuyenMon = value; }
        public SimpleButton BTNExportExcel { get => btnExportExcel; set => btnExportExcel = value; }
        #endregion
        public void Attach(IToChuyenMonPresenter presenter)
        {
            btnRefresh.Click += (s, e) => presenter.RefreshGrid();
            btnAdd.Click += (s, e) => presenter.AddNewRow();
            btnSave.Click += (s, e) => presenter.SaveData();
            btnDelete.Click += (s, e) => presenter.DeleteRow();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gcToChuyenMon.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvToChuyenMon.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvToChuyenMon.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvToChuyenMon.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}