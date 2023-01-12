using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;
using WebsiteSellingSuitcases.Common;
namespace WebsiteSellingSuitcases.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        // GET: Admin/DangNhap
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        public ActionResult Index()
        {
            Session.Remove(Sessions.SESSION_ADMIN);
            return View();
        }
        [HttpPost]
        public ActionResult Index(tNhanVien a)
        {
            tNhanVien nv = db.tNhanViens.SingleOrDefault(n => n.TenNguoiDung == a.TenNguoiDung && n.MatKhau == a.MatKhau);
            if (nv == null)
            {
                ModelState.AddModelError("check", "Tên người dùng hoặc mật khẩu không chính xác");
                return View();
            }
            else
            {
                Session[Sessions.SESSION_ADMIN] = nv;
                return RedirectToAction("Index", "TrangChu");
            }    
            
        }
    }
}