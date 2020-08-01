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
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int Sayfa=1)
        {
            var degerler = db.TBLURUNLER.ToList();/*.ToPagedList(Sayfa, 5);*/
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p1)
        {
            
            var ktg = db.TBLKATEGORILER.Where(m => m.KategoriID == p1.TBLKATEGORILER.KategoriID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urn = db.TBLURUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir", urn);
        }

        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var ur = db.TBLURUNLER.Find(p1.UrunID);
            ur.UrunAd = p1.UrunAd;
            ur.Marka = p1.Marka;
            //ur.TBLKATEGORILER.KategoriAd = p1.TBLKATEGORILER.KategoriAd;
            var ktg = db.TBLKATEGORILER.Where(m => m.KategoriID == p1.TBLKATEGORILER.KategoriID).FirstOrDefault();
            ur.UrunKategori = ktg.KategoriID;
            ur.Fiyat = p1.Fiyat;
            ur.Stok = p1.Stok;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}