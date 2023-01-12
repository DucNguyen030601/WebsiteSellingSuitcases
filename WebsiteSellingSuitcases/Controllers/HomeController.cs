using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Common;
using WebsiteSellingSuitcases.Models;
namespace WebsiteSellingSuitcases.Controllers
{
    public class HomeController : Controller
    {
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachYeuThich()
        {
            tKhachHang kh = Session[Sessions.SESSION_CUSTOMMER] as tKhachHang;
            return PartialView(db.tYeuThiches.Where(n => n.MaKH == kh.MaKH).ToList());
        }
        public ActionResult SanPhamBanChay()
        {
            return PartialView();
        }

    }
}