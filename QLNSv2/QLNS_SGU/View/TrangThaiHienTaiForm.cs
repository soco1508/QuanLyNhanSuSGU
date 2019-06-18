using DevExpress.XtraEditors;
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
    public interface ITrangThaiHienTaiForm : IView<ITrangThaiHienTaiPresenter>
    {
        LabelControl LBTrangThai { set; }
        LabelControl LBNgayBatDau { set; }
        LabelControl LBNgayKetThuc { set; }
        LabelControl LBMoTa { set; }
        LabelControl LBDiaDiem { set; }
        HyperlinkLabelControl LinkLBQuyetDinh { get; set; }
    }
    public partial class TrangThaiHienTaiForm : XtraForm, ITrangThaiHienTaiForm
    {
        public TrangThaiHienTaiForm()
        {
            InitializeComponent();
        }

        public LabelControl LBTrangThai { get => lbTrangThai; set => lbTrangThai = value; }
        public LabelControl LBNgayBatDau { get => lbNgayBatDau; set => lbNgayBatDau = value; }
        public LabelControl LBNgayKetThuc { get => lbNgayKetThuc; set => lbNgayKetThuc = value; }
        public LabelControl LBMoTa { get => lbMoTa; set => lbMoTa = value; }
        public LabelControl LBDiaDiem { get => lbDiaDiem; set => lbDiaDiem = value; }
        public HyperlinkLabelControl LinkLBQuyetDinh { get => linklbQuyetDinh; set => linklbQuyetDinh = value; }

        public void Attach(ITrangThaiHienTaiPresenter presenter)
        {
            Load += (s, e) => presenter.LoadForm();
            linklbQuyetDinh.Click += (s, e) => presenter.DownloadFile();
            btnClose.Click += (s, e) => presenter.Close();
        }

    }
}
