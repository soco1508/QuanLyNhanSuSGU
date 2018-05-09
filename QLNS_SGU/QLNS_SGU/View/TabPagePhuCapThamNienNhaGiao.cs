using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using QLNS_SGU.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNS_SGU.View
{
    public interface ITabPagePhuCapThamNienNhaGiao : IView<ITabPagePhuCapThamNienNhaGiaoPresenter>
    {
        OpenFileDialog OpenFileDialog { get; set; }
        SaveFileDialog SaveFileDialog { get; set; }
        TextEdit TXTMaVienChuc { get; set; }
        SpinEdit TXTHeSoPhuCap { get; set; }
        DateEdit DTNgayBatDau { get; set; }
        DateEdit DTNgayNangPhuCap { get; set; }
        TextEdit TXTLinkVanBanDinhKem { get; set; }
        GridControl GCPhuCapThamNienNhaGiao { get; set; }
        GridView GVPhuCapThamNienNhaGiao { get; set; }
    }
    public partial class TabPagePhuCapThamNienNhaGiao : XtraForm, ITabPagePhuCapThamNienNhaGiao
    {
        public TabPagePhuCapThamNienNhaGiao()
        {
            InitializeComponent();
        }
        #region Controls
        public OpenFileDialog OpenFileDialog { get => openFileDialog1; set => openFileDialog1 = value; }
        public SaveFileDialog SaveFileDialog { get => saveFileDialog1; set => saveFileDialog1 = value; }
        public TextEdit TXTMaVienChuc { get => txtMaVienChuc; set => txtMaVienChuc = value; }
        public SpinEdit TXTHeSoPhuCap { get => txtHeSoPhuCap; set => txtHeSoPhuCap = value; }
        public DateEdit DTNgayBatDau { get => dtNgayBatDau; set => dtNgayBatDau = value; }
        public DateEdit DTNgayNangPhuCap { get => dtNgayNangPhuCap; set => dtNgayNangPhuCap = value; }
        public TextEdit TXTLinkVanBanDinhKem { get => txtLinkVanBanDinhKem; set => txtLinkVanBanDinhKem = value; }    
        public GridControl GCPhuCapThamNienNhaGiao { get => gcPhuCapThamNienNhaGiao; set => gcPhuCapThamNienNhaGiao = value; }
        public GridView GVPhuCapThamNienNhaGiao { get => gvPhuCapThamNienNhaGiao; set => gvPhuCapThamNienNhaGiao = value; }
        #endregion
        public void Attach(ITabPagePhuCapThamNienNhaGiaoPresenter presenter)
        {
            Load += (s, e) => presenter.LoadForm();
            gvPhuCapThamNienNhaGiao.Click += (s, e) => presenter.ClickRowAndShowInfo();
            btnUpload.Click += (s, e) => presenter.UploadFileToGoogleDrive();
            btnDownload.Click += (s, e) => presenter.DownloadFileToDevice();
            btnSave.Click += (s, e) => presenter.Save();
            btnRefresh.Click += (s, e) => presenter.Refresh();
            btnAdd.Click += (s, e) => presenter.Add();
            btnDelete.Click += (s, e) => presenter.Delete();
            btnExportExcel.Click += (s, e) => presenter.ExportExcel();
            txtHeSoPhuCap.EditValueChanged += new EventHandler(presenter.HeSoPhuCapChanged);
            dtNgayBatDau.TextChanged += new EventHandler(presenter.NgayBatDauChanged);
            dtNgayNangPhuCap.TextChanged += new EventHandler(presenter.NgayNangPhuCapChanged);
            txtLinkVanBanDinhKem.TextChanged += new EventHandler(presenter.LinkVanBanDinhKemChanged);
            gvPhuCapThamNienNhaGiao.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}
