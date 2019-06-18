//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class NganhVienChuc
    {
        public int idNganhVienChuc { get; set; }
        public int idVienChuc { get; set; }
        public int idLoaiNganh { get; set; }
        public int idNganhDaoTao { get; set; }
        public int idChuyenNganh { get; set; }
        public int idHocHamHocViVienChuc { get; set; }
        public Nullable<bool> phanLoai { get; set; }
        public Nullable<System.DateTime> ngayBatDau { get; set; }
        public Nullable<System.DateTime> ngayKetThuc { get; set; }
        public string linkVanBanDinhKem { get; set; }
        public string trinhDoDay { get; set; }
    
        public virtual ChuyenNganh ChuyenNganh { get; set; }
        public virtual HocHamHocViVienChuc HocHamHocViVienChuc { get; set; }
        public virtual LoaiNganh LoaiNganh { get; set; }
        public virtual NganhDaoTao NganhDaoTao { get; set; }
        public virtual VienChuc VienChuc { get; set; }

        public NganhVienChuc() { }
        public NganhVienChuc(NganhVienChuc nganhVienChuc)
        {
            idNganhVienChuc = nganhVienChuc.idNganhVienChuc;
            idVienChuc = nganhVienChuc.idVienChuc;
            idLoaiNganh = nganhVienChuc.idLoaiNganh;
            idNganhDaoTao = nganhVienChuc.idNganhDaoTao;
            idChuyenNganh = nganhVienChuc.idChuyenNganh;
            idHocHamHocViVienChuc = nganhVienChuc.idHocHamHocViVienChuc;
            phanLoai = nganhVienChuc.phanLoai;
            ngayBatDau = nganhVienChuc.ngayBatDau;
            ngayKetThuc = nganhVienChuc.ngayKetThuc;
            linkVanBanDinhKem = nganhVienChuc.linkVanBanDinhKem;
            ChuyenNganh = nganhVienChuc.ChuyenNganh;
            HocHamHocViVienChuc = nganhVienChuc.HocHamHocViVienChuc;
            LoaiNganh = nganhVienChuc.LoaiNganh;
            NganhDaoTao = nganhVienChuc.NganhDaoTao;
            VienChuc = nganhVienChuc.VienChuc;
        }
    }
}
