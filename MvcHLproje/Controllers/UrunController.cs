using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcHLproje.Models.Siniflar;
using PagedList;//sayfalama için ekledim.
using PagedList.Mvc;

namespace MvcHLproje.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();

        public ActionResult Index(int sayfa = 1)
        {
            var urunler = c.Uruns.Where(x => x.Durum == true).ToList().ToPagedList(sayfa, 4);//ürünlerden sadece durumu true olanları listelemesi için linq where sorgusu kullandım.
            return View(urunler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriId.ToString()
                                           }).ToList();//dropdownlist için verileri text ve value olarak aldık.
            ViewBag.dgr1 = deger1;//viewBag controller tarafından view tarafına veri taşımak için kullanılır.dgr1 dinamik
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(Urun p)
        {
            c.Uruns.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int Id)
        {
            var deger = c.Uruns.Find(Id);
            deger.Durum = false;//ürünü silmek yerine pasif hale getiriyoruz(sorgu ile listede durumu true olanları göster diyeceğiz böylece ürünler veri tabanımda kalacak sadece kullanıcı görmeyecek.)
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int Id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriId.ToString()
                                           }).ToList();//dropdownlist için verileri text ve value olarak aldık.
            ViewBag.dgr1 = deger1;//viewBag controller tarafından view tarafına veri taşımak için kullanılır.dgr1 dinamik
            var urundeger = c.Uruns.Find(Id);
            return View("UrunGetir", urundeger);
        }
        public ActionResult UrunGuncelle(Urun p)
        {
            var urn = c.Uruns.Find(p.UrunId);
            urn.UrunAd = p.UrunAd;
            urn.Sanatci = p.Sanatci;
            urn.Stok = p.Stok;
            urn.AlisFiyat = p.AlisFiyat;
            urn.SatisFiyat = p.SatisFiyat;
            urn.Kategoriid = p.Kategoriid;
            urn.UrunGorsel = p.UrunGorsel;
            urn.Durum = p.Durum;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelId.ToString()
                                           }).ToList();
            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.CariId.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            ViewBag.dgr2 = deger2;

            //ürün id'si getirme işlemi için;
            var deger1 = c.Uruns.Find(id);
            ViewBag.dgr1 = deger1.UrunId;
            ViewBag.dgrad = deger1.UrunAd;
            ViewBag.dgrfiyat = deger1.SatisFiyat;

            return View();
        }

        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index", "Satis");

        }

    }
}