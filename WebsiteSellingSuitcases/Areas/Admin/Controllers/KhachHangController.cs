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
    public class KhachHangController : BaseController
    {
        private WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();

        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            return View(db.tKhachHangs.ToList());
        }

        // GET: Admin/KhachHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tKhachHang tKhachHang = db.tKhachHangs.Find(id);
            if (tKhachHang == null)
            {
                return HttpNotFound();
            }
            return View(tKhachHang);
        }

        // GET: Admin/KhachHang/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,TenNguoiDung,MatKhau,TenKH,DiaChi,DienThoai,Email,GioiTinh,Huyen,ThanhPho")] tKhachHang tKhachHang)
        {
            if (ModelState.IsValid)
            {
                db.tKhachHangs.Add(tKhachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tKhachHang);
        }

        // GET: Admin/KhachHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tKhachHang tKhachHang = db.tKhachHangs.Find(id);
            if (tKhachHang == null)
            {
                return HttpNotFound();
            }
            return View(tKhachHang);
        }

        // POST: Admin/KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,TenNguoiDung,MatKhau,TenKH,DiaChi,DienThoai,Email,GioiTinh,Huyen,ThanhPho")] tKhachHang tKhachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tKhachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tKhachHang);
        }

        // GET: Admin/KhachHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tKhachHang tKhachHang = db.tKhachHangs.Find(id);
            if (tKhachHang == null)
            {
                return HttpNotFound();
            }
            return View(tKhachHang);
        }

        // POST: Admin/KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tKhachHang tKhachHang = db.tKhachHangs.Find(id);
            db.tKhachHangs.Remove(tKhachHang);
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
        public ActionResult ChiTietKhachHang(int MaKH)
        {
            return PartialView(db.tKhachHangs.SingleOrDefault(n => n.MaKH == MaKH));
        }
    }
}
