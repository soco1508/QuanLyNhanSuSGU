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
    public interface ILoaiDonViForm : IView<ILoaiDonViPresenter>
    {
        GridControl GCLoaiDonVi { get; set; }
        GridView GVLoaiDonVi { get; set; }
        SaveFileDialog SaveFileDialog { get; set; }
        SimpleButton BTNExportExcel { get; set; }
    }
    public partial class LoaiDonViForm : XtraForm, ILoaiDonViForm
    {
        public LoaiDonViForm()
        {
            InitializeComponent();
        }

        #region Controls
        public GridControl GCLoaiDonVi { get => gcLoaiDonVi; set => gcLoaiDonVi = value; }
        public GridView GVLoaiDonVi { get => gvLoaiDonVi; set => gvLoaiDonVi = value; }
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public SimpleButton BTNExportExcel { get => btnExportExcel; set => btnExportExcel = value; }
        #endregion

        public void Attach(ILoaiDonViPresenter presenter)
        {
            btnRefresh.Click += (s, e) => presenter.RefreshGrid();
            btnAdd.Click += (s, e) => presenter.AddNewRow();
            btnSave.Click += (s, e) => presenter.SaveData();
            btnDelete.Click += (s, e) => presenter.DeleteRow();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gcLoaiDonVi.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvLoaiDonVi.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvLoaiDonVi.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvLoaiDonVi.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}