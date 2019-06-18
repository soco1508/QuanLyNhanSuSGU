using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNS_SGU.Presenter
{
    public interface IPresenterArgument
    {
        void Initialize(string name);
        object UI { get; }
    }
}
