using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;
namespace WebsiteSellingSuitcases.Controllers
{
    public class LienHeController : Controller
    {
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemLienHe(string hoten,string email,string tieude,string dienthoai,string tinnhan)
        {
            try
            {
                tLienHe lienHe = new tLienHe { HoTen = hoten, Email = email, TieuDe = tieude, DienThoai = dienthoai, TinNhan = tinnhan, Ngay = DateTime.Now };
                db.Entry(lienHe).State = System.Data.Entity.EntityState.Added;
                db.tLienHes.Add(lienHe);
                db.SaveChanges();
                TempData["GuiThanhCong"] = "Gửi thành công, chúng tôi sẽ liên lạc với bạn sau!";
            }
            catch
            {
                TempData["GuiThanhCong"] = "Gửi không thành công";
            }
            return RedirectToAction("Index");
        }
    }
}