using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;
using WebsiteSellingSuitcases.Common;
namespace WebsiteSellingSuitcases.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;

            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        [HttpPost]
        public ActionResult ThemGioHang(string sMaSP, int SoLuong, string strUrl)
        {
            tDanhMucSP sanpham = db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == sMaSP);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang spgh = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (spgh == null)
            {
                spgh = new GioHang(sMaSP, SoLuong);
                lstGioHang.Add(spgh);
                return Redirect(strUrl);
            }
            else
            {
                spgh.iSoLuong += SoLuong;
                return Redirect(strUrl);
            }
        }
        //tạo view Giỏ hàng
        public ActionResult Index()
        {
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSL = TongSoLuong();
            ViewBag.TongTien = TongTien();
            ViewBag.PhiShip = PhiShip();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult CapNhapGioHang(string sMaSP, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult XoaGioHang(string sMaSP, string strUrl)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.sMaSP == sMaSP);
            }
            return Redirect(strUrl);
        }
        private int TongSoLuong()
        {
            return LayGioHang().Sum(n => n.iSoLuong);
        }
        private double TongTien()
        {
            return LayGioHang().Sum(n => n.dThanhTien) + PhiShip();

        }
        private double PhiShip()
        {
            double monney = 0;
            if (TongSoLuong() > 0 && TongSoLuong() < 3)
            {
                monney = 70000;
            }
            return monney;
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult DetailsPartial(string strUrl)
        {
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSL = TongSoLuong();
            ViewBag.TongTien = TongTien();
            ViewBag.PhiShip = PhiShip();
            ViewBag.Url = strUrl;
            return PartialView(lstGioHang);
        }
        public ActionResult ThuTucThanhToan()
        {
            tKhachHang kh = Session[Sessions.SESSION_CUSTOMMER] as tKhachHang;
            if (kh == null) return RedirectToAction("Index", "DangNhap");
            ViewBag.GioHang = LayGioHang();
            ViewBag.TongSL = TongSoLuong();
            ViewBag.TongTien = TongTien();
            ViewBag.PhiShip = PhiShip();
            return View(kh);
        }
        public ActionResult DatHang()
        {
            tKhachHang kh = (tKhachHang)Session[Sessions.SESSION_CUSTOMMER];
            List<GioHang> lstGioHang = LayGioHang();
            if (kh == null)
            {
                return RedirectToAction("Index", "DangNhap");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (CheckSoLuongSanPham(lstGioHang))
            {
                tHoaDon ddh = new tHoaDon();
                ddh.MaKH = kh.MaKH;
                ddh.NgayBan_NgayNhap = DateTime.Now;
                ddh.Ship = PhiShip() != 0 ? true : false;
                ddh.NoiDung = "Đang kiểm tra";
                ddh.XacNhan = 0;
                ddh.Loai = false;
                ddh.TongTien = (decimal)TongTien();
                db.tHoaDons.Add(ddh);
                db.SaveChanges();

                AddCTHD(ddh.SoHD, lstGioHang);
                UpdateProduct(lstGioHang);
                TempData["thongtin"] = "Đặt hàng thành công!";
                return RedirectToAction("Index", "KhachHang", new { ThongTin = "DonDat" });
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        bool CheckSoLuongSanPham(List<GioHang> gios)
        {
            foreach (var item in gios)
            {
                if (item.iSoLuong > getSoLuongSanPham(item.sMaSP))
                {
                    TempData["ThongTinDatHang"] = "Số lượng sản phẩm " + item.sTenSP + " không còn trong kho!";
                    return false;
                }
            }
            return true;
        }
        int getSoLuongSanPham(string MaSP)
        {
            return (int)db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == MaSP).SoLuong;
        }
        public void AddCTHD(int SoHD, List<GioHang> lstGioHang)
        {
            foreach (var item in lstGioHang)
            {
                tChiTietHoaDon cthd = new tChiTietHoaDon();
                cthd.SoHD = SoHD;
                cthd.MaSP = item.sMaSP;
                cthd.SoLuong = item.iSoLuong;
                cthd.GiaBan = (decimal)item.sGiaBan;
                cthd.ThanhTien = (decimal)item.dThanhTien;
                cthd.ChietKhau = item.sChietKhau;
                db.Entry(cthd).State = System.Data.Entity.EntityState.Added;
                db.tChiTietHoaDons.Add(cthd);
                db.SaveChanges();
            }
        }
        public void UpdateProduct(List<GioHang> lstGioHang)
        {
            foreach (var item in lstGioHang)
            {
                tDanhMucSP sp = db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == item.sMaSP);
                sp.SoLuong -= item.iSoLuong;
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        
    }
}