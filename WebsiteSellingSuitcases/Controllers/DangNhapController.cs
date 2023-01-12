using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;
using WebsiteSellingSuitcases.Common;
namespace WebsiteSellingSuitcases.Controllers
{
    public class DangNhapController : Controller
    {
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        // GET: DangNhap
        public ActionResult Index()
        {
            Session.Remove(Sessions.SESSION_CUSTOMMER);
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string TenNguoiDung,string MatKhau)
        {
            tKhachHang kh = db.tKhachHangs.SingleOrDefault(n => n.TenNguoiDung == TenNguoiDung && n.MatKhau == MatKhau);
            if ( kh!= null)
            {
                Session[Sessions.SESSION_CUSTOMMER] = kh;
                TempData["XinChao"] = "Xin chào khách hàng " + kh.TenKH + "!";
                return Redirect(@"\Home");
            }
            else
            {
                TempData["DangNhapLoi"] = "Tên người dùng hoặc mật khẩu không chính xác.";
                return RedirectToAction("Index");
            }
            
        }
        [HttpPost]
        
        public ActionResult DangKy(FormCollection f)
        {
            tKhachHang kh = new tKhachHang();
            kh.TenKH = f["TenKH"];
            kh.TenNguoiDung = f["TenNguoiDung"];
            kh.DiaChi = f["DiaChi"];
            kh.DienThoai = f["DienThoai"];
            kh.Email = f["Email"];
            kh.Huyen = f["Huyen"];
            kh.ThanhPho = f["ThanhPho"];
            kh.MatKhau = f["MatKhau"];
            kh.GioiTinh=Convert.ToBoolean(f["GioiTinh"]);
            if(db.tKhachHangs.SingleOrDefault(n=>n.TenNguoiDung==kh.TenNguoiDung)==null)
            {
                db.Entry(kh).State = System.Data.Entity.EntityState.Added;
                db.tKhachHangs.Add(kh);
                db.SaveChanges();
                TempData["DangKyThanhCong"] = "Mời bạn đăng nhập với tư cách khách hàng.";
                TempData["TenNguoiDung"] = kh.TenNguoiDung;
                TempData["MatKhau"] = kh.MatKhau;
            }
            else
            {
                TempData["DangKyLoi"] = "Tên người dùng này đã được sử dụng.";
            }    
            return RedirectToAction("Index");
        }
        [HttpPost]

        public ActionResult CapNhat(FormCollection f)
        {
            tKhachHang kh = new tKhachHang();
            kh.TenKH = f["TenKH"];
            kh.TenNguoiDung = f["TenNguoiDung"];
            kh.DiaChi = f["DiaChi"];
            kh.DienThoai = f["DienThoai"];
            kh.Email = f["Email"];
            kh.Huyen = f["Huyen"];
            kh.ThanhPho = f["ThanhPho"];
            kh.MatKhau = f["MatKhau"];
            kh.GioiTinh = Convert.ToBoolean(f["GioiTinh"]);
            if (db.tKhachHangs.SingleOrDefault(n => n.TenNguoiDung == kh.TenNguoiDung) == null)
            {
                db.Entry(kh).State = System.Data.Entity.EntityState.Added;
                db.tKhachHangs.Add(kh);
                db.SaveChanges();
                TempData["CapNhatThanhCong"] = "Mời bạn đăng nhập với tư cách khách hàng.";
            }
            else
            {
                TempData["DangKyLoi"] = "Tên người dùng này đã được sử dụng.";
            }
            return RedirectToAction("Index");
        }
    }
}