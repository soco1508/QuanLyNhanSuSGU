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
    public interface ILoaiHopDongForm : IView<ILoaiHopDongPresenter>
    {
        GridControl GCLoaiHopDong { get; set; }
        GridView GVLoaiHopDong { get; set; }
        SaveFileDialog SaveFileDialog { get; set; }
        SimpleButton BTNExportExcel { get; set; }
    }
    public partial class LoaiHopDongForm : XtraForm, ILoaiHopDongForm
    {
        public LoaiHopDongForm()
        {
            InitializeComponent();
        }

        #region Controls
        public GridControl GCLoaiHopDong { get => gcLoaiHopDong; set => gcLoaiHopDong = value; }
        public GridView GVLoaiHopDong { get => gvLoaiHopDong; set => gvLoaiHopDong = value; }
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public SimpleButton BTNExportExcel { get => btnExportExcel; set => btnExportExcel = value; }
        #endregion

        public void Attach(ILoaiHopDongPresenter presenter)
        {
            btnRefresh.Click += (s, e) => presenter.RefreshGrid();
            btnAdd.Click += (s, e) => presenter.AddNewRow();
            btnSave.Click += (s, e) => presenter.SaveData();
            btnDelete.Click += (s, e) => presenter.DeleteRow();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gcLoaiHopDong.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvLoaiHopDong.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvLoaiHopDong.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvLoaiHopDong.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}