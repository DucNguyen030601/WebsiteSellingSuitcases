using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;
using PagedList;
namespace WebsiteSellingSuitcases.Areas.Admin.Controllers
{
    public class HoaDonController : BaseController
    {
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        // GET: Admin/HoaDon
        public ActionResult HoaDonDat(int? page,string TimKiem)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            string timkiem = TimKiem ?? "";
            return View(db.tHoaDons.Where(n=>n.Loai==false&&n.NgayThanhToan==null&&n.XacNhan==1&&n.tKhachHang.TenKH.Contains(timkiem)).OrderByDescending(n=>n.NgayBan_NgayNhap).ToPagedList(pageNumber,pageSize));
        }
        public ActionResult HoaDonNhap(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            return View(db.tHoaDons.Where(n => n.Loai == true && n.NgayThanhToan == null ).OrderByDescending(n => n.NgayBan_NgayNhap).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            return View(db.tHoaDons.Where(n => n.Loai == false && n.NgayThanhToan != null ).OrderByDescending(n => n.NgayBan_NgayNhap).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult HoaDonKhachDatPartial()
        {
            ViewBag.TongSo = db.tHoaDons.Where(n => n.Loai == false && n.NgayThanhToan == null && n.XacNhan == 1).Count();
            return PartialView();
        }
        public ActionResult ChiTietHoaDon(int SoHD)
        {
            List<GioHang> gioHang = (from p in db.tDanhMucSPs
                                     from q in db.tChiTietHoaDons
                                     where p.MaSP == q.MaSP && q.SoHD == SoHD
                                     select new GioHang
                                     {
                                         sTenSP = p.TenSP,
                                         sHinhAnh = p.Anh,
                                         iSoLuong = (int)q.SoLuong,
                                         sChietKhau = (double)q.ChietKhau,
                                         sGiaBan = (double)q.GiaBan,
                                         ThanhTien = (double)q.ThanhTien

                                     }).ToList();
            return PartialView(gioHang);
        }
    }
}