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
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using QLNS_SGU.Presenter;
using DevExpress.XtraLayout;

namespace QLNS_SGU.View
{
    public interface IExportDataForm : IView<IExportDataPresenter>
    {
        SaveFileDialog SaveFileDialog { get; set; }        
        GridControl GCCustom { get; set; }
        GridView GVCustom { get; set; }
        RadioGroup RADSelectTimeToFilter { get; set; }
        DateEdit DTTimeline { get; set; }
        DateEdit DTFromDuration { get; set; }
        DateEdit DTToDuration { get; set; }
        CheckEdit CHKThongTinCaNhan { get; set; }
        CheckEdit CHKCongTac { get; set; }
        CheckEdit CHKQuaTrinhLuong { get; set; }
        CheckEdit CHKHopDong { get; set; }
        CheckEdit CHKTrangThai { get; set; }
        CheckEdit CHKNganhHoc { get; set; }
        CheckEdit CHKNganhDay { get; set; }
        CheckEdit CHKChungChi { get; set; }
        CheckEdit CHKDangHocNangCao { get; set; }
        CheckEdit CHKSaveFilter { get; set; }
    }
    public partial class ExportDataMultiDomainForm : XtraForm, IExportDataForm
    {
        public ExportDataMultiDomainForm()
        {
            InitializeComponent();
        }
        #region Controls
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public GridControl GCCustom { get => gcCustom; set => gcCustom = value; }
        public GridView GVCustom { get => gvCustom; set => gvCustom = value; }
        public CheckEdit CHKThongTinCaNhan { get => chkThongTinCaNhan; set => chkThongTinCaNhan = value; }
        public CheckEdit CHKCongTac { get => chkCongTac; set => chkCongTac = value; }
        public CheckEdit CHKQuaTrinhLuong { get => chkQuaTrinhLuong; set => chkQuaTrinhLuong = value; }
        public CheckEdit CHKHopDong { get => chkHopDong; set => chkHopDong = value; }
        public CheckEdit CHKTrangThai { get => chkTrangThai; set => chkTrangThai = value; }
        public CheckEdit CHKNganhHoc { get => chkNganhHoc; set => chkNganhHoc = value; }
        public CheckEdit CHKNganhDay { get => chkNganhDay; set => chkNganhDay = value; }
        public CheckEdit CHKChungChi { get => chkChungChi; set => chkChungChi = value; }
        public CheckEdit CHKDangHocNangCao { get => chkDangHocNangCao; set => chkDangHocNangCao = value; }
        public RadioGroup RADSelectTimeToFilter { get => radSelectTimeToFilter; set => radSelectTimeToFilter = value; }
        public DateEdit DTTimeline { get => dtTimeline; set => dtTimeline = value; }
        public DateEdit DTFromDuration { get => dtFromDuration; set => dtFromDuration = value; }
        public DateEdit DTToDuration { get => dtToDuration; set => dtToDuration = value; }
        public CheckEdit CHKSaveFilter { get => chkSaveFilter; set => chkSaveFilter = value; }
        #endregion
        public void Attach(IExportDataPresenter presenter)
        {
            chkThongTinCaNhan.CheckedChanged += new EventHandler(presenter.CHKThongTinCaNhanChanged);
            chkCongTac.CheckedChanged += new EventHandler(presenter.CHKCongTacChanged);
            chkQuaTrinhLuong.CheckedChanged += new EventHandler(presenter.CHKQuaTrinhLuongChanged);
            chkHopDong.CheckedChanged += new EventHandler(presenter.CHKHopDongChanged);
            chkTrangThai.CheckedChanged += new EventHandler(presenter.CHKTrangThaiChanged);
            chkNganhHoc.CheckedChanged += new EventHandler(presenter.CHKNganhHocChanged);
            chkNganhDay.CheckedChanged += new EventHandler(presenter.CHKNganhDayChanged);
            chkChungChi.CheckedChanged += new EventHandler(presenter.CHKChungChiChanged);
            chkDangHocNangCao.CheckedChanged += new EventHandler(presenter.CHKDangHocNangCaoChanged);
            gvCustom.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
            btnExportExcel.ItemClick += (s, e) => presenter.ExportExcel();
            btnCheckAllAndUncheckAll.Click += (s, e) => presenter.CheckAllAndUncheckAll();
            radSelectTimeToFilter.SelectedIndexChanged += new EventHandler(presenter.EnableFilterDatetime);
            btnCancel.Click += (s, e) => presenter.Cancel();
            btnExportData.Click += (s, e) => presenter.ExportData();
        }
    }
}