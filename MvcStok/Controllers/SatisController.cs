using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class SatisController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        // GET: Satis
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.TBLSATISLAR.ToList().ToPagedList(sayfa, 5);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> degerler = (from i in db.TBLMUSTERILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.MusteriAd,
                                                 Value = i.MusteriID.ToString()
                                             }).ToList();
            ViewBag.musteriad = degerler;

            List<SelectListItem> degerler2 = (from i in db.TBLMUSTERILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.MusteriSoyad,
                                                 Value = i.MusteriID.ToString()
                                             }).ToList();
            ViewBag.musterisoyad = degerler2;

            List<SelectListItem> urunad = (from i in db.TBLURUNLER.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.UrunAd,
                                                  Value = i.UrunID.ToString()
                                              }).ToList();
            ViewBag.urun = urunad;
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(TBLSATISLAR p)
        {
            var musteri = db.TBLMUSTERILER.Where(m => m.MusteriID == p.TBLMUSTERILER.MusteriID).FirstOrDefault();
            p.TBLMUSTERILER = musteri;
            var urun = db.TBLURUNLER.Where(m => m.UrunID == p.TBLURUNLER.UrunID).FirstOrDefault();
            p.TBLURUNLER = urun;
            db.TBLSATISLAR.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}