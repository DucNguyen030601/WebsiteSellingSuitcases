using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebsiteSellingSuitcases.Models;
namespace WebsiteSellingSuitcases.Controllers
{
    public class BlogController : Controller
    {
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        // GET: Blog
        public ActionResult Index(int? page,string TheLoai, string TimKiem)
        {
            int pageIndex = page ?? 1;
            int pageSize = 6;
            string theloai = TheLoai ?? "";
            string timkiem = TimKiem ?? "";
            string url = Request.Url.ToString();


            return View(db.tBlogs.Where(n=>n.tLoaiBlog.Ten.Contains(theloai)&&n.TieuDe.Contains(timkiem)).OrderByDescending(n=>n.Ngay).ToPagedList(pageIndex,pageSize));
        }
        public ActionResult DocThem(int Blog_ID)
        {
            ViewBag.Comments = db.Reviews.Where(n => n.Blog_ID == Blog_ID).Count();
            tBlog blog = db.tBlogs.SingleOrDefault(n => n.Blog_ID == Blog_ID);
            blog.LuotXem += 1;
            db.Entry(blog).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(db.tBlogs.SingleOrDefault(n=>n.Blog_ID== Blog_ID));
        }
        public ActionResult SubmitComment(int Blog_ID, string email, string author, string comment)
        {
            Review review = new Review { Blog_ID = Blog_ID, Email = email, HoTen = author, NoiDung = comment, ThoiGian = DateTime.Now };
            db.Entry(review).State = System.Data.Entity.EntityState.Added;
            db.Reviews.Add(review);
            db.SaveChanges();
            return RedirectToAction("DocThem", new { Blog_ID = Blog_ID });
        }
        public ActionResult TheLoaiBlog()
        {
            return PartialView(db.tLoaiBlogs.OrderBy(n=>n.Ten).ToList());
        }
        public ActionResult BaiVietGanDayBlog()
        {
            return PartialView(db.tBlogs.OrderByDescending(n=>n.Ngay).Take(10).ToList());
        }
        public ActionResult PhoBienBlog()
        {
            return PartialView(db.tBlogs.OrderByDescending(n => n.LuotXem).Take(10).ToList());
        }
        public ActionResult BinhLuanBlog()
        {
            
            return PartialView(db.Reviews.Where(n=>n.Blog_ID!=null).OrderByDescending(n => n.ThoiGian).Take(10).ToList());
        }
    }
}