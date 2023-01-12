using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;

namespace WebsiteSellingSuitcases.Areas.Admin.Controllers
{
    public class NhanVienController : BaseController
    {
        private WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();

        // GET: Admin/NhanVien
        public ActionResult Index()
        {
            return View(db.tNhanViens.ToList());
        }

        // GET: Admin/NhanVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tNhanVien tNhanVien = db.tNhanViens.Find(id);
            if (tNhanVien == null)
            {
                return HttpNotFound();
            }
            return View(tNhanVien);
        }

        // GET: Admin/NhanVien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,TenNguoiDung,MatKhau,TenNV,DiaChi,DienThoai,Email,GioiTinh")] tNhanVien tNhanVien)
        {
            if (ModelState.IsValid)
            {
                db.tNhanViens.Add(tNhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tNhanVien);
        }

        // GET: Admin/NhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tNhanVien tNhanVien = db.tNhanViens.Find(id);
            if (tNhanVien == null)
            {
                return HttpNotFound();
            }
            return View(tNhanVien);
        }

        // POST: Admin/NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,TenNguoiDung,MatKhau,TenNV,DiaChi,DienThoai,Email,GioiTinh")] tNhanVien tNhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tNhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tNhanVien);
        }

        // GET: Admin/NhanVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tNhanVien tNhanVien = db.tNhanViens.Find(id);
            if (tNhanVien == null)
            {
                return HttpNotFound();
            }
            return View(tNhanVien);
        }

        // POST: Admin/NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tNhanVien tNhanVien = db.tNhanViens.Find(id);
            db.tNhanViens.Remove(tNhanVien);
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
        public ActionResult ChiTietNhanVien(int MaNV)
        {
            return PartialView(db.tNhanViens.SingleOrDefault(n => n.MaNV == MaNV));
        }
    }
}
