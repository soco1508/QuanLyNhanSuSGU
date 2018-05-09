using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using QLNS_SGU.Presenter;
using DevExpress.LookAndFeel;

namespace QLNS_SGU.View
{
    public interface IContainerForm : IView<IContainerPresenter>
    {
        string XinChao { set; }
    }
    public partial class ContainerForm : DevExpress.XtraBars.Ribbon.RibbonForm, IContainerForm
    {        
        public ContainerForm()
        {
            InitializeComponent();
        }

        public string XinChao { set => txtXinChao.Caption = value; }

        public void Attach(IContainerPresenter presenter)
        {
            btnExit.ItemClick += (s, e) => presenter.Exit();
            btnLogout.ItemClick += (s, e) => presenter.Logout();
            btnNaviMain.ItemClick += (s, e) => presenter.NaviMain();
            btnNaviImportData.ItemClick += (s, e) => presenter.NaviImportData();
            btnNaviExportDataMultiDomain.ItemClick += (s, e) => presenter.NaviExportDataMultiDomain();
            btnNaviExportDataOneDomain.ItemClick += (s, e) => presenter.NaviExportDataOneDomain();
            btnOpenCreateAndEditPersonInfoForm.ItemClick += (s, e) => presenter.OpenCreateAndEditPersonInfoForm();
            btnNaviLoaiNganh.ItemClick += (s, e) => presenter.NaviLoaiNganh();
            btnNaviNganhDaoTao.ItemClick += (s, e) => presenter.NaviNganhDaoTao();
            btnNaviChuyenNganh.ItemClick += (s, e) => presenter.NaviChuyenNganh();
            btnNaviLoaiDonVi.ItemClick += (s, e) => presenter.NaviLoaiDonVi();
            btnNaviDonVi.ItemClick += (s, e) => presenter.NaviDonVi();
            btnNaviToChuyenMon.ItemClick += (s, e) => presenter.NaviToChuyenMon();
            btnNaviLoaiChucVu.ItemClick += (s, e) => presenter.NaviLoaiChucVu();
            btnNaviChucVu.ItemClick += (s, e) => presenter.NaviChucVu();
            btnNaviNgach.ItemClick += (s, e) => presenter.NaviNgach();
            btnNaviBac.ItemClick += (s, e) => presenter.NaviBac();
            btnNaviLoaiHopDong.ItemClick += (s, e) => presenter.NaviLoaiHopDong();
            btnNaviLoaiHocHamHocVi.ItemClick += (s, e) => presenter.NaviLoaiHocHamHocVi();
            btnNaviLoaiChungChi.ItemClick += (s, e) => presenter.NaviLoaiChungChi();
            btnNaviTrangThai.ItemClick += (s, e) => presenter.NaviTrangThai();
        }       
    }
}