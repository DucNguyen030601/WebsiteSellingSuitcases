using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;
using PagedList;
using WebsiteSellingSuitcases.Common;

namespace WebsiteSellingSuitcases.Areas.Admin.Controllers
{
    public class DuyetHoaDonController : BaseController
    {
        // GET: Admin/DuyetHoaDon
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        public ActionResult Index(int? page,string TimKiem)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            string timkiem = TimKiem ?? "";
            ViewBag.TongSo = db.tHoaDons.Where(n => n.Loai == false && n.NgayThanhToan == null && n.XacNhan == 0&&n.tKhachHang.TenKH.Contains(timkiem)).Count();
            return View(db.tHoaDons.Where(n => n.Loai == false && n.NgayThanhToan == null&&n.XacNhan==0 && n.tKhachHang.TenKH.Contains(timkiem)).OrderByDescending(n => n.NgayBan_NgayNhap).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DuyetHoaDonPartial()
        {
            ViewBag.TongSo = db.tHoaDons.Where(n => n.Loai == false && n.NgayThanhToan == null && n.XacNhan !=1).Count();
            return PartialView();
        }
        public ActionResult HuyDonHangPartial()
        {
            ViewBag.TongSo = db.tHoaDons.Where(n => n.Loai == false && n.NgayThanhToan == null && (n.XacNhan != 1 && n.XacNhan != 0)).Count();
            return PartialView();
        }
        public ActionResult HoaDonHuy(int? page)
        {
            int pageNumber = page ?? 1;
             int pageSize = 5;
            return View(db.tHoaDons.Where(n => n.Loai == false && n.NgayThanhToan == null&&(n.XacNhan!=1&&n.XacNhan!=0)).OrderByDescending(n => n.NgayBan_NgayNhap).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost] 
        public ActionResult XoaHoaDon(int SoHD,string strUrl)
        {
            var result = db.tChiTietHoaDons.Where(n => n.SoHD == SoHD).ToList();
            foreach (var item in result)
            {
                tChiTietHoaDon cthd = db.tChiTietHoaDons.SingleOrDefault(n => n.SoHD == item.SoHD && n.MaSP == item.MaSP);
                db.tChiTietHoaDons.Remove(cthd);
            }

            tHoaDon hd = db.tHoaDons.SingleOrDefault(n => n.SoHD == SoHD);
            db.tHoaDons.Remove(hd);
            db.SaveChanges();
            TempData["thongtin"] = "Xoá hoá đơn thành công!";
            return Redirect(strUrl);
        }
        [HttpPost]
        public ActionResult CapNhatHoaDon(int SoHD,string NoiDung,string strUrl)
        {
            tHoaDon hd = db.tHoaDons.SingleOrDefault(n => n.SoHD == SoHD);
            hd.NoiDung = NoiDung;
            db.SaveChanges();
            TempData["thongtin"] = "Cập nhật thành công!";
            return Redirect(strUrl);
        }
        [HttpPost]
        public ActionResult XacNhanHoaDon(int SoHD)
        {
            tNhanVien nv = Session[Sessions.SESSION_ADMIN] as tNhanVien;
            tHoaDon hd = db.tHoaDons.SingleOrDefault(n => n.SoHD == SoHD);
            hd.XacNhan = 1;
            hd.MaNV = nv.MaNV;
            db.SaveChanges();
            TempData["thongtin"] = "Xác nhận thành công!";
            return RedirectToAction("HoaDonDat","HoaDon");
        }
        [HttpPost]
        public ActionResult ThanhToanHoaDon(int SoHD)
        {
            tHoaDon hd = db.tHoaDons.SingleOrDefault(n => n.SoHD == SoHD);
            hd.NgayThanhToan = DateTime.Now;
            hd.NoiDung = "Đã nhận được hàng";
            db.SaveChanges();
            TempData["thongtin"] = "Thanh toán thành công!";
            return RedirectToAction("Index","HoaDon");
        }
        public ActionResult TraHang(int SoHD)
        {
            try
            {
                var result = (from q in db.tChiTietHoaDons where q.SoHD == SoHD
                              select q).ToList();
                foreach (var item in result)
                {
                    tDanhMucSP sp = db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == item.MaSP);
                    sp.SoLuong += item.SoLuong;
                    db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                tHoaDon hd = db.tHoaDons.SingleOrDefault(n => n.SoHD == SoHD);
                hd.XacNhan = 3;
                hd.NoiDung = "Khách hàng đã trả lại hàng - " + DateTime.Now;
                db.Entry(hd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["thongtin"] = "Trả lại hàng thành công!";
            }
            catch
            {
                TempData["thongtin"] = "Trả lại hàng không thành công!";
            }
            return RedirectToAction("HoaDonHuy");
        }
        public ActionResult HuyHoaDon(int SoHD)
        {
            try
            {
                tNhanVien nv = Session[Sessions.SESSION_ADMIN] as tNhanVien;
                var result = (from q in db.tChiTietHoaDons
                              where q.SoHD == SoHD
                              select q).ToList();
                foreach (var item in result)
                {
                    tDanhMucSP sp = db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == item.MaSP);
                    sp.SoLuong += item.SoLuong;
                    db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                tHoaDon hd = db.tHoaDons.SingleOrDefault(n => n.SoHD == SoHD);
                hd.XacNhan = 4;
                hd.MaNV = nv.MaNV;
                hd.NoiDung = "Nhân viên đã huỷ hoá đơn - " + DateTime.Now;
                db.Entry(hd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["thongtin"] = "Huỷ hoá đơn thành công!";
            }
            catch
            {
                TempData["thongtin"] = "Huỷ hoá đơn không thành công!";
            }
            return RedirectToAction("HoaDonHuy");
        }
    }
}