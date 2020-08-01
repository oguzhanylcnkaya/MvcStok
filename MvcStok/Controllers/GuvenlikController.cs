using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using System.Web.Security;

namespace MvcStok.Controllers
{
    public class GuvenlikController : Controller
    {
        // GET: Guvenlik
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(TBLKULLANICI p)
        {
            var bilgiler = db.TBLKULLANICI.FirstOrDefault(x => x.AD == p.AD && x.SIFRE == p.SIFRE);
            if(bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.AD, false);
                return RedirectToAction("Index", "AnaSayfa");
            }
            else
            {
                return View();
            }
        }
    }
}