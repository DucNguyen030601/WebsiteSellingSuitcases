using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Models;

namespace WebsiteSellingSuitcases.Controllers
{
    public class LoaiSPController : Controller
    {
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        // GET: LoaiSP
        public ActionResult LoaiSPParial()
        {
            return PartialView(db.tLoaiSPs.OrderByDescending(n=>n.Loai).ToList()) ;
        }
        public ActionResult SidebarCategories()
        {
            return PartialView(db.tLoaiSPs.ToList());
        }
        public ActionResult PopularTags()
        {
           var result = db.tDinhKems.ToList();
            return PartialView(result);
        }
        public ActionResult PopularProducts()
        {
            var result = db.tDanhMucSPs.OrderByDescending(n => n.LuotXem).Take(2).ToList();
            return PartialView(result);
        }

    }
}