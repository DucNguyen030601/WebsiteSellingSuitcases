using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;
using PagedList;
namespace WebsiteSellingSuitcases.Controllers
{
    public class SanPhamController : Controller
    {
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        // GET: SanPham
        public ActionResult SanPham(string MaLoai)
        {
            ViewBag.MaLoai = MaLoai??"";
            ViewBag.SanPham = (from n in db.tDanhMucSPs where n.MaLoai.Contains(MaLoai) select n).ToList();
            return View();
        }
        public ActionResult Index(int ?page,string MaLoai,string MaDT, string MauSac,string Tag,string Gia,string TimKiem,int? SapXep)
        {
            int pageSize = 6;
            int pageIndex = page ?? 1; 

            string[] madts = (MaDT ?? "").Split(',');
            string[] mausacs = (MauSac ?? "").Split(',');
            string maloai = MaLoai ?? "";
            double[] price = convertPrice(Gia);
            string timkiem = TimKiem ?? "";
            int sapxep = SapXep ?? 1;

            var res = Tag==null ? GetAll(maloai, madts, mausacs,price[0],price[1],timkiem,sapxep) : GetAllByTags(maloai, madts, mausacs, Tag, price[0], price[1], timkiem,sapxep);

            ViewBag.Infor =res.Count()<pageSize?"Hiển thị "+res.Count()+" kết quả":"Hiển thị từ "+(pageSize*pageIndex-pageSize+1) +" đến " + (pageIndex*pageSize) + " trong " + res.Count()+" kết quả";
            ViewBag.MaLoai = maloai;
            ViewBag.MaDT = MaDT;
            ViewBag.MauSac = MauSac;
            ViewBag.Gia = Gia;
            ViewBag.TimKiem = timkiem;
            ViewBag.Page = pageIndex;
            ViewBag.SapXep = sapxep;
            return View(res.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult Details(string MaSP)
        {
            tDanhMucSP tDanhMucSP = db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == MaSP);
            tDanhMucSP.LuotXem += 1;
            db.Entry(tDanhMucSP).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(tDanhMucSP);
        }
        public ActionResult SubmitReview(string MaSP, string email,string author,string comment)
        {
            Review review = new Review { MaSP =MaSP ,Email = email, HoTen = author, NoiDung = comment, ThoiGian = DateTime.Now };
            db.Entry(review).State = System.Data.Entity.EntityState.Added;
            db.Reviews.Add(review);
            db.SaveChanges();
            return RedirectToAction("Details", new {@MaSP = MaSP});
        }
        //Function
        public List<tDanhMucSP> GetAll(string maloai, string[] madts, string[] mausacs,double startPrice,double endPrice,string ten,int sapxep)
        {
            ViewBag.SanPham = (from n in db.tDanhMucSPs where n.MaLoai.Contains(maloai) && 
                               (startPrice<=(double)(n.GiaBan-n.GiaBan*n.ChietKhau) &&
                               (double)(n.GiaBan-n.GiaBan*n.ChietKhau)<=endPrice)
                               &&n.TenSP.Contains(ten)
                               select n).ToList();
            var result = (from n in db.tDanhMucSPs
                          where n.MaLoai.Contains(maloai)
                          && startPrice <= (double)(n.GiaBan - n.GiaBan * n.ChietKhau) && (double)(n.GiaBan - n.GiaBan * n.ChietKhau) <= endPrice
                          && n.TenSP.Contains(ten) &&


                          (madts.Count() != 1 ? madts.Contains(n.MaDT) : n.MaDT.Contains("")) &&
                          (mausacs.Count() != 1 ? mausacs.Contains(n.MauSac) : n.MauSac.Contains(""))


                          select n).ToList();
            if (sapxep == 1) return result.OrderByDescending(n => n.NgayTao).ToList();
            if (sapxep == 2) return result.OrderByDescending(n => n.LuotXem).ToList();
            if (sapxep == 3) return result.OrderByDescending(n => n.GiaBan).ToList();
            if (sapxep == 4) return result.OrderBy(n => n.GiaBan).ToList();
            if (sapxep == 5) return result.OrderBy(n => n.TenSP).ToList();
            if (sapxep == 6) return result.OrderByDescending(n => n.TenSP).ToList();
            return result;

        }
        public List<tDanhMucSP> GetAllByTags(string maloai, string[] madts, string[] mausacs,string Tag, double startPrice, double endPrice,string ten,int sapxep)
        {
            ViewBag.SanPham = (from n in db.tDanhMucSPs
                               from dk in db.tDinhKems
                               from dksp in db.tDinhKemSPs
                               where dk.MaDK == dksp.MaDK && n.MaSP == dksp.MaSP &&
                               startPrice <= (double)(n.GiaBan-n.GiaBan*n.ChietKhau) && (double)(n.GiaBan-n.GiaBan*n.ChietKhau) <= endPrice &&
                               dk.TieuDe == Tag && n.MaLoai.Contains(maloai)
                               && n.TenSP.Contains(ten) 
                               select n).ToList();
            ViewBag.Tag = Tag;
            var result = (from n in db.tDanhMucSPs
                          from dk in db.tDinhKems
                          from dksp in db.tDinhKemSPs
                          where n.MaLoai.Contains(maloai) &&
                          dk.MaDK == dksp.MaDK && n.MaSP == dksp.MaSP &&
                          dk.TieuDe == Tag && startPrice <= (double)(n.GiaBan - n.GiaBan * n.ChietKhau) && (double)(n.GiaBan - n.GiaBan * n.ChietKhau) <= endPrice
                          && n.TenSP.Contains(ten) &&

                          (madts.Count() != 1 ? madts.Contains(n.MaDT) : n.MaDT.Contains("")) &&
                          (mausacs.Count() != 1 ? mausacs.Contains(n.MauSac) : n.MauSac.Contains(""))


                          select n).ToList();
            if (sapxep == 1) return result.OrderByDescending(n => n.NgayTao).ToList();
            if (sapxep == 2) return result.OrderByDescending(n => n.LuotXem).ToList();
            if (sapxep == 3) return result.OrderByDescending(n => n.GiaBan).ToList();
            if (sapxep == 4) return result.OrderBy(n => n.GiaBan).ToList();
            if (sapxep == 5) return result.OrderBy(n => n.TenSP).ToList();
            if (sapxep == 6) return result.OrderByDescending(n => n.TenSP).ToList();
            return result;

        }
        public double[] convertPrice(string s)
        { 
            double[] price = new double[2];
            if (s != null)
            {
                var amount = s.Split(new char[]{' ', ' '} );
                price[0] = double.Parse(amount[0].Replace(".", "")) ;
                price[1] = double.Parse(amount[3].Replace(".", ""));
            }
            else
            {
                price[0] = 0;
                price[1] = 20000000;
            }
            return price;
        }
        public double getPrice(double price,double dis)
        {
            return price - price * dis;
        }
        public ActionResult DetailsPartial(string MaSP,string strUrl)
        {
            ViewBag.Url = strUrl;
            return PartialView(db.tDanhMucSPs.SingleOrDefault(n => n.MaSP == MaSP));
        }
    }
}