using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLNS_SGU.Presenter;
using QLNS_SGU.View;

namespace QLNS_SGU.Presenter
{
    public interface IWaitPresenter : IPresenter
    {
    }
    public class WaitPresenter : IWaitPresenter
    {
        private WaitForm1 _view;

        public WaitPresenter(WaitForm1 view)
        {
            _view = view;
        }
        public object UI
        {
            get
            {
                return _view;
            }
        }

        public void Initialize()
        {
            _view.Attach(this);
        }
    }
}
