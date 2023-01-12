using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;

namespace WebsiteSellingSuitcases.Areas.Admin.Controllers
{
    public class AnhSanPhamController : Controller
    {
        private WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();

        // GET: Admin/AnhSanPham
        public ActionResult Index(string MaSP)
        {
            var tAnhSPs = db.tAnhSPs.Include(t => t.tDanhMucSP).Where(n=>n.MaSP==MaSP);
            ViewBag.MaSP = MaSP;
            return View(tAnhSPs.ToList());
        }

        // GET: Admin/AnhSanPham/Details/5
        public ActionResult Details(string MaSP,string Anh)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tAnhSP tAnhSP = db.tAnhSPs.SingleOrDefault(n=>n.TenFileAnh==Anh&&n.MaSP==MaSP);
            if (tAnhSP == null)
            {
                return HttpNotFound();
            }
            return View(tAnhSP);
        }

        // GET: Admin/AnhSanPham/Create
        public ActionResult Create(string MaSP)
        {
            ViewBag.MaSP = new SelectList(db.tDanhMucSPs.Where(n=>n.MaSP==MaSP), "MaSP", "TenSP");
            return View();
        }

        // POST: Admin/AnhSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, tAnhSP tAnhSP)
        {
            if (ModelState.IsValid)
            {
                PostedFile(file);
                db.tAnhSPs.Add(tAnhSP);
                db.SaveChanges();
                return RedirectToAction("Index", new { MaSP = tAnhSP.MaSP.Trim() } );
            }
            ViewBag.MaSP = new SelectList(db.tDanhMucSPs, "MaSP", "TenSP", tAnhSP.MaSP);
            return View(tAnhSP);
        }

        // GET: Admin/AnhSanPham/Edit/5
        public ActionResult Edit(string MaSP,string Anh)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tAnhSP tAnhSP = db.tAnhSPs.SingleOrDefault(n=>n.MaSP==MaSP&&n.TenFileAnh==Anh);
            if (tAnhSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSP = new SelectList(db.tDanhMucSPs, "MaSP", "TenSP", tAnhSP.MaSP);
            return View(tAnhSP);
        }

        // POST: Admin/AnhSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, tAnhSP tAnhSP,string Anh)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    db.Entry(tAnhSP).State = EntityState.Modified;
                }
                else
                {
                    tAnhSP asp = db.tAnhSPs.SingleOrDefault(n => n.MaSP == tAnhSP.MaSP && n.TenFileAnh == Anh); DeletedFile(asp.TenFileAnh); db.tAnhSPs.Remove(asp);
                    PostedFile(file);
                    db.tAnhSPs.Add(tAnhSP);
                }
                db.SaveChanges();
                return RedirectToAction("Index", new { MaSP = tAnhSP.MaSP.Trim() });
            }
            ViewBag.MaSP = new SelectList(db.tDanhMucSPs, "MaSP", "TenSP", tAnhSP.MaSP);
            return View(tAnhSP);
        }

        // GET: Admin/AnhSanPham/Delete/5
        public ActionResult Delete(string MaSP, string Anh)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tAnhSP tAnhSP = db.tAnhSPs.SingleOrDefault(n => n.MaSP == MaSP && n.TenFileAnh == Anh);
            if (tAnhSP == null)
            {
                return HttpNotFound();
            }
            return View(tAnhSP);
        }

        // POST: Admin/AnhSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string MaSP, string Anh)
        {
            tAnhSP tAnhSP = db.tAnhSPs.SingleOrDefault(n => n.MaSP == MaSP && n.TenFileAnh == Anh);
            DeletedFile(tAnhSP.TenFileAnh);
            db.tAnhSPs.Remove(tAnhSP);
            db.SaveChanges();
            return RedirectToAction("Index", new { MaSP = tAnhSP.MaSP.Trim() });
        }
        public void PostedFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Images/"),
                                    Path.GetFileName(file.FileName));
                file.SaveAs(path);
            }
        }
        public void DeletedFile(string image)
        {
            string path = Server.MapPath("~/Images/");
            System.IO.File.Delete(path + image);
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
