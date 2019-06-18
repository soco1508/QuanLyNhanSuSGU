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
    public interface IExportDataOneDomainForm : IView<IExportDataOneDomainPresenter>
    {
        SaveFileDialog SaveFileDialog { get; set; }
        GridControl GCCustom { get; set; }
        GridView GVCustom { get; set; }
        RadioGroup RADDomain { get; set; }
        RadioGroup RADSelectTimeToFilter { get; set; }
        DateEdit DTTimeline { get; set; }
        DateEdit DTFromDuration { get; set; }
        DateEdit DTToDuration { get; set; }
    }
    public partial class ExportDataOneDomainForm : XtraForm, IExportDataOneDomainForm
    {
        public ExportDataOneDomainForm()
        {
            InitializeComponent();
        }
        #region Controls
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public GridControl GCCustom { get => gcCustom; set => gcCustom = value; }
        public GridView GVCustom { get => gvCustom; set => gvCustom = value; }
        public RadioGroup RADDomain { get => radDomain; set => radDomain = value; }
        public RadioGroup RADSelectTimeToFilter { get => radSelectTimeToFilter; set => radSelectTimeToFilter = value; }
        public DateEdit DTTimeline { get => dtTimeline; set => dtTimeline = value; }
        public DateEdit DTFromDuration { get => dtFromDuration; set => dtFromDuration = value; }
        public DateEdit DTToDuration { get => dtToDuration; set => dtToDuration = value; }
        #endregion
        public void Attach(IExportDataOneDomainPresenter presenter)
        {
            radSelectTimeToFilter.SelectedIndexChanged += new EventHandler(presenter.EnableFilterDatetime);
            gvCustom.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
            btnExportExcel.ItemClick += (s, e) => presenter.ExportExcel();
            btnCancel.Click += (s, e) => presenter.Cancel();
            btnExportData.Click += (s, e) => presenter.ExportData();
        }
    }
}