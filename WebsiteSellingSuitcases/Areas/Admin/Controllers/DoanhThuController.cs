using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;
namespace WebsiteSellingSuitcases.Areas.Admin.Controllers
{
    public class DoanhThuController : BaseController
    {
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        // GET: Admin/DoanhThu
        public ActionResult Index(int? thang,int? nam)
        {
            int t = thang ?? DateTime.Now.Month;
            int n = nam ?? DateTime.Now.Year;
            ViewBag.Thang = t;
            ViewBag.Nam = n;
            ViewBag.HDB = (from p in db.tHoaDons where ((DateTime)p.NgayThanhToan).Month==t && ((DateTime)p.NgayThanhToan).Year==n && p.Loai==false
                           select p.TongTien).Sum()??0;
            ViewBag.HDN = (from p in db.tHoaDons
                           where ((DateTime)p.NgayBan_NgayNhap).Month == t && ((DateTime)p.NgayBan_NgayNhap).Year == n && p.Loai == true
                           select p.TongTien).Sum()??0;
            return View();
        }
    }
}