using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;
using WebsiteSellingSuitcases.Common;
namespace WebsiteSellingSuitcases.Controllers
{
    public class KhachHangController : Controller
    {
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        
        // GET: KhachHang
        public ActionResult Index(string ThongTin)
        {
           tKhachHang kh =  Session[Sessions.SESSION_CUSTOMMER] as tKhachHang;
            if (kh == null) return Redirect(@"/Home");
            string thongtin = ThongTin ?? "KhachHang";
            ViewData[thongtin] = "show active";
            return View();
        }
        
        public ActionResult CapNhat()
        {
            tKhachHang kh = Session[Sessions.SESSION_CUSTOMMER] as tKhachHang;
            return PartialView(kh);
        }
        [HttpPost]
        public ActionResult CapNhat(tKhachHang kh)
        {
            try
            {
                db.Entry(kh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["CapNhatThanhCong"] = "Cập nhật thành công!";
                Session[Sessions.SESSION_CUSTOMMER] = kh;
            }
            catch
            {
                TempData["CapNhatLoi"] = "Cập nhật không thành công!";
            }
            return RedirectToAction("Index");
        }
        public ActionResult YeuThich()
        {
            tKhachHang kh = Session[Sessions.SESSION_CUSTOMMER] as tKhachHang;
            List<tDanhMucSP> result = (from sp in db.tDanhMucSPs
                          from yt in db.tYeuThiches
                          where sp.MaSP == yt.MaSP && yt.MaKH == kh.MaKH
                          select sp).ToList();
            return PartialView(result);
        }
        [HttpPost]
        public ActionResult ThemYeuThich(string sMaSP,string strUrl)
        {
            tKhachHang kh = Session[Sessions.SESSION_CUSTOMMER] as tKhachHang;
            if (kh == null) return Redirect(strUrl); 
            tYeuThich y = db.tYeuThiches.SingleOrDefault(n => n.MaKH == kh.MaKH && n.MaSP == sMaSP);
            if(y==null)
            {
                tYeuThich yt = new tYeuThich();
                yt.MaKH = kh.MaKH;
                yt.MaSP = sMaSP;
                yt.Ngay = DateTime.Now;
                db.Entry(yt).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
            return Redirect(strUrl);
        }
        [HttpPost]
        public ActionResult XoaYeuThich(string sMaSP, string strUrl)
        {
            tKhachHang kh = Session[Sessions.SESSION_CUSTOMMER] as tKhachHang;
            if (kh == null) return Redirect(strUrl);
            tYeuThich yt = db.tYeuThiches.SingleOrDefault(n => n.MaKH == kh.MaKH && n.MaSP == sMaSP);
            db.Entry(yt).State = System.Data.Entity.EntityState.Deleted;
            db.tYeuThiches.Remove(yt);
            db.SaveChanges();
            return Redirect(strUrl);
        }
        public ActionResult DonDat()
        {
            tKhachHang kh = Session[Sessions.SESSION_CUSTOMMER] as tKhachHang;
            return PartialView(db.tHoaDons.Where(n => n.NgayThanhToan == null && n.MaKH == kh.MaKH&&(n.XacNhan==0||n.XacNhan==1)).OrderByDescending(n=>n.NgayBan_NgayNhap).ToList());
        }
        public ActionResult DonMua()
        {
            tKhachHang kh = Session[Sessions.SESSION_CUSTOMMER] as tKhachHang;
            return PartialView(db.tHoaDons.Where(n => n.NgayThanhToan != null && n.MaKH == kh.MaKH).OrderByDescending(n => n.NgayBan_NgayNhap).ToList());
        }
        public ActionResult ChiTietHoaDon(int SoHD)
        {
            List<GioHang> gioHang = (from p in db.tDanhMucSPs
                                     from q in db.tChiTietHoaDons
                                     where p.MaSP == q.MaSP&&q.SoHD==SoHD
                                     select new GioHang {
                                         sTenSP=p.TenSP,
                                         sHinhAnh = p.Anh,
                                         iSoLuong =(int) q.SoLuong,
                                         sChietKhau =(double) q.ChietKhau,
                                         sGiaBan = (double)q.GiaBan,
                                         ThanhTien =(double) q.ThanhTien
                          
                                     }).ToList();
            return PartialView(gioHang); 
        }
        [HttpPost]
        public ActionResult HuyHoaDon(int SoHD, string strUrl)
        {
            try
            {
                var result = (from q in db.tChiTietHoaDons
                              where q.SoHD == SoHD
                              select q).ToList();
                foreach (var item in result)
                {
                    tDanhMucSP sp = db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == item.MaSP);
                    sp.SoLuong += item.SoLuong;
                    db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                tHoaDon hd = db.tHoaDons.SingleOrDefault(n => n.SoHD == SoHD);
                hd.XacNhan = 2;
                hd.NoiDung = "Khách hàng đã huỷ hoá đơn - " + DateTime.Now;
                db.Entry(hd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["thongtin"] = "Huỷ đơn hàng thành công!";
            }
            catch
            {
                TempData["thongtin"] = "Huỷ đơn hàng không thành công!";
            }
            return Redirect(strUrl);
        }

    }
}