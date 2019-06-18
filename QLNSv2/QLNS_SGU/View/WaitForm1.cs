using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;
using QLNS_SGU.Presenter;
using DevExpress.XtraBars;

namespace QLNS_SGU.View
{
    public interface IWaitForm : IView<WaitPresenter>
    {
    }
    public partial class WaitForm1 : WaitForm, IWaitForm
    {
        public WaitForm1()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }

        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }
 
        public void Attach(WaitPresenter presenter)
        {
        }

        #endregion



        public enum WaitFormCommand
        {
        }
    }
}