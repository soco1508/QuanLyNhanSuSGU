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
    
    public partial class DanhMucThoiGian
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMucThoiGian()
        {
            this.QuaTrinhDanhGiaVienChucs = new HashSet<QuaTrinhDanhGiaVienChuc>();
        }
    
        public int idDanhMucThoiGian { get; set; }
        public string tenDanhMucThoiGian { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaTrinhDanhGiaVienChuc> QuaTrinhDanhGiaVienChucs { get; set; }
    }
}
