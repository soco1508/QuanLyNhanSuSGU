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
    
    public partial class ChungChiVienChuc
    {
        public int idChungChiVienChuc { get; set; }
        public int idVienChuc { get; set; }
        public int idLoaiChungChi { get; set; }
        public Nullable<System.DateTime> ngayCapChungChi { get; set; }
        public string ghiChu { get; set; }
        public string linkVanBanDinhKem { get; set; }
        public string capDoChungChi { get; set; }
    
        public virtual LoaiChungChi LoaiChungChi { get; set; }
        public virtual VienChuc VienChuc { get; set; }
    }
}