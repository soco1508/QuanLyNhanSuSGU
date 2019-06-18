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
    public interface ITabPageBaoHiemXaHoi : IView<ITabPageBaoHiemXaHoiPresenter>
    {
        GridControl GCQuaTrinhGianDoanBaoHiemXaHoi { get; set; }
        GridView GVQuaTrinhGianDoanBaoHiemXaHoi { get; set; }
        TextEdit TXTSoSo { get; set; }
        DateEdit DTNgayThamGia { get; set; }
        DateEdit DTNgayCapSo { get; set; }
        DateEdit DTNgayRutSo { get; set; }
        MemoEdit TXTGhiChu { get; set; }
    }
    public partial class TabPageBaoHiemXaHoi : XtraForm, ITabPageBaoHiemXaHoi
    {
        public TabPageBaoHiemXaHoi()
        {
            InitializeComponent();
        }
        #region Controls
        public GridControl GCQuaTrinhGianDoanBaoHiemXaHoi { get => gcQuaTrinhGianDoanBaoHiemXaHoi; set => gcQuaTrinhGianDoanBaoHiemXaHoi = value; }
        public GridView GVQuaTrinhGianDoanBaoHiemXaHoi { get => gvQuaTrinhGianDoanBaoHiemXaHoi; set => gvQuaTrinhGianDoanBaoHiemXaHoi = value; }
        public TextEdit TXTSoSo { get => txtSoSo; set => txtSoSo = value; }
        public DateEdit DTNgayThamGia { get => dtNgayThamGia; set => dtNgayThamGia = value; }
        public DateEdit DTNgayCapSo { get => dtNgayCapSo; set => dtNgayCapSo = value; }
        public DateEdit DTNgayRutSo { get => dtNgayRutSo; set => dtNgayRutSo = value; }
        public MemoEdit TXTGhiChu { get => txtGhiChu; set => txtGhiChu = value; }
        #endregion
        public void Attach(ITabPageBaoHiemXaHoiPresenter presenter)
        {
            Load += (s, e) => presenter.LoadForm();
            btnSave.Click += (s, e) => presenter.SaveBaoHiemXaHoi();
            txtSoSo.TextChanged += new EventHandler(presenter.SoSoChanged);
            dtNgayThamGia.DateTimeChanged += new EventHandler(presenter.NgayThamGiaChanged);
            dtNgayCapSo.DateTimeChanged += new EventHandler(presenter.NgayCapSoChanged);
            dtNgayRutSo.DateTimeChanged += new EventHandler(presenter.NgayRutSoChanged);
            txtGhiChu.TextChanged += new EventHandler(presenter.GhiChuChanged);
            gcQuaTrinhGianDoanBaoHiemXaHoi.EmbeddedNavigator.ButtonClick += new NavigatorButtonClickEventHandler(presenter.ButtonClick);
            gcQuaTrinhGianDoanBaoHiemXaHoi.MouseDoubleClick += new MouseEventHandler(presenter.MouseDoubleClick);
            gvQuaTrinhGianDoanBaoHiemXaHoi.HiddenEditor += new EventHandler(presenter.HiddenEditor);
            gvQuaTrinhGianDoanBaoHiemXaHoi.InitNewRow += new InitNewRowEventHandler(presenter.InitNewRow);
            gvQuaTrinhGianDoanBaoHiemXaHoi.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(presenter.RowIndicator);
        }
    }
}
