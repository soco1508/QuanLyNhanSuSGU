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
    
    public partial class DangHocNangCao
    {
        public int idDangHocNangCao { get; set; }
        public int idVienChuc { get; set; }
        public string soQuyetDinh { get; set; }
        public string linkAnhQuyetDinh { get; set; }
        public Nullable<System.DateTime> ngayBatDau { get; set; }
        public Nullable<System.DateTime> ngayKetThuc { get; set; }
        public Nullable<int> loai { get; set; }
        public int idLoaiHocHamHocVi { get; set; }
        public string tenHocHamHocVi { get; set; }
        public string coSoDaoTao { get; set; }
        public string nuocCapBang { get; set; }
        public string ngonNguDaoTao { get; set; }
        public string hinhThucDaoTao { get; set; }
    
        public virtual LoaiHocHamHocVi LoaiHocHamHocVi { get; set; }
        public virtual VienChuc VienChuc { get; set; }

        public DangHocNangCao() { }

        public DangHocNangCao(DangHocNangCao dangHocNangCao)
        {
            idDangHocNangCao = dangHocNangCao.idDangHocNangCao;
            idVienChuc = dangHocNangCao.idVienChuc;
            soQuyetDinh = dangHocNangCao.soQuyetDinh;
            linkAnhQuyetDinh = dangHocNangCao.linkAnhQuyetDinh;
            ngayBatDau = dangHocNangCao.ngayBatDau;
            ngayKetThuc = dangHocNangCao.ngayKetThuc;
            loai = dangHocNangCao.loai;
            idLoaiHocHamHocVi = dangHocNangCao.idLoaiHocHamHocVi;
            tenHocHamHocVi = dangHocNangCao.tenHocHamHocVi;
            coSoDaoTao = dangHocNangCao.coSoDaoTao;
            ngonNguDaoTao = dangHocNangCao.ngonNguDaoTao;
            nuocCapBang = dangHocNangCao.nuocCapBang;
            hinhThucDaoTao = dangHocNangCao.hinhThucDaoTao;
            LoaiHocHamHocVi = dangHocNangCao.LoaiHocHamHocVi;
            VienChuc = dangHocNangCao.VienChuc;
        }
    }
}
