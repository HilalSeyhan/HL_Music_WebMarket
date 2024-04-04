using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcHLproje.Models.Siniflar;

namespace MvcHLproje.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();

        public ActionResult Index()
        {
            var liste = c.Faturalars.ToList();
            return View(liste);
        }

        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaGetir(int Id)
        {
            var fatura = c.Faturalars.Find(Id);
            return View("FaturaGetir", fatura);
        }

        public ActionResult FaturaGuncelle (Faturalar f)
        {
            var fatura = c.Faturalars.Find(f.FaturaId);
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.FaturaSiraNo = f.FaturaSiraNo;
            fatura.VergiDairesi = f.VergiDairesi;
            fatura.Tarih = f.Tarih;
            fatura.Saat = f.Saat;
            fatura.TeslimEden = f.TeslimEden;
            fatura.TeslimAlan = f.TeslimAlan;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaDetay(int Id)
        {
            var degerler = c.FaturaKalems.Where(x => x.FaturaId == Id).ToList();//faturakalemleri içerisinde FaturaId'si Id'ye eşit olan değerleri listele.
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem p)
        {
            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}