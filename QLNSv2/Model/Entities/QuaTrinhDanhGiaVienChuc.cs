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
    
    public partial class QuaTrinhDanhGiaVienChuc
    {
        public int idQuaTrinhDanhGiaVienChuc { get; set; }
        public int idVienChuc { get; set; }
        public int idDanhMucThoiGian { get; set; }
        public int idMucDoDanhGia { get; set; }
        public Nullable<System.DateTime> ngayBatDau { get; set; }
        public Nullable<System.DateTime> ngayKetThuc { get; set; }
        public string linkVanBanDinhKem { get; set; }
        public string nhanXet { get; set; }
    
        public virtual DanhMucThoiGian DanhMucThoiGian { get; set; }
        public virtual MucDoDanhGia MucDoDanhGia { get; set; }
        public virtual VienChuc VienChuc { get; set; }
    }
}