using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;
using PagedList;
using System.IO;

namespace WebsiteSellingSuitcases.Areas.Admin.Controllers
{
    public class BlogController : BaseController
    {
        private WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();

        // GET: Admin/Blog
        public void PostedFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Images/Blogs/"),
                                    Path.GetFileName(file.FileName));
                file.SaveAs(path);
            }
        }
        public void DeletedFile(string image)
        {
            string path = Server.MapPath("~/Images/Blogs/");
            System.IO.File.Delete(path + image);
        }
        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            var tBlogs = db.tBlogs.Include(t => t.tLoaiBlog).Include(t => t.tNhanVien);
            return View(tBlogs.OrderByDescending(n => n.Ngay).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tBlog tBlog = db.tBlogs.Find(id);
            if (tBlog == null)
            {
                return HttpNotFound();
            }
            return View(tBlog);
        }

        // GET: Admin/Blog/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiBlog = new SelectList(db.tLoaiBlogs, "MaLoaiBlog", "Ten");
            ViewBag.MaNV = new SelectList(db.tNhanViens, "MaNV", "TenNV");
            return View();
        }

        // POST: Admin/Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, tBlog tBlog)
        {
            if (ModelState.IsValid)
            {
                PostedFile(file);
                tBlog.Ngay = DateTime.Now;
                tBlog.LuotXem = 0;
                
                db.tBlogs.Add(tBlog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiBlog = new SelectList(db.tLoaiBlogs, "MaLoaiBlog", "Ten", tBlog.MaLoaiBlog);
            ViewBag.MaNV = new SelectList(db.tNhanViens, "MaNV", "TenNV", tBlog.MaNV);
            return View(tBlog);
        }

        // GET: Admin/Blog/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tBlog tBlog = db.tBlogs.Find(id);
            if (tBlog == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiBlog = new SelectList(db.tLoaiBlogs, "MaLoaiBlog", "Ten", tBlog.MaLoaiBlog);
            ViewBag.MaNV = new SelectList(db.tNhanViens, "MaNV", "TenNV", tBlog.MaNV);
            return View(tBlog);
        }

        // POST: Admin/Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, tBlog tBlog)
        {
            if (ModelState.IsValid)
            {
                PostedFile(file);
                db.Entry(tBlog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiBlog = new SelectList(db.tLoaiBlogs, "MaLoaiBlog", "Ten", tBlog.MaLoaiBlog);
            ViewBag.MaNV = new SelectList(db.tNhanViens, "MaNV", "TenNguoiDung", tBlog.MaNV);
            return View(tBlog);
        }

        // GET: Admin/Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tBlog tBlog = db.tBlogs.Find(id);
            if (tBlog == null)
            {
                return HttpNotFound();
            }
            return View(tBlog);
        }

        // POST: Admin/Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tBlog tBlog = db.tBlogs.Find(id);
            DeletedFile(tBlog.AnhBlog);
            db.tBlogs.Remove(tBlog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
