using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLNS_SGU.Presenter;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars;

namespace QLNS_SGU.View
{
    public interface IStoreImageForm : IView<IStoreImagePresenter>
    {
        GridControl GridControl { get; set; }
        GridView GridView { get; set; }
        OpenFileDialog OpenFileDialog { get; set; }
        ToolTipController ToolTipController { get; set; }
        //SplashScreenManager SplashScreenManager { get; set; }
        BarEditItem MaTheVienChuc { get; set; }
        BarButtonItem DownLoad { get; set; }
    }
    public partial class StoreImageForm : XtraForm, IStoreImageForm
    {
        public StoreImageForm()
        {
            InitializeComponent();
        }

        public GridControl GridControl
        {
            get
            {
                return gridControl1;
            }
            set
            {
                gridControl1 = value;
            }
        }

        public GridView GridView
        {
            get
            {
                return gridView1;
            }
            set
            {
                gridView1 = value;
            }
        }

        public OpenFileDialog OpenFileDialog
        {
            get => openFileDialog1;
            set => openFileDialog1 = value;
        }

        public ToolTipController ToolTipController
        {
            get => toolTipController1;
            set => toolTipController1 = value;
        }

        //public SplashScreenManager SplashScreenManager
        //{
        //    get => splashScreenManager1;
        //    set => splashScreenManager1 = value;
        //}

        public BarEditItem MaTheVienChuc
        {
            get => txtMaTheVienChuc;
            set => txtMaTheVienChuc = value;
        }

        public BarButtonItem DownLoad
        {
            get => btnDownload;
            set => btnDownload = value;
        }

        public void Attach(IStoreImagePresenter presenter)
        {
            Load += (sender, e) => presenter.LoadDriveFiles();
            btnUpload.ItemClick += (sender, e) => presenter.Upload();
            btnDownload.ItemClick += (sender, e) => presenter.Download();
            btnDelete.ItemClick += (sender, e) => presenter.Delete();
            btnRefresh.ItemClick += (sender, e) => presenter.LoadDriveFiles();
        }       
    }
}
