//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class tChiTietHoaDon
    {
        public int SoHD { get; set; }
        public string MaSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public Nullable<decimal> ThanhTien { get; set; }
        public Nullable<double> ChietKhau { get; set; }
    
        public virtual tDanhMucSP tDanhMucSP { get; set; }
        public virtual tHoaDon tHoaDon { get; set; }
    }
}
