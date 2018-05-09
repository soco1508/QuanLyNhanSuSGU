using System.Windows.Forms;
using DevExpress.XtraEditors;
using QLNS_SGU.Presenter;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;

namespace QLNS_SGU.View
{
    public interface ILoaiHocHamHocViForm : IView<ILoaiHocHamHocViPresenter>
    {
        GridControl GCLoaiHocHamHocVi { get; set; }
        GridView GVLoaiHocHamHocVi { get; set; }
        SaveFileDialog SaveFileDialog { get; set; }
        SimpleButton BTNExportExcel { get; set; }
    }
    public partial class LoaiHocHamHocViForm : XtraForm, ILoaiHocHamHocViForm
    {
        public LoaiHocHamHocViForm()
        {
            InitializeComponent();
        }

        #region Controls
        public GridControl GCLoaiHocHamHocVi { get => gcLoaiHocHamHocVi; set => gcLoaiHocHamHocVi = value; }
        public GridView GVLoaiHocHamHocVi { get => gvLoaiHocHamHocVi; set => gvLoaiHocHamHocVi = value; }
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public SimpleButton BTNExportExcel { get => btnExportExcel; set => btnExportExcel = value; }
        #endregion

        public void Attach(ILoaiHocHamHocViPresenter presenter)
        {
            btnRefresh.Click += (s, e) => presenter.RefreshGrid();
            btnAdd.Click += (s, e) => presenter.AddNewRow();
            btnSave.Click += (s, e) => presenter.SaveData();
            btnDelete.Click += (s, e) => presenter.DeleteRow();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gcLoaiHocHamHocVi.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvLoaiHocHamHocVi.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvLoaiHocHamHocVi.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvLoaiHocHamHocVi.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}