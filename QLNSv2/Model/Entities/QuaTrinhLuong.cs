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
    
    public partial class QuaTrinhLuong
    {
        public int idQuaTrinhLuong { get; set; }
        public int idVienChuc { get; set; }
        public int idBac { get; set; }
        public Nullable<System.DateTime> ngayBatDau { get; set; }
        public Nullable<System.DateTime> ngayLenLuong { get; set; }
        public Nullable<bool> dangHuongLuong { get; set; }
        public Nullable<int> truocHan { get; set; }
        public Nullable<double> heSoVuotKhung { get; set; }
        public string linkVanBanDinhKem { get; set; }
    
        public virtual Bac Bac { get; set; }
        public virtual VienChuc VienChuc { get; set; }
    }
}
