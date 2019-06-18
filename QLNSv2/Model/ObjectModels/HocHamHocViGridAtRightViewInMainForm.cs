using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ObjectModels
{
    public class HocHamHocViGridAtRightViewInMainForm
    {
        public string TenHocHamHocVi { get; set; }
        public string LoaiHocHamHocVi { get; set; }
        public string NganhDaoTao { get; set; }
        public string ChuyenNganh { get; set; }

        public HocHamHocViGridAtRightViewInMainForm()
        {
            TenHocHamHocVi = string.Empty;
            LoaiHocHamHocVi = string.Empty;
            NganhDaoTao = string.Empty;
            ChuyenNganh = string.Empty;
        }
    }
}
