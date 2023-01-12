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
    public class LienHeController : BaseController
    {
        private WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();

        // GET: Admin/LienHe
        public ActionResult Index()
        {
            return View(db.tLienHes.ToList());
        }

        // GET: Admin/LienHe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tLienHe tLienHe = db.tLienHes.Find(id);
            if (tLienHe == null)
            {
                return HttpNotFound();
            }
            return View(tLienHe);
        }

        // GET: Admin/LienHe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LienHe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Contact_ID,HoTen,TieuDe,Email,DienThoai,TinNhan,Ngay")] tLienHe tLienHe)
        {
            if (ModelState.IsValid)
            {
                db.tLienHes.Add(tLienHe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tLienHe);
        }

        // GET: Admin/LienHe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tLienHe tLienHe = db.tLienHes.Find(id);
            if (tLienHe == null)
            {
                return HttpNotFound();
            }
            return View(tLienHe);
        }

        // POST: Admin/LienHe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Contact_ID,HoTen,TieuDe,Email,DienThoai,TinNhan,Ngay")] tLienHe tLienHe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tLienHe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tLienHe);
        }

        // GET: Admin/LienHe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tLienHe tLienHe = db.tLienHes.Find(id);
            if (tLienHe == null)
            {
                return HttpNotFound();
            }
            return View(tLienHe);
        }

        // POST: Admin/LienHe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tLienHe tLienHe = db.tLienHes.Find(id);
            db.tLienHes.Remove(tLienHe);
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
