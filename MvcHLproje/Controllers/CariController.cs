using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcHLproje.Models.Siniflar;

namespace MvcHLproje.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();

        public ActionResult Index()
        {
            var degerler = c.Carilers.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniCari(Cariler p)
        {
            p.Durum = true;
            c.Carilers.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariSil(int Id)
        {
            var cr = c.Carilers.Find(Id);
            cr.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariGetir(int Id)
        {
            var cari = c.Carilers.Find(Id);
            return View("CariGetir", cari);
        }

        public ActionResult CariGuncelle(Cariler p)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }//boş geçilemez şartı koyduk. kaydetmeden önce kontorlü yapıyor sağlamıyorsa alta geçmeyip yeniden veri getirme aşamasına dönüyor.
            var cari = c.Carilers.Find(p.CariId);
            cari.CariAd = p.CariAd;
            cari.CariSoyad = p.CariSoyad;
            cari.CariSehir = p.CariSehir;
            cari.CariMail = p.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriSatinAlmaGecmisi(int Id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Cariid == Id).ToList();
            var cr = c.Carilers.Where(x => x.CariId == Id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cr;
            return View(degerler);
        }


    }
}