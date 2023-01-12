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
    public class SanPhamController : BaseController
    {
        private WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();

        // GET: Admin/SanPham
        public ActionResult Index(int? page,string TimKiem)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            string timkiem = TimKiem ?? "";
            var tDanhMucSPs = db.tDanhMucSPs.Include(t => t.tChatLieu).Include(t => t.tHangSX).Include(t => t.tKichThuoc).Include(t => t.tLoaiDT).Include(t => t.tLoaiSP).Include(t => t.tQuocGia);
            return View(tDanhMucSPs.Where(n=>n.TenSP.Contains(timkiem)).OrderByDescending(n=>n.NgayTao).ToPagedList(pageNumber,pageSize));
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(string MaSP)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tDanhMucSP tDanhMucSP = db.tDanhMucSPs.Find(MaSP);
            if (tDanhMucSP == null)
            {
                return HttpNotFound();
            }
            return View(tDanhMucSP);
        }

        // GET: Admin/SanPham/Create
        public ActionResult Create()
        {
            ViewBag.MaChatLieu = new SelectList(db.tChatLieux, "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSX = new SelectList(db.tHangSXes, "MaHangSX", "HangSX");
            ViewBag.MaKichThuoc = new SelectList(db.tKichThuocs, "MaKichThuoc", "KichThuoc");
            ViewBag.MaDT = new SelectList(db.tLoaiDTs, "MaDT", "TenLoai");
            ViewBag.MaLoai = new SelectList(db.tLoaiSPs, "MaLoai", "Loai");
            ViewBag.MaNuocSX = new SelectList(db.tQuocGias, "MaNuoc", "TenNuoc");
            return View();
        }

        // POST: Admin/SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, tDanhMucSP tDanhMucSP)
        {
            if (ModelState.IsValid)
            {
                PostedFile(file);
                tDanhMucSP.LuotXem = 0;
                tDanhMucSP.SoLuong = 0;
                tDanhMucSP.NgayTao = DateTime.Now;
                db.tDanhMucSPs.Add(tDanhMucSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaChatLieu = new SelectList(db.tChatLieux, "MaChatLieu", "ChatLieu", tDanhMucSP.MaChatLieu);
            ViewBag.MaHangSX = new SelectList(db.tHangSXes, "MaHangSX", "HangSX", tDanhMucSP.MaHangSX);
            ViewBag.MaKichThuoc = new SelectList(db.tKichThuocs, "MaKichThuoc", "KichThuoc", tDanhMucSP.MaKichThuoc);
            ViewBag.MaDT = new SelectList(db.tLoaiDTs, "MaDT", "TenLoai", tDanhMucSP.MaDT);
            ViewBag.MaLoai = new SelectList(db.tLoaiSPs, "MaLoai", "Loai", tDanhMucSP.MaLoai);
            ViewBag.MaNuocSX = new SelectList(db.tQuocGias, "MaNuoc", "TenNuoc", tDanhMucSP.MaNuocSX);
            return View(tDanhMucSP);
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit( string MaSP)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tDanhMucSP tDanhMucSP = db.tDanhMucSPs.Find(MaSP);
            if (tDanhMucSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaChatLieu = new SelectList(db.tChatLieux, "MaChatLieu", "ChatLieu", tDanhMucSP.MaChatLieu);
            ViewBag.MaHangSX = new SelectList(db.tHangSXes, "MaHangSX", "HangSX", tDanhMucSP.MaHangSX);
            ViewBag.MaKichThuoc = new SelectList(db.tKichThuocs, "MaKichThuoc", "KichThuoc", tDanhMucSP.MaKichThuoc);
            ViewBag.MaDT = new SelectList(db.tLoaiDTs, "MaDT", "TenLoai", tDanhMucSP.MaDT);
            ViewBag.MaLoai = new SelectList(db.tLoaiSPs, "MaLoai", "Loai", tDanhMucSP.MaLoai);
            ViewBag.MaNuocSX = new SelectList(db.tQuocGias, "MaNuoc", "TenNuoc", tDanhMucSP.MaNuocSX);
            return View(tDanhMucSP);
        }

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, tDanhMucSP tDanhMucSP)
        {
            if (ModelState.IsValid)
            {
                PostedFile(file);
                db.Entry(tDanhMucSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaChatLieu = new SelectList(db.tChatLieux, "MaChatLieu", "ChatLieu", tDanhMucSP.MaChatLieu);
            ViewBag.MaHangSX = new SelectList(db.tHangSXes, "MaHangSX", "HangSX", tDanhMucSP.MaHangSX);
            ViewBag.MaKichThuoc = new SelectList(db.tKichThuocs, "MaKichThuoc", "KichThuoc", tDanhMucSP.MaKichThuoc);
            ViewBag.MaDT = new SelectList(db.tLoaiDTs, "MaDT", "TenLoai", tDanhMucSP.MaDT);
            ViewBag.MaLoai = new SelectList(db.tLoaiSPs, "MaLoai", "Loai", tDanhMucSP.MaLoai);
            ViewBag.MaNuocSX = new SelectList(db.tQuocGias, "MaNuoc", "TenNuoc", tDanhMucSP.MaNuocSX);
            return View(tDanhMucSP);
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(string MaSP)
        {
            if (MaSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tDanhMucSP tDanhMucSP = db.tDanhMucSPs.Find(MaSP);
            if (tDanhMucSP == null)
            {
                return HttpNotFound();
            }
            return View(tDanhMucSP);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string MaSP)
        {
            tDanhMucSP tDanhMucSP = db.tDanhMucSPs.Find(MaSP);
            List<string> images = db.tAnhSPs.Where(n => n.MaSP == MaSP).Select(n => n.TenFileAnh).ToList();
            DeletedFile(images);
            db.tAnhSPs.RemoveRange(db.tAnhSPs.Where(n => n.MaSP == MaSP));

            DeletedFile(tDanhMucSP.Anh); 
            db.tDanhMucSPs.Remove(tDanhMucSP);

            db.SaveChanges();
            return RedirectToAction("Index");
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
        public void DeletedFile(List<string> images)
        {
            string path = Server.MapPath("~/Images/");
            foreach (var item in images)
            {
                System.IO.File.Delete(path + item);
            }
            
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
