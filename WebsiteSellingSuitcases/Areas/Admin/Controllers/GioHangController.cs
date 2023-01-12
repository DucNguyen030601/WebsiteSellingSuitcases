using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;
using WebsiteSellingSuitcases.Common;
namespace WebsiteSellingSuitcases.Areas.Admin.Controllers
{
    public class GioHangController : BaseController
    {
        // GET: GioHang
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session[Sessions.SESSION_CART_ADMIN] as List<GioHang>;

            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session[Sessions.SESSION_CART_ADMIN] = lstGioHang;
            }
            return lstGioHang;
        }
        [HttpPost]
        public ActionResult ThemGioHang(string sMaSP, int SoLuong, string strUrl)
        {
            tDanhMucSP sanpham = db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang spgh = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (spgh == null)
            {
                spgh = new GioHang(sMaSP, SoLuong);
                lstGioHang.Add(spgh);
                return Redirect(strUrl);
            }
            else
            {
                spgh.iSoLuong += SoLuong;
                return Redirect(strUrl);
            }
        }
        //tạo view Giỏ hàng
        public ActionResult Index()
        {
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSL = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult CapNhapGioHang(string sMaSP, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult XoaGioHang(string sMaSP, string strUrl)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.sMaSP == sMaSP);
            }
            return Redirect(strUrl);
        }
        private int TongSoLuong()
        {
            return LayGioHang().Sum(n => n.iSoLuong);
        }
        private double TongTien()
        {
            return LayGioHang().Sum(n => n.dThanhTienNhap);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
   
        public ActionResult DatHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            tNhanVien nv = Session[Sessions.SESSION_ADMIN] as tNhanVien;
                tHoaDon ddh = new tHoaDon();
                ddh.NgayBan_NgayNhap = DateTime.Now;
                ddh.Loai = true;
                ddh.MaNV = nv.MaNV;
                ddh.TongTien = (decimal)TongTien();
                db.tHoaDons.Add(ddh);
                db.SaveChanges();
                  
                AddCTHD(ddh.SoHD, lstGioHang);
                UpdateProduct(lstGioHang);

                TempData["thongtin"] = "Nhập hàng thành công!";
                return RedirectToAction("HoaDonNhap", "HoaDon");
            

        }
   
       
        public void AddCTHD(int SoHD, List<GioHang> lstGioHang)
        {
            foreach (var item in lstGioHang)
            {
                tChiTietHoaDon cthd = new tChiTietHoaDon();
                cthd.SoHD = SoHD;
                cthd.MaSP = item.sMaSP;
                cthd.SoLuong = item.iSoLuong;
                cthd.GiaBan = (decimal)item.dDonGiaNhap;
                cthd.ThanhTien = (decimal)item.dThanhTienNhap;
                cthd.ChietKhau = 0;
                db.Entry(cthd).State = System.Data.Entity.EntityState.Added;
                db.tChiTietHoaDons.Add(cthd);
                db.SaveChanges();
            }
        }
        public void UpdateProduct(List<GioHang> lstGioHang)
        {
            foreach (var item in lstGioHang)
            {
                tDanhMucSP sp = db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == item.sMaSP);
                sp.SoLuong += lstGioHang.SingleOrDefault(n => n.sMaSP == item.sMaSP).iSoLuong;
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}