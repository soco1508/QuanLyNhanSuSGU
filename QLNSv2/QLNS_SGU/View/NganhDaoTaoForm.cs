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
    public interface INganhDaoTaoForm : IView<INganhDaoTaoPresenter>
    {
        GridControl GCNganhDaoTao { get; set; }
        GridView GVNganhDaoTao { get; set; }
        SaveFileDialog SaveFileDialog { get; set; }
        SimpleButton BTNExportExcel { get; set; }
    }
    public partial class NganhDaoTaoForm : XtraForm, INganhDaoTaoForm
    {
        public NganhDaoTaoForm()
        {
            InitializeComponent();
        }

        public GridControl GCNganhDaoTao { get => gcNganhDaoTao; set => gcNganhDaoTao = value; }
        public GridView GVNganhDaoTao { get => gvNganhDaoTao; set => gvNganhDaoTao = value; }
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public SimpleButton BTNExportExcel { get => btnExportExcel; set => btnExportExcel = value; }

        public void Attach(INganhDaoTaoPresenter presenter)
        {
            btnRefresh.Click += (s, e) => presenter.RefreshGrid();
            btnAdd.Click += (s, e) => presenter.AddNewRow();
            btnSave.Click += (s, e) => presenter.SaveData();
            btnDelete.Click += (s, e) => presenter.DeleteRow();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            gcNganhDaoTao.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvNganhDaoTao.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvNganhDaoTao.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvNganhDaoTao.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}