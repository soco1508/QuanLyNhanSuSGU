﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLNSSGU_1Entities : DbContext
    {
        public QLNSSGU_1Entities()
            : base("name=QLNSSGU_1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bac> Bacs { get; set; }
        public virtual DbSet<BaoHiemXaHoi> BaoHiemXaHois { get; set; }
        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<ChucVuDonViVienChuc> ChucVuDonViVienChucs { get; set; }
        public virtual DbSet<ChungChiVienChuc> ChungChiVienChucs { get; set; }
        public virtual DbSet<ChuyenNganh> ChuyenNganhs { get; set; }
        public virtual DbSet<DangHocNangCao> DangHocNangCaos { get; set; }
        public virtual DbSet<DanhMucThoiGian> DanhMucThoiGians { get; set; }
        public virtual DbSet<DanToc> DanTocs { get; set; }
        public virtual DbSet<DonVi> DonVis { get; set; }
        public virtual DbSet<HocHamHocViVienChuc> HocHamHocViVienChucs { get; set; }
        public virtual DbSet<HopDongVienChuc> HopDongVienChucs { get; set; }
        public virtual DbSet<LoaiChucVu> LoaiChucVus { get; set; }
        public virtual DbSet<LoaiChungChi> LoaiChungChis { get; set; }
        public virtual DbSet<LoaiDonVi> LoaiDonVis { get; set; }
        public virtual DbSet<LoaiHocHamHocVi> LoaiHocHamHocVis { get; set; }
        public virtual DbSet<LoaiHopDong> LoaiHopDongs { get; set; }
        public virtual DbSet<LoaiNganh> LoaiNganhs { get; set; }
        public virtual DbSet<MucDoDanhGia> MucDoDanhGias { get; set; }
        public virtual DbSet<Ngach> Ngaches { get; set; }
        public virtual DbSet<NganhDaoTao> NganhDaoTaos { get; set; }
        public virtual DbSet<NganhVienChuc> NganhVienChucs { get; set; }
        public virtual DbSet<QuanTriVien> QuanTriViens { get; set; }
        public virtual DbSet<QuaTrinhDanhGiaVienChuc> QuaTrinhDanhGiaVienChucs { get; set; }
        public virtual DbSet<QuaTrinhGianDoanBaoHiemXaHoi> QuaTrinhGianDoanBaoHiemXaHois { get; set; }
        public virtual DbSet<QuaTrinhLuong> QuaTrinhLuongs { get; set; }
        public virtual DbSet<QuaTrinhPhuCapThamNienNhaGiao> QuaTrinhPhuCapThamNienNhaGiaos { get; set; }
        public virtual DbSet<ThamSo> ThamSoes { get; set; }
        public virtual DbSet<ToChuyenMon> ToChuyenMons { get; set; }
        public virtual DbSet<TonGiao> TonGiaos { get; set; }
        public virtual DbSet<TrangThai> TrangThais { get; set; }
        public virtual DbSet<TrangThaiVienChuc> TrangThaiVienChucs { get; set; }
        public virtual DbSet<VienChuc> VienChucs { get; set; }
    }
}
