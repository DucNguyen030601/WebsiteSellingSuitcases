﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteSellingSuitcases.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebsiteSellingSuitcasesEntities : DbContext
    {
        public WebsiteSellingSuitcasesEntities()
            : base("name=WebsiteSellingSuitcasesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<tADMIN> tADMINs { get; set; }
        public virtual DbSet<tAnhSP> tAnhSPs { get; set; }
        public virtual DbSet<tChatLieu> tChatLieux { get; set; }
        public virtual DbSet<tChiTietHoaDon> tChiTietHoaDons { get; set; }
        public virtual DbSet<tDanhMucSP> tDanhMucSPs { get; set; }
        public virtual DbSet<tDinhKem> tDinhKems { get; set; }
        public virtual DbSet<tDinhKemSP> tDinhKemSPs { get; set; }
        public virtual DbSet<tHangSX> tHangSXes { get; set; }
        public virtual DbSet<tKhachHang> tKhachHangs { get; set; }
        public virtual DbSet<tKichThuoc> tKichThuocs { get; set; }
        public virtual DbSet<tLoaiBlog> tLoaiBlogs { get; set; }
        public virtual DbSet<tLoaiDT> tLoaiDTs { get; set; }
        public virtual DbSet<tLoaiSP> tLoaiSPs { get; set; }
        public virtual DbSet<tNhanVien> tNhanViens { get; set; }
        public virtual DbSet<tQuocGia> tQuocGias { get; set; }
        public virtual DbSet<tYeuThich> tYeuThiches { get; set; }
        public virtual DbSet<tBlog> tBlogs { get; set; }
        public virtual DbSet<tLienHe> tLienHes { get; set; }
        public virtual DbSet<tHoaDon> tHoaDons { get; set; }
    }
}
