using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebsiteSellingSuitcases.Models
{
    public class GioHang
    {
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        public string sMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinhAnh { get; set; }
        public double sChietKhau { get; set; }
        public double sGiaBan { get; set; }
        public double dDonGia { get { return sGiaBan - sGiaBan * sChietKhau; } }
        public int iSoLuong { get; set; }
        public double dThanhTien
        { 
            get { return iSoLuong * dDonGia; }
        }
        public double dDonGiaNhap { get; set; }
        public double dThanhTienNhap
        {
            get { return iSoLuong * dDonGiaNhap; }

        }
        public GioHang(string MaSP,int SoLuong)
        {
            sMaSP = MaSP;
            tDanhMucSP sanpham = db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == sMaSP);
            sTenSP = sanpham.TenSP;
            sHinhAnh = sanpham.Anh;
            sGiaBan = (double)sanpham.GiaBan;
            sChietKhau = (double)sanpham.ChietKhau;
            iSoLuong = SoLuong;
            dDonGiaNhap = (double)sanpham.GiaNhap;
        }
        public GioHang() { }
        public double ThanhTien { get; set; }
    }
}