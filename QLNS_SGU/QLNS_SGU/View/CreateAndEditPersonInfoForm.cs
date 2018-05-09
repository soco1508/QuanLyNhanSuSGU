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
using DevExpress.XtraTab;
using DevExpress.XtraTabbedMdi;

namespace QLNS_SGU.View
{
    public interface ICreateAndEditPersonInfoForm : IView<ICreateAndEditPersonInfoPresenter>
    {
        XtraTabbedMdiManager XtraTabbedMdiManager { get; set; }
    }
    public partial class CreateAndEditPersonInfoForm : XtraForm, ICreateAndEditPersonInfoForm
    {
        public CreateAndEditPersonInfoForm()
        {
            InitializeComponent();
        }

        public XtraTabbedMdiManager XtraTabbedMdiManager { get => xtraTabbedMdiManager1; set => xtraTabbedMdiManager1 = value; }

        public void Attach(ICreateAndEditPersonInfoPresenter presenter)
        {
            Load += (s, e) => presenter.LoadForm();
            FormClosing += new FormClosingEventHandler(presenter.FormClosing);
        }
    }
}