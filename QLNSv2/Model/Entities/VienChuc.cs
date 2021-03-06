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
    
    public partial class VienChuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VienChuc()
        {
            this.BaoHiemXaHois = new HashSet<BaoHiemXaHoi>();
            this.ChucVuDonViVienChucs = new HashSet<ChucVuDonViVienChuc>();
            this.ChungChiVienChucs = new HashSet<ChungChiVienChuc>();
            this.DangHocNangCaos = new HashSet<DangHocNangCao>();
            this.HocHamHocViVienChucs = new HashSet<HocHamHocViVienChuc>();
            this.HopDongVienChucs = new HashSet<HopDongVienChuc>();
            this.NganhVienChucs = new HashSet<NganhVienChuc>();
            this.QuaTrinhDanhGiaVienChucs = new HashSet<QuaTrinhDanhGiaVienChuc>();
            this.QuaTrinhGianDoanBaoHiemXaHois = new HashSet<QuaTrinhGianDoanBaoHiemXaHoi>();
            this.QuaTrinhLuongs = new HashSet<QuaTrinhLuong>();
            this.QuaTrinhPhuCapThamNienNhaGiaos = new HashSet<QuaTrinhPhuCapThamNienNhaGiao>();
            this.TrangThaiVienChucs = new HashSet<TrangThaiVienChuc>();
        }
    
        public int idVienChuc { get; set; }
        public string maVienChuc { get; set; }
        public string ho { get; set; }
        public string ten { get; set; }
        public Nullable<bool> gioiTinh { get; set; }
        public Nullable<System.DateTime> ngaySinh { get; set; }
        public string sDT { get; set; }
        public string soChungMinhNhanDan { get; set; }
        public string noiSinh { get; set; }
        public string queQuan { get; set; }
        public int idDanToc { get; set; }
        public int idTonGiao { get; set; }
        public string hoKhauThuongTru { get; set; }
        public string noiOHienNay { get; set; }
        public Nullable<bool> laDangVien { get; set; }
        public Nullable<System.DateTime> ngayVaoDang { get; set; }
        public Nullable<System.DateTime> ngayThamGiaCongTac { get; set; }
        public Nullable<System.DateTime> ngayVaoNganh { get; set; }
        public Nullable<System.DateTime> ngayVeTruong { get; set; }
        public string vanHoa { get; set; }
        public string ghiChu { get; set; }
        public byte[] anh { get; set; }
        public Nullable<System.DateTime> ngayTuyenDungChinhThuc { get; set; }
        public string email { get; set; }
        public Nullable<int> gioChuan { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaoHiemXaHoi> BaoHiemXaHois { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChucVuDonViVienChuc> ChucVuDonViVienChucs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChungChiVienChuc> ChungChiVienChucs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangHocNangCao> DangHocNangCaos { get; set; }
        public virtual DanToc DanToc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HocHamHocViVienChuc> HocHamHocViVienChucs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HopDongVienChuc> HopDongVienChucs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NganhVienChuc> NganhVienChucs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaTrinhDanhGiaVienChuc> QuaTrinhDanhGiaVienChucs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaTrinhGianDoanBaoHiemXaHoi> QuaTrinhGianDoanBaoHiemXaHois { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaTrinhLuong> QuaTrinhLuongs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuaTrinhPhuCapThamNienNhaGiao> QuaTrinhPhuCapThamNienNhaGiaos { get; set; }
        public virtual TonGiao TonGiao { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrangThaiVienChuc> TrangThaiVienChucs { get; set; }
    }
}
