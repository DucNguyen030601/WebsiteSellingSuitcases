using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteSellingSuitcases.Common;
using WebsiteSellingSuitcases.Models;
namespace WebsiteSellingSuitcases.Areas.Admin.Controllers
{
    public class TrangChuController : BaseController
    {
        WebsiteSellingSuitcasesEntities db = new WebsiteSellingSuitcasesEntities();
        // GET: Admin/TrangChu
        public ActionResult Index()
        {
            @ViewData["nv"] = (Session[Sessions.SESSION_ADMIN] as tNhanVien).TenNV;
            return View();
        }
    }
}